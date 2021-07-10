using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Models;

namespace Kaizen.CaseStudy.Consumer.Services.ConsumerAService
{
    public interface IConsumerAService
    {
        Task<IEnumerable<ConsumerA>> GetAllConsumersA();
        Task<ConsumerA> GetConsumerA(int id);
        Task<ConsumerA> CreateConsumerA(ConsumerA newConsumerA);
        Task<ConsumerA> UpdateConsumer(ConsumerA newConsumerA);
        Task DeleteConsumer(ConsumerA consumerA);
    }
}
