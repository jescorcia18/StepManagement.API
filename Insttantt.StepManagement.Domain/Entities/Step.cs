using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Domain.Entities
{
    
    [Table("Step")]
    public class Step
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StepId { get; set; }
        [Required]
        [MaxLength(200)]
        public string StepName { get; set; } = string.Empty;
        public string? StepDescription { get; set; }
        public string? UrlEndPoint { get; set; }
        
        [MaxLength(6), MinLength(3)]
        public string? RequestType { get; set; }
        public string? ParameterType { get; set; }

        public virtual ICollection<StepFields>? StepFields { get; set; }

        // Constructor para inicializar la colección
        public Step()
        {
            StepFields = new List<StepFields>();
        }
    }
}
