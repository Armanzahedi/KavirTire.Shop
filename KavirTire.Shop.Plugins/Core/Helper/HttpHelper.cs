using KavirTire.Shop.Plugins.Core;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Web;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Core.Helper
{
    public class HttpHelper
    {
        private readonly HttpClient _client;
        private readonly JavaScriptSerializer _jss;

        public HttpHelper(PluginBase.LocalPluginContext ctx,string baseAddress)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            _client = client;

            _jss = new JavaScriptSerializer { MaxJsonLength = 2147483647 };
        }
        public async Task<T2> Post<T1, T2>(string path, T1 request)
        {
            try
            {
                var json = _jss.Serialize(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync(path, httpContent)
                    .ConfigureAwait(continueOnCapturedContext: false);
                if (result.IsSuccessStatusCode)
                {
                    using (var content = result.Content)
                    {
                        var message = await content.ReadAsStringAsync()
                            .ConfigureAwait(continueOnCapturedContext: false);
                        return _jss.Deserialize<T2>(message);
                    }
                }

                using (var content = result.Content)
                {
                    var exceptionMessage = await GetExceptionMessage(content);
                    throw new HttpException((int)result.StatusCode, exceptionMessage);
                }
            }
            catch (HttpException e)
            {
                throw new InvalidPluginExecutionException(e.Message, e);

            }
        }
        public async Task<T2> Put<T1, T2>(string path, T1 request)
        {
            try
            {
                var json = _jss.Serialize(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await _client.PutAsync(path, httpContent)
                    .ConfigureAwait(continueOnCapturedContext: false);
                if (result.IsSuccessStatusCode)
                {
                    using (var content = result.Content)
                    {
                        var message = await content.ReadAsStringAsync()
                            .ConfigureAwait(continueOnCapturedContext: false);
                        return _jss.Deserialize<T2>(message);
                    }
                }

                using (var content = result.Content)
                {
                    var exceptionMessage = await GetExceptionMessage(content);
                    throw new HttpException((int)result.StatusCode, exceptionMessage);
                }
            }
            catch (HttpException e)
            {
                throw new InvalidPluginExecutionException(e.Message, e);
            }
        }
        public async Task<T1> Delete<T1>(string path)
        {
            try
            {
                var result = await _client.DeleteAsync(path)
                    .ConfigureAwait(continueOnCapturedContext: false);
                if (result.IsSuccessStatusCode)
                {
                    using (var content = result.Content)
                    {
                        var message = await content.ReadAsStringAsync()
                            .ConfigureAwait(continueOnCapturedContext: false);
                        return _jss.Deserialize<T1>(message);
                    }
                }

                using (var content = result.Content)
                {
                    var exceptionMessage = await GetExceptionMessage(content);
                    throw new HttpException((int)result.StatusCode, exceptionMessage);
                }
            }
            catch (HttpException e)
            {
                throw new InvalidPluginExecutionException(e.Message, e);

            }
        }
        public async Task<bool> Delete(string path)
        {
            try
            {
                var result = await _client.DeleteAsync(path)
                    .ConfigureAwait(continueOnCapturedContext: false);

                return result.IsSuccessStatusCode;
            }
            catch (HttpException e)
            {
                throw new InvalidPluginExecutionException(e.Message, e);

            }
        }
        public async Task<T> Get<T>(string path)
        {

            try
            {
                var result = await _client.GetAsync(path).ConfigureAwait(continueOnCapturedContext: false);

                if (result.IsSuccessStatusCode)
                {
                    using (var content = result.Content)
                    {
                        var message = await content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                        return _jss.Deserialize<T>(message);
                    }
                }
                using (var content = result.Content)
                {
                    var exceptionMessage = await GetExceptionMessage(content);
                    throw new HttpException((int)result.StatusCode, exceptionMessage);
                }
            }
            catch (HttpException e)
            {
                throw new InvalidPluginExecutionException(e.Message, e);

            }
        }
        public async Task Get(string path)
        {

            try
            {
                var result = await _client.GetAsync(path).ConfigureAwait(continueOnCapturedContext: false);

                if (result.IsSuccessStatusCode)
                {
                    using (var content = result.Content)
                    {
                        var message = await content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                    }
                } 
                else
                {
                    using (var content = result.Content)
                    {
                        var exceptionMessage = await GetExceptionMessage(content);
                        throw new HttpException((int)result.StatusCode, exceptionMessage);
                    }
                }
               
            }
            catch (HttpException e)
            {
                throw new InvalidPluginExecutionException(e.Message, e);

            }
        }
        private async Task<string> GetExceptionMessage(HttpContent content)
        {
            var message = await content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
            var details = "";
            var error = _jss.Deserialize<dynamic>(message);
            if (error?.Message != null)
                details = error.Message;
 
            return $"Http Exception: {details}";
        }

    }
}
