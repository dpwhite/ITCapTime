using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCapTime.Data
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public DateTime CreatedOn { get; set; }
        public string PhoneNumber { get; set; }
        public int EmailDigestHour { get; set; }
        public string TimeZone { get; set; }
        public ICollection<ClaimResponse> Claims { get; set; } = new List<ClaimResponse>();
    }
}
