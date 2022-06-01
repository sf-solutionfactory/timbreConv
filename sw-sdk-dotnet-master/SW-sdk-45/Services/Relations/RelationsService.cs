﻿using SW.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SW.Services.Relations
{
    public abstract class RelationsService : Services
    {
        protected RelationsService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected RelationsService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract RelationsResponse RelationsRequest(string cer, string key, string rfc, string password, string uuid);
        internal abstract RelationsResponse RelationsRequest(byte[] xmlCancelation);
        internal abstract RelationsResponse RelationsRequest(string pfx, string rfc, string password, string uuid);
        internal abstract RelationsResponse RelationsRequest(string rfc, string uuid);
        internal virtual Dictionary<string, string> GetHeaders()
        {
            this.SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
            return headers;
        }
        internal virtual StringContent RequestRelations(string cer, string key, string rfc, string password, string uuid)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new RelationsRequestCSD()
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                rfc = rfc,
                uuid = uuid
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }
        internal virtual MultipartFormDataContent RequestRelations(byte[] xmlCancelation)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(xmlCancelation);
            content.Add(fileContent, "xml", "xml");
            return content;
        }
        internal virtual StringContent RequestRelations(string pfx, string rfc, string password, string uuid)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new RelationsRequestPFX()
            {
                b64Pfx = pfx,
                password = password,
                rfc = rfc,
                uuid = uuid
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }
        internal virtual HttpWebRequest RequestRelations(string rfc, string uuid)
        {
            this.SetupRequest();
            string path = $"relations/{rfc}/{uuid}";
            var request = (HttpWebRequest)WebRequest.Create(this.Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
    }
}
