using core.application.interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;
    public UsersController(IUserRepository repo) => _repo = repo;

    [HttpGet("{name}/balance")]
    public async Task<ActionResult<object>> GetBalance([FromRoute] string name, CancellationToken ct)
    {
        var balance = await _repo.GetBalanceAsync(name, ct);
        return Ok(new { name, balance });
    }

    public sealed class SaveBalanceRequest
    {
        public string Name { get; set; } = default!;
        public decimal Delta { get; set; }
    }

    [HttpPost("save-balance")]
    public async Task<ActionResult<object>> SaveBalance([FromBody] SaveBalanceRequest req, CancellationToken ct)
    {
        var newBalance = await _repo.SaveDeltaAsync(req.Name, req.Delta, ct);
        return Ok(new { name = req.Name, balance = newBalance });
    }
}
