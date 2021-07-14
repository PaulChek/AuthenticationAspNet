using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailVerification.Model {
    public class AuthDbContext : IdentityDbContext {
        public AuthDbContext(DbContextOptions options) : base(options) {
        }
    }
}
