using Insttantt.StepManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Application.Common.Interfaces.Repository
{
    public interface IStepRepository
    {
        Task<IEnumerable<Step>> GetAllStepAsync();
        Task<Step> GetStepByIdAsync(int id);
        Task<Step> AddStepAsync(Step step);
        Task UpdateStepAsync(Step step);
        Task DeleteStepAsync(int id);
    }
}
