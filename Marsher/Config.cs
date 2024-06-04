using Newtonsoft.Json;
using System.IO;

namespace Marsher
{
    internal class Config
    {
        private static Config _instance;
        public static Config Instance => _instance;
        public static void Init()
        {
            var fileName = "config.json";
            try
            {
                if (!File.Exists(fileName))
                    return;

                var content = File.ReadAllText(fileName);
                _instance = JsonConvert.DeserializeObject<Config>(content);
            }
            catch
            {
                //ignore
            }
            finally
            {
                if (_instance == null)
                {
                    _instance = new Config();
                    File.WriteAllText(fileName, JsonConvert.SerializeObject(_instance, Formatting.Indented));
                }
            }
        }

        public bool DisableMarshmallow { get; set; } = false;
        public bool DisablePeing { get; set; } = false;
        public bool DisableKikubox { get; set; } = false;
        public bool DisableJoiAsk { get; set; } = false;
        public string JoiAskUrl { get; set; } = null;
    }
}
