using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort
{
    public static class DataConverter
    {
        private static readonly StringBuilder s_StrBuilder = new StringBuilder();
        public static readonly char[] HexTable = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        /// <summary>
        /// 清除字符串
        /// </summary>
        private static void ClearStringBuilder()
        {
            if(s_StrBuilder.Length > 0) 
            { 
                s_StrBuilder.Clear(); 
            }
        }
        /// <summary>
        /// 在不同的计算机体系结构中，大小结束的顺序是不同的，而不仅仅是网络字节顺序。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetBigEndianBytes(byte[] data)
        {
            if(data == null || data.Length <= 0)
            {
                throw new ArgumentNullException("数据不能为空");
            }
            if(data.Length == 1) 
            {
                return new byte[] { 0x00, data[0] };
            }
            if(BitConverter.IsLittleEndian && data.Length >= 2) 
            { 
                Array.Reverse(data);
            }
            return data;
        }
        /// <summary>
        /// byte[] -->Hex String.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string HexBytesToHexStr(byte[] data ,[CallerMemberName] string caller = "")
        {
            if(data == null) 
            {
                throw new ArgumentException($"数据不能为空.{caller}");
            }
            ClearStringBuilder();
            for(int j =0; j < data.Length; j++)
            {
                s_StrBuilder.Append(data[j].ToString("X2"));
            }
            return s_StrBuilder.ToString();
        }
        /// <summary>
        /// Hex string -->Byte []
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] HexStrToHexBytes(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentException("数据不能为空");
            }
            data = data.Replace(" ", "");
            if((data.Length % 2) !=0 )
            {
                throw new ArgumentException($"{data} Can't convert to Bytes.");
            }
            byte[] returnBytes = new byte[data.Length / 2];
            for (int i = 0; i < data.Length; i ++ ) 
            {
                returnBytes[i] = byte.Parse( data.Substring( i *2,2),System.Globalization.NumberStyles.HexNumber );
            }
            return returnBytes;
        }
        /// <summary>
        /// Hex string --> int 16
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns
        public static int HexStrToInt16(string data)
        {
            if(string.IsNullOrEmpty(data)) 
            { 
                throw new ArgumentException("数据不能为空"); 
            }
            if(data == "00" || data == "0000")
            {
                return 0;
            }
            byte[] temp_array = GetBigEndianBytes(HexStrToHexBytes(data));
            return BitConverter.ToInt16(temp_array, 0);
        }
    }
}
