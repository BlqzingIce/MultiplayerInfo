using MultiplayerInfo.Patches;
using MultiplayerInfo.Rank;
using MultiplayerInfo.UI;
using Zenject;

namespace MultiplayerInfo
{
    internal class AppInstaller : Installer
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

            if (Plugin.MultiplayerCoreEnabled)
            {
                Container.BindInterfacesTo<MpPlayerPatch>().AsSingle();
                Container.BindInterfacesAndSelfTo<RankGetter>().AsSingle();
                Container.BindInterfacesAndSelfTo<PlayerHandler>().AsSingle();
            }
        }
    }

    internal class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ModifierUI>().AsSingle();

            Container.Bind<NicknameFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<NicknameListViewController>().FromNewComponentAsViewController().AsSingle();

            Container.Bind<ScoreFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<ScoreSettingsViewController>().FromNewComponentAsViewController().AsSingle();

            Container.Bind<RankFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<RankListViewController>().FromNewComponentAsViewController().AsSingle();

            Container.BindInterfacesTo<MultiplayerLevelScenesTransitionPatch>().AsSingle();
        }
    }
}