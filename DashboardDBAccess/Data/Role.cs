using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DashboardDBAccess.Contracts;
using DashboardDBAccess.Data.JoiningEntity;
using Microsoft.AspNetCore.Identity;

namespace DashboardDBAccess.Data
{

    public class Role : IdentityRole<int>, IPoco, IHasName, IHasUserRoles
    {
        [Required]
        [MaxLength(20)]
        public override string Name { get; set; }

        [ForeignKey("RoleId")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        
        [ForeignKey("RoleId")]
        public virtual ICollection<DefaultRoles> DefaultRoles { get; set; }
    }
}
