using MultiplayerInfo.Settings;
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
        }
    }
}