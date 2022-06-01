﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;
using Test_SW.Helpers;
using System.Xml;

namespace Test_SW.Services.Stamp_Test
{
    [TestClass]
    public class StampV2_Test_45
    {
        [TestMethod]
        public void Stamp_Test_45_StampV2XMLV1()
        {
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
            response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }
        
        [TestMethod]
        public void Stamp_Test_45_StampV2XMLV2()
        {
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
            response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV2XMLV2WithAddenda307()
        {
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
            response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
            Assert.IsTrue(response.data.cfdi.Contains("cfdi:Addenda"));
        }

        [TestMethod]
        public void Stamp_Test_45_StampV2XMLV3byToken()
        {
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
            response = (StampResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV2XMLV3WithAddenda307()
        {
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");

            response = (StampResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.status == "error"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            Assert.IsTrue(response.data.cfdi.Contains("cfdi:Addenda"));

        }
        [TestMethod]
        public void Stamp_Test_45_StampV2XMLV4byToken()
        {
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV4)stamp.TimbrarV4(xml);
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
            response = (StampResponseV4)stamp.TimbrarV4(xml);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV2XMLV4WithAddenda307()
        {
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV4)stamp.TimbrarV4(xml);
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
            response = (StampResponseV4)stamp.TimbrarV4(xml);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
            Assert.IsTrue(response.data.cfdi.Contains("cfdi:Addenda"));

        }
        [TestMethod]
        public void Stamp_Test_45_ValidateServerError()
        {
            var resultExpect = "404";
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url + "/ot", build.Token);
            var xml = File.ReadAllText("Resources/File.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateFormatToken()
        {
            var resultExpect = "Token Mal Formado";
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.Token + ".");
            var xml = File.ReadAllText("Resources/file.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains("401"), (string)resultExpect);
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateExistToken()
        {
            var resultExpect = "401 Unauthorized";
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, "");
            var xml = File.ReadAllText("Resources/file.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains("401"), (string)resultExpect);
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateEmptyXML()
        {
            var resultExpect = "Xml CFDI33 no proporcionado o viene vacio.";
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.Token);
            var xml = File.ReadAllText("Resources/EmptyXML.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateSpecialCharactersFromXML()
        {
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.Token);
            var xml = File.ReadAllText("Resources/SpecialCharacters.xml");
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.CerPassword);
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.status == "success", "Result not expected. Error: " + response.message);
            Assert.IsFalse(string.IsNullOrEmpty(response.data.tfd), "Result not expected. Error: " + response.message);
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateIsUTF8FromXML()
        {
            var resultExpect = "301";
            var build = new BuildSettings();
            StampV2 stamp = new StampV2(build.Url, build.Token);
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/fileANSI.xml"));            
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains(resultExpect), "Result not expected. Error: " + response.message);
        }
        [TestMethod]
        public void Stamp_Test_45_MultipleStampV2XMLV1byToken()
        {
            var build = new BuildSettings();
            var resultExpect = false;
            int iterations = 10;
            StampV2 stamp = new StampV2(build.Url, build.Token);
            List<StampResponseV1> listXmlResult = new List<StampResponseV1>();
            for (int i = 0; i < iterations; i++)
            {
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
                xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.CerPassword);
                var response = (StampResponseV1)stamp.TimbrarV1(xml);
                listXmlResult.Add(response);
            }
            if (listXmlResult != null)
                resultExpect = listXmlResult.FindAll(w => w.status == ResponseType.success.ToString() || w.message.Contains("72 horas")).Count == iterations;

            Assert.IsTrue((bool)resultExpect);
        }
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.CerPassword);
            return xml;
        }
    }
}
