using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Models;

using KPSServiceReference;

namespace Kaizen.CaseStudy.Consumer.Services.MernisService
{
    public class MernisService : IMernisService
    {
        public readonly string serviceUrl = "https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx";
        public readonly EndpointAddress _endpointAddress;
        public readonly BasicHttpBinding basicHttpBinding;
        public MernisService()
        {
            _endpointAddress = new EndpointAddress(serviceUrl);

            basicHttpBinding =
                new BasicHttpBinding(_endpointAddress.Uri.Scheme.ToLower() == "http" ?
                    BasicHttpSecurityMode.None : BasicHttpSecurityMode.Transport);

            //Please set the time accordingly, this is only for demo
            basicHttpBinding.OpenTimeout = TimeSpan.MaxValue;
            basicHttpBinding.CloseTimeout = TimeSpan.MaxValue;
            basicHttpBinding.ReceiveTimeout = TimeSpan.MaxValue;
            basicHttpBinding.SendTimeout = TimeSpan.MaxValue;
        }
        public async Task<KPSPublicSoapClient> GetInstanceAsync()
        {
            return await Task.Run(() => new KPSPublicSoapClient(basicHttpBinding, _endpointAddress));
        }

        public async Task<bool> Validate(ConsumerA consumer)
        {
            var client = await GetInstanceAsync();
            var response = await client.TCKimlikNoDogrulaAsync(Convert.ToInt64(consumer.CitizenNo), consumer.Name, consumer.Surname,
                consumer.BirthDate.Year);
            return response.Body.TCKimlikNoDogrulaResult;
        }
    }
}
