using System;
using System.Collections.Generic;

namespace BRRF.IdentityServer.Repository.Entities
{
    public partial class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
