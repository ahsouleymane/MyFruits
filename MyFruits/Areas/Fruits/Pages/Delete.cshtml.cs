using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFruits.Data;
using MyFruits.Models;

namespace MyFruits.Areas.Fruits.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext ctx;

        public DeleteModel(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        [BindProperty]
        public Fruit Fruit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruit = await ctx.Fruits
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fruit == null)
            {
                return NotFound();
            }
            else
            {
                Fruit = fruit;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruitToDelete = await ctx.Fruits
                .Include(f => f.Image)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (fruitToDelete == null)
            {
                return NotFound();
            }

            var fruit = await ctx.Fruits.FindAsync(id);
            if (fruit != null)
            {
                Fruit = fruit;
                ctx.Fruits.Remove(Fruit);
                await ctx.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
