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
    public class ConsumerBRepository : Repository<ConsumerB>,IConsumerBRepository
    {
        public ConsumerBRepository(DbContext context) : base(context)
        {
        }

        private ConsumerBContext ConsumerBContext => Context as ConsumerBContext;
    }
}
