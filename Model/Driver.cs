using System.ComponentModel.DataAnnotations;

namespace Transport.Model
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Display(Name = "License Category")]
        public int LicenseCategoryId { get; set; }

        // nullable vehicle id
        public int? VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        public LicenseCategory LicenseCategory { get; set; } = default!;
        
    }
}
