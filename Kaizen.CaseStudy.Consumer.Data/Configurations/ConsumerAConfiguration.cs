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
    public class ConsumerAConfiguration : IEntityTypeConfiguration<ConsumerA>
    {
        public void Configure(EntityTypeBuilder<ConsumerA> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Surname)
                .IsRequired();

            builder.Property(c => c.BirthDate)
                .IsRequired();

            builder.Property(c => c.CitizenNo)
                .IsRequired();

            builder.ToTable("Consumers");
        }
    }
}
