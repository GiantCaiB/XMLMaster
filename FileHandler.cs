using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace XMLMaster
{
    class FileHandler
    {
        Dictionary<string, List<Application>> ErrorGroup { get; set; }
        public FileHandler()
        {
            ErrorGroup = new Dictionary<string, List<Application>>();
        }
        public XmlDocument ToXML(string xmlString)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            return xmlDoc;
        }
        public XmlDocument TransformByXSLT(XmlDocument xmlDoc, string path)
        {
            XmlDocument transformedXml = new XmlDocument();
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(Config.Config.XSLT_PATH);
            XmlTextWriter writer = new XmlTextWriter(path, null);
            writer.Formatting = Formatting.Indented;
            xslt.Transform(xmlDoc, null, writer, null);
            writer.Close();
            transformedXml.Load(path);
            return transformedXml;
        }
        public string ValidateAgainstSchema(XmlDocument xmlDoc,string path)
        {
            DIXmlFunctions.XmlSchemaValidator validator = new DIXmlFunctions.XmlSchemaValidator();
            if (!validator.fnValidate(xmlDoc.OuterXml, Config.Config.SCHEMA_PATH))
            {
                var error = validator.fnGetErrorMessage();
                //Console.WriteLine("Schema error:" + HandleError(error));
                //WriteFile(error.ToString(), path);
                return HandleError(error);
            }
            else
            {
                //Console.WriteLine("No schema error.");
                return null;
            }
        }
        public List<Application> ModifyXML(string schemaError, List<Application> applicationList)
        {
            XmlDocument modifiedXml = new XmlDocument();
            foreach (Application application in applicationList)
            {
                modifiedXml.LoadXml(application.Xml.OuterXml);
                // TODO
                switch (schemaError)
                {
                    case "The element 'loan_product' has invalid child element 'no_interest_rate'. List of possible elements expected: 'cd_group'.":
                        
                        break;
                    case "The 'cd_type' attribute is not declared.":

                        break;
                    default:
                        break;
                }
                application.Xml = modifiedXml;
            }
            return applicationList;
        }
        public void WriteFile(string content, string path)
        {
            System.IO.File.WriteAllText(path, content);
        }
        public string GenerateFileName(string name, string applicationID,string sequenceNumber, string type)
        {
            return Config.Config.DIR_PATH + @"\" + name + "_" + applicationID + "_seq"+ sequenceNumber + type;
        }
        private string HandleError(string error)
        {
            return error.Split(new string[] { " - " }, StringSplitOptions.None)[0];
        }
    }
}
