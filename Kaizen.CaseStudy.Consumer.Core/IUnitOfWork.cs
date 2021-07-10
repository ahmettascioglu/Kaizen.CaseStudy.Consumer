using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Repositories;

namespace Kaizen.CaseStudy.Consumer.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IConsumerARepository ConsumersA { get; }
        IConsumerBRepository ConsumersB { get; }
        IConsumerCRepository ConsumersC { get; }
        Task<int> CommitAsync();
    }
}
