﻿using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace RentalFlat_API.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly MailJetSettings _mailJetSettings;

        public EmailSender(IOptions<MailJetSettings> mailjetSettings)
        {
            _mailJetSettings = mailjetSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient(_mailJetSettings.PublicKey, _mailJetSettings.PrivateKey)
            {
                Version = ApiVersion.V3_1,
            };

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }.Property(Send.Messages, new JArray {
                new JObject {
                    {"From", new JObject {
                        {"Email", _mailJetSettings.Email},
                        {"Name", "Mailjet Pilot"}
                    }},
                    {"To", new JArray {
                        new JObject {
                            {"Email", email},
                            {"Name", "Hello"}}
                        }},
                    {"Subject", subject},
                    {"HTMLPart", htmlMessage} //You can change message in OrderController 59-60
                }
            });
            MailjetResponse response = await client.PostAsync(request);
        }
    }
}
