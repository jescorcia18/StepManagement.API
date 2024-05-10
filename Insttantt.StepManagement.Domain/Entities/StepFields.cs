using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Domain.Entities
{
    [Table(name: "StepFields")]
    public class StepFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StepFieldId { get; set; }

        [Column(name: "StepId")]
        public int StepId { get; set; }

        [Column(name: "FieldId")]
        public int FieldId { get; set; }

        [MaxLength(1)]
        [Description("I = Input, O = Output")]
        [Column(name: "InputOuput")]
        public string InputOutput { get; set; } = string.Empty;
    }
}
