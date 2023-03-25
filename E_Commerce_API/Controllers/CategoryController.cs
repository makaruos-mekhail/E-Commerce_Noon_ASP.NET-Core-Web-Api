using Application.Contracts;
using Context;
using E_Commerce_API.Reposatories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {


        //public DContext _context= new DContext();

        //private DContext _Context;
        //private readonly RepoCategory RepoCategory;
        //public CategoryController(DContext context)
        //{
        //    _context = context;

        //    //_Context = context;
        //}



        //private CategoryController(RepoCategory _Category)
        //{
        //    RepoCategory = _Category;
        //}

        //      [HttpGet]
        //      public async Task<IActionResult> GetCategories(string? filter = null)
        //      {
        //	var data = await RepoCategory.FilterByAsync(filter);
        //	return Ok(data);
        //}
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetCategoryByID(int id)
        {
            var categories =await _categoryRepository.GetByIdAsync(id);
            return Ok(categories);
           //var categories = _context.Category.Find(id);
           // return Ok(categories);  
            //return  Ok(await RepoCategory.GetByIDAsync(id));
        }

        //public  IEnumerable<Category> GetCategories()
        //{
        //    return  _Context.Category.ToList();
        //}

        //public IActionResult CreateCategory(Category category)
        //{
        //    var data = _Context.Category.AddAsync(category);
        //    _Context.SaveChanges();
        //    return(Ok(data));
        //}

        //public async Task<bool> updateAsync(Category category)
        //{
        //    var EN = _Context.Category.Update(category);
        //    if (EN != null)
        //    {
        //        _Context.SaveChangesAsync();
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    //var category = new Category();
        //    //category.Name = name;
        //    //category.Id = id;
        //    //return Ok(category);
        //}
        //public async Task<bool> DeleteCategory(int id)
        //{
        //    var i = await _Context.Category.FindAsync(id);
        //    if (i != null)
        //    {
        //        _Context.Category.Remove(i);
        //        await _Context.SaveChangesAsync(); 
        //        return true;
        //    }
        //    return false;

        //}
        //public async Task<IEnumerable<Category>> FilterByAsync(string? filter = null)
        //{
        //    IEnumerable<Category> result = null;
        //    if (filter != null)
        //    {
        //        result = _Context.Category.Where(cat => cat.Name.Contains(filter));
        //        return result;

        //    }
        //    else
        //    {
        //        result = _Context.Category.ToList();
        //        return result;
        //    }

        //}
    }
}
