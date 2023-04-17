using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_prictices
{
    /// <summary>
    /// Dongyu_Zhang
    /// 原则上非整体的物件应该也要作为非整体处理，
    /// 以尽量减少可能存在的误差
    /// </summary>
    public class XMLStepMap
    {
        /// <summary>
        /// 初定简单版本号
        /// </summary>
        public int Version { get; set; } = 5;
        /// <summary>
        /// 96样品区的坐标4
        /// </summary>
        public StepLine SimpleArea4;
        /// <summary>
        /// 96样品区的坐标3
        /// </summary>
        public StepLine SimpleArea3;
        /// <summary>
        /// 96样品区的坐标2
        /// </summary>
        public StepLine SimpleArea2;
        /// <summary>
        /// 96样品区的坐标1
        /// </summary>
        public StepLine SimpleArea1;
        /// <summary>
        /// 废液区1
        /// </summary>
        public XYZDPoint Waste1;
        /// <summary>
        /// 废液区2
        /// </summary>
        public XYZDPoint Waste2;
        /// <summary>
        /// 枪头区坐标4
        /// </summary>
        public StepRect TIPArea4;
        /// <summary>
        /// 枪头区坐标3
        /// </summary>
        public StepRect TIPArea3;
        /// <summary>
        /// 枪头区坐标2
        /// </summary>
        public StepRect TIPArea2;
        /// <summary>
        /// 枪头区坐标1
        /// </summary>
        public StepRect TIPArea1;
        /// <summary>
        /// 试剂区1(右上角圆)
        /// </summary>
        public XYZDPoint ReagentCircleTopRight;
        /// <summary>
        /// 试剂区2（中间最左边圆）
        /// </summary>
        public XYZDPoint ReagentCircleLeftCenter;
        /// <summary>
        /// 试剂区3（右下角圆）
        /// </summary>
        public XYZDPoint ReagentCircleBottomRight;
        /// <summary>
        /// 混匀、震荡区坐标
        /// </summary>
        public StepRect MixingVibrationArea;
        /// <summary>
        /// 混匀、震荡区坐标(抓钩和xy电机的坐标系不是一个)
        /// </summary>
        public XYZDPoint MixingVibrationAreaCenter;
        /// <summary>
        /// 氮吹
        /// </summary>
        public XYZDPoint NitrogenBlowingArea;
        /// <summary>
        /// 存样区坐标1
        /// </summary>
        public XYZDPoint StoreArea1;
        /// <summary>
        /// 存样区坐标2
        /// </summary>
        public XYZDPoint StoreArea2;
        /// <summary>
        /// 存样区坐标3
        /// </summary>
        public XYZDPoint StoreArea3;
        /// <summary>
        /// 存样区坐标4
        /// </summary>
        public XYZDPoint StoreArea4;
        /// <summary>
        /// 存样区坐标5
        /// </summary>
        public XYZDPoint StoreArea5;
        /// <summary>
        /// 存样区坐标6
        /// </summary>
        public XYZDPoint StoreArea6;
        /// <summary>
        /// 存样区坐标7
        /// </summary>
        public XYZDPoint StoreArea7;
        /// <summary>
        /// 存样区坐标8
        /// </summary>
        public XYZDPoint StoreArea8;
        /// <summary>
        /// 存样区坐标9(坐标9是正压)
        /// </summary>
        public XYZDPoint StoreArea9;
        /// <summary>
        /// 存样区坐标10
        /// </summary>
        public XYZDPoint StoreArea10;
        /// <summary>
        /// 存样区坐标11
        /// </summary>
        public XYZDPoint StoreArea11;
        /// <summary>
        /// 存样区坐标12
        /// </summary>
        public XYZDPoint StoreArea12;
    }
}
