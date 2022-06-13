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
        //public string Token = @"T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbXB3YVZxTHdOdHAwVXY2NTdJb1hkREtXTzE3dk9pMmdMdkFDR2xFWFVPUXpTUm9mTG1ySXdZbFNja3FRa0RlYURqbzdzdlI2UUx1WGJiKzViUWY2dnZGbFloUDJ6RjhFTGF4M1BySnJ4cHF0YjUvbmRyWWpjTkVLN3ppd3RxL0dJPQ.T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbFlVcU92YUJTZWlHU3pER1kySnlXRTF4alNUS0ZWcUlVS0NhelhqaXdnWTRncklVSWVvZlFZMWNyUjVxYUFxMWFxcStUL1IzdGpHRTJqdS9Zakw2UGRDN2VkNEVBUGM1UUt3NVhZVC9QUTViWmZ6dGNJUG5JZVJ1d1hPdmFlN2s3cEp3UW5UY2hORXVWeS9mNnZ6YVZQTlg1OTdBbWJUNzJ4NHFJNVJnOFBxTEo3TGQwank2dlVwektHUmJwY2RqNGdYRG5yaTVZUTBaZ05vR1Y0Z0xsNzg5MlM0cWJUK2hRamV2bXUwcFVGM3E4SzZMNFkvVE5LTCtJZFFEVHNob05QVmRzY2dSUGxBUXBoc29JcVp1TW9MV1FkUUtTRVdROVNPTVRMYkg5dmIrM25LM3pRbDBKN2RHaEI5TDZLK2hqVUhJU3RsZ3dEeGc0NnlWUXUvZEpmc3F6c1pNZHF4YitvYzhLQ1BSWW1vejE2ZGNNVHdETitIckl3OGhVRXFSZFFGY2lQSktqQW5LRWdCNm1jT2VzQmR4TWxFRXg1NTFXZ1UzSGNobTNXbGtUaUo5cmNucnYrWXM5cVQ0Q0NlODFPaldKZjVTRHR6alNodjc0VFgwZGE.loaXVczHpJjV8E_3NYByEmxRJKHFCS0qHPOr7LJKtPM";
        //CONVERGRAM
        public string Rfc           = "CME930714K32";
        public string noCertificado = "00001000000505173025";
        public string Cer           = Convert.ToBase64String(File.ReadAllBytes("../../../Resources/CME930714K32.cer"));
        public string Key           = Convert.ToBase64String(File.ReadAllBytes("../../../Resources/CME930714K32.key"));
        //public string Pfx = Convert.ToBase64String(File.ReadAllBytes("Resources/CertificadosDePrueba/CSD_XIA190128J61.pfx"));
        //public byte[] Acuse = File.ReadAllBytes("Resources/acuse.xml");
        //public byte[] RelationsXML = File.ReadAllBytes("Resources/RelationsXML.xml");
        public byte[] CancelacionXML = File.ReadAllBytes("../../../Resources/CancelacionXML.xml");

        //public Dictionary<string, string> observaciones = new Dictionary<string, string>() { { "Observaciones", "Entregar de 9am a 6pm" } };
        public string templateId    = "3a12dabd-66fa-4f18-af09-d1efd77ae9ce";
    }
}
