namespace Transport.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = default!;
        public int LicenseCategoryId { get; set; }

        public LicenseCategory LicenseCategory { get; set; } = default!;
    }
}
