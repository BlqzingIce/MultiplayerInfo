using SiraUtil.Affinity;
using Zenject;

namespace MultiplayerInfo.Patches
{
    internal class ServerPlayerTableCellPatch : IAffinity
    {
        [Inject] PluginConfig _config = null!;

        [AffinityPostfix]
        [AffinityPatch(typeof(GameServerPlayerTableCell), nameof(GameServerPlayerTableCell.SetData))]
        private void Postfix(IConnectedPlayer connectedPlayer, HMUI.CurvedTextMeshPro ____playerNameText)
        {
            for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
            {
                if (_config.Nicknames[i].PlayerId.Equals(connectedPlayer.userId))
                {
                    _config.Nicknames[i].Name = connectedPlayer.userName;

                    if (_config.EnableNicknames)
                    {
                        ____playerNameText.text = _config.Nicknames[i].Nick;
                        break;
                    }
                }
            }
        }
    }
}