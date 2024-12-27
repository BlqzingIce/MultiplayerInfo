using IPA;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using System.Linq;
using IPALogger = IPA.Logging.Logger;

namespace MultiplayerInfo
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static PluginConfig Config { get; private set; } = null;
        private IPALogger _log = null;
        private Zenjector _zenjector = null;

        internal static bool MultiplayerCoreEnabled = false;
        internal static bool SongPlayHistoryEnabled = false;

        [Init]
        public void Init(IPALogger logger, Zenjector zenjector, IPA.Config.Config config)
        {
            _log = logger;
            _zenjector = zenjector;

            Config = config.Generated<PluginConfig>();

            zenjector.UseMetadataBinder<Plugin>();
            zenjector.UseLogger(logger);
            zenjector.UseHttpService();
        }

        [OnStart]
        public void OnApplicationStart()
        {
            if (IPA.Loader.PluginManager.EnabledPlugins.Any(x => x.Id == "MultiplayerCore"))
                MultiplayerCoreEnabled = true;
            else
                _log.Info("MultiplayerCore not installed, some features will be disabled!");

            if (IPA.Loader.PluginManager.EnabledPlugins.Any(x => x.Id == "SongPlayHistory"))
                SongPlayHistoryEnabled = true;

            _zenjector.Install<AppInstaller>(Location.App);
            _zenjector.Install<MenuInstaller>(Location.Menu);
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            
        }
    }
}
