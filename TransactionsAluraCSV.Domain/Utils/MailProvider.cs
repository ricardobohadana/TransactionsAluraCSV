using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Interfaces.Mail;

namespace TransactionsAluraCSV.Domain.Utils
{
    public class MailProvider: IMailProvider
    {
        private string _api_key;
        private string _api_secret;

        public MailProvider(string api_key, string api_secret)
        {
            _api_key = api_key;
            _api_secret = api_secret;
        }

        private MailjetClient CreateMailjetClient()
        {
            return new MailjetClient(_api_key, _api_secret);
        }

        public async Task SendPassword(string emailAddress, string password)
        {
            var client =  CreateMailjetClient();
            var request = new MailjetRequest
            {
                Resource = Send.Resource
            }
            .Property(Send.Messages, new JArray {
             new JObject {
              {
               "From",
               new JObject {
                {"Email", "cacolorde@gmail.com"},
                {"Name", "AnaliseDeTransacoes"}
               }
              }, {
               "To",
               new JArray {
                new JObject {
                 {
                  "Email",
                  emailAddress
                 }, {
                  "Name",
                  emailAddress
                 }
                }
               }
              }, {
               "Subject",
               "Bem vindo, aqui está sua senha de acesso"
              }, {
               "TextPart",
               "A sua senha é: "
              }, {
               "HTMLPart",
               $"<strong> {password}</strong>"
              }, {
               "CustomID",
               emailAddress
              }
             }
            });

            MailjetResponse response = await client.PostAsync(request);
            if (!response.IsSuccessStatusCode) 
            {
                throw new Exception($"Status de erro: {response.StatusCode}\nInformações: {response.GetErrorInfo()}\nMensagem de erro: {response.GetErrorMessage()}");
            }


        }
    }
}
