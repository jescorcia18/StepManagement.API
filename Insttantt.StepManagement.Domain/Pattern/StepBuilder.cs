using Insttantt.StepManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Domain.Pattern
{
    public class StepBuilder
    {
        private readonly Step _step;

        public StepBuilder()
        {
            _step = new Step();
        }

        public StepBuilder WithId(int id)
        {
            _step.StepId = id;
            return this;
        }
        public StepBuilder WithName(string name)
        {
            _step.StepName = name;
            return this;
        }

        public StepBuilder WithDescription(string description)
        {
            _step.StepDescription = description;
            return this;
        }

        public StepBuilder WithUrlEndPoint(string urlEndPoint)
        {
            _step.UrlEndPoint = urlEndPoint;
            return this;
        }

        public StepBuilder WithRequestType(string requestType)
        {
            _step.RequestType = requestType;
            return this;
        }

        public StepBuilder WithParameterType(string parameterType)
        {
            _step.ParameterType = parameterType;
            return this;
        }

        public Step Build()
        {
            return _step;
        }
    }
}
