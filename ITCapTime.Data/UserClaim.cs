using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCapTime.Data
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
