using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.CaseStudy.Consumer.Services.SmsService
{
    public interface ISmsService
    {
        void SendSms(string otpCode,string phoneNumber);
        bool IsValid(string otpCode, string phoneNumber);
    }
}
