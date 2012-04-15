using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace SystemEx
{
    public class XmlDataNodeEnumerator : IEnumerator<XmlDataNode>
    {
        private XmlNode _node;
        private XmlNode _current = null;

        internal XmlDataNodeEnumerator(XmlNode node)
        {
            _node = node;
        }

        public XmlDataNode Current
        {
            get { return new XmlDataNode(_current); }
        }

        public void Dispose()
        {
            // Nothing to do
        }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        public bool MoveNext()
        {
            if (_current == null)
            {
                _current = _node.FirstChild;
            }
            else
            {
                _current = _current.NextSibling;
            }

            while (_current != null && _current.NodeType != XmlNodeType.Element)
            {
                _current = _current.NextSibling;
            }

            return (_current != null);
        }

        public void Reset()
        {
            _current = null;
        }
    }
}
