using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Transport.Data;
using Transport.Model;

namespace Transport.Pages.Vehicles
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
            return Page();
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.Vehicle == null || Vehicle == null)
            {
                return Page();
            }

            _context.Vehicle.Add(Vehicle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
