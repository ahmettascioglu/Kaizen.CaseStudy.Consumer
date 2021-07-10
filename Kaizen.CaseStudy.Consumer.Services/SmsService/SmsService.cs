using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.CaseStudy.Consumer.Services.SmsService
{
    class SmsService : ISmsService
    {
        /// <summary>
        /// Sends OTP Code via Sms Message. All Sms Service Providers Has Their Own Mechanism or API's so that this method is blank.
        /// </summary>
        /// <param name="otpCode">OTP Code</param>
        /// <param name="phoneNumber">Phone Number</param>
        public void SendSms(string otpCode, string phoneNumber)
        {
            // Send Sms via sms provider in here
        }


        /// <summary>
        ///  We Can Valide code and phone number if they match or not. We can use Database system or Cache System In here
        /// </summary>
        /// <param name="otpCode">OTP Code</param>
        /// <param name="phoneNumber">Phone Number</param>
        /// <returns></returns>
        public bool IsValid(string otpCode, string phoneNumber)
        {
            return true;
        }
    }
}
