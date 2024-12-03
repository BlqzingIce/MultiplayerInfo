using SiraUtil.Affinity;
using System.Collections.Generic;
using Zenject;

namespace MultiplayerInfo.Patches
{
    internal class ScoreRingPatch : IAffinity
    {
        [Inject] PluginConfig _config = null!;

        [AffinityPostfix]
        [AffinityPatch(typeof(MultiplayerScoreRingManager), "SpawnTexts")]
        private void Postfix(List<IConnectedPlayer> ____allActivePlayers, Dictionary<string, MultiplayerScoreRingItem> ____scoreRingItems)
        {
            if (_config.EnableNicknames)
            {
                foreach (IConnectedPlayer player in ____allActivePlayers)
                {
                    for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
                    {
                        if (_config.Nicknames[i].PlayerId.Equals(player.userId))
                        {
                            ____scoreRingItems[player.userId].SetName(_config.Nicknames[i].Nick);
                            break;
                        }
                    }
                }
            }
        }
    }
}