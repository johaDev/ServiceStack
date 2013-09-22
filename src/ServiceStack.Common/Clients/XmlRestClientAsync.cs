using System;
using System.IO;
using ServiceStack.Clients;
using ServiceStack.Server;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.Web;

namespace ServiceStack.Clients
{
    [Obsolete("Use XmlServiceClient")]
    public class XmlRestClientAsync 
        : IRestClientAsync
    {
        public const string ContentType = "application/xml";

        public XmlRestClientAsync(string baseUri)
            : this()
        {
            this.BaseUri = baseUri.WithTrailingSlash();
        }

        public XmlRestClientAsync()
        {
            this.client = new AsyncServiceClient {
                ContentType = ContentType,
                StreamSerializer = SerializeToStream,
                StreamDeserializer = XmlSerializer.DeserializeFromStream
            };
        }

        public TimeSpan? Timeout
        {
            get { return this.client.Timeout; }
            set { this.client.Timeout = value; }
        }

        private static void SerializeToStream(IRequestContext requestContext, object dto, Stream stream)
        {
            XmlSerializer.SerializeToStream(dto, stream);
        }

        private readonly AsyncServiceClient client;

        public string BaseUri { get; set; }

        public void SetCredentials(string userName, string password)
        {
            this.client.SetCredentials(userName, password);
        }

        public void GetAsync<TResponse>(IReturn<TResponse> request, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
        {
            throw new NotImplementedException();
        }

        private string GetUrl(string relativeOrAbsoluteUrl)
        {
            return relativeOrAbsoluteUrl.StartsWith("http:")
                || relativeOrAbsoluteUrl.StartsWith("https:")
                     ? relativeOrAbsoluteUrl
                     : this.BaseUri + relativeOrAbsoluteUrl;
        }

        public void GetAsync<TResponse>(string relativeOrAbsoluteUrl, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
        {
            this.client.SendAsync(HttpMethods.Get, GetUrl(relativeOrAbsoluteUrl), null, onSuccess, onError);
        }

        public void DeleteAsync<TResponse>(string relativeOrAbsoluteUrl, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
        {
            this.client.SendAsync(HttpMethods.Delete, GetUrl(relativeOrAbsoluteUrl), null, onSuccess, onError);
        }

        public void DeleteAsync<TResponse>(IReturn<TResponse> request, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
        {
            throw new NotImplementedException();
        }

        public void PostAsync<TResponse>(IReturn<TResponse> request, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
        {
            throw new NotImplementedException();
        }

        public void PostAsync<TResponse>(string relativeOrAbsoluteUrl, object request, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
        {
            this.client.SendAsync(HttpMethods.Post, GetUrl(relativeOrAbsoluteUrl), request, onSuccess, onError);
        }

        public void PutAsync<TResponse>(IReturn<TResponse> request, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
        {
            throw new NotImplementedException();
        }

        public void PutAsync<TResponse>(string relativeOrAbsoluteUrl, object request, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
        {
            this.client.SendAsync(HttpMethods.Put, GetUrl(relativeOrAbsoluteUrl), request, onSuccess, onError);
        }

        public void CustomMethodAsync<TResponse>(string httpVerb, IReturn<TResponse> request, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
        {
            throw new NotImplementedException();
        }

        public void CancelAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}