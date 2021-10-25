using Microsoft.EntityFrameworkCore;
using Restart.Domain.Models;
using Restart.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restart.Infrastructure.Data.Repositories
{
    public class MessageRepository
    {
        private readonly MySQLContext _context;

        public MessageRepository(MySQLContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Message>> ListMessages()
        {
            List<Message> list = await _context.Message.Include(m => m.Chat).ToListAsync();

            return list;
        }

        public async Task<List<Message>> ListMessagesByChatId(string chatId)
        {
            List<Message> list = await _context.Message.Where(m => m.ChatId.Equals(chatId)).OrderBy(p => p.Data).Include(m => m.Chat).ToListAsync();

            return list;
        }

        public async Task<List<Message>> ListMessagesByUsers(string userChatOwner, string userChatReceiver)
        {
            List<Message> list = await _context.Message.Where(m => 
            (m.Chat.UserChatOwnerId.Equals(userChatOwner) && m.Chat.UserChatReceiverId.Equals(userChatReceiver)) 
            || 
            (m.Chat.UserChatOwnerId.Equals(userChatReceiver) && m.Chat.UserChatReceiverId.Equals(userChatOwner))).OrderBy(p => p.Data).Include(m => m.Chat).ToListAsync();
            
            return list;
        }

        public async Task<Message> GetMessageById(int messageId)
        {
            Message message = await _context.Message.Include(m => m.UserMessageOwner).Include(m => m.UserMessageReceiver).Include(m => m.Chat).FirstOrDefaultAsync(m => m.Id == messageId);

            return message;
        }

        public async Task<Message> CreateMessage(Message message)
        {
            var ret = await _context.Message.AddAsync(message);
            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<int> UpdateMessage(Message message)
        {
            _context.Entry(message).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteMessageAsync(int messageId)
        {
            var item = await _context.Message.FindAsync(messageId);
            _context.Message.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Message>> ListMessagesByChatId(int chatId)
        {
            List<Message> list = await _context.Message.Where(m => m.ChatId == chatId).Include(m => m.UserMessageReceiver).OrderBy(p => p.Data).ToListAsync();

            return list;
        }
    }
}
