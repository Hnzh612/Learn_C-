using SerialPortClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Tools
{
    public static class ArrayEx
    {
        public static byte[] RemoveRange(this byte[] _bytes, int _startIndex, int _endIndex)
        {
            int _bytesLenght = _bytes.Length;
            if (_bytesLenght <= 0 || _endIndex < _startIndex || _startIndex >= _bytesLenght || _endIndex >= _bytesLenght)
            {
                throw new ArgumentException($"Argument error.");
            }
            int _length = _endIndex - _startIndex;
            if (_length == 0)
            {
                return _bytes;
            }
            byte[] _newByte = new byte[_bytesLenght - _length];
            Array.Copy(_bytes, 0, _newByte, 0, _startIndex);
            if (_endIndex == _bytesLenght - 1)
            {
                return _newByte;
            }
            Array.Copy(_bytes, _startIndex + _length, _newByte, _startIndex, _bytesLenght - _startIndex - _length);
            return _newByte;
        }
        ///// <summary>
        ///// 获取要 [移除的数组] 和 [移除后的数组]
        ///// </summary>
        ///// <param name="_bytes"></param>
        ///// <param name="_startIndex"></param>
        ///// <param name="_endIndex"></param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentException"></exception>
        //public static Tuple<byte[],byte[]> RemoveRange(this byte[] _bytes, int _startIndex, int _endIndex)
        //{
        //    /*
        //     * 需要考虑 ：
        //     * 移除 头部
        //     * 移除 中间
        //     * 移除 尾部
        //     *  并且 入参检查 验证数组长度 0|1|2|3等
        //     */
        //    int _bytesLenght = _bytes.Length;
        //    if (_bytesLenght <= 0 || _endIndex < _startIndex || _startIndex >= _bytesLenght || _endIndex >= _bytesLenght)
        //    {
        //        throw new ArgumentException($"Argument error.");
        //    }
        //    int _length = _endIndex - _startIndex;
        //    if (_length == 0)
        //    {
        //        return Tuple.Create<byte[], byte[]>(_bytes, null);
        //    }
        //    byte[] _afterByte = new byte[_bytesLenght - _length];
        //    byte[] _newByte = new byte[_length];
        //    Array.Copy(_bytes, 0, _afterByte, 0, _startIndex);
        //    if (_endIndex == _bytesLenght - 1)//要移除的是尾部的字节
        //    {
        //        Tuple.Create<byte[], byte[]>(null, _afterByte);
        //    }
        //    Array.Copy(_bytes, _startIndex + _length, _afterByte, _startIndex, _bytesLenght - _startIndex - _length);            
        //    Array.Copy(_bytes, _startIndex, _newByte, 0, _newByte.Length);
        //    return Tuple.Create<byte[], byte[]>(_afterByte, _newByte);
        //}
        public static byte[] SubBytes(this byte[] _bytes, int _startIndex, int _endIndex)
        {
            int _bytesLenght = _bytes.Length;
            if (_bytesLenght <= 0 || _endIndex < _startIndex || _startIndex >= _bytesLenght || _endIndex >= _bytesLenght)
            {
                throw new ArgumentException($"SubBytes:Argument error._startIndex：{_startIndex}     _endIndex:{_endIndex}     _bytesLenght:{_bytesLenght}   ");
            }
            int _length = _endIndex - _startIndex;
            if (_length == 0)
            {
                return new byte[] { _bytes[_startIndex] };
            }
            byte[] _newByte = new byte[_length];
            Array.Copy(_bytes, _startIndex, _newByte, 0, _newByte.Length);
            return _newByte;
        }
        public static Int16 GetSum(this byte[] _bytes, int _startIndex, int _endIndex)
        {
            int _bytesLenght = _bytes.Length;
            if (_bytesLenght <= 0 || _endIndex < _startIndex || _startIndex >= _bytesLenght || _endIndex >= _bytesLenght)
            {
                throw new ArgumentException($"GetSum:Argument error.[{DataConverter.HexBytesToHexStr(_bytes.ToArray())}] _startIndex：{_startIndex}     _endIndex:{_endIndex}     _bytesLenght:{_bytesLenght}");
            }
            int _length = _endIndex - _startIndex;
            if (_length == 0)
            {
                return (short)(_bytes[_startIndex] & 0x0f);
            }
            int sum = 0;
            for (int i = 0; i < _length; i++)
            {
                sum += _bytes[_startIndex + i];
            }
            return (short)(sum & 0xff);
        }

    }
}
