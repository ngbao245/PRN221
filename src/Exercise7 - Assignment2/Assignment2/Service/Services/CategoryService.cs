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
            var categories = _unitOfWork.Category.GetAll();
            return _mapper.Map<IEnumerable<CategoryModel>>(categories);
        }

        public bool AddCategory(CategoryModel model)
        {
            try
            {
                var category = _mapper.Map<Category>(model);
                _unitOfWork.Category.Insert(category);
                _unitOfWork.Completed();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCategory(CategoryModel model)
        {
            var category = 
        }

        //public bool DeleteCategory(int id) { }
    }
}
