using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Models;

namespace Kaizen.CaseStudy.Consumer.Services.ConsumerBService
{
    public interface IConsumerBService
    {
        Task<IEnumerable<ConsumerB>> GetAllConsumersB();
        Task<ConsumerB> GetConsumerB(int id);
        Task<ConsumerB> CreateConsumerB(ConsumerB newConsumerB);
        Task<ConsumerB> UpdateConsumer(ConsumerB newConsumerB);
        Task DeleteConsumer(ConsumerB consumerB);
    }
}
