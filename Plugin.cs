using IPA;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;
using MultiplayerInfo.Installers;
using MultiplayerInfo.Patches;

namespace MultiplayerInfo
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static PluginConfig Config { get; private set; } = null!;
        private IPALogger _log = null!;

        [Init]
        public void Init(IPALogger logger, Zenjector zenjector, IPA.Config.Config config)
        {
            _log = logger;

            Config = config.Generated<PluginConfig>();

            zenjector.UseMetadataBinder<Plugin>();
            zenjector.UseLogger(logger);

            zenjector.Install<MultiplayerInfoAppInstaller>(Location.App);
            zenjector.Install<MultiplayerInfoMenuInstaller>(Location.Menu);
        }
    }
}
