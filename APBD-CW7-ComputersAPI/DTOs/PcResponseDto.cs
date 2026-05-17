namespace APBD_CW7_ComputersAPI.DTOs;
//get /api/pcs post /api/pcs response
public class PcResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Weight { get; set; }

    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Stock { get; set; }
}