namespace APBD_CW7_ComputersAPI.DTOs;

public class PcRequestDto
{
    public string Name { get; set; } = null!;

    public double Weight { get; set; }

    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Stock { get; set; }
}