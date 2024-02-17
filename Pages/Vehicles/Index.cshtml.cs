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

namespace Transport.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public IndexModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

        public IList<Vehicle> Vehicle { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Vehicle != null)
            {
                ViewData["LicenseCategoryId"] = new SelectList(_context.LicenseCategory, "Id", "Category");
                Vehicle = await _context.Vehicle
                .Include(v => v.LicenseCategory)
                .ToListAsync();
            }
        }
    }
}
