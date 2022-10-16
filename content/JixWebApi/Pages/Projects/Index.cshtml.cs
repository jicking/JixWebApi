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
    public class IndexModel : PageModel
    {
        private readonly JixWebApi.Data.JixWebApiDbContext _context;

        public IndexModel(JixWebApi.Data.JixWebApiDbContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Projects != null)
            {
                Project = await _context.Projects.ToListAsync();
            }
        }
    }
}
