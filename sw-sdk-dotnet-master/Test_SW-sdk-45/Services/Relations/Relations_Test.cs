﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Helpers;
using Test_SW.Helpers;
using SW.Services.Relations;

namespace Test_SW.Services.Relations_Test
{
    [TestClass]
    public class Relations_Test
    {
        [TestMethod]
        public void ValidateParameters()
        {
            var resultExpect = "El UUID proporcionado inválido. Favor de verificar.";
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            RelationsResponse response = relations.RelationsByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, "");
            Assert.IsTrue(response.messageDetail.Contains((string)resultExpect));
        }
        [TestMethod]
        public void RelationsByRfcUuid()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            RelationsResponse response = relations.RelationsByRfcUuid(build.Rfc, build.uuid);
            Assert.IsTrue(response.status == "success");
        }
        [TestMethod]
        public void RelationsByCSD()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            RelationsResponse response = relations.RelationsByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, build.uuid);
            Assert.IsTrue(response.status == "success");
        }
        [TestMethod]
        public void RelationsRejectByPfx()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            RelationsResponse response = relations.RelationsByPFX(build.Pfx, build.Rfc, build.CerPassword, build.uuid);
            Assert.IsTrue(response.status == "success");
        }
        [TestMethod]
        [Ignore]
        public void RelationsByXml()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            RelationsResponse response = relations.RelationsByXML(build.RelationsXML);
            Assert.IsTrue(response.status == "success");
        }
    }
}
