namespace ConsoleApp2.Core.DTO.TabTwo.AdditionalQuestions
{
    public class ParagraphQuestionDTO: BaseQuestionDTO
    {
        public override string ToString()
        {
            return $"{this.Question}\n" + $"{this.Type}\n";
        }
    }

    public class YesNoQuestionDTO: BaseQuestionDTO 
    { 
        public bool? DisqualifyIfNo {  get; set; }

        public override string ToString()
        {
            return $"{this.Question}\n" + $"{this.Type}\n";
        }
    }

    public class DropdownQuestionDTO : BaseQuestionDTO
    {
        public List<string>? Options { get; set; }
        public override string ToString()
        {
            return $"{this.Question}\n" + $"{this.Type}\n" + $"{this.Options}\n";
        }
    }

    public class MultipleChoiceQuestionDTO: BaseQuestionDTO
    {
        public List<string>? Options { get; set; }
        public bool ShowOther {  get; set; }
        public int MaxChoiceAllowed {  get; set; }
    }
    public class DateQuestionDTO : BaseQuestionDTO{}

    public class NumberQuestionDTO: BaseQuestionDTO{}

}
