using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restart.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public int CountLikes { get; set; }
        public DateTime Data { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [JsonIgnore]
        public List<Like> LikesPost { get; set; }

    }
}
