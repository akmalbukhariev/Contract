using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
//using TLSharp.Core;

namespace TestAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            SendMessage();

            Console.ReadLine();
        }

        static void SendMessage()
        {
            var accountSid = "ACaabae6218993aca7c7509cfdd1ee0654";
            var authToken = "a0b32fc81e2255eb2c8bafa17392348a";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber("+821084410694"));
            messageOptions.Body = "Hello from Kontract maker";
            var message = MessageResource.Create(messageOptions);
            
            Console.WriteLine(message.Body);
        }
    }
}
