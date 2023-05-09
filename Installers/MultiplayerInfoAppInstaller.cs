using MultiplayerInfo.Patches;
using MultiplayerInfo.Rank;
using Zenject;

namespace MultiplayerInfo.Installers
{
    internal class MultiplayerInfoAppInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<PluginConfig>().FromInstance(Plugin.Config).AsSingle();

            Container.BindInterfacesTo<ConnectedPlayerNamePatch>().AsSingle();
            Container.BindInterfacesTo<ResultsCellPatch>().AsSingle();
            Container.BindInterfacesTo<ScoreRingPatch>().AsSingle();
            Container.BindInterfacesTo<ServerPlayerTableCellPatch>().AsSingle();
            Container.BindInterfacesTo<SongStartPatch>().AsSingle();
            Container.BindInterfacesTo<MultiplayerLeaderboardPatch>().AsSingle();
            Container.BindInterfacesTo<SpectatingSpotPatch>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerHandler>().AsSingle();

            if (Plugin.MpCoreEnabled)
            {
                Container.BindInterfacesTo<MpPlayerPatch>().AsSingle();
                Container.BindInterfacesAndSelfTo<RankGetter>().AsSingle();
            }
        }
    }
}