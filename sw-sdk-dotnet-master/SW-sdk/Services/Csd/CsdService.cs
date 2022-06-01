﻿using SW.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SW.Services.Csd
{
    public abstract class CsdService : Services
    {
        protected CsdService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected CsdService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract CsdResponse UploadCsd(string cer, string key, string password, string certificateType, bool isActive);
        internal abstract CsdResponse DisableCsd(string certificateNumber);
        internal abstract InfoCsdResponse InfoCsd(string certificateNumber);
        internal abstract InfoCsdResponse ActiveCsd(string rfc, string type);
        internal abstract ListInfoCsdResponse ListCsd();
        internal abstract ListInfoCsdResponse ListCsdByType(string type);
        internal abstract ListInfoCsdResponse ListCsdByRfc(string rfc);
        internal virtual HttpWebRequest RequestUploadCsd(string cer, string key, string password, string certificateType, bool isActive)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url +"certificates/save");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new UploadCsdRequest()
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                type = certificateType,
                is_active = isActive
            });
            request.ContentLength = body.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }

        internal virtual HttpWebRequest RequestDisableCsd(string certificateNumber)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "certificates/" + certificateNumber);
            request.ContentType = "application/json";
            request.Method = "DELETE";
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestInfoCsd(string certificateNumber)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "certificates/" + certificateNumber);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }

        internal virtual HttpWebRequest RequestActiveCsd(string rfc, string type)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "certificates/rfc/" + rfc + "/" + type);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestListCsd()
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "certificates");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }

        internal virtual HttpWebRequest RequestListCsdByType(string type)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "certificates/type/" + type);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestListCsdByRfc(string rfc)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "certificates/rfc/" + rfc);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
    }
}
