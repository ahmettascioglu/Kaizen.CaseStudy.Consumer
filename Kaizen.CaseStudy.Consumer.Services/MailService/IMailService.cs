namespace Kaizen.CaseStudy.Consumer.Services.MailService
{
    public interface IMailService
    {
        void SendMail(string code,string emailTo);
        bool IsValid(string code,string emailTo);
    }
}
