using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Restart.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public List<Post> Posts { get; set; }

        [JsonIgnore]
        public List<Like> Likes { get; set; }

        [JsonIgnore]
        public List<Chat> ChatsReceiver { get; set; }
        [JsonIgnore]
        public List<Chat> ChatsSender { get; set; }

        [JsonIgnore]
        public List<Message> MessagesReceiver { get; set; }
        [JsonIgnore]
        public List<Message> MessagesSender { get; set; }


    }
}