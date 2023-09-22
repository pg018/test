
using ConsoleApp2.Core.CustomValidators;

namespace ConsoleApp2.Core.Models
{
    public class TabOneModel
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string? UserId { get; set; }
        public string? ProgramTitle { get; set; }

        public string ProgramSummary { get; set; } = string.Empty;

        public string? ProgramDescription { get; set; }

        public List<string> KeySkills { get; set; } = new List<string>();
        public string ProgramBenifits { get; set; } = string.Empty;
        public string ApplicationCriteria { get; set; } = string.Empty;

        public string? ProgramType { get; set; }

        public DateTime ProgramStart { get; set; }

        public DateTime ApplicationStart { get; set; }

        public DateTime ApplicationClose { set; get; }

        public string Duration { set; get; } = string.Empty;

        public string? Location { get; set; }

        public bool FullyRemote { get; set; }

        public string MinQualifications { get; set; } = string.Empty;

        public int? maxApplications { get; set; } = null;
    }
}
