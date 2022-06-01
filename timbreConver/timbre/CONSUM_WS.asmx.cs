using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SW.Services.Authentication;
using SW.Services.Cancelation;
using SW.Services.Stamp;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace timbre
{
    /// <summary>
    /// Descripción breve de CONSUM_WS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CONSUM_WS : System.Web.Services.WebService
    {
        [WebMethod]
        public RESULT consum_ws(
              string _user,
              string _password,
              string _syst,
              string _option,
              string xml_in,
              string uuid,
              string rfc,
              string passKey,
              string name_key,
              string name_cer,
              string motivo, string folioSustitucion = null)//ADD SF RSG 01.06.2022)
        {
            RESULT result = new RESULT();
            string _url = !(_syst == "P") ? "http://services.test.sw.com.mx" : "https://services.sw.com.mx";
            string str = "";
            if (_option == "4D")
            {
                str = "D";
                _option = "4";
            }
            List<string> stringList = new List<string>();
            string empty = string.Empty;
            switch (_option)
            {
                case "1":
                    CONSUM_WS.TimbrarV1(empty, _url, _user, _password);
                    goto case "6";
                case "2":
                    CONSUM_WS.TimbrarV2(empty, _url, _user, _password);
                    goto case "6";
                case "3":
                    CONSUM_WS.TimbrarV3(empty, _url, _user, _password);
                    goto case "6";
                case "4":
                    string xmlInfo = xml_in;
                    if (str == "D")
                        xmlInfo = CONSUM_WS.GetXmlFile(xml_in);
                    result = CONSUM_WS.TimbrarV4(xmlInfo, _url, _user, _password);
                    if (str == "D")
                    {
                        try
                        {
                            XDocument.Parse(result.Xml).Save(xml_in);
                            result.Xml = "SAVED-WSSMART";
                            goto case "6";
                        }
                        catch (Exception ex)
                        {
                            goto case "6";
                        }
                    }
                    else
                        goto case "6";
                case "5":
                    CONSUM_WS.Autenticacion(_url, _user, _password);
                    goto case "6";
                case "6":
                case "7":
                case "s":
                    return result;
                case "8":
                    result = CONSUM_WS.CancelarCSD(_url, _user, _password, uuid, rfc, passKey, name_key, name_cer, motivo, folioSustitucion);
                    goto case "6";
                default:
                    result.List_res.Add("ERROR-WS");
                    result.List_res.Add("");
                    result.List_res.Add("Opción no válida.->" + _option + "<-");
                    goto case "6";
            }
        }

        private static void Autenticacion(string _url, string _user, string _password)
        {
            Console.WriteLine("Autenticación");
            AuthResponse token = new SW.Services.Authentication.Authentication(_url, _user, _password).GetToken();
            if (token.status == "success")
            {
                Console.WriteLine("Se ha autenticado de manera correcta");
                Console.WriteLine("Token:" + token.data.token + "\n");
            }
            else
            {
                Console.WriteLine("Error en la autenticación");
                Console.WriteLine(token.message + " : " + token.messageDetail + "\n");
            }
        }

        private static void TimbrarV1(string xmlInfo, string _url, string _user, string _password)
        {
            Console.WriteLine("Timbrado V1");
            SW.Services.Stamp.Stamp stamp = new SW.Services.Stamp.Stamp(_url, _user, _password);
            StampResponseV1 stampResponseV1;
            try
            {
                Convert.FromBase64String(xmlInfo);
                stampResponseV1 = stamp.TimbrarV1(xmlInfo, true);
            }
            catch
            {
                stampResponseV1 = stamp.TimbrarV1(xmlInfo);
            }
            if (stampResponseV1.status == "success")
            {
                Console.WriteLine("Respuesta del Timbrado\n\n");
                Console.WriteLine("CFDI+TFD:" + stampResponseV1.data.tfd + "\n");
            }
            else
            {
                Console.WriteLine("Error al timbrar\n\n");
                Console.WriteLine(stampResponseV1.message + " : " + stampResponseV1.messageDetail + "\n");
            }
        }

        private static void TimbrarV2(string xmlInfo, string _url, string _user, string _password)
        {
            Console.WriteLine("Timbrado V2");
            SW.Services.Stamp.Stamp stamp = new SW.Services.Stamp.Stamp(_url, _user, _password);
            StampResponseV2 stampResponseV2;
            try
            {
                Convert.FromBase64String(xmlInfo);
                stampResponseV2 = stamp.TimbrarV2(xmlInfo, true);
            }
            catch
            {
                stampResponseV2 = stamp.TimbrarV2(xmlInfo);
            }
            if (stampResponseV2.status == "success")
            {
                Console.WriteLine("Respuesta del Timbrado\n\n");
                Console.WriteLine("TFD:" + stampResponseV2.data.tfd + "\n");
                Console.WriteLine("CFDI:" + stampResponseV2.data.cfdi + "\n");
            }
            else
            {
                Console.WriteLine("Error al timbrar\n\n");
                Console.WriteLine(stampResponseV2.message + " : " + stampResponseV2.messageDetail + "\n");
            }
        }

        private static void TimbrarV3(string xmlInfo, string _url, string _user, string _password)
        {
            SW.Services.Stamp.Stamp stamp = new SW.Services.Stamp.Stamp(_url, _user, _password);
            StampResponseV3 stampResponseV3;
            try
            {
                Convert.FromBase64String(xmlInfo);
                stampResponseV3 = stamp.TimbrarV3(xmlInfo, true);
            }
            catch
            {
                stampResponseV3 = stamp.TimbrarV3(xmlInfo);
            }
            if (stampResponseV3.status == "success")
            {
                Console.WriteLine("TIMBRADO");
                Console.WriteLine("CFDI:" + stampResponseV3.data.cfdi + "\n");
            }
            else
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(stampResponseV3.message + " : " + stampResponseV3.messageDetail + "\n");
            }
        }

        private static RESULT TimbrarV4(
          string xmlInfo,
          string _url,
          string _user,
          string _password)
        {
            RESULT result = new RESULT();
            SW.Services.Stamp.Stamp stamp = new SW.Services.Stamp.Stamp(_url, _user, _password);
            List<string> stringList = new List<string>();
            StampResponseV4 stampResponseV4;
            try
            {
                Convert.FromBase64String(xmlInfo);
                stampResponseV4 = stamp.TimbrarV4(xmlInfo, true);
            }
            catch
            {
                stampResponseV4 = stamp.TimbrarV4(xmlInfo);
            }
            if (stampResponseV4.status == "success")
            {
                string str1 = "TIMBRADO";
                stringList.Add(str1);
                stringList.Add(stampResponseV4.data.uuid);
                string str2 = "Fecha de Timbrado:" + stampResponseV4.data.fechaTimbrado;
                stringList.Add(str2);
                string str3 = "Número de Certificado CFDI:" + stampResponseV4.data.noCertificadoCFDI;
                stringList.Add(str3);
                string str4 = "Número de Certificado SAT:" + stampResponseV4.data.noCertificadoSAT;
                stringList.Add(str4);
                result.Xml = stampResponseV4.data.cfdi;
            }
            else
            {
                string str5 = "INCIDENCIA";
                stringList.Add(str5);
                if (stampResponseV4.message == "307. El comprobante contiene un timbre previo.")
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    try
                    {
                        xmlDocument.InnerXml = stampResponseV4.messageDetail;
                        result.Xml = xmlDocument.InnerXml;
                    }
                    catch (Exception ex)
                    {
                        string str6 = "";
                        stringList.Add(str6);
                        string message = stampResponseV4.message;
                        stringList.Add(message);
                        string messageDetail = stampResponseV4.messageDetail;
                        stringList.Add(messageDetail);
                        result.List_res = stringList;
                        return result;
                    }
                    if (xmlDocument.GetElementsByTagName("cfdi:Complemento")[0] != null)
                    {
                        XmlNode xmlNode = xmlDocument.GetElementsByTagName("tfd:TimbreFiscalDigital")[0];
                        try
                        {
                            string str7 = xmlNode.Attributes["UUID"].Value;
                            stringList.Add(str7);
                        }
                        catch (Exception ex)
                        {
                            string str8 = "UUID_ERROR_GET";
                            stringList.Add(str8);
                        }
                        string message = stampResponseV4.message;
                        stringList.Add(message);
                    }
                }
                else
                {
                    string str9 = "";
                    stringList.Add(str9);
                    string str10 = stampResponseV4.message + " : " + stampResponseV4.messageDetail;
                    stringList.Add(str10);
                }
            }
            result.List_res = stringList;
            return result;
        }

        private static RESULT CancelarCSD(
          string _url,
          string _user,
          string _password,
          string uuid,
          string rfc,
          string passKey,
          string name_key,
          string name_cer,
          string motivo, string folioSustitucion = null)//ADD SF RSG 01.06.2022)
        {
            List<string> stringList = new List<string>();
            RESULT result = new RESULT();
            byte[] inArray1 = new byte[0];
            byte[] inArray2 = new byte[0];
            try
            {
                inArray1 = File.ReadAllBytes(Path.Combine("C:\\inetpub\\wwwroot\\WSSMART\\CER_KEY", name_key));
                inArray2 = File.ReadAllBytes(Path.Combine("C:\\inetpub\\wwwroot\\WSSMART\\CER_KEY", name_cer));
            }
            catch (Exception ex)
            {
                stringList.Add("ERROR-CANCELAR");
                stringList.Add(ex.Message.ToString());
            }
            string base64String1 = Convert.ToBase64String(inArray1);
            string base64String2 = Convert.ToBase64String(inArray2);
            string password = passKey;
            CancelationResponse cancelationResponse = new SW.Services.Cancelation.Cancelation(_url, _user, _password).CancelarByCSD(base64String2, base64String1, rfc, password, uuid, motivo, folioSustitucion);
            if (cancelationResponse.status == "success" && cancelationResponse.data != null)
            {
                stringList.Add("CANCELADO");
                foreach (KeyValuePair<string, string> keyValuePair in cancelationResponse.data.uuid)
                {
                    stringList.Add(keyValuePair.Value);
                    stringList.Add(keyValuePair.Value);
                }
            }
            else
            {
                stringList.Add("ERROR-CANCELAR");
                stringList.Add(cancelationResponse.message);
                stringList.Add(cancelationResponse.messageDetail);
            }
            result.List_res = stringList;
            return result;
        }

        private static string GetXmlFile(string path)
        {
            bool flag = false;
            string str = "1";
            if (!(str == "1"))
            {
                if (str == "2")
                    flag = true;
            }
            else
                flag = false;
            string empty = string.Empty;
            try
            {
                string s = File.ReadAllText(path, Encoding.UTF8);
                return flag ? Convert.ToBase64String(Encoding.UTF8.GetBytes(s)) : s;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Error al obtener la información del xml: " + ex.Message);
                throw;
            }
        }
    }
}
