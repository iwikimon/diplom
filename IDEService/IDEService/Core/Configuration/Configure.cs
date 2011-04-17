using System;
using System.Xml;
using System.Xml.Linq;

namespace IDEService.Core
{
    public class Configure
    {
        #region Singleton

        private static volatile Configure _cfg;
        
        private static  object _cfgLocker = new object();

        public static Configure Cfg
        {
            get
            {
                if(_cfg == null)
                    lock(_cfgLocker)
                        if (_cfg == null)
                            _cfg = new Configure();
                return _cfg;
            }
        }

        #endregion

        private XDocument _xdoc;

        private const string partition = "configuration";
        private const string _cfgFileName = "settings.xml";

        private Configure()
        {
            _xdoc = XDocument.Load(_cfgFileName);
        }

        public void Write<T>(string sectName, string valName, string attrName, T value) where T:IConvertible
        {
            if (_xdoc.Document == null) return;
            string val;
            try
            {

                var m = typeof(XmlConvert).GetMethod("ToString", new[] { typeof(T) });
                val = (string)m.Invoke(null, new object[] { value });
            }
            catch
            {
                val = value.ToString();
            }
            var sect = getSection(sectName);
            var elem = getChild(sect, valName);
            var attr = getAttr(elem, attrName);
            attr.Value = val;
            _xdoc.Save(_cfgFileName);
        }
        
        public T Read<T>(string sectName, string valName, string attrName)  where T: IConvertible
        {
            if (_xdoc.Document == null)
                throw new Exception("Ошибка при чтении конфиг-файла");
            var sect = getSection(sectName);
            var elem = getChild(sect, valName);
            var attr = getAttr(elem, attrName);
            return (T) Convert.ChangeType(attr.Value, typeof(T));
        }

        private XElement getSection(string sectName)
        {
            var parts = _xdoc.Document.Element(partition);
            if (parts == null)
            {
                parts = new XElement(partition);
                if (_xdoc.Root != null) _xdoc.Root.Add(parts);
            }
            var sect = parts.Element(sectName);
            if (sect == null)
            {
                sect = new XElement(sectName);
                parts.Add(sect);
            }
            return sect;
        }

        private XElement getChild(XElement parent,string name)
        {
            var elem = parent.Element(name);
            if(elem == null)
            {
                elem = new XElement(name);
                parent.Add(elem);
            }
            return elem;
        }

        private XAttribute getAttr(XElement elem, string attrName)
        {
            var attr = elem.Attribute(attrName);
            if(attr == null)
            {
                attr = new XAttribute(attrName,new object());
                elem.Add(attr);
            }
            return attr;
        }
    }
}
