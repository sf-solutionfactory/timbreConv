using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZWSSMART4
{
    [DataContract]
    public class RESULT
    {
        private string xml = "";
        private List<string> list_res = new List<string>();

        [DataMember]
        public string Xml
        {
            get => this.xml;
            set => this.xml = value;
        }

        [DataMember]
        public List<string> List_res
        {
            get => this.list_res;
            set => this.list_res = value;
        }
    }
}