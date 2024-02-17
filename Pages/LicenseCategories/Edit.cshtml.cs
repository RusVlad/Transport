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

namespace Transport.Pages.LicenseCategories
{
    public class EditModel : PageModel
    {
        private readonly Transport.Data.TransportContext _context;

        public EditModel(Transport.Data.TransportContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LicenseCategory LicenseCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LicenseCategory == null)
            {
                return NotFound();
            }

            var licensecategory =  await _context.LicenseCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (licensecategory == null)
            {
                return NotFound();
            }
            LicenseCategory = licensecategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LicenseCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LicenseCategoryExists(LicenseCategory.Id))
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

        private bool LicenseCategoryExists(int id)
        {
          return (_context.LicenseCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
