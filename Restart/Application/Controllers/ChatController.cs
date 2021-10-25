using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restart.Domain.Models;
using Restart.Domain.Services.Implementations;
using Restart.Domain.Services.Interfaces;
using Restart.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Restart.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("create-chat")]
        public async Task<ActionResult> NovoPost([FromBody] Chat chat)              // userReceiverId do body
        {
            try
            {
                return Ok(await _chatService.CreateChat(chat));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-chat")]
        public async Task<ActionResult> GetChat([FromQuery] int chatId)
        {
            try
            {
                Chat chat = await _chatService.GetChat(chatId);

                return Ok(chat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-chats")]
        public async Task<ActionResult> ListChats()
        {
            try
            {
                List<Chat> list = await _chatService.ListChats();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("send-message")]
        public async Task<ActionResult> SendMenssage([FromBody] Message message)
        {
            try
            {
                return Ok(await _chatService.CreateMessage(message));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-message")]
        public async Task<ActionResult> GetMessage([FromQuery] int messageId)
        {
            try
            {
                Message message = await _chatService.GetMessage(messageId);

                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("update-message")]
        //public async Task<ActionResult> UpdateMessage([FromBody] Message message)
        //{
        //    try
        //    {
        //        return Ok(await _chatService.UpdateMessage(message));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("get-conversation")]
        public async Task<ActionResult> GetConvasation([FromQuery] int chatId)
        {
            try
            {
                List<Message> list = await _chatService.GetConversation(chatId);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
