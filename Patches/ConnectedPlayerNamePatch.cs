using SiraUtil.Affinity;
using Zenject;

namespace MultiplayerInfo.Patches
{
    internal class ConnectedPlayerNamePatch : IAffinity
    {
        [Inject] PluginConfig _config;
        
        [AffinityPostfix]
        [AffinityPatch(typeof(ConnectedPlayerName), nameof(ConnectedPlayerName.Start))]
        private void Postfix(IConnectedPlayer ____connectedPlayer, TMPro.TextMeshProUGUI ____nameText)
        {
            if (_config.EnableNicknames)
            {
                for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
                {
                    if (_config.Nicknames[i].PlayerId.Equals(____connectedPlayer.userId))
                    {
                        ____nameText.text = _config.Nicknames[i].Nick;
                        break;
                    }
                }
            }
        }
    }
}