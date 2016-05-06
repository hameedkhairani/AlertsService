using Twilio;

namespace NotificationService
{
    public class SMSNotification
    {
        private  string AccountSid = "AC86ea011ed3fe1293647e41bc8910a9d7";
        private  string AuthToken = "05a3af8210b71fe7e2cef7ca17e39200";
        private readonly string fromNumber = "+441286727032";

        public void SendSMS(string toNumber, string message)
        {
            var twilio = new TwilioRestClient(AccountSid, AuthToken);

            var ms = twilio.SendMessage(fromNumber, toNumber, message);
        }
    }
}