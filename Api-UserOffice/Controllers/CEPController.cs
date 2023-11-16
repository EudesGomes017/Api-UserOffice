using Domain.Integracao.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api_UserOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CEPController : ControllerBase
    {
       
        [HttpGet(Name = "Busca CEP")]
        public async Task<IActionResult> GetEndress([FromServices] IViaCepIntegracao service, string cep)
        {
            var users = await service.ObterDadosViaCep(cep);
            return this.StatusCode(StatusCodes.Status200OK, users);
        }
    }
}

