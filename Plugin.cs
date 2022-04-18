using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPALogger = IPA.Logging.Logger;

namespace MultiplayerInfo
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public static Harmony harmony;

        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Debug("MultiplayerInfo initialized.");
        }
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");

            harmony = new Harmony("com.BlqzingIce.BeatSaber.MultiplayerInfo");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());

            //ui thing here eventually
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }
    }
}
