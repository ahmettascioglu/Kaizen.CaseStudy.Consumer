using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core;
using Kaizen.CaseStudy.Consumer.Core.Models;

namespace Kaizen.CaseStudy.Consumer.Services.ConsumerAService
{
    public class ConsumerAService : IConsumerAService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsumerAService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ConsumerA>> GetAllConsumersA()
        {
            return await _unitOfWork.ConsumersA.GetAllAsync();
        }

        public async Task<ConsumerA> GetConsumerA(int id)
        {
            return await _unitOfWork.ConsumersA.GetByIdAsync(id);
        }

        public async Task<ConsumerA> CreateConsumerA(ConsumerA newConsumerA)
        {
            await _unitOfWork.ConsumersA.AddAsync(newConsumerA);
            await _unitOfWork.CommitAsync();
            return newConsumerA;
        }

        public async Task<ConsumerA> UpdateConsumer(ConsumerA newConsumerA)
        {
            _unitOfWork.ConsumersA.UpdateAsync(newConsumerA);
            await _unitOfWork.CommitAsync();
            return newConsumerA;
        }

        public async Task DeleteConsumer(ConsumerA consumerA)
        {
            _unitOfWork.ConsumersA.Remove(consumerA);

            await _unitOfWork.CommitAsync();
        }
    }
}
