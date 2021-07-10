using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core;
using Kaizen.CaseStudy.Consumer.Core.Models;

namespace Kaizen.CaseStudy.Consumer.Services.ConsumerBService
{
    public class ConsumerBService : IConsumerBService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsumerBService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ConsumerB>> GetAllConsumersB()
        {
            return await _unitOfWork.ConsumersB.GetAllAsync();
        }

        public async Task<ConsumerB> GetConsumerB(int id)
        {
            return await _unitOfWork.ConsumersB.GetByIdAsync(id);
        }

        public async Task<ConsumerB> CreateConsumerB(ConsumerB newConsumerB)
        {
            await _unitOfWork.ConsumersB.AddAsync(newConsumerB);

            return newConsumerB;
        }

        public async Task<ConsumerB> UpdateConsumer(ConsumerB newConsumerB)
        {
            _unitOfWork.ConsumersB.UpdateAsync(newConsumerB);
            await _unitOfWork.CommitAsync();
            return newConsumerB;
        }

        public async Task DeleteConsumer(ConsumerB consumerB)
        {
            _unitOfWork.ConsumersB.Remove(consumerB);

            await _unitOfWork.CommitAsync();
        }
    }
}
