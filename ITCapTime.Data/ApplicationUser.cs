using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITCapTime.Data
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public Guid ApplicationUserId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string FirstName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        public string LastName { get; set; }
        public string UserType { get; set; }

        public ApplicationUser CreatedBy { get; set; }
        public ApplicationUser UpdatedBy { get; set; }
         public ICollection<UserClaim> Claims { get; set; } = new List<UserClaim>();
    }
}
