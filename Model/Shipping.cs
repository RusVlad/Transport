namespace Transport.Model
{
    public class Shipping
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int ClientId { get; set; }
        public string Status { get; set; } = string.Empty;

        // Navigation properties
        public Vehicle Vehicle { get; set; } = default!;
        public Driver Driver { get; set; } = default!;
        public Client Client { get; set; } = default!;
    }
}
