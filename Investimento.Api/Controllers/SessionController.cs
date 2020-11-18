using Investimento.Domain.Commands;
using Investimento.Domain.Handlers;
using Investimento.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Investimento.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("session")]
    public class SessionController : BaseController
    {
        private readonly AuthUserHandler _authUserHandler;

        public SessionController(AuthUserHandler authUserHandler)
        {
            _authUserHandler = authUserHandler;
        }

        /// <summary>
        /// Realizar Login na aplicação
        /// </summary>
        /// <returns>Retorna o objeto de retorno encapsulado no objeto 'data'.</returns>
        /// <response code="200">Retorna o resultado da operação</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="500">Erro interno no servidor</response>
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(DataResult<OutToken>), 200)]
        [HttpPost]
        public async Task<IActionResult> Login(SessionCommand command)
        {
            return DataResult(await _authUserHandler.Handle(command));
        }
    }
}
