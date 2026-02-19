using Infrastructure.DtoBase.RequestBase;

namespace Infrastructure.Dto.Request;

public record CommonIdRequest : RequestBase 
{
    public long Id { get; set; }
}