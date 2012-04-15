using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SystemEx
{
    public class XmlData : XmlDataNode
    {
        public XmlData(string filename)
            : base(LoadRootNode(filename))
        {
        }

        private static XmlNode LoadRootNode(string filename)
        {
            var doc = new XmlDocument();

            doc.Load(filename);

            XmlNode root = null;

            foreach (XmlNode node in doc.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    root = node;
                    break;
                }
            }

            if (root == null)
            {
                throw new Exception("Invalid configuration document");
            }

            return root;
        }
    }
}
