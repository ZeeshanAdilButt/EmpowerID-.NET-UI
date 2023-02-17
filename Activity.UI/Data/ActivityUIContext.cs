using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ActivityApp.Domain.Data;

namespace Activity.UI.Data
{
    public class ActivityUIContext : DbContext
    {
        public ActivityUIContext (DbContextOptions<ActivityUIContext> options)
            : base(options)
        {
        }

        public DbSet<ActivityApp.Domain.Data.Employee> Employee { get; set; } = default!;
    }
}
