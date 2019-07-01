using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLMaster
{
    class Application
    {
        public string ApplicationID { get; set; }
        public int SequenceNumber { get; set; }
        public XmlDocument Xml { get; set; }
        public string SchemaError { get; set; }
        public Application(string applicationID, int sequenceNumber)
        {
            ApplicationID = applicationID;
            SequenceNumber = sequenceNumber;
            Xml = new XmlDocument();
            SchemaError = null;
        }
    }
}
