using Insttantt.StepManagement.Application.Common.Interfaces.Repository;
using Insttantt.StepManagement.Application.Middleware;
using Insttantt.StepManagement.Domain.Entities;
using Insttantt.StepManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Infrastructure.Repositories
{
    public class StepFieldsRepository: IStepFieldsRepository
    {
        #region Global Variables
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExceptionHandler> _logger;
        #endregion

        #region Constructor
        public StepFieldsRepository(ApplicationDbContext context, ILogger<ExceptionHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region Public Methods

        public async Task<IEnumerable<StepFields>> GetAllStepFieldsAsync()
        {
            try
            {
                return await _context.stepFields.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"StepFields Repository (GetAll) error: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<StepFields>> GetAllStepFieldsByIdAsync( int stepId)
        {
            try
            {
                return await _context.stepFields.Where(s=>s.StepId.Equals(stepId)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"StepFields Repository (GetAll by StepId) error: {ex.Message}");
                throw;
            }
        }

        public async Task<StepFields> GetStepFieldsByIdAsync(int idStep, int idField)
        {
            try
            {
                var result = await _context.stepFields.Where(x => x.StepId == idStep && x.FieldId == idField).FirstOrDefaultAsync();

                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"StepFields Repository (GetById) error: {ex.Message}");
                throw;
            }

        }

        public async Task<StepFields> AddStepFieldsAsync(StepFields stepFields)
        {
            try
            {
                _context.stepFields.Add(stepFields);
                await _context.SaveChangesAsync();
                return stepFields;
            }
            catch (Exception ex)
            {
                _logger.LogError($"StepFields Repository (Add) error:{ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateStepFieldsAsync(StepFields stepFields)
        {
            try
            {
                var existingEntity = await _context.stepFields.FindAsync(stepFields.StepFieldId);

                if (existingEntity != null)
                {
                    existingEntity.StepFieldId = stepFields.StepFieldId;
                    existingEntity.StepId = stepFields.StepId;
                    existingEntity.FieldId = stepFields.FieldId;
                    existingEntity.InputOutput = stepFields.InputOutput;
                    _context.Entry(existingEntity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"StepFields Repository (Update) error:{ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteStepFieldsAsync(int stepFieldId)
        {
            try
            {
                var stepFieldsToDelete = await _context.stepFields.FindAsync(stepFieldId);
                _context.stepFields.Remove(stepFieldsToDelete!);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"StepFields Repository (Delete) error:{ex.Message}");
                return false;
            }
        }
        #endregion
    }
}
