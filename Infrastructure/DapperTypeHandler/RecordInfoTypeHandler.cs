using System.Data;
using System.Text.Json;
using Dapper;
using Infrastructure.ModelBase;

namespace Infrastructure.DapperTypeHandler;

public class RecordInfoTypeHandler : SqlMapper.TypeHandler<RecordInfo>
{
    public override void SetValue(IDbDataParameter parameter, RecordInfo value)
    {
        parameter.Value = JsonSerializer.Serialize(value);
    }

    public override RecordInfo Parse(object value)
    {
        return JsonSerializer.Deserialize<RecordInfo>(value.ToString());
    }
}