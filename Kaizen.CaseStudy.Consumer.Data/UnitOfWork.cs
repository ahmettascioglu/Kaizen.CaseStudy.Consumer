using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core;
using Kaizen.CaseStudy.Consumer.Core.Repositories;
using Kaizen.CaseStudy.Consumer.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.CaseStudy.Consumer.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _context;
        private ConsumerARepository _consumerARepository;
        private ConsumerBRepository _consumerBRepository;
        private ConsumerCRepository _consumerCRepository;



        public IConsumerARepository ConsumersA =>
            _consumerARepository ??= new ConsumerARepository(_context);

        public IConsumerBRepository ConsumersB => _consumerBRepository ??= new ConsumerBRepository(_context);
        public IConsumerCRepository ConsumersC => _consumerCRepository ??= new ConsumerCRepository(_context);
        public async Task<int> CommitAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public UnitOfWork(DbContext context)
        {
            this._context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
