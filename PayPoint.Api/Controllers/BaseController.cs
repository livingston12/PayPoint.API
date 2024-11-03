
using Microsoft.AspNetCore.Mvc;

namespace PayPoint.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected static string ErrorMessageBadRequest => "Error Inesperado: intente de nuevo o contacte con el administrador.";
}
