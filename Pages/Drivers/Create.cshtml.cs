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
    public class CreateModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public CreateModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["LicenseCategoryId"] = new SelectList(_context.LicenseCategory, "Id", "Category");

            // add viewdata VehicleId for dropdown list based on selected license category

            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "LicensePlate");
            return Page();
        }

        [BindProperty]
        public Driver Driver { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.Driver == null || Driver == null)
            {
                return Page();
            }

          _context.Driver.Add(Driver);
            await _context.SaveChangesAsync();
           return RedirectToPage("./Index");
        }

        public async Task<IActionResult> onChangeLicenseCategory(int LicenseCategoryId)
        {
            Driver.LicenseCategoryId = LicenseCategoryId;

            // get vehicles based on selected license category

            var vehicles = await _context.Vehicle.Where(v => v.LicenseCategoryId == LicenseCategoryId).ToListAsync();

            // add viewdata VehicleId for dropdown list based on selected license category

            ViewData["VehicleId"] = new SelectList(vehicles, "Id", "LicensePlate");

            return Page();
        }
    }
}
