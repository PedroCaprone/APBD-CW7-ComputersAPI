using APBD_CW7_ComputersAPI.DTOs;
using APBD_CW7_ComputersAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_CW7_ComputersAPI.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IPcService _pcService;

    public PcsController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PcResponseDto>>> GetAllPcs()
    {
        var pcs = await _pcService.GetAllPcsAsync();

        return Ok(pcs);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PcResponseDto>> GetPcById(int id)
    {
        var pc = await _pcService.GetPcByIdAsync(id);

        if (pc is null)
        {
            return NotFound();
        }

        return Ok(pc);
    }

    [HttpGet("{id:int}/components")]
    public async Task<ActionResult<IEnumerable<PcComponentResponseDto>>> GetPcComponents(int id)
    {
        var components = await _pcService.GetPcComponentsAsync(id);

        if (components is null)
        {
            return NotFound();
        }

        return Ok(components);
    }

    [HttpPost]
    public async Task<ActionResult<PcResponseDto>> CreatePc(PcRequestDto request)
    {
        var createdPc = await _pcService.CreatePcAsync(request);

        return CreatedAtAction(nameof(GetPcById), new { id = createdPc.Id }, createdPc);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePc(int id, PcRequestDto request)
    {
        var updated = await _pcService.UpdatePcAsync(id, request);

        if (!updated)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePc(int id)
    {
        var deleted = await _pcService.DeletePcAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}