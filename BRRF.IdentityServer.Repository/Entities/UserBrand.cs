using System;
using System.Collections.Generic;

namespace BRRF.IdentityServer.Repository.Entities
{
    public partial class UserBrand
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BrandId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual User User { get; set; }
    }
}
