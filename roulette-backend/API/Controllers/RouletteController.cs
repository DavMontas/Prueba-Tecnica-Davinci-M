using core.application.dtos;
using core.application.interfaces;
using Microsoft.AspNetCore.Mvc;
using Roulette.Application.Contracts;

namespace Roulette.Api.Controllers;

[ApiController]
[Route("api/roulette")]
public sealed class RouletteController : ControllerBase
{
    private readonly IRouletteService _svc;
    public RouletteController(IRouletteService svc) => _svc = svc;

    /// <summary>Genera un spin aleatorio (servidor).</summary>
    [HttpGet("spin")]
    [ProducesResponseType(typeof(SpinResult), StatusCodes.Status200OK)]
    public ActionResult<SpinResult> Spin() => Ok(_svc.Spin());

    /// <summary>Resuelve una apuesta. El servidor genera el spin. Devuelve 'prize' (monto del premio) y el 'outcome'.</summary>
    [HttpPost("resolve-bet")]
    [ProducesResponseType(typeof(ResolveBetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ResolveBetResponse> ResolveBet([FromBody] ResolveBetRequest req)
    {
        var result = _svc.ResolveBet(req);
        return Ok(result);
    }
}
