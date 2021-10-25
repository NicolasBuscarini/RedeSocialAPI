using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restart.Domain.Models
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }


        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
