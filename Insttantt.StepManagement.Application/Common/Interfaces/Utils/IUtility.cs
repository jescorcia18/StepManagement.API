using Insttantt.StepManagement.Domain.Entities;
using Insttantt.StepManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Application.Common.Interfaces.Utils
{
    public interface IUtility
    {
        Task<string> Decrypt(string key, string cipherText);
        Task<string> Encrypt(string key, string data);
        Task<IEnumerable<StepResponse>> MapToStepResponse(IEnumerable<Step> step);
        Task<StepResponse> MapToStepResponse(Step step);
        Task<StepFieldsResponse> MapToStepFieldsResponse(StepFields stepFields);
        Task<IEnumerable<StepFieldsResponse>> MapToStepFieldsResponse(IEnumerable<StepFields> step);
        Task<StepFieldsRequest> MapToStepFieldsRequest(StepFieldsResponse stepFields);

    }
}
