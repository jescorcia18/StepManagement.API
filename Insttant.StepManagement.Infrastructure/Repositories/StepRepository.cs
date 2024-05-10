using Insttantt.StepManagement.Application.Common.Interfaces.Repository;
using Insttantt.StepManagement.Domain.Entities;
using Insttantt.StepManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Insttantt.StepManagement.Application.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Insttantt.StepManagement.Infrastructure.Repositories
{
    public class StepRepository : IStepRepository
    {
        #region Global Variables
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExceptionHandler> _logger;
        #endregion

        #region Constructor
        public StepRepository(ApplicationDbContext context, ILogger<ExceptionHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<Step>> GetAllStepAsync()
        {
            try
            {
                return await _context.step.Include(x => x.StepFields).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Step Repository (GetAll) error: {ex.Message}");
                throw;
            }
        }
        public async Task<Step> GetStepByIdAsync(int id)
        {
            try
            {
                var result = await _context.step
                    .Include(s => s.StepFields)
                    .FirstOrDefaultAsync(s=> s.StepId == id);

                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Step Repository (GetById) error: {ex.Message}");
                throw;
            }

        }

        public async Task<Step> AddStepAsync(Step step)
        {
            try
            {
                _context.step.Add(step);
                await _context.SaveChangesAsync();
                return step;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Step Repository (Add) error: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateStepAsync(Step step)
        {
            try
            {
                var existingEntity = await _context.step.FindAsync(step.StepId);

                if (existingEntity != null)
                {
                    existingEntity.StepId = step.StepId;
                    existingEntity.StepName = step.StepName;
                    existingEntity.StepDescription = step.StepDescription;
                    existingEntity.RequestType = step.RequestType;
                    existingEntity.ParameterType = step.ParameterType;
                    existingEntity.UrlEndPoint =step.UrlEndPoint;
                    _context.Entry(existingEntity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Step Repository (Update) error: {ex.Message}");
                throw;
            }

        }

        public async Task DeleteStepAsync(int id)
        {
            try
            {
                var stepToDelete = await _context.step.FindAsync(id);
                _context.step.Remove(stepToDelete!);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Step Repository (Delete) error: {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
