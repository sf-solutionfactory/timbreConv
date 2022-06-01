﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test_SW.Helpers;
using SW.Services.Issue;
using SW.Services.Stamp;
using System.IO;
using System.Xml;

namespace Test_SW.Services.Issue
{
    [TestClass]
    public class IssueV2_Test
    {
        [TestMethod]
        public void IssueV2XMLV1()
        {
            var build = new BuildSettings();
            SW.Services.Issue.IssueV2 issue = new SW.Services.Issue.IssueV2(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV1)issue.TimbrarV1(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
            response= (StampResponseV1)issue.TimbrarV1(xml);
            Assert.IsTrue(response.status == "error" && response.message== "307. El comprobante contiene un timbre previo.");
        }

        [TestMethod]
        public void IssueV2XMLV2()
        {
            var build = new BuildSettings();
            SW.Services.Issue.IssueV2 issue = new SW.Services.Issue.IssueV2(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV2)issue.TimbrarV2(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
            response = (StampResponseV2)issue.TimbrarV2(xml);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }

        [TestMethod]
        public void IssueV2XMLV3()
        {
            var build = new BuildSettings();
            SW.Services.Issue.IssueV2 issue = new SW.Services.Issue.IssueV2(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV3)issue.TimbrarV3(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
            response = (StampResponseV3)issue.TimbrarV3(xml);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }

        [TestMethod]
        public void StampXMLV4byToken()
        {
            var build = new BuildSettings();
            SW.Services.Issue.IssueV2 issue = new SW.Services.Issue.IssueV2(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV4)issue.TimbrarV4(xml);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.uuid), "El resultado data.uuid viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloSAT), "El resultado data.selloSAT viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.qrCode), "El resultado data.qrCode viene vacio.");
            response = (StampResponseV4)issue.TimbrarV4(xml);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }

        static Random randomNumber = new Random(1);
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/fileIssue.xml"));
            xml = SW.Tools.Fiscal.RemoverCaracteresInvalidosXml(xml);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            doc.DocumentElement.SetAttribute("Fecha", DateTime.Now.AddHours(-12).ToString("s"));
            doc.DocumentElement.SetAttribute("Folio", DateTime.Now.Ticks.ToString() + randomNumber.Next(100));
            xml = doc.OuterXml;
            return xml;
        }

    }
}
