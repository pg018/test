using ConsoleApp2.Core.DTO.TabOne;
using ConsoleApp2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Core.ModelExtensions
{
    public static class TabOneModelExtension
    {
        public static TabOneModel ConvertTabOneDTOToModel(this IncomingTabOneDTO tabOneModel)
        {
            return new TabOneModel
            {
                id = Guid.NewGuid().ToString(),
                UserId = tabOneModel.UserId,
                ProgramTitle = tabOneModel.ProgramTitle,
                ProgramSummary = tabOneModel.ProgramSummary,
                ProgramDescription = tabOneModel.ProgramDescription,
                KeySkills = tabOneModel.KeySkills,
                ProgramBenifits = tabOneModel.ProgramBenifits,
                ApplicationCriteria = tabOneModel.ApplicationCriteria,
                ProgramType = tabOneModel.ProgramType,
                ProgramStart = tabOneModel.ProgramStart,
                ApplicationStart = tabOneModel.ApplicationStart,
                ApplicationClose = tabOneModel.ApplicationClose,
                Duration = tabOneModel.Duration,
                Location = tabOneModel.Location,
                FullyRemote = tabOneModel.FullyRemote,
                MinQualifications = tabOneModel.MinQualifications,
                maxApplications = tabOneModel.maxApplications,
            };
        }

        public static TabOneModel ConvertOutTabOneToModel(this OutgoingTabOneDTO tabOneModel)
        {
            return new TabOneModel
            {
                
                id = tabOneModel.id,
                UserId = tabOneModel.UserId,
                ProgramTitle = tabOneModel.ProgramTitle,
                ProgramSummary = tabOneModel.ProgramSummary,
                ProgramDescription = tabOneModel.ProgramDescription,
                KeySkills = tabOneModel.KeySkills,
                ProgramBenifits = tabOneModel.ProgramBenifits,
                ApplicationCriteria = tabOneModel.ApplicationCriteria,
                ProgramType = tabOneModel.ProgramType,
                ProgramStart = tabOneModel.ProgramStart,
                ApplicationStart = tabOneModel.ApplicationStart,
                ApplicationClose = tabOneModel.ApplicationClose,
                Duration = tabOneModel.Duration,
                Location = tabOneModel.Location,
                FullyRemote = tabOneModel.FullyRemote,
                MinQualifications = tabOneModel.MinQualifications,
                maxApplications = tabOneModel.maxApplications,
            };
        }
    }
}
