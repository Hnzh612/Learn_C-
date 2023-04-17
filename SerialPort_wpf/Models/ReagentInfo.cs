using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerialPort_wpf.Models
{
    /// <summary>
    /// 试剂信息
    /// </summary>
    public class ReagentInfo
    {
        /// <summary>
        /// 试剂的名字
        /// </summary>
        [XmlAttribute(nameof(Name))]
        public string Name { get; set; }
        public bool IsUsable { get; set; }
        /// <summary>
        /// 试剂存放的位置
        /// </summary>
        //[XmlAttribute(nameof(IsUsable))]
        public XYZDPoint CurrentLocation { get; set; }
        public PipletteGunConfig PipletteGunConfig { get; set; } = new PipletteGunConfig();
    }
}
