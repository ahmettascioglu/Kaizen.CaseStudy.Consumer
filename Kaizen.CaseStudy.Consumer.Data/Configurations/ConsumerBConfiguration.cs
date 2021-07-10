using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.CaseStudy.Consumer.Data.Configurations
{
    class ConsumerBConfiguration : IEntityTypeConfiguration<ConsumerB>
    {
        public void Configure(EntityTypeBuilder<ConsumerB> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Surname)
                .IsRequired();

            builder.Property(c => c.Education)
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .IsRequired();

            builder.ToTable("Consumers");
        }
    }
}
