using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace DAPRO
{
    public static class XMLTools
    {
        public static void CreateStoreProcedureInfoXml(string SOURCE,string TIME)
        {
            try
            {
                FileStream writer = null;
                if (File.Exists(Application.StartupPath + "\\StoreProcedureInfo.xml"))
                {
                    File.Delete(Application.StartupPath + "\\StoreProcedureInfo.xml");
                    writer = new FileStream(Application.StartupPath + "\\StoreProcedureInfo.xml", FileMode.Create);
                }
                else
                {
                    writer = new FileStream(Application.StartupPath + "\\StoreProcedureInfo.xml", FileMode.Create);

                }
                //else
                //{
                //    writer = new FileStream(Application.StartupPath + "\\StoreProcedureInfo.xml", FileMode.Open);
                //}

                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    xmlWriter.WriteStartElement("StoreProcedureInfo");//root tag
                    xmlWriter.WriteElementString("BACKUPSOURCE", SOURCE);//child tag
                    xmlWriter.WriteElementString("BACKUPTIME", TIME);

                    xmlWriter.WriteEndDocument();

                    writer.Flush();
                }
            }
            catch (Exception eeeee)
            {
                MessageBox.Show(eeeee.Message);
            }
        }

        public static string ReadXmlElementString(string pathWithFileName, string tagName)
        {
            string text = string.Empty;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pathWithFileName);

                XmlNodeList ErrorIdTags = doc.GetElementsByTagName(tagName);
                if (ErrorIdTags.Count >= 1)
                {
                    text = ErrorIdTags[0].InnerText;
                }
            }
            catch (Exception)
            {
            }
            return text;
        }
    }
}
