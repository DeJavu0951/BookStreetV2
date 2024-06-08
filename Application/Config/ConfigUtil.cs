using System.Text.Json;

namespace Application.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigUtil
    {
        static CenterConfig _centerConfig = null;

        /// <summary>
        /// 
        /// </summary>
        public static CenterConfig ConfigGlobal
        {
            get
            {
                return _centerConfig ??= new CenterConfig();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerConfig"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetConfigGlobal(CenterConfig centerConfig)
        {
            if (centerConfig == null)
            {
                throw new ArgumentNullException(nameof(centerConfig));
            }

            _centerConfig = centerConfig;
        }

        /// <summary>
        /// Get value from key in appsettings.json
        /// </summary>
        public static T ReadConfigFromJson<T>(string configPath)
        {
            if (string.IsNullOrEmpty(configPath) || !File.Exists(configPath))
            {
                throw new ArgumentNullException(nameof(configPath));
            }

            var config = JsonSerializer.Deserialize<T>(File.ReadAllText(configPath));

            return config;
        }
    }
}
