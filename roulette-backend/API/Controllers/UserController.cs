using core.application.interfaces;
using Microsoft.AspNetCore.Mvc;
using Roulette.Application.Contracts;

namespace Roulette.Api.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _svc;
    public UsersController(IUserService svc) => _svc = svc;

    [HttpGet("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetUser([FromRoute] string name, CancellationToken ct)
    {
        var balance = await _svc.GetBalance(name, ct);
        if (balance is null) return NotFound();
        return Ok(new { name, balance });
    }

    [HttpGet("{name}/balance")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<object>> GetBalance([FromRoute] string name, CancellationToken ct)
    {
        var balance = await _svc.GetBalance(name, ct) ?? 0m;
        return Ok(new { name, balance });
    }

    [HttpPost("save-balance")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> SaveBalance([FromBody] SaveBalanceRequest req, CancellationToken ct)
    {
        var balance = await _svc.SaveBalance(req, ct);
        return Ok(new { name = req.Name, balance });
    }
}
