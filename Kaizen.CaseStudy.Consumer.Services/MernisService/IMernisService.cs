using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Models;
using KPSServiceReference;

namespace Kaizen.CaseStudy.Consumer.Services.MernisService
{
    public interface IMernisService
    {
        Task<KPSPublicSoapClient> GetInstanceAsync();
        Task<bool> Validate(ConsumerA consumer);
    }
}
