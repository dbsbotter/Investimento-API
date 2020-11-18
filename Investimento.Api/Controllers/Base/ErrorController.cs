using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Investimento.Api.Controllers
{
    [Route("/errors")]
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public class ErrorsController : ControllerBase
    {
        [Route("{code}")]
        public IActionResult Error(int code)
        {
            string detail = string.Empty;

            switch (code)
            {
                case 400:
                    detail = "Requisição inválida";
                    break;
                case 401:
                    detail = "Acesso ao recurso não autorizado";
                    break;
                case 403:
                    detail = "Acesso ao recurso negado";
                    break;
                case 404:
                    detail = "Recurso não encontrado";
                    break;
                case 405:
                    detail = "Método não permitido";
                    break;
                case 500:
                    detail = "Um erro ocorreu no servidor de aplicação";
                    break;

            }

            return new ObjectResult(new ProblemDetails
            {
                Status = code,
                Type = $"https://httpstatuses.com/{code}",
                Detail = detail ?? $"{code}"
            });
        }
    }
}