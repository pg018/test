using ConsoleApp2.Core.CustomValidators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConsoleApp2.Core.DTO.TabOne
{
    public class IncomingTabOneDTO
    {
        [DefaultPropertyValidator("User ID")]
        public string? UserId { get; set; }

        [DefaultPropertyValidator("Program Title")]
        public string? ProgramTitle { get; set; }

        public string ProgramSummary { get; set; } = string.Empty;

        [DefaultPropertyValidator("Program Description")]
        public string? ProgramDescription { get; set; }

        public List<string> KeySkills { get; set; } = new List<string>();
        public string ProgramBenifits { get; set; } = string.Empty;
        public string ApplicationCriteria { get; set; } = string.Empty;

        [DefaultPropertyValidator("Program Type")]
        public string? ProgramType { get; set; }

        [JsonPropertyName("ProgramStart")]
        public DateTime ProgramStart { get; set; }

        [StartEndDateValidator("ApplicationClose")]
        public DateTime ApplicationStart { get; set; }

        public DateTime ApplicationClose { set; get; }

        public string Duration { set; get; } = string.Empty;

        [LocationOrRemoteValidator("FullyRemote")]
        public string? Location { get; set; }

        public bool FullyRemote { get; set; }

        public string MinQualifications { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Max Applications must atleast be 1"), JsonPropertyName("maxApplications")]
        public int? maxApplications { get; set; } = null;

        public override string ToString()
        {
            return $"ProgramTitle: {ProgramTitle},\n" +
                   $"ProgramSummary: {ProgramSummary},\n" +
                   $"ProgramDescription: {ProgramDescription},\n" +
                   $"KeySkills: [{string.Join(", ", KeySkills)}],\n" +
                   $"ProgramBenifits: {ProgramBenifits},\n" +
                   $"ApplicationCriteria: {ApplicationCriteria},\n" +
                   $"ProgramType: {ProgramType},\n" +
                   $"ProgramStart: {ProgramStart},\n" +
                   $"ApplicationStart: {ApplicationStart},\n" +
                   $"ApplicationClose: {ApplicationClose},\n" +
                   $"Duration: {Duration},\n" +
                   $"Location: {Location},\n" +
                   $"FullyRemote: {FullyRemote},\n" +
                   $"MinQualifications: {MinQualifications},\n" +
                   $"maxApplications: {maxApplications}";
        }

    }
}
