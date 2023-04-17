using SerialPort_wpf.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerialPort_wpf.Models
{
    /// <summary>
    /// 样品信息
    /// </summary>
    public class SimpleInfo
    {
        public SimpleInfo(XYZDPoint _location)
        {
            CurrentLocation = _location;
        }
        /// <summary>
        /// XmlSerial:
        ///             可以序列化的属性
        ///                             属性
        ///                             字段
        ///                             数组
        ///                             链表
        ///                             https://learn.microsoft.com/zh-cn/dotnet/api/system.xml.serialization.xmlserializer?view=net-6.0
        ///                             但是如果没有默认构造函数，则会造成 无法序列化失败的情况.
        /// </summary>
        public SimpleInfo()
        {

        }
        /// <summary>
        /// 样品的名字
        /// </summary>
        [XmlAttribute(nameof(Name))]
        public string Name { get; set; }
        /// <summary>
        /// 当前样品是否可用(不扫码的样品默认无效)
        /// </summary>
        [XmlAttribute(nameof(IsUsable))]
        public bool IsUsable { get; set; }
        /// <summary>
        /// 样品所在矩阵的位置[0,95]
        /// </summary>
        [XmlAttribute(nameof(Index))]
        public int Index { get; set; }
        /// <summary>
        ///样品原始位置
        /// </summary>
       // [XmlAttribute(nameof(CurrentLocation))]
        public XYZDPoint CurrentLocation { get; set; }
        public PipletteGunConfig PipletteGunConfig { get; set; } = new PipletteGunConfig();
        public Simpletype Simpletyle { get; set; } = Simpletype.Normal;
        /// <summary>
        /// 已经添加的试剂列表
        /// </summary>
        public ReagentInfo[] ReagentInfos { get; set; } = new ReagentInfo[20];
    }
}
