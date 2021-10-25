using Microsoft.EntityFrameworkCore;
using Restart.Domain.Models;
using Restart.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restart.Infrastructure.Data.Repositories
{
    public class ChatRepository
    {
        private readonly MySQLContext _context;

        public ChatRepository(MySQLContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Chat>> ListChats()
        {
            List<Chat> list = await _context.Chat.Include(c => c.ListMessages).ToListAsync();


            return list;
        }

        public async Task<Chat> GetChatById(int chatId)
        {
            Chat chat = await _context.Chat.Include(c => c.UserChatReceiver).Include(c => c.UserChatOwner).Include(c => c.ListMessages).FirstOrDefaultAsync(c => c.Id == chatId);

            return chat;
        }

        public async Task<Chat> GetChatByUsers(string userChatOwnerId, string userReceiverId)
        {
            Chat chat = await _context.Chat.FirstOrDefaultAsync(c => c.UserChatOwnerId == userChatOwnerId && c.UserChatReceiverId == userReceiverId);

            return chat;
        }

        public async Task<Chat> CreateChat(Chat chat)
        {
            var ret = await _context.Chat.AddAsync(chat);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<int> UpdateChat(Chat chat)
        {
            _context.Entry(chat).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteChatAsync(int chatId)
        {
            var item = await _context.Chat.FindAsync(chatId);
            _context.Chat.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
