namespace Remind.Core.Models;

public class Observation: ICloneable
{
    public Guid Id { get; set; }
    public String DataId { get; set; }
    public String Stratum { get; set; }
    public String BlockId { get; set; }
    public int BlockSize { get; set; }
    public String Treatment { get; set; }
    public string? Note { get; set; }
    public Boolean Assigned { get; set; }
    public object Clone()
    {
        return MemberwiseClone();
    }
}