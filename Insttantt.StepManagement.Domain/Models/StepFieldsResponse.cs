
namespace Insttantt.StepManagement.Domain.Models
{
    public class StepFieldsResponse
    {
        public int StepFieldId { get; set; }
        public int StepId { get; set; }
        public int FieldId { get; set; }
        public string InputOuput { get; set; } = string.Empty;
    }
}
