using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Patrones_Estructurales.Adapterr.FormatoJsonXML
{
    public class AdaptadorXmlJson : IConvertidorFormato
    {
        private ArchivoXml ArchivoXml;

        public AdaptadorXmlJson(ArchivoXml archivoXml)
        {
            ArchivoXml = archivoXml;
        }

        public string Convertir()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ArchivoXml.GenerarXml());

            return JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
