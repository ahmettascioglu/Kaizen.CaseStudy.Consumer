using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Models;
using Kaizen.CaseStudy.Consumer.Core.Repositories;
using Kaizen.CaseStudy.Consumer.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.CaseStudy.Consumer.Data.Repositories
{
    public class ConsumerARepository : Repository<ConsumerA>,IConsumerARepository
    {
        public ConsumerARepository(DbContext context) : base(context)
        {
        }

        private ConsumerAContext ConsumerAContext => Context as ConsumerAContext;
    }
}
