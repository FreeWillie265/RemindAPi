namespace Remind.Core.Models;

public class Observation: ICloneable
{
    public String Id { get; set; }
    public String Statum { get; set; }
    public String BlockId { get; set; }
    public int BlockSize { get; set; }
    public String Treatment { get; set; }
    public Boolean Assigned { get; set; }
    public object Clone()
    {
        return MemberwiseClone();
    }
}