using HF_WEB_API.Data;
using HF_WEB_API.Helper;
using HF_WEB_API.Helper.UserServices;
using HF_WEB_API.Models.News;
using HF_WEB_API.Repositories.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HF_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _repo;
        private readonly IUserService _userService;

        public NewsController(INewsRepository repo, IUserService userService)
        {
            _repo = repo;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            try
            {
                return Ok(await _repo.GetAllNewsAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var news = await _repo.GetNewsAsync(id);
            return news == null ? NotFound() : Ok(news);
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> AddNews(AddNewsModel model)
        {
            try
            {
                string uid = _userService.GetUserId();
                var news = await _repo.AddNewsAsync(model, uid);
                return news == null ? NotFound() : Ok(news);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromBody] AddNewsModel model)
        {
            var news = await _repo.GetNewsAsync(id);
            if (news != null)
            {
                await _repo.UpdateNewsAsync(id, model);
                return Ok(new Response { Status = "Success", Message = $"{id} edited" });
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews([FromRoute] int id)
        {
            await _repo.DeleteNewsAsync(id);
            return Ok(new Response { Status = "Success", Message = $"Delete id = {id}" });
        }
    }
}
