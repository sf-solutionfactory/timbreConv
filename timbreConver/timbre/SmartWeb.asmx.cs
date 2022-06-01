using System;
using System.IO;
using System.Web.Services;
using SW.Services.Cancelation;
using SW.Services.Cancelation2;

namespace timbre
{
    /// <summary>
    /// Descripción breve de SmartWeb
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class SmartWeb : System.Web.Services.WebService
    {

        [WebMethod]
        public CancelationResponse2 Cancelarv4(string USER, string PASSWORD, string SYST, string OPTION,
                                 string XML_IN, string UUID, string RFC, string PASS_KEY,
                                 string NAME_KEY, string NAME_CER, string MOTIVO, string UUID_SUST)
        {
            string url = "http://services.test.sw.com.mx";
            if (SYST == "P")
            {
                url = "https://services.sw.com.mx";
            }
            var appData = Server.MapPath("~/App_Data");
            var file = Path.Combine(appData, Path.GetFileName(NAME_CER));
            string Cer = Convert.ToBase64String(File.ReadAllBytes(file));
            file = Path.Combine(appData, Path.GetFileName(NAME_KEY));
            string Key = Convert.ToBase64String(File.ReadAllBytes(file));

            string uuid_sust = UUID_SUST;
            if (UUID_SUST == "")
            {
                uuid_sust = null;
            }

            Cancelation cancelation = new Cancelation(url, USER, PASSWORD);
            CancelationResponse response = cancelation.CancelarByCSD(Cer, Key, RFC, PASS_KEY, UUID, MOTIVO, uuid_sust);

            CancelationResponse2 response2 = new CancelationResponse2();
            if (response.data != null)
                response2.data.acuse = response.data.acuse;
            response2.message       = response.message;
            response2.messageDetail = response.messageDetail;
            response2.status        = response.status;

            return response2;
        }

    }
}
