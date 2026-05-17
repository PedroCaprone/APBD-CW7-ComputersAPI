namespace APBD_CW7_ComputersAPI.Models;

public class Component
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ComponentManufacturerId { get; set; }

    public int ComponentTypeId { get; set; }

    public ComponentManufacturer ComponentManufacturer { get; set; } = null!;

    public ComponentType ComponentType { get; set; } = null!;

    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}