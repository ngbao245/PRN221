using AutoMapper;
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

    }
}
