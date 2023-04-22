using SiraUtil.Affinity;
using Zenject;
using TMPro;

namespace MultiplayerInfo.Patches
{
    internal class MultiplayerLeaderboardPatch : IAffinity
    {
        [Inject] PluginConfig _config;

        [AffinityPostfix]
        [AffinityPatch(typeof(MultiplayerLeaderboardPanelItem), nameof(MultiplayerLeaderboardPanelItem.SetData))]
        private void Postfix(string playerName, TextMeshProUGUI ____playerNameText)
        {
            if (_config.EnableNicknames)
            {
                for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
                {
                    if (_config.Nicknames[i].Name.Equals(playerName))
                    {
                        ____playerNameText.text = _config.Nicknames[i].Nick;
                        break;
                    }
                }
            }
        }
    }
}