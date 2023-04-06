using GiangNLH.ArtShop.Models.EntityBase;
using System;
using System.Collections.Generic;

namespace Tutor_SP23_BL2_NET104.Models
{
    public partial class Category : EntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
