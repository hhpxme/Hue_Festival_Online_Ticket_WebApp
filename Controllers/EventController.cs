using HF_WEB_API.Data;
using HF_WEB_API.Helper;
using HF_WEB_API.Models;
using HF_WEB_API.Models.Event;
using HF_WEB_API.Repositories.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HF_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repo;

        public EventController(IEventRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvent()
        {
            try
            {
                return Ok(await _repo.GetAllEventAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var ev = await _repo.GetEventAsync(id);
            return ev == null ? NotFound() : Ok(ev);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddEvent(EventModel model)
        {
            try
            {
                var ev = await _repo.AddEventAsync(model);

                return Ok(ev);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventModel model)
        {
            var ev = await _repo.GetEventAsync(id);
            if (ev != null)
            {
                await _repo.UpdateEventAsync(id, model);
                return Ok(new Response { Status = "Success", Message = $"{id} edited" });
            }
            else
            {
                return NotFound();
            };
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            await _repo.DeleteEventAsync(id);
            return Ok();
        }
    }
}
