using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace SystemEx
{
    public class XmlDataNode : IEnumerable<XmlDataNode>
    {
        private XmlNode _node;
        private XmlDataNodeAttributes _attributes = null;

        public XmlDataNode(XmlNode node)
        {
            _node = node;
        }

        public IEnumerator<XmlDataNode> GetEnumerator()
        {
            return new XmlDataNodeEnumerator(_node);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public XmlDataNode this[string key]
        {
            get
            {
                foreach (var node in this)
                {
                    if (node.Name == key)
                    {
                        return node;
                    }
                }

                throw new ArgumentException("XML key not found");
            }
        }

        public string Name
        {
            get { return _node.Name; }
        }

        public string Value
        {
            get { return _node.InnerText.Trim(); }
        }

        public XmlDataNodeAttributes Attributes
        {
            get
            {
                if (_attributes == null)
                {
                    _attributes = new XmlDataNodeAttributes(_node);
                }

                return _attributes;
            }
        }

        public static implicit operator string(XmlDataNode node)
        {
            return node.Value;
        }
    }
}
