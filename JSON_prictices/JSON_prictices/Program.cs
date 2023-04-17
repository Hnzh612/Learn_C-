using Newtonsoft.Json;

namespace JSON_prictices
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XMLStepMap obj = new XMLStepMap();


            string _dirPath = $"{Environment.CurrentDirectory}/Params";
            if(!Directory.Exists(_dirPath))
            {
                Directory.CreateDirectory(_dirPath);
            }
            string FilePath = $"{_dirPath}/params.json";
            if(!File.Exists(_dirPath))
            {
                File.Create(FilePath).Close();
            }
            
            using(StreamWriter file = new StreamWriter(FilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
            }
        }
    }
    public class MyObj
    {
        public string name { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public List<string> Games { get; set; }
    }
}