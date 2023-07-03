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
using BarcodeStandard;
using System;
using IronBarCode;
using System.Runtime.CompilerServices;
using System.Net.WebSockets;
using Microsoft.VisualBasic;

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

        #region Generate Barcode and QRcode for ticket
        private string generateBarcode(string value)
        {
            string root = "Contents/Tickets/Barcode/";
            var barcode = BarcodeWriter.CreateBarcode(value, BarcodeWriterEncoding.Code128);
            string link = root + value + ".jpeg";
            barcode.SaveAsImage(link);

            return link;
        }

        private string generateQRCode(string value)
        {
            string root = "Contents/Tickets/QRCode/";
            var qrCode = QRCodeWriter.CreateQrCode(value, Size: 512, QRCodeWriter.QrErrorCorrectionLevel.Highest);
            string link = root + value + ".jpeg";
            qrCode.SaveAsImage(link);

            return link;
        }
        #endregion

        #region HashSHA256 for TicketId
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
        #endregion

        [Authorize]
        [HttpPost("buy-ticket")]
        public async Task<IActionResult> BuyTicket(int eventId, TicketOwnerModel model)
        {
            string userId = _userService.GetUserId();
            string username = _userService.GetUserName();
            DateTime createTime = DateTime.Now;
            // id = day + username + month + random integer + hour + accountId + minute + -event- + eventId
            string idx = createTime.Day.ToString()
                + username + createTime.Month.ToString()
                + new Random().Next(1000, 9999) + createTime.Hour.ToString()
                + userId + createTime.Minute.ToString()
                + "-event-" + eventId.ToString();
            string idHash = ComputeSha256Hash(idx); // Mã hóa Id dạng SHA256

            string barcodeLink = generateBarcode(idHash);
            string qrcodeLink = generateQRCode(idHash);

            try
            {
                var ticket = await _ticketRepository.CreateTicketAsync(idHash, userId, eventId, createTime, barcodeLink, qrcodeLink, model);

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

        [HttpGet("ReadCode/")]
        [Obsolete]
        public string ReadCode(string codeLink)
        {
            //var res = await BarcodeReader.ReadAsync(codeLink); // From a file
            BarcodeResult res = BarcodeReader.QuicklyReadOneBarcode(codeLink);
            return res.Text;

            // Contents/Tickets/Barcode/c84f7be19f1db21cf4b038cfc11ee10c90022dfbd3e797ac24a17d14361c33d4.jpeg
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPut("active-ticket")]
        public async Task<IActionResult> ActiveTicket(string codeLink)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var idx = BarcodeReader.QuicklyReadOneBarcode(codeLink).Text.Trim();
            if (Directory.Exists(codeLink))
            {
                return Ok(new Response { Status = "Failed", Message = $"Barcode is not found" });
            }
#pragma warning restore CS0618 // Type or member is obsolete

            var ticket = await _ticketRepository.GetTicketByIdAsync(idx);
            if (ticket != null)
            {
                if (ticket.IsActive == false)
                {
                    await _ticketRepository.ActiveTicketAsync(idx);
                    return Ok(new Response { Status = "Success", Message = $"Ticket {idx} activation successfully" });
                }
                else
                {
                    return Ok(new Response { Status = "Failed", Message = $"Ticket {idx} activated" });
                }
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
    }
}
