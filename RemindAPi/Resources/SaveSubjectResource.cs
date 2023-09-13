namespace RemindAPi.Resources;

public class SaveSubjectResource
{
    public string AgeGroup { get; set; }
    
    public String DataId { get; set; }
    public string Sex { get; set; }
    public string BlockId { get; set; }
    public int BlockSize { get; set; }
    public String Treatment { get; set; }
    public string ClinicName { get; set; }
    public string District { get; set; }
    public string Clerk { get; set; }
    public string Note { get; set; }
    public Boolean Assigned { get; set; }
}