using SerialPort_wpf.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Log;

namespace SerialPort_wpf.Tools
{
    public static class XMLRW
    {
        public static object Deserialize<T>(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException(" filePath IsNullOrEmpty.");
            }
            XMLStepMap xsm = new XMLStepMap();
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (var myFileStream = new FileStream(filePath, FileMode.Open))
            {
                return mySerializer.Deserialize(myFileStream);
            }
        }
        public static bool Serializer<T>(T _t, string _filePath = "MSProcedure.xml")
        {
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(T));
                using (var _streamWriter = new StreamWriter(_filePath))
                {
                    mySerializer.Serialize(_streamWriter, _t);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogCore.CreateInstance().AsyncLog(ex);
                return false;
            }
        }
        public static bool SaveObject(string filePath, object _obj)
        {
            XElement x = new XElement(_obj.GetType().Name);
            x = GetXElement(_obj);
            x.Save(filePath);
            return true;
        }
        public static object ReadObject<T>(string filePath)
        {
            XElement x = XElement.Load(filePath);
            return GetObject(typeof(T), x);
        }
        public static XElement GetXElement(object _t)
        {
            if (_t == null)
            {
                return null;
            }
            XElement _newElement = new XElement(_t.GetType().Name);
            foreach (PropertyInfo p in _t.GetType().GetProperties())
            {
                XElement _child = new XElement(p.Name);
                var _obj = p.GetValue(_t);
                switch (p.PropertyType)
                {
                    case var t when t == typeof(string): _newElement.SetAttributeValue(p.Name, _obj == null ? "" : _obj); break;
                    case var t when t == typeof(bool): _newElement.SetAttributeValue(p.Name, _obj == null ? "" : _obj); break;
                    case var t when t == typeof(int): _newElement.SetAttributeValue(p.Name, _obj == null ? "" : _obj); break;
                    case var t when t == typeof(double): _newElement.SetAttributeValue(p.Name, _obj == null ? "" : _obj); break;
                    case var t when t == typeof(float): _newElement.SetAttributeValue(p.Name, _obj == null ? "" : _obj); break;
                    case var t when t == typeof(DateTime):
                        {
                            _newElement.SetAttributeValue(p.Name, _obj == null && ((DateTime)_obj) == DateTime.MinValue ? "" : _obj);
                        }
                        break;
                    case var t when t == typeof(byte): _newElement.SetAttributeValue(p.Name, _obj == null ? "" : _obj); break;
                    case var t when t == typeof(char): _newElement.SetAttributeValue(p.Name, _obj == null ? "" : _obj); break;
                    case var t when t.IsClass:
                        {
                            XElement chil = GetXElement(_obj);
                            chil.SetAttributeValue("Name", p.Name);
                            _newElement.Add(chil);
                        }
                        break;
                    default: break;
                }
            }
            return _newElement;
        }
        public static object GetObject(Type type, XElement element)
        {
            if (element == null || type.Name != element.Name)
            {
                return null;
            }
            var _t = Activator.CreateInstance(type);
            foreach (XAttribute ar in element.Attributes())
            {
                PropertyInfo pro = type.GetProperty(ar.Name.ToString());
                if (pro != null && !string.IsNullOrEmpty(ar.Value))
                {
                    switch (pro.PropertyType)
                    {
                        case var t when t == typeof(string): pro.SetValue(_t, ar.Value); break;
                        case var t when t == typeof(bool): pro.SetValue(_t, Convert.ToBoolean(ar.Value)); break;
                        case var t when t == typeof(int): pro.SetValue(_t, Convert.ToInt32(ar.Value)); break;
                        case var t when t == typeof(double): pro.SetValue(_t, Convert.ToDouble(ar.Value)); break;
                        case var t when t == typeof(float): pro.SetValue(_t, Convert.ToSingle(ar.Value)); break;
                        case var t when t == typeof(DateTime): pro.SetValue(_t, Convert.ToDateTime(ar.Value)); break;
                        case var t when t == typeof(byte): throw new Exception("未实现 需要按位Char转换");
                        case var t when t == typeof(char):
                            {
                                if (ar.Value.Length != 1)
                                {
                                    throw new ArgumentException($"{ar.Value} 无法转换成Char类型.");
                                }
                            }
                            break;
                        default: break;
                    }
                }
            }
            foreach (XElement ele in element.Elements())
            {
                string TEMP = ele.Attribute("Name").Value;
                if (!String.IsNullOrEmpty(TEMP))
                {
                    PropertyInfo child = type.GetProperty(TEMP);
                    if (child != null)
                    {
                        var _c = GetObject(child.PropertyType, ele);
                        child.SetValue(_t, _c);
                    }
                }
            }
            return _t;
        }
        static string GetAttributeValue(XAttribute ar)
        {
            if (ar != null && !string.IsNullOrEmpty(ar.Value))
            {
                return ar.Value;
            }
            return null;
        }
        public static bool SetProcedureInstance(string xmlPath = "MSProcedure.xml")
        {
            XElement x = XElement.Load(xmlPath);
            var _t = Procedure.CreateInstance();
            _t.Name = GetAttributeValue(x.Attribute("Name")) ?? "";
            _t.Describle = GetAttributeValue(x.Attribute("Description")) ?? "";
            if (!string.IsNullOrEmpty(GetAttributeValue(x.Attribute("RunningCount"))))
            {
                _t.SetRunningCount(Convert.ToInt32(GetAttributeValue(x.Attribute("RunningCount"))));
            }
            XElement ele = x.Elements()?.First();
            while (ele != null)
            {
                if (_t.HeadModule == null)
                {
                    switch (GetAttributeValue(x.Attribute("CurrentTypeEnum")))
                    {
                        case "Empty": break;
                        case "ClawModule": ClawModule claw = new ClawModule(); _t.HeadModule.NextModule = claw; break;
                        case "MixingVibrationModule": break;
                        case "PipetteGunModule": break;
                        case "ReagentModule": break;
                        case "SimpleModule": break;
                        case "SleepModule": break;
                        case "StepperMotorModule": break;
                        default: break;
                    }
                }
                ele = ele.Elements()?.First();
            }
            return true;
        }
        public static bool SaveProcedureInstance(string xmlPath = "MSProcedure.xml")
        {
            XElement x = XElement.Load(xmlPath);
            var _t = Procedure.CreateInstance();
            _t.Name = GetAttributeValue(x.Attribute("Name")) ?? "";
            _t.Describle = GetAttributeValue(x.Attribute("Description")) ?? "";
            if (!string.IsNullOrEmpty(GetAttributeValue(x.Attribute("RunningCount"))))
            {
                _t.SetRunningCount(Convert.ToInt32(GetAttributeValue(x.Attribute("RunningCount"))));
            }
            XElement ele = x.Elements()?.First();
            while (ele != null)
            {
                if (_t.HeadModule == null)
                {
                    switch (GetAttributeValue(x.Attribute("CurrentTypeEnum")))
                    {
                        case "Empty": break;
                        case "ClawModule": ClawModule claw = new ClawModule(); _t.HeadModule.FrontModule = claw; break;
                        case "MixingVibrationModule": break;
                        case "PipetteGunModule": break;
                        case "ReagentModule": break;
                        case "SimpleModule": break;
                        case "SleepModule": break;
                        case "StepperMotorModule": break;
                        default: break;
                    }
                }
                ele = ele.Elements()?.First();
            }
            return true;
        }
    }
}
