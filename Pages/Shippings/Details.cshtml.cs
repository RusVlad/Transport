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
    public class DetailsModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public DetailsModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

      public Shipping Shipping { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Shipping == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shipping.FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
