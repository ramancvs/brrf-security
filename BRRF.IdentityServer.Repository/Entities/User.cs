using System;
using System.Collections.Generic;

namespace BRRF.IdentityServer.Repository.Entities
{
    public partial class User
    {
        public User()
        {
            UserBrand = new HashSet<UserBrand>();
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<UserBrand> UserBrand { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }

    }
}
