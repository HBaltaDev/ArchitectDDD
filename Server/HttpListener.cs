using System.Reflection;
using System.Text.Json;
using Dapper;
using Infrastructure.DbContext;
using Infrastructure.DtoBase.ResponseBase;
using Infrastructure.ExceptionHandling;
using Server.Applications.UserManagement.Abstract;
using Server.Infrastructure.ExectionHandling;
using Server.Infrastructure.ExectionHandling.ErrorMessage;
using Server.Infrastructure.ExectionHandling.Localization;
using Server.UserManagement.Dto.Request;

namespace Server;

public class HttpListener
{
    private readonly Dictionary<string, MethodInfo> _actions;
    private readonly Dictionary<string, Type> _parameterTypes;
    private readonly IServiceProvider _serviceProvider;
    private readonly IErrorLocalizer _errorLocalizer;
    
    public HttpListener(IServiceProvider serviceProvider, IErrorLocalizer errorLocalizer)
    {
        _actions = new Dictionary<string, MethodInfo>();
        _parameterTypes = new Dictionary<string, Type>();
        
        _serviceProvider = serviceProvider;
        _errorLocalizer = errorLocalizer;
        
        using var scope = _serviceProvider.CreateScope();
        
        var methods = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IApplicationService).IsAssignableFrom(t) && t.IsInterface)
            .SelectMany(t => t.GetMethods())
            .Where(m => m.ReturnType == typeof(Task<ResponseBase>));
        
        foreach (var method in methods)
        {
            _actions.Add(method.Name, method);
            _parameterTypes.Add(method.Name, method.GetParameters().FirstOrDefault()?.ParameterType);
        }

        AddSqlMapperHandlerTypes();
    }

    public void Start()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", configurePolicy => {
                configurePolicy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var webApp = builder.Build();
 
        webApp.UseCors("AllowAllOrigins");

        webApp.MapMethods("/", new[] { "GET", "PUT", "DELETE", "PATCH" }, () => "Template Project API sadece HTTP POST üzerinden gelen taleplere cevap vermektedir");

        webApp.MapPost("/", HttpRequestReceived);

        webApp.Run();
    }
    
    private async Task HttpRequestReceived(HttpContext context)
    {
        var actionName = "test";
        var response = "";
        if (context.Request.Headers["Action"].Count != 0)
        {
            actionName = context.Request.Headers["Action"];
        }
        
        using var scope = _serviceProvider.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

        var isTransactionRequired = false;
        
        var isAuditLogRequired = false;
        
        try
        {
            if (_actions.TryGetValue(actionName!, out var actionMethod) && _parameterTypes.TryGetValue(actionName!, out var paramType))
            {
                var action = ActionConfig.GetActionConfig(actionMethod.Name);

                isTransactionRequired = action!.TransactionRequired;
                isAuditLogRequired = action!.AuditLogRequired;
                
                var dtoRequest = paramType.Name != "RequestBase" ? await JsonSerializer.DeserializeAsync(context.Request.Body, paramType) : null;
                    
                var serviceType = actionMethod.DeclaringType!;

                var service = scope.ServiceProvider.GetRequiredService(serviceType);
                    
                await dbContext.CreateConnectionAsync(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")!);
                    
                var responseObj = await (Task<ResponseBase>)actionMethod.Invoke(service, [dtoRequest])!;
                
                response = JsonSerializer.Serialize(responseObj, responseObj.GetType());

                if (isTransactionRequired)
                {
                    await dbContext.CommitAsync();
                }

                await dbContext.CloseConnectionAsync();
            }

            context.Response.Headers.Append("Content-Type", "application/json");
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(response);
        }
        catch (ServiceException exception)
        {
            context.Response.Headers.Append("Content-Type", "application/json");
            context.Response.StatusCode = (int)exception.StatusCode;
            await context.Response.WriteAsync(_errorLocalizer.GetDescription(exception.Description, "en"));
        }
        catch (Exception exception)
        {
            if (isTransactionRequired)
            {
                await dbContext.RollbackAsync();
            }
            
            context.Response.Headers.Append("Content-Type", "application/json");
            context.Response.StatusCode = (int)ErrorDefinitions.SystemError.StatusCode;
            Console.WriteLine(exception.Message);
            await context.Response.WriteAsync(_errorLocalizer.GetDescription(ErrorDefinitions.SystemError.Description, "en"));
        }
    }
    
    private static void AddSqlMapperHandlerTypes()
    {
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        var allDerivedTypes = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t =>
                t is { IsClass: true, IsAbstract: false, BaseType.IsGenericType: true } &&
                t.BaseType.GetGenericTypeDefinition() == typeof(SqlMapper.TypeHandler<>))
            .ToList();
        
        foreach (var derivedType in allDerivedTypes)
        {
            if (Activator.CreateInstance(derivedType) is SqlMapper.ITypeHandler instance)
            {
                var genericArgumentType = derivedType.BaseType!.GenericTypeArguments.FirstOrDefault();

                if (genericArgumentType != null)
                {
                    var method = typeof(SqlMapper).GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .FirstOrDefault(m => m is { Name: "AddTypeHandler", IsGenericMethod: true });

                    if (method != null)
                    {
                        var genericMethod = method.MakeGenericMethod(genericArgumentType);

                        genericMethod.Invoke(null, [instance]);
                    }
                }
            }
        }
    }
}