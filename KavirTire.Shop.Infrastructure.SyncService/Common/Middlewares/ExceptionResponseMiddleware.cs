using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;

namespace KavirTire.Shop.Infrastructure.SyncService.Common.Middlewares
{
    public class ExceptionResponseMiddleware : DelegatingHandler
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                logger.Error(e);
                var error = new Dictionary<string, string>
                {
                    { "Message", e.Message },
                    { "StackTrace", e.StackTrace }
                };

                foreach (DictionaryEntry data in e.Data)
                    error.Add(data.Key.ToString(), data.Value.ToString());

                string json = JsonConvert.SerializeObject(error, Formatting.Indented);

                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new StringContent(json);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }

        }
    }
}