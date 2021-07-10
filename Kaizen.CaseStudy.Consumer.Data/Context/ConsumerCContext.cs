using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Models;
using Kaizen.CaseStudy.Consumer.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.CaseStudy.Consumer.Data.Context
{
    public class ConsumerCContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<ConsumerC> Consumers { get; set; }

        public ConsumerCContext(DbContextOptions<ConsumerCContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ConsumerCConfiguration());
        }
    }
}
