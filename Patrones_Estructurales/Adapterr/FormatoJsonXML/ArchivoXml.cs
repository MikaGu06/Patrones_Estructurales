using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Patrones_Estructurales.Adapterr.FormatoJsonXML
{
    public class ArchivoXml
    {
        private List<ProductoApi> Productos;

        public ArchivoXml(List<ProductoApi> productos)
        {
            Productos = productos;
        }

        public string GenerarXml()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement raiz = doc.CreateElement("productos");

            foreach (ProductoApi item in Productos)
            {
                XmlElement producto = doc.CreateElement("producto");

                XmlElement nombre = doc.CreateElement("nombre");
                nombre.InnerText = item.nombre;

                XmlElement precio = doc.CreateElement("precio");
                precio.InnerText = item.precio;

                XmlElement stock = doc.CreateElement("stock");
                stock.InnerText = item.stock;

                producto.AppendChild(nombre);
                producto.AppendChild(precio);
                producto.AppendChild(stock);

                raiz.AppendChild(producto);
            }

            doc.AppendChild(raiz);

            StringBuilder sb = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  "
            };

            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }

            return sb.ToString();
        }
    }
}
