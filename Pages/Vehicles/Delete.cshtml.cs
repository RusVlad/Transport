using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Transport.Data;
using Transport.Model;

namespace Transport.Pages.Vehicles
{
    public class DeleteModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public DeleteModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Vehicle Vehicle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.LicenseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }
            else 
            {
                Vehicle = vehicle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }
            var vehicle = await _context.Vehicle.FindAsync(id);

            // if vehicle is used in driver, do not delete
            var driver = await _context.Driver.FirstOrDefaultAsync(m => m.VehicleId == id);

            if (driver != null)
            {
                return RedirectToPage("./Index");
            }

            // if vehicle is used in shipping, do not delete

            var shipping = await _context.Shipping.FirstOrDefaultAsync(m => m.VehicleId == id);
            if (shipping != null)
            {
                return RedirectToPage("./Index");
            }

            if (vehicle != null)
            {
                Vehicle = vehicle;
                _context.Vehicle.Remove(Vehicle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
