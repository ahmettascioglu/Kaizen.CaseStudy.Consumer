using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Models;

namespace Kaizen.CaseStudy.Consumer.Services.ConsumerCService
{
    public interface IConsumerCService
    {
        Task<IEnumerable<ConsumerC>> GetAllConsumersC();
        Task<ConsumerC> GetConsumerC(int id);
        Task<ConsumerC> CreateConsumerC(ConsumerC newConsumerC);
        Task<ConsumerC> UpdateConsumer(ConsumerC newConsumerC);
        Task DeleteConsumer(ConsumerC consumerC);
    }
}
