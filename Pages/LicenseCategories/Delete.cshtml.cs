using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Transport.Data;
using Transport.Model;

namespace Transport.Pages.LicenseCategories
{
    public class DeleteModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public DeleteModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

        [BindProperty]
      public LicenseCategory LicenseCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LicenseCategory == null)
            {
                return NotFound();
            }

            var licensecategory = await _context.LicenseCategory.FirstOrDefaultAsync(m => m.Id == id);

            if (licensecategory == null)
            {
                return NotFound();
            }
            else 
            {
                LicenseCategory = licensecategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.LicenseCategory == null)
            {
                return NotFound();
            }
            var licensecategory = await _context.LicenseCategory.FindAsync(id);

            // if license is used by a driver, do not delete

            var driver = await _context.Driver.FirstOrDefaultAsync(m => m.LicenseCategoryId == id);
            if (driver != null)
            {
                return RedirectToPage("./Index");
            }

            // if license is used by a vehicle, do not delete

            var vehicle = await _context.Vehicle.FirstOrDefaultAsync(m => m.LicenseCategoryId == id);
            if (vehicle != null)
            {
                return RedirectToPage("./Index");
            }

            if (licensecategory != null)
            {
                LicenseCategory = licensecategory;
                _context.LicenseCategory.Remove(LicenseCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
