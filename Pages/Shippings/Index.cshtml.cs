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
    public class IndexModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public IndexModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

        public IList<Shipping> Shipping { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Shipping != null)
            {
                Shipping = await _context.Shipping
                .Include(s => s.Client)
                .Include(s => s.Driver)
                .Include(s => s.Vehicle).ToListAsync();
            }
        }
    }
}
