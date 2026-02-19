namespace Infrastructure.ModelBase;

public class RecordInfo
{
    public DateTime CreatedDate { get; set; }
    public long CreatedById { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifedDate { get; set; }
    public long? ModifedById { get; set; }
    public string? ModifedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public long? DeletedById { get; set; }
    public string? DeletedBy { get; set; }
}