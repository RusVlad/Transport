using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Transport.Data;
using Transport.Model;

namespace Transport.Pages.LicenseCategories
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
            return Page();
        }

        [BindProperty]
        public LicenseCategory LicenseCategory { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.LicenseCategory == null || LicenseCategory == null)
            {
                return Page();
            }

            _context.LicenseCategory.Add(LicenseCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
