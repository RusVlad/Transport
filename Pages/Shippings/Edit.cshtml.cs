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

namespace Transport.Pages.Shippings
{
    public class EditModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public EditModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shipping Shipping { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Shipping == null)
            {
                return NotFound();
            }

            var shipping =  await _context.Shipping.FirstOrDefaultAsync(m => m.Id == id);
            if (shipping == null)
            {
                return NotFound();
            }
            Shipping = shipping;
           ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
           ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "Name");
           ViewData["VehicleId"] = new SelectList(_context.Vehicle, "Id", "LicensePlate");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Attach(Shipping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingExists(Shipping.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ShippingExists(int id)
        {
          return (_context.Shipping?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
