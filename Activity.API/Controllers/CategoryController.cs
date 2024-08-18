using Activity.BLL;
using Activity.BLL.Repository;
using Activity.DAL.ORM;
using Activity.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Activity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddCategory(CreateCategoryRequestDto model)
        {
            Category category = new Category();
            category.Name = model.Name;
            category.Description = model.Description;
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Save();

            CreateCategoryResponseDto response = new CreateCategoryResponseDto();
            response.Id = category.ID;
            response.Name = category.Name;
            response.Description = category.Description;

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var result = _unitOfWork.CategoryRepository.GetAll();

            var response = new List<GetAllCategoriesResponseDto>();

            foreach (var item in result)
            {
                GetAllCategoriesResponseDto dto = new GetAllCategoriesResponseDto();
                dto.Id = item.ID;
                dto.Name = item.Name;
                dto.Description = item.Description;
                response.Add(dto);
            }

            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetCategoryById(Guid id)
        {
            var result = _unitOfWork.CategoryRepository.GetById(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            _unitOfWork.CategoryRepository.Remove(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryRequestDto model)
        {
            Category category = new Category();
            category.ID = model.Id;
            category.Name = model.Name;
            category.Description = model.Description;
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Save();

            return Ok(model);
        }

    }
}
