using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFruits.Data;
using MyFruits.Models;

namespace MyFruits.Areas.Fruits.Pages
{
    public class EditModel : PageModel
    {
        private readonly MyFruits.Data.ApplicationDbContext _context;

        public EditModel(MyFruits.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Fruit Fruit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruit =  await _context.Fruits.FirstOrDefaultAsync(m => m.Id == id);
            if (fruit == null)
            {
                return NotFound();
            }
            Fruit = fruit;
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

            _context.Attach(Fruit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FruitExists(Fruit.Id))
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

        private bool FruitExists(int id)
        {
            return _context.Fruits.Any(e => e.Id == id);
        }
    }
}
