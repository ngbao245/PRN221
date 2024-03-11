using CodeInBlue.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAll();
        CategoryModel GetCategoryById(int? id);
        bool AddCategory(CategoryModel model);
        bool DeleteCategory(int id);
    }
}
