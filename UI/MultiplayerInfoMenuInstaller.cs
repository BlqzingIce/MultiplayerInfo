using MultiplayerInfo.Settings;
using Zenject;

namespace MultiplayerInfo.Installers
{
    internal class MultiplayerInfoMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ModifierUI>().AsSingle();

            // Flow Coordinators need to binded like this, as a component since it is a Unity Component
            Container.Bind<NicknameFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();

            // Even though ViewControllers are also Unity Components, we bind them with this helper method provided by SiraUtil (FromNewComponentAsViewController)
            Container.Bind<NicknameListViewController>().FromNewComponentAsViewController().AsSingle();
        }
    }
}