using Restart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restart.Domain.Services.Interfaces
{
    public interface IChatService
    {
        Task<Tuple<Chat, Chat>> CreateChat(Chat chat);
        Task<Chat> GetChat(int chatId);
        Task<List<Chat>> ListChats();
        Task<Tuple<Message, Message>> CreateMessage(Message message);
        Task<Message> GetMessage(int messageId);
        //Task<int> UpdateMessage(Message message);
        Task<List<Message>> GetConversation(int chatId);
    }
}
