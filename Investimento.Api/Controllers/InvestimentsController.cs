using Investimento.Domain.Commands;
using Investimento.Domain.Entities;
using Investimento.Domain.Handlers;
using Investimento.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investimento.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("investiments")]
    [Authorize]
    public class InvestimentsController : BaseController
    {
        private readonly CreateInvestimentHandler _createInvestimentHandler;
        private readonly IInvestimentRepository _investimentRepository;

        public InvestimentsController(CreateInvestimentHandler createInvestimentHandler,
                                      IInvestimentRepository investimentRepository)
        {
            _createInvestimentHandler = createInvestimentHandler;
            _investimentRepository = investimentRepository;
        }

        /// <summary>
        /// Consultar os investimentos do usuário
        /// </summary>
        /// <returns>Retorna o objeto de retorno encapsulado no objeto 'data'.</returns>
        /// <response code="200">Retorna o resultado da operação</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="500">Erro interno no servidor</response>
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(DataResult<IEnumerable<Investiment>>), 200)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return DataResult(await _investimentRepository.GetAllByUser(Guid.Parse(User.Identity.Name)));
        }

        /// <summary>
        /// Realizar a gravação de um novo Investimento
        /// </summary>
        /// <returns>Retorna o objeto de retorno encapsulado no objeto 'data'.</returns>
        /// <response code="200">Retorna o resultado da operação</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="500">Erro interno no servidor</response>
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(Investiment), 200)]
        [HttpPost]
        public async Task<IActionResult> Create(InvestimentCommand command)
        {
            return DataResult(await _createInvestimentHandler.Handle(command));
        }
    }
}