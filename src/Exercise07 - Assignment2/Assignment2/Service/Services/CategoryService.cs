using AutoMapper;
using CodeInBlue.Entities;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<CategoryModel> GetAll()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryModel>>(categories);
        }

        public CategoryModel GetCategoryById(int? id)
        {
                //var category = _unitOfWork.Category.GetAll().FirstOrDefault(c => c.CategoryId == id);
                var category = _unitOfWork.CategoryRepository.GetById(id);
                return _mapper.Map<CategoryModel>(category);
        }

        public bool AddCategory(CategoryModel model)
        {
            try
            {
                var category = _mapper.Map<Category>(model);

                int maxCategoryId = _unitOfWork.CategoryRepository.GetAll().Max(_ => _.CategoryId);
                category.CategoryId = maxCategoryId + 1;

                _unitOfWork.CategoryRepository.Insert(category);
                _unitOfWork.Completed();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCategory(int id)
        {
            var categoryToDelete = _unitOfWork.CategoryRepository.Get(_ => _.CategoryId.Equals(id)).FirstOrDefault();
            if (categoryToDelete != null)
            {
                _unitOfWork.CategoryRepository.Delete(categoryToDelete);
                _unitOfWork.Completed();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
