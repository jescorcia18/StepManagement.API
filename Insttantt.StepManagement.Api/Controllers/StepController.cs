using Insttantt.StepManagement.Application.Common.Interfaces.Services;
using Insttantt.StepManagement.Application.Middleware;
using Insttantt.StepManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Insttantt.StepManagement.Api.Controllers
{
    /// <summary>
    /// Step Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : ControllerBase
    {
        #region Global Variables
        private readonly IStepService _stepService;
        private readonly ILogger<ExceptionHandler> _logger;
        #endregion

        public StepController(IStepService stepService, ILogger<ExceptionHandler> logger)
        {
            _stepService = stepService;
            _logger = logger;

        }

        /// <summary>
        /// Get Step ALL
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StepResponse>>> GetStep()
        {
            try
            {
                _logger.LogInformation($"Start Endpoint : StepController.GetAllStep");
                var step = await _stepService.GetAllStepAsync();
                _logger.LogInformation($"Finish Endpoint : StepController.GetAllStep");
                return Ok(step);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error StepController.GetAllStep: {ex.Message}");
                return BadRequest(ex.Message);
            }           
        }

        /// <summary>
        /// Get Step
        /// </summary>
        /// <param name="id">Step's id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StepResponse>> GetStep(int id)
        {
            try
            {
                _logger.LogInformation($"Start Endpoint : StepController.GetStepById");
                var step = await _stepService.GetStepByIdAsync(id);
                _logger.LogInformation($"Finish Endpoint : StepController.GetStepById");
                if (step == null)
                {
                    return NotFound("Step not found");
                }               
                return Ok(step);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error StepController.GetStepById: {ex.Message}");
                return BadRequest(ex.Message);
            }           
        }

        /// <summary>
        /// Add Step
        /// </summary>
        /// <param name="step">Step add </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<StepResponse>> AddStep(StepRequest step)
        {
            try
            {
                _logger.LogInformation($"Start Endpoint : StepController.AddStep");
                var newStep = await _stepService.AddStepAsync(step);
                _logger.LogInformation($"Finish Endpoint : StepController.AddStep");
                return CreatedAtAction(nameof(GetStep), new { id = newStep.StepId }, newStep);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error StepController.AddStep: {ex.Message}");
                return BadRequest(ex.Message);
            }
            
        }
        
        /// <summary>
        /// Update Step
        /// </summary>
        /// <param name="id">step's id</param>
        /// <param name="step">step record properties to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStep(int id, StepRequest step)
        {
            try
            {
                _logger.LogInformation($"Start Endpoint : StepController.UpdateStep");
                var stepExist = await _stepService.GetStepByIdAsync(id);
                if (id != stepExist.StepId)
                {
                    return BadRequest($"Step with Id: {id} does not exist");
                }
                await _stepService.UpdateStepAsync(id, step);
                _logger.LogInformation($"Finish Endpoint : StepController.UpdateStep");
                return Ok("Update step is successull");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error StepController.UpdateStep: {ex.Message}");
                return BadRequest(ex.Message);
            }            
        }


        /// <summary>
        /// Step Delete
        /// </summary>
        /// <param name="id">Step's id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStep(int id)
        {
            try
            {
                _logger.LogInformation($"Start Endpoint : StepController.DeleteStep");
                await _stepService.DeleteStepAsync(id);
                _logger.LogInformation($"Finish Endpoint : StepController.DeleteStep");
                return Ok("Delete step is successull");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error StepController.DeleteStep: {ex.Message}");
                return BadRequest(ex.Message);
            }           
        }
    }
}
