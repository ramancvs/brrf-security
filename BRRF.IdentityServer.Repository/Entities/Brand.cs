using System;
using System.Collections.Generic;

namespace BRRF.IdentityServer.Repository.Entities
{
    public partial class Brand
    {
        public Brand()
        {
            UserBrand = new HashSet<UserBrand>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<UserBrand> UserBrand { get; set; }
    }
}
