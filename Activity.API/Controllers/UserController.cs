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
    public class UserController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Post([FromForm] CreateUserRequestDto createUserRequestDto, [FromForm] IFormFile formFile)
        {

            var user = new User()
            {
                Email = createUserRequestDto.Email,
                Password = createUserRequestDto.Password
            };
            
            if (formFile != null)
            {

                var ext = Path.GetExtension(formFile.FileName);

                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                {
                    return BadRequest("File type is not valid");
                }

                var fileName = Guid.NewGuid() + ext;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                user.ProfileImage = fileName;
            }

            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Save();


            return Ok(user.ID);
        }


        [HttpGet]
        public IActionResult Get()
        {
            var response = new List<GetAllUsersResponseDto>();

            var users = _unitOfWork.UserRepository.GetAll();

            foreach (var user in users)
            {
                var userDto = new GetAllUsersResponseDto();
                userDto.ID = user.ID;
                userDto.Email = user.Email;
                userDto.ProfileImage = user.ProfileImage;

                response.Add(userDto);

            }


            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var user = _unitOfWork.UserRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            var response = new GetUserByIdResponseDto();
            response.ID = user.ID;
            response.Email = user.Email;
            response.ProfileImage = user.ProfileImage;

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _unitOfWork.UserRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            _unitOfWork.UserRepository.Remove(id);
            _unitOfWork.Save();

            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromForm] UpdateUserRequestDto updateUserRequestDto, [FromForm] IFormFile formFile)
        {
            var user = _unitOfWork.UserRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Email = updateUserRequestDto.EMail;

            if (formFile != null)
            {
                var ext = Path.GetExtension(formFile.FileName);

                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                {
                    return BadRequest("File type is not valid");
                }

                var fileName = Guid.NewGuid() + ext;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                user.ProfileImage = fileName;
            }

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();

            return Ok();
        }


    }
}
