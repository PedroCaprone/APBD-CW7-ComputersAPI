using APBD_CW7_ComputersAPI.DTOs;

namespace APBD_CW7_ComputersAPI.Services;

public interface IPcService
{
    Task<IEnumerable<PcResponseDto>> GetAllPcsAsync();
    
    Task<PcResponseDto?> GetPcByIdAsync(int id);

    Task<IEnumerable<PcComponentResponseDto>?> GetPcComponentsAsync(int id);

    Task<PcResponseDto> CreatePcAsync(PcRequestDto request);

    Task<bool> UpdatePcAsync(int id, PcRequestDto request);

    Task<bool> DeletePcAsync(int id);
}