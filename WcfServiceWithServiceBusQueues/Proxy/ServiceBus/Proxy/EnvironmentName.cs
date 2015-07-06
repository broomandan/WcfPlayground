using Microsoft.Azure;

namespace Proxy.ServiceBus.Proxy
{
    internal class EnvironmentName
    {
        public const string Local = "Development";
        public const string Ci = "CI";
        public const string Staging = "Staging";
        public const string AzureCi = "AzureCI";
        public const string AzureStaging = "AzureStaging";
        public const string AzureProduction = "AzureProduction";

        public static string Current
        {
            get { return CloudConfigurationManager.GetSetting("Environment");}
        }
    }
}