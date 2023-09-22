
using ConsoleApp2.Core.DTO.TabTwo;
using ConsoleApp2.Core.DTO.TabTwo.PersonalInfo;
using ConsoleApp2.Core.Models;

namespace ConsoleApp2.Core.ModelExtensions
{
    public static class TabTwoModelExtension
    {
        public static TabTwoModel ConvertTabTwoDTOToModel(this IncomingTabTwoDTO incomingTabTwoDTO, TabOneModel tabOneModel)
        {
            PersonalInfoTwoModel personalInfo = new PersonalInfoTwoModel
            {
                PhoneNumber = incomingTabTwoDTO.PersonalInfo.PhoneNumber,
                Nationality = incomingTabTwoDTO.PersonalInfo.Nationality,
                CurrentResidence = incomingTabTwoDTO.PersonalInfo.CurrentResidence,
                IDNumber = incomingTabTwoDTO.PersonalInfo.IDNumber,
                DOB = incomingTabTwoDTO.PersonalInfo.DOB,
                Gender = incomingTabTwoDTO.PersonalInfo.Gender,
                AdditionalQuestions = incomingTabTwoDTO.PersonalInfo.AdditionalQuestions,
                FirstName = new PersonalInfoItemTabTwoDTO { Internal = true, Hide = false },
                LastName = new PersonalInfoItemTabTwoDTO { Internal = true, Hide = false },
                Email = new PersonalInfoItemTabTwoDTO { Internal = true, Hide = false }
            };

            return new TabTwoModel
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
                maxApplications = tabOneModel.maxApplications,
                CoverImage = incomingTabTwoDTO.CoverImage,
                PersonalInfo = personalInfo,
                ProfileInfo = incomingTabTwoDTO.ProfileInfo,
                AdditionalQuestions = incomingTabTwoDTO.AdditionalQuestions,
            };
        }
    }
}
