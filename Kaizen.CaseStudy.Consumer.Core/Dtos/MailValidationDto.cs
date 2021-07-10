using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.CaseStudy.Consumer.Core.Dtos
{
    public class MailValidationDto
    {
        public int ConsumerId { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
