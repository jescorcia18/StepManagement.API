using Insttantt.StepManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Application.Common.Interfaces.Repository
{
    public interface IStepFieldsRepository
    {
        Task<StepFields> AddStepFieldsAsync(StepFields stepFields);
        Task<bool> UpdateStepFieldsAsync(StepFields stepFields);
        Task<bool> DeleteStepFieldsAsync(int stepFieldId);
        Task<StepFields> GetStepFieldsByIdAsync(int idStep, int idField);
        Task<IEnumerable<StepFields>> GetAllStepFieldsAsync();
        Task<IEnumerable<StepFields>> GetAllStepFieldsByIdAsync(int stepId);
    }
}
