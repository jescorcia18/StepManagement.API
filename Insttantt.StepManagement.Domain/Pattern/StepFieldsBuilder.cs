using Insttantt.StepManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Domain.Pattern
{
    public class StepFieldsBuilder
    {
        private readonly StepFields _stepFields;

        public StepFieldsBuilder()
        {
            _stepFields = new StepFields();
        }

        public StepFieldsBuilder WithStepFieldId(int stepFieldId)
        {
            _stepFields.StepFieldId = stepFieldId;
            return this;
        }
        public StepFieldsBuilder WithStepId(int stepId)
        {
            _stepFields.StepId = stepId;
            return this;
        }
        public StepFieldsBuilder WithFieldId(int fieldId)
        {
            _stepFields.FieldId= fieldId;
            return this;
        }
        public StepFieldsBuilder WithInputOutput(string inputOutput)
        {
            _stepFields.InputOutput = inputOutput;
            return this;
        }

        public StepFields Build()
        {
            return _stepFields;
        }
    }
}
