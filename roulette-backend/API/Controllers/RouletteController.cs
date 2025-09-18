using core.application.dtos;
using core.application.interfaces;
using core.application.mapper;
using core.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Roulette.Api.Controllers;

[ApiController]
[Route("api/roulette")]
public class RouletteController : ControllerBase
{
    private readonly IRouletteService _svc;

    public RouletteController(IRouletteService svc) => _svc = svc;

    /// <summary>Generate a random spin of the roulette.</summary>
    [HttpGet("spin")]
    [ProducesResponseType(typeof(SpinResult), StatusCodes.Status200OK)]
    public ActionResult<SpinResult> Spin()
    {
        var result = _svc.Spin();
        return Ok(result);
    }

    /// <summary>Calculate the prize based on bet and spin result.</summary>
    [HttpPost("calculate-prize")]
    [ProducesResponseType(typeof(PrizeResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PrizeResult> CalculatePrize([FromBody] CalculatePrizeRequest req)
    {
        var bet = ApiToDomainMapper.ToDomainBet(req);
        var spin = ApiToDomainMapper.ToDomainSpin(req.Spin);
        var prize = _svc.CalculatePrize(bet, spin);
        return Ok(prize);
    }
}