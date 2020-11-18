using Investimento.Domain.Commands;
using Investimento.Domain.Entities;
using Investimento.Domain.Handlers;
using Investimento.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investimento.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("users")]

    public class UsersController : BaseController
    {
        private readonly CreateUserHandler _createUserHandler;
        private readonly IUserRepository _userRepository;

        public UsersController(CreateUserHandler createUserHandler,
                               IUserRepository userRepository)
        {
            _createUserHandler = createUserHandler;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Consultar todos os usuários
        /// </summary>
        /// <returns>Retorna o objeto de retorno encapsulado no objeto 'data'.</returns>
        /// <response code="200">Retorna o resultado da operação</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="500">Erro interno no servidor</response>
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(DataResult<IEnumerable<User>>), 200)]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return DataResult(await _userRepository.GetAll());
        }

        /// <summary>
        /// Cadastrar um usuário
        /// </summary>
        /// <returns>Retorna o objeto de retorno encapsulado no objeto 'data'.</returns>
        /// <response code="200">Retorna o resultado da operação</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="500">Erro interno no servidor</response>
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(User), 200)]
        [HttpPost]
        public async Task<IActionResult> Create(UserCommand command)
        {
            return DataResult(await _createUserHandler.Handle(command));
        }
    }
}