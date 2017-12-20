using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dev.SDK
{
    /// <summary>
    /// HTTP请求供应商
    /// </summary>
    public class RequestProvier
    {
        private static readonly Lazy<RequestProvier> Lazy = new Lazy<RequestProvier>(() => new RequestProvier());
        private static readonly HttpClient _client;

        static RequestProvier() { }

        /// <summary>
        /// 获取RequestManager实例并指定一个发送请求的基础地址
        /// </summary>
        /// <param name="baseAddress">发送请求时使用的Internet资源的统一资源标识符（URI）的基础地址</param>
        /// <returns></returns>
        public static RequestProvier GetInstance(string baseAddress, bool isOptimize = true)
        {
            if (_client.BaseAddress == null)
                _client.BaseAddress = new Uri(baseAddress);
            //HttpClient 预热 重用 TCP连接
            if (isOptimize && !string.IsNullOrEmpty(baseAddress))
            {
                _client.SendAsync(new HttpRequestMessage
                {
                    Method = new HttpMethod("HEAD"),
                    RequestUri = new Uri(baseAddress)
                }).Result.EnsureSuccessStatusCode();
            }
            return Lazy.Value;
        }

        public static RequestProvier Instance { get => Lazy.Value; }

        /// <summary>
        /// HttpClient实例
        /// </summary>
        public HttpClient Client { get => _client; }

        #region async

        public async Task<ResponseData<TResult>> GetAsync<TResult>(string requestUri)
        {
            var respData = new ResponseData<TResult>();
            HttpResponseMessage resp = null;
            try
            {
                resp = await Client.GetAsync(requestUri);
                resp.EnsureSuccessStatusCode();
                respData.Content = await resp.Content.ReadAsAsync<TResult>();
                respData.Message = resp.ReasonPhrase;
            }
            catch (Exception ex)
            {

                respData.Message = ex.Message;
            }
            return respData;
        }

        public async Task<ResponseData<TResult>> PostAsync<TResult, TParameter>(string requestUri, TParameter parameter)
        {
            var respData = new ResponseData<TResult>();
            HttpResponseMessage resp = null;
            try
            {

                resp = await Client.PostAsJsonAsync(requestUri, parameter);
                resp.EnsureSuccessStatusCode();
                respData.Content = await resp.Content.ReadAsAsync<TResult>();
                respData.Message = resp.ReasonPhrase;

            }
            catch (Exception ex)
            {

                respData.Message = ex.Message;
            }
            return respData;
        }

        public async Task<ResponseData<TResult>> PutAsync<TResult, TParameter>(string requestUri, TParameter parameter)
        {
            var respData = new ResponseData<TResult>();
            HttpResponseMessage resp = null;
            try
            {
                resp = await Client.PutAsJsonAsync(requestUri, parameter);
                resp.EnsureSuccessStatusCode();
                respData.Content = await resp.Content.ReadAsAsync<TResult>();
                respData.Message = resp.ReasonPhrase;
            }
            catch (Exception ex)
            {

                respData.Message = ex.Message;
            }
            return respData;
        }

        public async Task<ResponseData<TResult>> DeleteAsync<TResult>(string requestUri)
        {
            var respData = new ResponseData<TResult>();
            HttpResponseMessage resp = null;
            try
            {
                resp = await Client.DeleteAsync(requestUri);
                resp.EnsureSuccessStatusCode();
                respData.Content = await resp.Content.ReadAsAsync<TResult>();
                respData.Message = resp.ReasonPhrase;
            }
            catch (Exception ex)
            {
                respData.Message = ex.Message;
            }
            return respData;
        }

        #endregion
    }
}