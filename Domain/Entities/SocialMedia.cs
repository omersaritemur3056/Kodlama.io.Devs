using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SocialMedia : Entity
    {
        public int UserId { get; set; }
        public string Url { get; set; }

        public User? User { get; set; }

        public SocialMedia()
        {

        }

        public SocialMedia(int id, int userId, string url) : this()
        {
            Id = id;
            UserId = userId;
            Url = url;
        }
    }
}
