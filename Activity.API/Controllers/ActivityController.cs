using Activity.BLL;
using Activity.Dto;
using Activity.Dto.Activity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Activity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public ActivityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        public IActionResult AddActivity(CreateActivityRequestDto model)
        {
            Activity.DAL.ORM.Activity activity = new Activity.DAL.ORM.Activity();
            activity.Name = model.Name;
            activity.Description = model.Description;
            activity.CategoryId = model.CategoryId;
            _unitOfWork.ActivityRepository.Add(activity);
            _unitOfWork.Save();


            return Ok(activity.ID);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new List<GetAllActivitiesResponseDto>();

            var activities = _unitOfWork.ActivityRepository.GetAllWithIncludes("Category");

            foreach (var activity in activities)
            {
                var activityDto = new GetAllActivitiesResponseDto();
                activityDto.ID = activity.ID;
                activityDto.Name = activity.Name;
                activityDto.Description = activity.Description;
                activityDto.StartDate = activity.StartDate;
                activityDto.EndDate = activity.EndDate;
                activityDto.CategoryId = activity.CategoryId;
                activityDto.CategoryName = activity.Category?.Name;

                response.Add(activityDto);

            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var activity = _unitOfWork.ActivityRepository.GetByIdWithIncludes(id, "Category");
            if (activity == null)
            {
                return NotFound();
            }
            else
            {
                var response = new GetActivityByIdResponseDto();
                response.ID = activity.ID;
                response.Name = activity.Name;
                response.Description = activity.Description;
                response.StartDate = activity.StartDate;
                response.EndDate = activity.EndDate;
                response.CategoryName = activity.Category?.Name;


                return Ok(response);
            }
        }
    }
}
