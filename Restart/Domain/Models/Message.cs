using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restart.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Conteudo { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }

        public string UserMessageOwnerId { get; set; }
        [ForeignKey("UserMessageOwnerId")]
        public ApplicationUser UserMessageOwner { get; set; }


        public string UserMessageReceiverId { get; set; }
        [ForeignKey("UserMessageReceiverId")]
        public ApplicationUser UserMessageReceiver { get; set; }

    }
}
