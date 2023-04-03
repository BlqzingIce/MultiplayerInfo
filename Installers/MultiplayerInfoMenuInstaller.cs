using Zenject;
using MultiplayerInfo.UI;
using MultiplayerInfo.Patches;

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

            Container.BindInterfacesTo<MultiplayerLevelScenesTransitionPatch>().AsSingle();
        }
    }
}