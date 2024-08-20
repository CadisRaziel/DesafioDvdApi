using Asp.Versioning;
using DesafioDvD.Core;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDvD.WebApi.Controllers
{
    //ApiVersion -> do pacaote Asp.Versioning.MVC
    //v{version:apiVersion} -> vai pegar o valor daqui [ApiVersion("1")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        //Metodo de resposta customizada utilizada no nosso Controller
        protected ActionResult CustomResponse(int status, bool success, object data = null)
        {
            //(status, success) -> Tupla, dois retornos
            return (status, success) switch
            {
                (404, false) => NotFound(new BaseResponse { StatusCode = status, Success = success, Message = "No element found." }),
                (400, false) => BadRequest(new BaseResponse { StatusCode = status, Success = success, Message = "Erros during the transaction." }),
                (201, true) => Ok(new BaseResponse { StatusCode = status, Success = success, Message = "Created", Data = data }),
                (200, true) => Ok(new BaseResponse { StatusCode = status, Success = success, Data = data }),
            };
        }
    }
}
