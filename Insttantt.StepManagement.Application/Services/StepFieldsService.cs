using Insttantt.StepManagement.Application.Common.Interfaces.Repository;
using Insttantt.StepManagement.Application.Common.Interfaces.Services;
using Insttantt.StepManagement.Application.Common.Interfaces.Utils;
using Insttantt.StepManagement.Domain.Entities;
using Insttantt.StepManagement.Domain.Models;
using Insttantt.StepManagement.Domain.Pattern;


namespace Insttantt.StepManagement.Application.Services
{
    public class StepFieldsService : IStepFieldsService
    {
        #region Global Variables
        private readonly IStepFieldsRepository _stepFieldsRepository;
        private readonly IUtility _utility;
        #endregion

        #region Constructor
        public StepFieldsService(IStepFieldsRepository stepFieldsRepository, IUtility utility)
        {
            _stepFieldsRepository = stepFieldsRepository;
            _utility = utility;
        }
        #endregion

        #region Public Methods
        public async Task<StepFieldsResponse> GetStepFieldsByIdAsync(StepFieldsRequest stepFieldsRequest)
        {
            var result = await _stepFieldsRepository.GetStepFieldsByIdAsync(stepFieldsRequest.StepId, stepFieldsRequest.FieldId);
            return await _utility.MapToStepFieldsResponse(result);
        }

        public async Task<IEnumerable<StepFieldsResponse>> GetAllStepFieldsAsync()
        {
            var result = await _stepFieldsRepository.GetAllStepFieldsAsync();
            return await _utility.MapToStepFieldsResponse(result);
        }

        public async Task<IEnumerable<StepFieldsResponse>> GetAllStepFieldsByIdAsync(int stepId)
        {
            var result = await _stepFieldsRepository.GetAllStepFieldsByIdAsync(stepId);
            return await _utility.MapToStepFieldsResponse(result);
        }

        public async Task<StepFieldsResponse> AddStepFieldsAsync(StepFieldsRequest stepField)
        {
            var entity = await ToStepFieldsBuild(stepField);
            var result = await _stepFieldsRepository.AddStepFieldsAsync(entity);
            return await _utility.MapToStepFieldsResponse(result);
        }

        public async Task<bool> UpdateStepFieldsAsync(StepFieldsResponse stepFields)
        {
            var entity = await ToStepFieldsBuild(stepFields);
            var result = await _stepFieldsRepository.UpdateStepFieldsAsync(entity);
            return result;

        }
        public async Task<bool> DeleteStepAsync(int id)
        {
            return await _stepFieldsRepository.DeleteStepFieldsAsync(id);
        }
        #endregion

        #region Private Methods
        private async Task<StepFields> ToStepFieldsBuild(StepFieldsRequest stepField)
        {
            return await Task.FromResult(
                new StepFieldsBuilder()
                .WithStepId(stepField.StepId)
                .WithFieldId(stepField.FieldId)
                .WithInputOutput(stepField.InputOuput)
                .Build());
        }

        private async Task<StepFields> ToStepFieldsBuild(StepFieldsResponse stepField)
        {
            return await Task.FromResult(
                new StepFieldsBuilder()
                .WithStepFieldId(stepField.StepFieldId)
                .WithStepId(stepField.StepId)
                .WithFieldId(stepField.FieldId)
                .WithInputOutput(stepField.InputOuput)
                .Build());
        }
        #endregion
    }
}
