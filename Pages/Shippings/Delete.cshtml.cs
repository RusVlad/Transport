using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Transport.Data;
using Transport.Model;

namespace Transport.Pages.Shippings
{
    public class DeleteModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public DeleteModel(Transport.Data.TransportContext context)
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

            var shipping = await _context.Shipping
                .Include(s => s.Client)
                .Include(s => s.Driver)
                .Include(s => s.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (shipping == null)
            {
                return NotFound();
            }
            else 
            {
                Shipping = shipping;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Shipping == null)
            {
                return NotFound();
            }
            var shipping = await _context.Shipping
                .Include(s => s.Client)
                .Include(s => s.Driver)
                .Include(s => s.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (shipping != null)
            {
                Shipping = shipping;
                _context.Shipping.Remove(Shipping);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
