﻿using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Poof.DB.Models;

namespace Poof.DB.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {}

        public DbSet<DbFellowship> Fellowships { get; set; }
        public DbSet<DbTransaction> Transactions { get; set; }
        public DbSet<DbMembership> Memberships { get; set; }
    }
}
