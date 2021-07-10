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
    public class ConsumerBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<ConsumerB> Consumers { get; set; }

        public ConsumerBContext(DbContextOptions<ConsumerBContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ConsumerBConfiguration());
        }
    }
}
