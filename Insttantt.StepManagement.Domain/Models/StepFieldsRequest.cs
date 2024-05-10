using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Domain.Models
{
    public class StepFieldsRequest
    {
        public int StepId { get; set; }
        public int FieldId { get; set; }
        public string InputOuput { get; set; } = string.Empty;
    }
}
