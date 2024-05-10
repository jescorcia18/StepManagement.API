using Insttantt.StepManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Application.Common.Interfaces.Services
{
    public interface IStepService
    {
        Task<IEnumerable<StepResponse>> GetAllStepAsync();
        Task<StepResponse> GetStepByIdAsync(int id);
        Task<StepResponse> AddStepAsync(StepRequest step);
        Task UpdateStepAsync(int id, StepRequest step);
        Task DeleteStepAsync(int id);
    }
}
