using APBD_CW7_ComputersAPI.Data;
using APBD_CW7_ComputersAPI.DTOs;
using APBD_CW7_ComputersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW7_ComputersAPI.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PcResponseDto>> GetAllPcsAsync()
    {
        return await _context.PCs
            .Select(pc => new PcResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }
    
    public async Task<PcResponseDto?> GetPcByIdAsync(int id)
    {
        return await _context.PCs
            .Where(pc => pc.Id == id)
            .Select(pc => new PcResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PcComponentResponseDto>?> GetPcComponentsAsync(int id)
    {
        var pcExists = await _context.PCs.AnyAsync(pc => pc.Id == id);

        if (!pcExists)
        {
            return null;
        }

        return await _context.PCComponents
            .Where(pcComponent => pcComponent.PCId == id)
            .Select(pcComponent => new PcComponentResponseDto
            {
                ComponentCode = pcComponent.Component.Code,
                ComponentName = pcComponent.Component.Name,
                Description = pcComponent.Component.Description,
                ComponentType = pcComponent.Component.ComponentType.Name,
                Amount = pcComponent.Amount
            })
            .ToListAsync();
    }

    public async Task<PcResponseDto> CreatePcAsync(PcRequestDto request)
    {
        var pc = new PC
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return new PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> UpdatePcAsync(int id, PcRequestDto request)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc is null)
        {
            return false;
        }

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePcAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc is null)
        {
            return false;
        }

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }
}