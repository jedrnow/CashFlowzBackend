using CashFlowzBackend.Data;
using CashFlowzBackend.Data.Models;
using CashFlowzBackend.Data.Models.View;
using Microsoft.EntityFrameworkCore;

namespace CashFlowzBackend.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CFDbContext _context;

        public CategoryRepository(CFDbContext context)
        {
            _context = context;
        }

        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task DeleteCategory(int categoryId)
        {
            Category? categoryToDelete = await _context.Categories
                .Where(x => !x.Deleted)
                .SingleOrDefaultAsync(x => x.Id == categoryId);

            categoryToDelete!.Delete();
        }

        public async Task<bool> CheckCategoryExistsById(int categoryId)
        {
            return (await _context.Categories.SingleOrDefaultAsync(x => x.Id == categoryId && !x.Deleted)) != null;
        }

        public async Task<List<CategoryViewModel>> GetCategoryList()
        {
            return await _context.Categories
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name= x.Name
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdToEdit(int categoryId)
        {
            return await _context.Categories
                .Where(x => !x.Deleted)
                .SingleOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<bool> CheckCategoryHasActiveBudgets(int categoryId)
        {
            return await _context.Budgets
                .Where(x => !x.Deleted)
                .AnyAsync(x=>x.CategoryId == categoryId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
