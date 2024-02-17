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
    public class IndexModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public IndexModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

        public IList<Driver> Driver { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Driver != null)
            {
                Driver = await _context.Driver.Include(d => d.Vehicle)
                    .Include(d => d.LicenseCategory)
                    .ToListAsync();

            }
        }
    }
}
