using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transport.Model;

namespace Transport.Data
{
    public class TransportContext : DbContext
    {
        public TransportContext (DbContextOptions<TransportContext> options)
            : base(options)
        {
        }

        public DbSet<Transport.Model.LicenseCategory> LicenseCategory { get; set; } = default!;

        public DbSet<Transport.Model.Driver>? Driver { get; set; }

        public DbSet<Transport.Model.Client>? Client { get; set; }

        public DbSet<Transport.Model.Vehicle>? Vehicle { get; set; }

        public DbSet<Transport.Model.Shipping>? Shipping { get; set; }
    }
}
