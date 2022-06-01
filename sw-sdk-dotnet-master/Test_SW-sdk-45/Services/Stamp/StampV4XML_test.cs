﻿using System;
using System.IO;
using System.Text;
using SW.Services.Stamp;
using Test_SW.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Test_SW.Services.Stamp_Test
{
    [TestClass]
    public class StampV4XML_Test
    {
        [TestMethod]
        public void Stamp_Test_StampV4XMLV2_SameCustomID_byToken_Ok()
        {
            string CustomId = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV2) stamp.TimbrarV2(xml, null, CustomId);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            System.Threading.Thread.Sleep(5000);
            xml = GetXml(build);
            response = (StampResponseV2) stamp.TimbrarV2(xml, null, CustomId);
            Assert.IsTrue(response.status == "error" && response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV4_SameCustomID_byToken_Ok()
        {
            string CustomId = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build);
            var response = stamp.TimbrarV4(xml, null, CustomId);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            System.Threading.Thread.Sleep(5000);
            xml = GetXml(build);
            response = stamp.TimbrarV4(xml, null, CustomId);
            ValidateResponseV4(response);
            Assert.IsTrue(response.status == "error" && response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV2_SameCustomID_byToken_NoExistURLXML()
        {
                string CustomId = Guid.NewGuid().ToString();
                var build = new BuildSettings();
                StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
                var xml = GetXml(build);
                var response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomId);
                Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
                xml = GetXml(build);
                response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomId);
                Assert.IsTrue(response.status == "error" && response.message == "No es posible obtener el url para descargar el XML");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV4_SameCustomID_byToken_NoExistURLXML()
        {
            string CustomId = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build);
            var response = stamp.TimbrarV4(xml, null, CustomId);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            xml = GetXml(build);
            response = stamp.TimbrarV4(xml, null, CustomId);
            Assert.IsTrue(response.status == "error" && response.message == "No es posible obtener el url para descargar el XML");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV2_DifCustomID_byToken()
        {
            string CustomIdfirstRequest = Guid.NewGuid().ToString();
            string CustomIdSecondRequest = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            BaseStampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV2) stamp.TimbrarV2(xml, null, CustomIdfirstRequest);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            response = (StampResponseV2) stamp.TimbrarV2(xml, null, CustomIdSecondRequest);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV4_DifCustomID_byToken()
        {
            string CustomIdfirstRequest = Guid.NewGuid().ToString();
            string CustomIdSecondRequest = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            BaseStampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build);
            var response = stamp.TimbrarV4(xml, null, CustomIdfirstRequest);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            response = stamp.TimbrarV4(xml, null, CustomIdSecondRequest);
            ValidateResponseV4(response);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.CerPassword);
            return xml;
        }
        private static void ValidateResponseV4(StampResponseV4 response)
        {
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.cadenaOriginalSAT));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.cfdi));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.fechaTimbrado));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.noCertificadoCFDI));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.noCertificadoSAT));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.qrCode));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.selloCFDI));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.selloSAT));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.uuid));
        }
    }
}
