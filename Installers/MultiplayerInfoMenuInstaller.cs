using MultiplayerInfo.Patches;
using MultiplayerInfo.UI;
using Zenject;

namespace MultiplayerInfo.Installers
{
    internal class MultiplayerInfoMenuInstaller : Installer
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