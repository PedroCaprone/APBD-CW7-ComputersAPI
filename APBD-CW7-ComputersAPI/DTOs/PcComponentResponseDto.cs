namespace APBD_CW7_ComputersAPI.DTOs;

public class PcComponentResponseDto
{
    public string ComponentCode { get; set; } = null!;

    public string ComponentName { get; set; } = null!;

    public string Description { get; set; } = null!;
    
    public string ComponentType { get; set; } = null!;

    public int Amount { get; set; }
}