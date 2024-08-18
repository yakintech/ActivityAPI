using Activity.BLL;
using Activity.DAL.ORM;
using Activity.Dto;
using Activity.Validations.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Activity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public BlogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Create(CreateBlogRequestDto model)
        {
            var blog = new Blog();
            blog.Title = model.Title;
            blog.Content = model.Content;

            _unitOfWork.BlogRepository.Add(blog);
            _unitOfWork.Save();

            return Ok(blog.ID);
        }


        [HttpGet]
        public IActionResult Get()
        {
            var blogs = _unitOfWork.BlogRepository.GetAll();

            var response = new List<GetAllBlogsResponseDto>();

            foreach (var blog in blogs)
            {
                response.Add(new GetAllBlogsResponseDto
                {
                    Id = blog.ID,
                    Title = blog.Title,
                    Content = blog.Content
                });
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var blog = _unitOfWork.BlogRepository.GetById(id);

            if (blog == null)
            {
                return NotFound();
            }

            var response = new GetBlogByIdResponseDto
            {
                Title = blog.Title,
                Content = blog.Content
            };

            return Ok(response);
        }
    }
}
