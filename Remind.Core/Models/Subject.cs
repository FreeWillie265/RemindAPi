namespace Remind.Core.Models;

public class Subject : ICloneable
{
    public Guid Id { get; set; }
    public string AgeGroup { get; set; }
    public string Sex { get; set; }
    public string BlockId { get; set; }
    public int BlockSize { get; set; }
    public String Treatment { get; set; }
    public string? ClinicName { get; set; }
    public string? District { get; set; }
    public string? Clerk { get; set; }
    public string? Etc { get; set; }
    public Boolean Traversed { get; set; }
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}