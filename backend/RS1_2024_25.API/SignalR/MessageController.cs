using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RS1_2024_25.API.SignalR.DTOs;
using RS1_2024_25.API.SignalR.Interfaces;

namespace RS1_2024_25.API.SignalR
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<MessageDto>>> GetHistory(int userId1, int userId2)
        {
            var messages = await _messageService.GetMessageHistoryAsync(userId1, userId2);
            return Ok(messages);
        }

        [HttpGet("conversations/{userId}")]
        public async Task<IActionResult> GetConversations(int userId)
        {
            var result = await _messageService.GetConversationListAsync(userId);
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<MessageDto>> SendMessage([FromBody] SendMessageRequest request)
        {
            var result = await _messageService.SendMessageAsync(request.SenderId, request.ReceiverId, request.Content);
            return Ok(result);
        }
    }

    public class SendMessageRequest
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
    }

}

