using Investimento.Api.Extensions;
using Investimento.Domain.Handlers;
using Investimento.Domain.Models;
using Investimento.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Investimento.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("alpha-vantage")]
    [Authorize]
    public class AlphaVantageController : BaseController
    {
        private readonly CreateInvestimentHandler _createInvestimentHandler;
        private readonly IInvestimentRepository _investimentRepository;
        private readonly IHttpClientFactory _client;

        public AlphaVantageController(CreateInvestimentHandler createInvestimentHandler,
                                      IInvestimentRepository investimentRepository,
                                      IHttpClientFactory client)
        {
            _createInvestimentHandler = createInvestimentHandler;
            _investimentRepository = investimentRepository;
            _client = client;
        }

        /// <summary>
        /// Consultar o preço de um ticker
        /// </summary>
        /// <returns>Retorna o objeto de retorno encapsulado no objeto 'data'.</returns>
        /// <response code="200">Retorna o resultado da operação</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="500">Erro interno no servidor</response>
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(DataResult<IEnumerable<OutGlobalQuote>>), 200)]
        [HttpGet("global-quote")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> GetGlobalQuote([FromQuery] string stock)
        {
            var httpClient = _client.CreateClient("AlphaVantage");

            var query = new Dictionary<string, string>
            {
                ["function"] = "GLOBAL_QUOTE",
                ["symbol"] = stock,
                ["apikey"] = "H6NNH3NVT95GRZTL"
            };

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("/query", query));

            return DataResult(await response.Content.ReadAsJsonAsync<OutGlobalQuote>());
        }
    }
}