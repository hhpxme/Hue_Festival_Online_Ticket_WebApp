using HF_WEB_API.Data;
using HF_WEB_API.Helper;
using HF_WEB_API.Helper.UserServices;
using HF_WEB_API.Models.Event;
using HF_WEB_API.Models.News;
using HF_WEB_API.Models.Ticket;
using HF_WEB_API.Repositories.Event;
using HF_WEB_API.Repositories.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace HF_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        public readonly ITicketRepository _ticketRepository;
        public readonly IEventRepository _eventRepository;
        public readonly IUserService _userService;

        public TicketController(ITicketRepository ticketRepository, IEventRepository eventRepository, IUserService userService)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _userService = userService;
        }

        
        [Authorize]
        [HttpPost("buy-ticket")]
        public async Task<IActionResult> BuyTicket(int eventId, TicketOwnerModel model)
        {
            string userId = _userService.GetUserId();
            string username = _userService.GetUserName();
            DateTime createTime = DateTime.Now;
            // id = day + username + month + random integer + hour + accountId + minute
            string idx = createTime.Day.ToString()
                + username + createTime.Month.ToString()
                + new Random().Next(1000, 9999) + createTime.Hour.ToString()
                + userId + createTime.Minute.ToString();
            string idHash = ComputeSha256Hash(idx); // Mã hóa Id dạng SHA256

            try
            {
                var ticket = await _ticketRepository.CreateTicketAsync(idHash, userId, eventId, createTime, "", "", model);

                return Ok(ticket);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet("get-all-ticket")]
        public async Task<IActionResult> GetAllTicket()
        {
            try
            {
                return Ok(await _ticketRepository.GetAllTicketAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("event-ticket/{eventId}")]
        public async Task<IActionResult> GetAllTicketOfEvent(int eventId)
        {
            var tickets = await _ticketRepository.GetAllTicketOfEventAsync(eventId);
            return tickets == null ? NotFound() : Ok(tickets);
        }

        [Authorize]
        [HttpGet("user-ticket/{userId}")]
        public async Task<IActionResult> GetAllTicketOfUser(string userId)
        {
            var tickets = await _ticketRepository.GetAllTicketOfUserAsync(userId);
            return tickets == null ? NotFound() : Ok(tickets);
        }

        [Authorize]
        [HttpGet("your-cart")]
        public async Task<IActionResult> GetYourCart()
        {
            var tickets = await _ticketRepository.GetAllTicketForUserAsync(_userService.GetUserId(), false);
            return tickets == null ? NotFound() : Ok(tickets);
        }

        [Authorize]
        [HttpGet("your-tickets")]
        public async Task<IActionResult> GetYourTickets()
        {
            var tickets = await _ticketRepository.GetAllTicketForUserAsync(_userService.GetUserId(), true);
            return tickets == null ? NotFound() : Ok(tickets);
        }

        [Authorize]
        [HttpGet("get-a-ticket/{id}")]
        public async Task<IActionResult> GetTicketById(string id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            return ticket == null ? NotFound() : Ok(ticket);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPut("active-ticket/{id}")]
        public async Task<IActionResult> ActiveTicket(string id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            if (ticket != null)
            {
                await _ticketRepository.ActiveTicketAsync(id);
                return Ok(new Response { Status = "Success", Message = $"Ticket {id} activation successfully" });
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPut("pay-ticket/{id}")]
        public async Task<IActionResult> PayTicket(string id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            if (ticket != null)
            {
                await _ticketRepository.PayTicketAsync(id);
                return Ok(new Response { Status = "Success", Message = $"Ticket {id} has been paid succesfully" });
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews([FromRoute] string id)
        {
            await _ticketRepository.DeleteTicketAsync(id);
            return Ok(new Response { Status = "Success", Message = $"Delete Ticket: {id}" });
        }

        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
