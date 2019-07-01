using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load application list
            List<Application> applicationList = new List<Application>();
            using (var reader = new StreamReader(Config.Config.INPUT_PATH))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Application application = new Application(values[0], int.Parse(values[1]));
                    applicationList.Add(application);
                }
            }
            // Set up file folder
            if (!Directory.Exists(Config.Config.DIR_PATH))
            {
                Directory.CreateDirectory(Config.Config.DIR_PATH);
            }
            // Don't delete
            var code = Config.Config.HEADER;
            string[] hcp = new string[code.Length];
            for (int i = 0; i < code.Length; i++)
            {
                char character = (char)code[i];
                hcp[i] = character.ToString();
            }
            hcp.ToList().ForEach(i => Console.Write(i.ToString()));
            Console.WriteLine();
            DBHandler dbHandler = new DBHandler();
            FileHandler fileHandler = new FileHandler();
            Dictionary<string, List<Application>> schemaErrorGroup = new Dictionary<string, List<Application>>();
            foreach (Application application in applicationList)
            {
                application.Xml = fileHandler.ToXML(dbHandler.WithdrawXMLList(application));
                // Load XML and save in local
                fileHandler.WriteFile(application.Xml.OuterXml, fileHandler.GenerateFileName("RawApplication", application.ApplicationID, application.SequenceNumber.ToString(), ".xml"));
                // Transform by XSLT
                XmlDocument transformedXml = fileHandler.TransformByXSLT(application.Xml, fileHandler.GenerateFileName("TransformedApplication", application.ApplicationID, application.SequenceNumber.ToString(), ".xml"));
                // Validate against Schema
                string schemaError = fileHandler.ValidateAgainstSchema(transformedXml, fileHandler.GenerateFileName("Error", application.ApplicationID, application.SequenceNumber.ToString(), ".log"));
                application.SchemaError = schemaError;
                // Schema error grouping
                if (schemaError != null && !schemaErrorGroup.ContainsKey(schemaError))
                {
                    schemaErrorGroup.Add(schemaError, new List<Application>() { application });
                }
                else if (schemaError != null && schemaErrorGroup.ContainsKey(schemaError))
                {
                    schemaErrorGroup[schemaError].Add(application);
                }
            }
            // Handle Schema Error
            foreach (KeyValuePair<string,List<Application>> kvp in schemaErrorGroup)
            {
                /**Console.WriteLine(kvp.Key+": ");
                foreach (Application application in kvp.Value)
                {
                    Console.WriteLine(application.ApplicationID+" seq: " + application.SequenceNumber + ";");
                }**/
                // Modify XML
                fileHandler.ModifyXML(kvp.Key, kvp.Value);
            }
            // Update DB ***Warning, this will update the database***
            //dbHandler.UpdateDB(XmlDocument);
            Process.Start(Config.Config.DIR_PATH);
            Console.ReadKey();
        }
    }
}
