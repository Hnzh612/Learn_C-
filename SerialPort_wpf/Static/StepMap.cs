using Microsoft.Win32;
using SerialPort_wpf.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Log;
using System.Windows.Forms;

namespace SerialPort_wpf.Static
{
    /// <summary>
    /// 整个操作区的矩阵图
    /// </summary>
    public class StepMap
    {
        private static XMLStepMap XMP { get; set; }
        static string Path { get; set; } = "StepperMotorStepMap.xml";
        static int TargetVersion { get; set; } = 5;
        public static bool IsDataUseable { get; private set; } = false;
        static StepMap()
        {
            try
            {
                XMLStepMap xsm = new XMLStepMap();
                XmlSerializer mySerializer = new XmlSerializer(typeof(XMLStepMap));
                //if (!File.Exists(Path))
                //{
                //    File.Create(Path);
                //    //StreamWriter myWriter = new StreamWriter("StepperMotorStepMap.xml");
                //    //mySerializer.Serialize(myWriter, xsm);
                //    //myWriter.Close();
                //}
                using (var myFileStream = new FileStream(Path, FileMode.Open))
                {
                    XMP = (XMLStepMap)mySerializer.Deserialize(myFileStream);
                    Version = XMP.Version;
                    InitSimples();
                    InitWaste();
                    InitTIP();
                    InitReagent();
                    InitMixingVibration();
                    InitStoreArea();
                    // Export_CSV();
                }
                Validation();
            }
            catch (Exception ex)
            {
                LogCore.CreateInstance().AsyncLog(ex);
            }
        }
        public XMLStepMap ReadStepMapConfigFile(string _filePath = "StepperMotorStepMap.xml")
        {
            try
            {
                XMLStepMap xsm = new XMLStepMap();
                XmlSerializer mySerializer = new XmlSerializer(typeof(XMLStepMap));
                using (var myFileStream = new FileStream(_filePath, FileMode.Open))
                {
                    xsm = (XMLStepMap)mySerializer.Deserialize(myFileStream);
                    return xsm;
                }
            }
            catch (Exception ex)
            {
                LogCore.CreateInstance().AsyncLog(ex);
                return null;
            }
        }
        public bool WriteStepMapConfigFile(string _filePath = "StepperMotorStepMap.xml")
        {
            try
            {
                XMLStepMap xsm = new XMLStepMap();
                XmlSerializer mySerializer = new XmlSerializer(typeof(XMLStepMap));
                using (var StreamWriter = new StreamWriter(_filePath))
                {
                    mySerializer.Serialize(StreamWriter, xsm);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogCore.CreateInstance().AsyncLog(ex);
                return false;
            }
        }
        static bool Validation()
        {
            //验证版本
            if (Version != TargetVersion)
            {
                return false;
            }
            IsDataUseable = ValidationSimples(SimpleArea)
               && ValidationTIPs(TIPArea1)
               // && ValidationTIPs(TIPArea2) 
               //&& ValidationTIPs(TIPArea3) 
               //  && ValidationTIPs(TIPArea4)
               && ValidationWastes(Waste1)
               && ValidationWastes(Waste2)
               && ValidataionReagents(ReagentArea)
               && ValidataionMixingViration(MixingVibrationArea)
               ;//&& ValidationStoreArea(StoreArea);
            return IsDataUseable;
        }
        #region ValidationSimples
        private static bool ValidationSimples(XYZDPoint[,] simples)
        {
            for (int i = 0; i < 24; i++)//row
            {
                for (int j = 0; j < 4; j++)//col
                {
                    if (
                        (simples[i, j].x < XMP.SimpleArea4.Start.x && simples[i, j].x < XMP.SimpleArea4.End.x)
                        ||
                        (simples[i, j].y < XMP.SimpleArea4.Start.y && simples[i, j].x < XMP.SimpleArea1.Start.y))
                    {
                        LogCore.CreateInstance().AsyncLog($"ValidationSimples[{i},{j}] {simples[i, j]} error!!");
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region ValidationTIPs
        private static bool ValidationTIPs(XYZDPoint[,] _tip)
        {
            for (int i = 0; i < 8; i++)//row
            {
                for (int j = 0; j < 12; j++)//col
                {
                    if (_tip[i, j].x <= 0 || _tip[i, j].y <= 0)
                    {
                        LogCore.CreateInstance().AsyncLog($"ValidationTIPs[{i},{j}] error!!");
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region ValidationWastes
        private static bool ValidationWastes(XYZDPoint _p)
        {
            for (int i = 0; i < 8; i++)//row
            {
                for (int j = 0; j < 12; j++)//col
                {
                    if (_p.x <= 0 || _p.y <= 0)
                    {
                        LogCore.CreateInstance().AsyncLog($"ValidationWastes[{i},{j}] error!!");
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region ValidataionReagents
        private static bool ValidataionReagents(XYZDPoint[] _points)
        {
            for (int i = 0; i < _points.Count(); i++)//row
            {
                if (_points[i].x <= 0 || _points[i].y <= 0)
                {
                    LogCore.CreateInstance().AsyncLog($"ValidataionReagents[{i}] error!!");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region ValidationMixingVibration
        private static bool ValidataionMixingViration(XYZDPoint[,] _mix)
        {
            for (int i = 0; i < 8; i++)//row
            {
                for (int j = 0; j < 12; j++)//col
                {
                    if (_mix[i, j].x <= 0 || _mix[i, j].y <= 0)
                    {
                        LogCore.CreateInstance().AsyncLog($"ValidataionMixingViration[{i},{j}] error!!");
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region ValidationStoreArea
        private static bool ValidationStoreArea(XYZDPoint[,] _points)
        {
            return _points.Length == 12
                 && _points[0, 0].x > _points[0, 1].x && _points[0, 1].x > _points[0, 2].x
                 && _points[1, 0].x > _points[1, 1].x && _points[1, 1].x > _points[1, 2].x
                 && _points[2, 0].x > _points[2, 1].x && _points[2, 1].x > _points[2, 2].x
                 && _points[3, 0].x > _points[3, 1].x && _points[3, 1].x > _points[3, 2].x
                 && _points[3, 0].y > _points[2, 0].y && _points[2, 0].y > _points[1, 0].y && _points[1, 0].y > _points[0, 0].y
                 && _points[3, 1].y > _points[2, 1].y && _points[2, 1].y > _points[1, 1].y && _points[1, 1].y > _points[0, 1].y
                 && _points[3, 2].y > _points[2, 2].y && _points[2, 2].y > _points[1, 2].y && _points[1, 2].y > _points[0, 2].y;
        }
        #endregion

        #region GetPoints
        private static XYZDPoint[,] GetPoints(int _rowCount, int _columnCount, StepRect _rect, bool translation = true)
        {
            XYZDPoint[,] points = new XYZDPoint[_rowCount, _columnCount];
            float rowStep = (_rect.TopRight.x - _rect.TopLeft.x) / (_columnCount - 1);
            float columnStep = (_rect.BottomLeft.y - _rect.TopLeft.y) / (_rowCount - 1);
            float CS = (_rect.TopRight.y - _rect.TopLeft.y) / (_columnCount - 1);
            float RS = (_rect.BottomLeft.x - _rect.TopLeft.x) / (_rowCount - 1);
            for (int rowIndex = 0; rowIndex < _rowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _columnCount; columnIndex++)
                {
                    int _currentPointX = (int)(_rect.TopLeft.x + (RS + rowStep) * columnIndex);
                    int _currentPointY = (int)(_rect.TopLeft.y + (CS + columnStep) * rowIndex);
                    points[rowIndex, columnIndex] = new XYZDPoint(_currentPointX, _currentPointY, 600);
                }
            }
            return points;
        }
        /// <summary>
        /// 由几个点组成的线
        /// </summary>
        /// <param name="_count">点的数量</param>
        /// <param name="_lines"></param>
        /// <returns></returns>
        private static XYZDPoint[,] GetPoints(List<StepLine> _lines, int _count)
        {
            XYZDPoint[,] points = new XYZDPoint[_count, _lines.Count];
            float rowStep = (_lines[0].End.y - _lines[0].Start.y) / (_count - 1);
            float columnStep = (_lines[_lines.Count - 1].Start.x - _lines[0].End.x) / (_lines.Count - 1);
            for (int i = 0; i < _count; i++)
            {
                for (int j = 0; j < _lines.Count; j++)
                {
                    int _currentPointX = (int)(_lines[j].Start.x + columnStep * j);
                    int _currentPointY = (int)(_lines[j].Start.y + rowStep * i);
                    points[i, j] = new XYZDPoint(_currentPointX, _currentPointY, 0);
                }
            }
            return points;
        }
        #endregion

        #region InitSimples
        private static void InitSimples()
        {
            int _columnCount = 4;
            XYZDPoint[,] points = new XYZDPoint[24, _columnCount];
            List<StepLine> _lines = new List<StepLine>();
            _lines.Add(XMP.SimpleArea4);
            _lines.Add(XMP.SimpleArea3);
            _lines.Add(XMP.SimpleArea2);
            _lines.Add(XMP.SimpleArea1);
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < _lines.Count; j++)
                {
                    float rowsXStep = (_lines[j].End.x - _lines[j].Start.x) / 20.0f;
                    float rowsYStep = (_lines[j].End.y - _lines[j].Start.y) / 20.0f;
                    int _currentPointX = (int)(_lines[j].Start.x + rowsXStep * i);
                    int _currentPointY = (int)(_lines[j].Start.y + rowsYStep * i);
                    points[i, j] = new XYZDPoint(_currentPointX, _currentPointY, 25500);
                }
            }
            SimpleArea = points;
        }
        #endregion

        #region InitWaste
        private static void InitWaste()
        {
            Waste1 = new XYZDPoint(XMP.Waste1.x, XMP.Waste1.y, XMP.Waste1.z);
            Waste2 = new XYZDPoint(XMP.Waste2.x, XMP.Waste2.y, XMP.Waste1.z);
        }
        #endregion

        #region InitTIP
        private static void InitTIP()
        {
            TIPArea1 = SetTIP(XMP.TIPArea1);
            TIPArea2 = SetTIP(XMP.TIPArea2);
            TIPArea3 = SetTIP(XMP.TIPArea3);
            TIPArea4 = SetTIP(XMP.TIPArea4);
            XYZDPoint[,] SetTIP(StepRect _rect, int _rowCount = 8, int _columnCount = 12)
            {
                XYZDPoint[,] points = new XYZDPoint[_rowCount, _columnCount];
                float rowStep = _rect.BottomLeft.y - _rect.TopLeft.y;
                float columnStep = (_rect.TopRight.x - _rect.TopLeft.x) / 11;
                for (int i = 0; i < _rowCount; i++)
                {
                    for (int j = 0; j < _columnCount; j++)
                    {
                        int _currentPointX = (int)(_rect.TopLeft.x + columnStep * j);
                        int _currentPointY = (int)(_rect.TopLeft.y + rowStep * i);
                        points[i, j] = new XYZDPoint(_currentPointX, _currentPointY, 25100);
                    }
                }
                return points;
            }
        }
        #endregion

        #region InitReagent
        private static void InitReagent()
        {
            XYZDPoint CTR = XMP.ReagentCircleTopRight;//试剂 0=>1
            XYZDPoint CLC = XMP.ReagentCircleLeftCenter;//试剂19=>2
            XYZDPoint CBR = XMP.ReagentCircleBottomRight;// 试剂 5=>3
            ReagentArea = new XYZDPoint[20];
            int rowStep = (int)(CBR.y - CTR.y) / 4;
            int columnStep = (int)(CTR.x - CLC.x) / 4;
            //
            ReagentArea[0] = new XYZDPoint(CTR.x, CTR.y, 0);
            ReagentArea[1] = new XYZDPoint(CTR.x, CTR.y + rowStep, 0);
            ReagentArea[2] = new XYZDPoint(CTR.x, rowStep * 2, 0);
            ReagentArea[3] = new XYZDPoint(CTR.x, rowStep * 3, 0);
            //                                 
            ReagentArea[4] = new XYZDPoint(CBR.x, CBR.y, 0);
            ReagentArea[5] = new XYZDPoint(CBR.x, CBR.y + rowStep, 0);
            ReagentArea[6] = new XYZDPoint(CBR.x, CBR.y + rowStep * 2, 0);
            ReagentArea[7] = new XYZDPoint(CBR.x, CBR.y + rowStep * 3, 0);
            //                                 
            ReagentArea[8] = new XYZDPoint(CLC.x + columnStep * 3, CTR.y, 0);
            ReagentArea[9] = new XYZDPoint(CLC.x + columnStep * 3, CLC.y, 0);
            ReagentArea[10] = new XYZDPoint(CLC.x + columnStep * 3, CBR.y, 0);
            //
            ReagentArea[11] = new XYZDPoint(CLC.x + columnStep * 2, CTR.y, 0);
            ReagentArea[12] = new XYZDPoint(CLC.x + columnStep * 2, CLC.y, 0);
            ReagentArea[13] = new XYZDPoint(CLC.x + columnStep * 2, CBR.y, 0);
            //
            ReagentArea[14] = new XYZDPoint(CLC.x + columnStep, CTR.y, 0);
            ReagentArea[15] = new XYZDPoint(CLC.x + columnStep, CLC.y, 0);
            ReagentArea[16] = new XYZDPoint(CLC.x + columnStep, CBR.y, 0);
            //
            ReagentArea[17] = new XYZDPoint(CLC.x, CTR.y, 0);
            ReagentArea[18] = new XYZDPoint(CLC.x, CLC.y, 0);
            ReagentArea[19] = new XYZDPoint(CLC.x, CBR.y, 0);
        }
        #endregion
        #region InitMixingVibration
        private static void InitMixingVibration()
        {
            MixingVibrationArea = GetPoints(8, 12, XMP.MixingVibrationArea);//8行12列
        }
        #endregion

        #region Debug_Test
        public static void Export_CSV()
        {
            try
            {
                using (System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog())
                {
                    saveFileDialog.Filter = "Execl文件(*.csv)|*.csv";
                    saveFileDialog.FilterIndex = 0;
                    saveFileDialog.OverwritePrompt = true;
                    saveFileDialog.Title = "导出CSV文件";
                    saveFileDialog.DefaultExt = ".csv";
                    saveFileDialog.FileName = $"数据文件{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}";
                    DialogResult result = saveFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        using (StreamWriter sw = new StreamWriter(saveFileDialog.OpenFile(), System.Text.Encoding.GetEncoding("gb2312")))
                        {
                            sw.WriteLine("样本组");
                            StringBuilder _sb = new StringBuilder();
                            for (int i = 0; i < 24; i++)
                            {
                                _sb.Clear();
                                _sb.Append($"{SimpleArea[i, 0]},{SimpleArea[i, 1]},{SimpleArea[i, 2]},{SimpleArea[i, 3]}");
                                sw.WriteLine(_sb.ToString());
                            }
                            sw.WriteLine("废液区1|2");
                            sw.WriteLine($"{Waste1},{Waste2}");
                            sw.WriteLine("枪头组1");
                            for (int i = 0; i < 8; i++)
                            {
                                _sb.Clear();
                                _sb.Append($"{TIPArea4[i, 0]},{TIPArea4[i, 1]},{TIPArea4[i, 2]},{TIPArea4[i, 3]},{TIPArea4[i, 4]},{TIPArea4[i, 5]},{TIPArea4[i, 6]},{TIPArea4[i, 7]},{TIPArea4[i, 8]},{TIPArea4[i, 9]},{TIPArea4[i, 10]},{TIPArea4[i, 11]}");
                                sw.WriteLine(_sb.ToString());
                            }
                            sw.WriteLine("混匀震荡");
                            for (int i = 0; i < 8; i++)
                            {
                                _sb.Clear();
                                _sb.Append($"{MixingVibrationArea[i, 0]},{MixingVibrationArea[i, 1]},{MixingVibrationArea[i, 2]},{MixingVibrationArea[i, 3]},{MixingVibrationArea[i, 4]},{MixingVibrationArea[i, 5]},{MixingVibrationArea[i, 6]},{MixingVibrationArea[i, 7]},{MixingVibrationArea[i, 8]},{MixingVibrationArea[i, 9]},{MixingVibrationArea[i, 10]},{MixingVibrationArea[i, 11]}");
                                sw.WriteLine(_sb.ToString());
                            }
                            sw.WriteLine("试剂区");
                            _sb.Clear();
                            _sb.Append($",,,,{ReagentArea[0]}");
                            sw.WriteLine(_sb.ToString());
                            _sb.Clear();
                            _sb.Append($",,,,{ReagentArea[1]}");
                            sw.WriteLine(_sb.ToString());
                            _sb.Clear();
                            _sb.Append($"{ReagentArea[17]},{ReagentArea[14]},{ReagentArea[11]},{ReagentArea[8]},{ReagentArea[2]}");
                            sw.WriteLine(_sb.ToString());
                            _sb.Clear();
                            _sb.Append($",,,,{ReagentArea[3]}");
                            sw.WriteLine(_sb.ToString());
                            _sb.Clear();
                            _sb.Append($"{ReagentArea[18]},{ReagentArea[15]},{ReagentArea[12]},{ReagentArea[9]},");
                            sw.WriteLine(_sb.ToString());
                            _sb.Clear();
                            _sb.Append($",,,,{ReagentArea[4]}");
                            sw.WriteLine(_sb.ToString());
                            _sb.Clear();
                            _sb.Append($"{ReagentArea[19]},{ReagentArea[16]},{ReagentArea[13]},{ReagentArea[10]},{ReagentArea[5]}");
                            sw.WriteLine(_sb.ToString());
                            _sb.Clear();
                            _sb.Append($",,,,{ReagentArea[6]}");
                            sw.WriteLine(_sb.ToString());
                            _sb.Clear();
                            _sb.Append($",,,,{ReagentArea[7]}");
                            sw.WriteLine(_sb.ToString());
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                LogCore.CreateInstance().AsyncLog(EX, "Debug_tEST:Export_CSV");
            }
        }
        #endregion


        #region InitStoreArea
        private static bool InitStoreArea()
        {
            StoreArea = new XYZDPoint[,]
            {
                {new XYZDPoint(XMP.StoreArea9),new XYZDPoint(XMP.StoreArea5),new XYZDPoint(XMP.StoreArea1)},
                {new XYZDPoint(XMP.StoreArea10),new XYZDPoint(XMP.StoreArea6),new XYZDPoint(XMP.StoreArea2)},
                {new XYZDPoint(XMP.StoreArea11),new XYZDPoint(XMP.StoreArea7),new XYZDPoint(XMP.StoreArea3)},
                {new XYZDPoint(XMP.StoreArea12),new XYZDPoint(XMP.StoreArea8),new XYZDPoint(XMP.StoreArea4)},
            };
            return true;
        }
        #endregion

        #region 
        #endregion

        #region 
        #endregion

        #region 
        #endregion

        #region 
        #endregion

        #region 
        #endregion

        #region 
        #endregion

        #region 
        #endregion

        #region 
        #endregion

        #region 
        #endregion


        /// <summary>
        /// 初定简单版本号
        /// </summary>
        public static int Version { get; private set; } = 5;
        /// <summary>
        /// 96样品区的坐标
        /// </summary>
        public static XYZDPoint[,] SimpleArea { get; private set; }
        /// <summary>
        /// 废液区1
        /// </summary>
        public static XYZDPoint Waste1 { get; private set; }
        /// <summary>
        /// 废液区2
        /// </summary>
        public static XYZDPoint Waste2 { get; private set; }
        /// <summary>
        /// 枪头区坐标1
        /// </summary>
        public static XYZDPoint[,] TIPArea1 { get; private set; }
        /// <summary>
        /// 枪头区坐标2
        /// </summary>
        public static XYZDPoint[,] TIPArea2 { get; private set; }
        /// <summary>
        /// 枪头区坐标3
        /// </summary>
        public static XYZDPoint[,] TIPArea3 { get; private set; }
        /// <summary>
        /// 枪头区坐标4
        /// </summary>
        public static XYZDPoint[,] TIPArea4 { get; private set; }
        /// <summary>
        /// 试剂区坐标
        /// </summary>
        public static XYZDPoint[] ReagentArea { get; private set; }
        /// <summary>
        /// 混匀区坐标
        /// </summary>
        public static XYZDPoint[,] MixingVibrationArea { get; private set; }
        /// <summary>
        /// 氮吹
        /// </summary>
        public static XYZDPoint NitrogenBlowingArea;
        /// <summary>
        /// 存样区坐标
        /// </summary>
        public static XYZDPoint[,] StoreArea { get; private set; }
    }
}
