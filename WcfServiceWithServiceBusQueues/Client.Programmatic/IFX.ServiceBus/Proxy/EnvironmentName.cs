using Microsoft.Azure;

namespace Client.Programmatic.IFX.ServiceBus.Proxy
{
    internal class EnvironmentName
    {
        public const string Local = "Development";
        public const string Ci = "CI";
        public const string Staging = "UAT";
        public const string AzureCi = "Production";

        public static string Current
        {
            get { return CloudConfigurationManager.GetSetting("Environment");}
        }
    }
}