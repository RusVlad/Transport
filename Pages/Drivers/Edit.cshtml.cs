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
    public class EditModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public EditModel(Transport.Data.TransportContext context)
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

            var driver =  await _context.Driver
                .Include(d => d.Vehicle)
                .Include(d => d.LicenseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }
            Driver = driver;
            ViewData["LicenseCategoryId"] = new SelectList(_context.LicenseCategory, "Id", "Category");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "LicensePlate");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // update driver

            if (id == null || _context.Driver == null || Driver == null)
            {
                return NotFound();
            }

            var driverToUpdate = await _context.Driver
                .Include(d => d.Vehicle)
                .Include(i => i.LicenseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            // update with driverToUpdate
            if (driverToUpdate == null)
            {
                return NotFound();
            }


            try
            {
                await TryUpdateModelAsync<Driver>(driverToUpdate, "driver", i => i.Name, i => i.LicenseCategoryId, i => i.VehicleId);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(Driver.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool DriverExists(int id)
        {
          return (_context.Driver?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
