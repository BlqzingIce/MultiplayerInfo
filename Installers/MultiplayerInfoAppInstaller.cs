using MultiplayerInfo.Patches;
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
            Container.BindInterfacesTo<ServerPlayerTableViewPatch>().AsSingle();
            Container.BindInterfacesTo<SongStartPatch>().AsSingle();
        }
    }
}