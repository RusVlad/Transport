using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Transport.Data;
using Transport.Model;

namespace Transport.Pages.Drivers
{
    public class DeleteModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public DeleteModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Driver Driver { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Driver == null)
            {
                return NotFound();
            }

            var driver = await _context.Driver
                .Include(d => d.LicenseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Driver == null)
            {
                return NotFound();
            }
            var driver = await _context.Driver.FindAsync(id);

            // if driver is used in shipping, do not delete
            var shipping = await _context.Shipping.FirstOrDefaultAsync(m => m.DriverId == driver.Id);

            if (shipping != null)
            {
                return RedirectToPage("./Index");
            }

            if (driver != null)
            {
                Driver = driver;
                _context.Driver.Remove(Driver);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
