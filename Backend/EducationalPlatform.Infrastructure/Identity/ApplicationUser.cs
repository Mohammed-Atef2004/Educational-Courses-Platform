using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public sealed class ApplicationUser : IdentityUser
    {
        public Guid DomainUserId { get; set; }
    }
}
