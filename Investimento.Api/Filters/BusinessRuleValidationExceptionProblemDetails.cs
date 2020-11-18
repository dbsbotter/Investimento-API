using Investimento.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Investimento.Api.Filters
{
    public class BusinessRuleValidationExceptionProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            this.Title = exception.Message;
            this.Status = StatusCodes.Status400BadRequest;
            this.Detail = exception.Details;
            this.Type = "https://httpstatuses.com/400";
        }
    }
}
