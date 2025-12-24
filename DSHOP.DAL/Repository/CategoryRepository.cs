using DSHOP.DAL.Data;
using DSHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category request)
        {
            await _context.Categories.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;

        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.User).Include(c => c.Translations).ToListAsync();
        }
        public async Task<Category?> FindByIdAsync(int id)
        {
            return await _context.Categories.Include(c=>c.Translations).FirstOrDefaultAsync(c=>c.Id==id);
        }
        public async Task DeleteAsync(Category cat) { 
            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
        }
        public async Task<Category?> UpdateAsync(Category cat)
        {
            _context.Categories.Update(cat);
            await _context.SaveChangesAsync();
            return cat;
        }
    } 
}
