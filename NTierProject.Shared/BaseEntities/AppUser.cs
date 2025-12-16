using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Shared.BaseEntities
{
    public class AppUser : IdentityUser
    {

        public bool IsActive { get; set; } 
        public DateTime CreatedOn { get; set; } = DateTime.Now; 
    }
}
