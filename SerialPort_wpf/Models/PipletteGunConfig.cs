using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Models
{
    /// <summary>
    /// 上海
    /// 2022/11/12
    /// Dony 
    /// 移液枪的配置文件 ，来告诉或者说明：
    ///                                 移液枪 在工作的时候 
    ///                                                     1：从哪里取TIP
    ///                                                     2.从哪里吸液 以及吸液时候的参数
    ///                                                     3.液的目的地 以及吐液时候的参数
    ///                                                     
    /// </summary>
    public class PipletteGunConfig
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 从哪里取液
        /// </summary>
        public XYZDPoint SourceZDYPoint { get; set; }
        /// <summary>
        /// 取液后放在哪里
        /// </summary>
        public XYZDPoint TargetZDYPoint { get; set; }
        /// <summary>
        /// 从哪里取枪头
        /// </summary>
        public XYZDPoint TIPZDYPoint { get; set; }
        /// <summary>
        ///  吸液体积(μl)[0, 1000]
        /// </summary>
        public int? ImbibitionVolume { get; set; }
        /// <summary>
        /// 吸液速度(μl/s)[0, 2000]
        /// </summary>
        public int? ImbibitionSpeed { get; set; }
        /// <summary>
        /// 截留速度(ul/s)[0, 2000]
        /// </summary>
        public int? InterceptionSpeed { get; set; }
        /// <summary>
        /// 排液体积(μl)[0,100000]
        /// </summary>
        public int? DrainVolume { get; set; }
        /// <summary>
        /// 回吸体积0.01ul[0,10000]
        /// </summary>
        public int? ResorptionVolume { get; set; }
        /// <summary>
        /// 排液速度(μl/s)[0,20000]
        /// </summary>
        public int? DrainSpeed { get; set; }
        /// <summary>
        /// 停止速度(ul/s)[0,2000]
        /// </summary>
        public int? StopSpeed { get; set; }
        /// <summary>
        /// 系数
        /// </summary>
        public float? Coefficient { get; set; }

    }
}
