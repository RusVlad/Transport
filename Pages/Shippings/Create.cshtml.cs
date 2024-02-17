using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Transport.Data;
using Transport.Model;

namespace Transport.Pages.Shippings
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
        ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
        ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "Name");
        ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "LicensePlate");
            return Page();
        }

        [BindProperty]
        public Shipping Shipping { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.Shipping == null || Shipping == null)
            {
                return Page();
            }

            _context.Shipping.Add(Shipping);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
