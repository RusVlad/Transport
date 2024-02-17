using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Transport.Data;
using Transport.Model;

namespace Transport.Pages.LicenseCategories
{
    public class DetailsModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public DetailsModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

      public LicenseCategory LicenseCategory { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LicenseCategory == null)
            {
                return NotFound();
            }

            var licensecategory = await _context.LicenseCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (licensecategory == null)
            {
                return NotFound();
            }
            else 
            {
                LicenseCategory = licensecategory;
            }
            return Page();
        }
    }
}
