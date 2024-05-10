using Insttantt.StepManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Application.Common.Interfaces.Services
{
    public interface IStepFieldsService
    {
        Task<bool> UpdateStepFieldsAsync(StepFieldsResponse stepFields);
        Task<StepFieldsResponse> GetStepFieldsByIdAsync(StepFieldsRequest stepFieldsRequest);
        Task<IEnumerable<StepFieldsResponse>> GetAllStepFieldsAsync();
        Task<IEnumerable<StepFieldsResponse>> GetAllStepFieldsByIdAsync(int stepId);
        Task<StepFieldsResponse> AddStepFieldsAsync(StepFieldsRequest stepField);
        Task<bool> DeleteStepAsync(int id);
    }
}
