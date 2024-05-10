using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Domain.Models
{
    public class StepRequest
    {
        public string StepName { get; set; } = string.Empty;
        public string? StepDescription { get; set; }
        public string? UrlEndPoint { get; set; }
        public string? RequestType { get; set; }
        public string? ParameterType { get; set; }
        public List<StepFieldsResponse>? StepFieldsList { get; set; }
    }
}
