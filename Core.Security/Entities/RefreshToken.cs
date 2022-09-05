using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Entities
{
    public class RefreshToken : Entity
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }

        public string? ReasonRevoked { get; set; }

        public virtual User User { get; set; }

        public RefreshToken()
        {
        }

        public RefreshToken(int id, string token, DateTime expires, DateTime created, string createdByIp, DateTime? revoked,
                            string revokedByIp, string replacedByToken, string reasonRevoked)
        {
            Id = id;
            Token = token;
            Expires = expires;
            Created = created;
            CreatedByIp = createdByIp;
            Revoked = revoked;
            RevokedByIp = revokedByIp;
            ReplacedByToken = replacedByToken;
            ReasonRevoked = reasonRevoked;
        }
    }
}
