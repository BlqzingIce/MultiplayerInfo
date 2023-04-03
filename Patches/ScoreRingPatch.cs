using SiraUtil.Affinity;
using Zenject;
using System.Collections.Generic;

namespace MultiplayerInfo.Patches
{
    internal class ScoreRingPatch : IAffinity
    {
        [Inject] PluginConfig _config;

        [AffinityPostfix]
        [AffinityPatch(typeof(MultiplayerScoreRingManager), nameof(MultiplayerScoreRingManager.SpawnTexts))]
        private void Postfix(List<IConnectedPlayer> ____allActivePlayers, Dictionary<string, MultiplayerScoreRingItem> ____scoreRingItems)
        {
            if (_config.EnableNicknames)
            {
                foreach (IConnectedPlayer allActivePlayer in ____allActivePlayers)
                {
                    for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
                    {
                        if (_config.Nicknames[i].PlayerId.Equals(allActivePlayer.userId))
                        {
                            ____scoreRingItems[allActivePlayer.userId].SetName(_config.Nicknames[i].Name);
                            break;
                        }
                    }
                }
            }
        }
    }
}