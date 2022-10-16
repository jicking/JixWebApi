using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JixWebApi.Core.Entities;
using JixWebApi.Data;

namespace JixWebApi.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly JixWebApi.Data.JixWebApiDbContext _context;

        public DetailsModel(JixWebApi.Data.JixWebApiDbContext context)
        {
            _context = context;
        }

      public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            else 
            {
                Project = project;
            }
            return Page();
        }
    }
}
