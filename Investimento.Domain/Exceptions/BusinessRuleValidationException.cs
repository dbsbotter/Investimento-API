using System;

namespace Investimento.Domain.Exceptions
{
    public class BusinessRuleValidationException : Exception
    {
        public string Details { get; }

        public BusinessRuleValidationException(string message) : base(message)
        {
            this.Details = message;
        }
    }
}
