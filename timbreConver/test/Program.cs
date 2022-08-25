using System;
using System.IO;
using System.Text;
using SW;
using SW.Services.Cancelation;
using SW.Services.Stamp;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            //var response = cancelation.CancelarByXML(build.CancelacionXML);
            //var response = cancelation.CancelarByRfcUuid(build.Rfc, build.uuid, "01", build.templateId);
            //var response = cancelation.CancelarByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, build.uuid, "01", build.templateId);
            var response = cancelation.CancelarByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, build.uuid, "02");
            Console.WriteLine(response.message);
            //Assert.IsTrue(response != null && response.status == "success");
        }
    }

    class BuildSettings
    {
        public string Url           = "http://services.test.sw.com.mx";
        public string UrlApi        = "http://api.test.sw.com.mx";
        public string User          = "gustavom@conver.com.mx";
        public string Password      = "Conver2021$";
        public string CerPassword   = "Qwertyuiop1";
        public string uuid          = "ece55ed2-c5be-4f68-9967-c1895f321daf";
        //CONVERGRAM
        public string Rfc           = "CME930714K32";
        public string noCertificado = "00001000000505173025";
        public string Cer           = Convert.ToBase64String(File.ReadAllBytes("../../../Resources/CERC010.cer"));
        public string Key           = Convert.ToBase64String(File.ReadAllBytes("../../../Resources/KEYC010.key"));


        //public string User = "gustavom@conver.com.mx";
        //public string Password = "Conver2021$";
        //public string CerPassword = "12345678a";
        //public string uuid = "ece55ed2-c5be-4f68-9967-c1895f321daf";
        ////CONVERGRAM
        //public string Rfc = "IIA040805DZ4";
        //public string noCertificado = "30001000000400002427";
        //public string Cer = Convert.ToBase64String(File.ReadAllBytes("../../../Resources/CSD_IIA040805DZ4_20190617_133143s.cer"));
        //public string Key = Convert.ToBase64String(File.ReadAllBytes("../../../Resources/CSD_IIA040805DZ4_20190617_133143.key"));
        //public string Pfx = Convert.ToBase64String(File.ReadAllBytes("Resources/CertificadosDePrueba/CSD_XIA190128J61.pfx"));
        //public byte[] Acuse = File.ReadAllBytes("Resources/acuse.xml");
        //public byte[] RelationsXML = File.ReadAllBytes("Resources/RelationsXML.xml");
        public byte[] CancelacionXML = File.ReadAllBytes("../../../Resources/CancelacionXML.xml");

        //public Dictionary<string, string> observaciones = new Dictionary<string, string>() { { "Observaciones", "Entregar de 9am a 6pm" } };
        public string templateId    = "3a12dabd-66fa-4f18-af09-d1efd77ae9ce";
    }
}
