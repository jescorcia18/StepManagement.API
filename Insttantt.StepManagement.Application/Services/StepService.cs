using Insttantt.StepManagement.Application.Common.Interfaces.Repository;
using Insttantt.StepManagement.Application.Common.Interfaces.Services;
using Insttantt.StepManagement.Application.Common.Interfaces.Utils;
using Insttantt.StepManagement.Application.Middleware;
using Insttantt.StepManagement.Domain.Entities;
using Insttantt.StepManagement.Domain.Enum;
using Insttantt.StepManagement.Domain.Models;
using Insttantt.StepManagement.Domain.Pattern;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.WebSockets;

namespace Insttantt.StepManagement.Application.Services
{
    public class StepService : IStepService
    {
        #region Global Variables
        private readonly IStepRepository _stepRepository;
        private readonly IStepFieldsService _stepFieldsService;
        private readonly IUtility _utility;
        private readonly ILogger<ExceptionHandler> _logger;
        #endregion

        #region Constructor
        public StepService(IStepRepository stepRepository, IStepFieldsService stepFieldsService, IUtility utility, ILogger<ExceptionHandler> logger)
        {
            _stepRepository = stepRepository;
            _stepFieldsService = stepFieldsService;
            _utility = utility;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<StepResponse>> GetAllStepAsync()
        {
            var result = await _stepRepository.GetAllStepAsync();
            return await _utility.MapToStepResponse(result);
        }

        public async Task<StepResponse> GetStepByIdAsync(int id)
        {
            var result = await _stepRepository.GetStepByIdAsync(id);
            if (result == null)
                return null!;
            return await _utility.MapToStepResponse(result);
        }

        public async Task<StepResponse> AddStepAsync(StepRequest step)
        {
            try
            {
                var watch = Stopwatch.StartNew();
                _logger.LogInformation($"Starts the process of adding Step and fields - Time: {watch}");

                if (await ValidateFields(step.StepFieldsList!))
                {
                    var entity = await ToStepBuild(step);
                    var stepResp = await _stepRepository.AddStepAsync(entity);
                    _logger.LogInformation($"Step is added: {JsonConvert.SerializeObject(stepResp)}");

                    if (step.StepFieldsList != null && stepResp != null)
                    {
                        step.StepFieldsList.ForEach(step => step.StepId = stepResp.StepId);
                        var listStepFields = await StepFieldsProcess(step.StepFieldsList, MethodType.Add);
                        var result = await _utility.MapToStepResponse(stepResp!);
                        result.StepFieldsList = listStepFields;

                        watch.Stop();
                        _logger.LogInformation($"Finish the process of adding Step and fields - Time: {watch}");

                        return result;
                    }
                    throw new Exception("Validate Fields is False.");
                }
                else
                    throw new Exception("No step was added");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when adding Step and fields:  {ex.Message}");
                throw;
            }
        }

        public async Task UpdateStepAsync(int id, StepRequest step)
        {
            try
            {
                var entity = await ToStepBuild(id, step);
                await _stepRepository.UpdateStepAsync(entity);

                if (step.StepFieldsList != null)
                {
                    var listStepFields = await StepFieldsProcess(step.StepFieldsList, MethodType.Update);
                    if (listStepFields == null)
                        throw new Exception($"Error when updating Step and fields");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when updating Step and fields:  {ex.Message}");
                throw;
            }
        }

        public async Task DeleteStepAsync(int id)
        {
            var stepFieldsrResp = await GetStepByIdAsync(id);
            await _stepRepository.DeleteStepAsync(id);

            if(stepFieldsrResp.StepFieldsList != null)
            {
                await StepFieldsProcess(stepFieldsrResp.StepFieldsList, MethodType.Delete);
            }
                
        }
        #endregion

        #region Private Methods

        private async Task<List<StepFieldsResponse>> StepFieldsProcess(List<StepFieldsResponse> stepFields, MethodType methodType)
        {
            var method = methodType.ToString();
            try
            {
                var listStepFields = new List<StepFieldsResponse>();
                var stepFieldsResp = new StepFieldsResponse();
                bool resp = false;

                foreach(var stepfield in stepFields)
                {
                    if (stepfield.StepFieldId != 0 && stepfield.StepId > 0 && stepfield.FieldId > 0 && methodType!=MethodType.Delete) 
                        methodType = MethodType.Update;
                    else
                    {
                        if (stepfield.StepFieldId == 0 && stepfield.StepId > 0 && stepfield.FieldId > 0 && methodType != MethodType.Delete)
                            methodType = MethodType.Add;
                    }

                    if (methodType == MethodType.Add)
                    {
                        var stepfieldReq = await _utility.MapToStepFieldsRequest(stepfield);
                        stepFieldsResp = await _stepFieldsService.AddStepFieldsAsync(stepfieldReq);
                    }
                    else if (methodType == MethodType.Update)
                    {
                        resp = await _stepFieldsService.UpdateStepFieldsAsync(stepfield);

                        if (resp)
                            stepFieldsResp = stepfield;
                        else
                            _logger.LogError($"Error when updating Fields");
                    }
                    else
                    {
                        await _stepFieldsService.DeleteStepAsync(stepfield.StepFieldId);
                        resp = true;
                        if (resp!)
                            _logger.LogError($"Error when Deleting Fields");
                    }

                    if (stepFieldsResp.StepFieldId <= 0 || resp == false)
                        _logger.LogError($"Error when {method} Step and fields");
                    else
                    {
                        if (methodType != MethodType.Delete)
                            listStepFields.Add(stepFieldsResp);

                        _logger.LogInformation($"StepFields is {method}: {JsonConvert.SerializeObject(stepFieldsResp)}");
                    }
                }
                return await Task.FromResult(listStepFields);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when {method} Step and fields:  {ex.Message}");
                throw new Exception($"Error when {method} Step and fields");
            }
        }
        private async Task<Step> ToStepBuild(int id, StepRequest step)
        {
            return await Task.FromResult(
                new StepBuilder()
                .WithId(id)
                .WithName(step.StepName)
                .WithDescription(step.StepDescription!)
                .WithUrlEndPoint(step.UrlEndPoint!)
                .WithRequestType(step.RequestType!)
                .WithParameterType(step.ParameterType!)
                .Build());

        }
        private async Task<Step> ToStepBuild(StepRequest step)
        {
            return await Task.FromResult(
                new StepBuilder()
                .WithName(step.StepName)
                .WithDescription(step.StepDescription!)
                .WithUrlEndPoint(step.UrlEndPoint!)
                .WithRequestType(step.RequestType!)
                .WithParameterType(step.ParameterType!)
                .Build());

        }

        private async Task<bool> ValidateFields(List<StepFieldsResponse> stepFields)
        {
            var isValid = false;
            foreach (var item in stepFields)
            {
                if (item.FieldId > 0)
                    if (string.IsNullOrEmpty(item.InputOuput))
                        isValid = true; break;
            }

            return await Task.FromResult(isValid);
        }
        #endregion
    }
}
