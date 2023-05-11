using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prictices
{
    public class Capacity
    {
        public static double allExpotend { get; set; }
        public static Dictionary<string,double> VirusMap { get; set; }
        private static Dictionary<string, double> callbackVirusMap = new Dictionary<string, double>();
        private static double maxValue = 0;
        private static string maxKey;

        public static Dictionary<string,double> CalPercentage()
        {
            VirusMap.OrderBy(s => s.Value);
            foreach (KeyValuePair<string,double> virus in VirusMap)
            {
                double percentage = virus.Value / allExpotend;
                if(percentage > 0.99) 
                {
                    maxValue = 0.99;
                    maxKey = virus.Key;
                }
                else
                {
                    if (percentage < 0.01)
                    {
                        percentage = 0.01;
                        maxValue -= 0.01;
                    }
                    callbackVirusMap.Add(virus.Key.ToString(), percentage);
                }
            }
            callbackVirusMap.Add(maxKey, maxValue);
            return callbackVirusMap;
        }
    }
}
