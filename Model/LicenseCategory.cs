using System.ComponentModel.DataAnnotations;

namespace Transport.Model
{
    public class LicenseCategory
    {
        public int Id { get; set; }

        public string Category { get; set; } = string.Empty;
    }
}
