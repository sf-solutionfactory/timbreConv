using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ZWSSMART4
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código y en el archivo de configuración a la vez.
    public class ServiceTimbra4 : IServiceTimbra4
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public RESULT WsSmart(
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
            return new CONSUM_WS4().consum_ws4(_user, _password, _syst, _option, xml_in, uuid, rfc, passKey, name_key, name_cer, motivo, folioSustitucion);
        }
    }
}
