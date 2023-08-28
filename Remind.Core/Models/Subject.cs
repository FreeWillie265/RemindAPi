namespace Remind.Core.Models;

public class Subject
{
    public Guid Id { get; set; }
    public String AgeGroup { get; set; }
    public String Sex { get; set; }
    public String ClinicName { get; set; }
    public String District { get; set; }
    public String Clerk { get; set; }
    public String Etc { get; set; }
    public Boolean Traversed { get; set; }
}