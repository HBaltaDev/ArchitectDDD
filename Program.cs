// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Hosting;
using NetStructre;
var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddHostedService<Worker>();

await builder.Build().RunAsync();