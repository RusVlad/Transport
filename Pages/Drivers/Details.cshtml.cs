using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transport.Data;
using Transport.Model;

namespace Transport.Pages.Drivers
{
    public class DetailsModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public DetailsModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

      public Driver Driver { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Driver == null)
            {
                return NotFound();
            }

            var driver = await _context.Driver
                .Include(d => d.Vehicle)
                .Include(d => d.LicenseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            // get license category name in viewdata for display
            var licenseCategory = await _context.LicenseCategory.FirstOrDefaultAsync(m => m.Id == driver.LicenseCategoryId);
            ViewData["LicenseCategory"] = licenseCategory.Category;
            if (driver == null)
            {
                return NotFound();
            }
            else 
            {
                Driver = driver;
            }
            return Page();
        }
    }
}
