namespace Technico.Dtos;

public class PropertyDTO
{
        public Guid PropertyIDNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public int YearOfConstruction { get; set; }
        public Guid OwnerID { get; set; }
}


public class PropertyResponseDTO
{
    public Guid PropertyIDNumber { get; set; }
    public string Address { get; set; } = string.Empty;
    public int YearOfConstruction { get; set; }
    public Guid OwnerID { get; set; }
    public List<RepairDTO> Repairs { get; set; } = new List<RepairDTO>();
}

