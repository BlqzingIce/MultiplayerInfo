using SiraUtil.Affinity;
using Zenject;
using BeatSaber.AvatarCore;

namespace MultiplayerInfo.Patches
{
    internal class ConnectedPlayerNamePatch : IAffinity
    {
        [Inject] PluginConfig _config = null;

        [AffinityPostfix]
        [AffinityPatch(typeof(ConnectedPlayerName), "Start")]
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