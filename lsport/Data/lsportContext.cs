using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace lsport.Models
{
    public class lsportContext : DbContext
    {
        public lsportContext(DbContextOptions<lsportContext> options)
            : base(options)
        {
        }

        public DbSet<Giphy> Giphy { get; set; }
    }
}
