using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm.Utils
{
    class Utils
    {
        public static bool IsContains(byte[] ldata, byte[] rdata)
        {
            if(ldata == null || rdata == null || ldata.Length <= 1 || rdata.Length <= 1)
                return false;
            if(ldata.Length < rdata.Length)
                return false;
            for(int i = 0; i < ldata.Length && i < rdata.Length; i++)
            {
                if (ldata[i] == rdata[i] && ldata[i + 1] == rdata[i+1] && ldata[i+2] == rdata[i+2] && ldata[i+3] == rdata[i+3] && ldata[i+4] == rdata[i+4])
                    return true;
            }
            return false;
        }
    }
}
