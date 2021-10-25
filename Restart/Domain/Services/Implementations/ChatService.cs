using Restart.Domain.Models;
using Restart.Domain.Services.Interfaces;
using Restart.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restart.Domain.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly MessageRepository _messageRepository;
        private readonly ChatRepository _chatRepository;
        private readonly IAuthService _authService;
        public ChatService(ChatRepository chatRepository, IAuthService authService, MessageRepository messageRepository)
        {
            _authService = authService;
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
        }

        public async Task<Tuple<Chat, Chat>> CreateChat(Chat chat)
        {
            ApplicationUser findUser = await _authService.GetUserById(chat.UserChatReceiverId);

            if (findUser == null)
                throw new ArgumentException("Usuário não existe."); ;

            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Chat findChat = await _chatRepository.GetChatByUsers(currentUser.Id, chat.UserChatReceiverId);

            if (chat.UserChatOwnerId == currentUser.Id)
                throw new ArgumentException("Não pode iniciar um chat com você mesmo!");

            if (findChat != null)
                throw new ArgumentException("Chat já existe!");


            Chat novoChat1 = new Chat();
            Chat novoChat2 = new Chat();

            novoChat1.UserChatOwnerId = currentUser.Id;
            novoChat1.UserChatReceiverId = chat.UserChatReceiverId;
            novoChat1 = await _chatRepository.CreateChat(novoChat1);

            novoChat2.UserChatOwnerId = chat.UserChatReceiverId;
            novoChat2.UserChatReceiverId = currentUser.Id;
            novoChat2 = await _chatRepository.CreateChat(novoChat2);

            return new Tuple<Chat, Chat>(novoChat1, novoChat2);
        }

        public async Task<Chat> GetChat(int chatId)
        {
            Chat chat = await _chatRepository.GetChatById(chatId);

            if (chat == null)
                throw new ArgumentException("Chat não existe!");

            return chat;
        }

        public async Task<List<Chat>> ListChats()
        {
            List<Chat> list = await _chatRepository.ListChats();

            return list;
        }

        public async Task<Tuple<Message, Message>> CreateMessage(Message message)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Chat findChat = await _chatRepository.GetChatById(message.ChatId);

            if (findChat == null)
                throw new ArgumentException("Chat não existe!");

            if (findChat.UserChatOwnerId != currentUser.Id)
                throw new ArgumentException("Usuário não tem permissao de enviar mensagem neste chat!");

            //Criando a mensagem para o Proprietario da Mensagem
            Message novaMessage1 = new Message();
            novaMessage1.ChatId = message.ChatId;
            novaMessage1.Data = DateTime.Now;
            novaMessage1.Conteudo = message.Conteudo;
            novaMessage1.UserMessageOwnerId = currentUser.Id;
            novaMessage1.UserMessageReceiverId = findChat.UserChatReceiverId;
            novaMessage1 = await _messageRepository.CreateMessage(novaMessage1);

            findChat.ListMessages.Add(novaMessage1);
            await _chatRepository.UpdateChat(findChat);

            //Criando a mensagem para quem Recebe a mensagem
            findChat = await _chatRepository.GetChatByUsers(findChat.UserChatReceiverId, currentUser.Id);
            Message novaMessage2 = new Message();
            novaMessage2.ChatId = findChat.Id;
            novaMessage2.Data = novaMessage1.Data;
            novaMessage2.Conteudo = novaMessage1.Conteudo;
            novaMessage2.UserMessageOwnerId = novaMessage1.UserMessageOwnerId;
            novaMessage2.UserMessageReceiverId = novaMessage1.UserMessageReceiverId;
            novaMessage1 = await _messageRepository.CreateMessage(novaMessage2);

            findChat.ListMessages.Add(novaMessage2);
            await _chatRepository.UpdateChat(findChat);

            return new Tuple<Message, Message>(novaMessage1, novaMessage2);
        }
        public async Task<Message> GetMessage(int messageId)
        {
            Message message = await _messageRepository.GetMessageById(messageId);

            if (message == null)
                throw new ArgumentException("Mensagem não existe!");

            return message;
        }

        //public async Task<int> UpdateMessage(Message message)
        //{
        //    ApplicationUser currentUser = await _authService.GetCurrentUser();

        //    Message findMessage = await _messageRepository.GetMessageById(message.Id);

        //    if (findMessage == null)
        //        throw new ArgumentException("Mensagem não existe!");

        //    if (!(findMessage.Chat.UserChatOwnerId.Equals(currentUser.Id)))
        //        throw new ArgumentException("Sem permissão.");

        //    findMessage.Conteudo = message.Conteudo;

        //    return await _messageRepository.UpdateMessage(findMessage);
        //}

        public async Task<List<Message>> GetConversation(int chatId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Message> list = await _messageRepository.ListMessagesByChatId(chatId);

            if (list == null)
                throw new ArgumentException("Não há mensagens nesta conversa");

            return list;
        }


    }
}
