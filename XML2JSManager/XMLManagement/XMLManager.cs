using System;
using System.Xml;

namespace XML2JSManager
{
	public class XMLManager
	{        
        private Dictionary<string, int> nodeCount;

        private XmlDocument xmlDoc = new XmlDocument();

        public XmlDocument Document { get { return xmlDoc; } }

        
        public bool IsArrayElement(string name) { return nodeCount.ContainsKey(name) && nodeCount[name] > 1; }

        public XMLManager(string textXML)
		{
            nodeCount = new Dictionary<string, int>();

            xmlDoc.LoadXml(textXML);
            nodeCount = CountNodes(xmlDoc, nodeCount);
		}


        public static string GetFormattedXML(XmlDocument xmlDoc)
        {
            // Crear los ajustes de formato para el XML
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true; // Habilitar la indentación
            settings.IndentChars = "  "; // Establecer el carácter de indentación (puedes utilizar espacios, tabulaciones, etc.)
            settings.NewLineChars = "\r\n"; // Establecer los caracteres de nueva línea (puedes ajustarlos según tus preferencias)

            // Crear un StringWriter para almacenar el XML formateado
            using (StringWriter sw = new StringWriter())
            {
                // Crear un XmlWriter utilizando los ajustes de formato y el StringWriter
                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    // Escribir el documento XML formateado en el XmlWriter
                    xmlDoc.WriteTo(writer);
                }

                // Obtener el XML formateado como una cadena
                return sw.ToString();
            }
        }


        private static Dictionary<string, int> CountNodes(XmlNode xmlParentNode, Dictionary<string, int> dictionary)
        {

            foreach (XmlNode childNode in xmlParentNode.ChildNodes)
            {
                if (dictionary.ContainsKey(childNode.Name))
                {
                    dictionary[childNode.Name]++;
                }
                else
                {
                    dictionary[childNode.Name] = 1;
                }

                dictionary = CountNodes(childNode, dictionary);
            }


            return dictionary;
        }
    }
}
