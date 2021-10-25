using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restart.Domain.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public string UserChatOwnerId { get; set; }
        [ForeignKey("UserChatOwnerId")]
        public ApplicationUser UserChatOwner { get; set; }

        public string UserChatReceiverId { get; set; }
        [ForeignKey("UserChatReceiverId")]
        public ApplicationUser UserChatReceiver { get; set; }

        [JsonIgnore]
        public List<Message> ListMessages { get; set; }


    }
}
