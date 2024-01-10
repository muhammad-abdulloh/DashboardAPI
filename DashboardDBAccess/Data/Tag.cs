using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DashboardDBAccess.Contracts;
using DashboardDBAccess.Data.JoiningEntity;

namespace DashboardDBAccess.Data
{
    public class Tag : IPoco, IHasName, IHasPostTag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("TagId")]
        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
