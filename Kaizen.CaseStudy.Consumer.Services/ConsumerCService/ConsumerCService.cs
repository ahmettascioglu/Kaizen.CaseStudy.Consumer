using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core;
using Kaizen.CaseStudy.Consumer.Core.Models;

namespace Kaizen.CaseStudy.Consumer.Services.ConsumerCService
{
    public class ConsumerCService : IConsumerCService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsumerCService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ConsumerC>> GetAllConsumersC()
        {
            return await _unitOfWork.ConsumersC.GetAllAsync();
        }

        public async Task<ConsumerC> GetConsumerC(int id)
        {
            return await _unitOfWork.ConsumersC.GetByIdAsync(id);
        }

        public async Task<ConsumerC> CreateConsumerC(ConsumerC newConsumerC)
        {
            await _unitOfWork.ConsumersC.AddAsync(newConsumerC);

            return newConsumerC;
        }

        public async Task<ConsumerC> UpdateConsumer(ConsumerC newConsumerC)
        {
            _unitOfWork.ConsumersC.UpdateAsync(newConsumerC);
            await _unitOfWork.CommitAsync();
            return newConsumerC;
        }

        public async Task DeleteConsumer(ConsumerC consumerC)
        {
            _unitOfWork.ConsumersC.Remove(consumerC);

            await _unitOfWork.CommitAsync();
        }
    }
}
