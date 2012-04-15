using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SystemEx
{
    public class XmlDataNodeAttributes
    {
        private XmlNode _node;

        internal XmlDataNodeAttributes(XmlNode node)
        {
            _node = node;
        }

        public string this[string key]
        {
            get
            {
                var attribute = _node.Attributes[key];

                if (attribute == null)
                {
                    throw new ArgumentException("XML key not found");
                }

                return attribute.Value;
            }
        }
    }
}
