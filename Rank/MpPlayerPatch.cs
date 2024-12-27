using MultiplayerCore.Players;
using MultiplayerInfo.Models;
using SiraUtil.Affinity;
using SiraUtil.Logging;
using System.Collections.Generic;
using Zenject;

namespace MultiplayerInfo.Rank
{
    internal class MpPlayerPatch : IAffinity
    {
        [Inject] private readonly SiraLog _log = null;
        [Inject] private readonly PluginConfig _config = null;
        [Inject] private readonly MpPlayerManager _mpPlayerManager = null;
        [Inject] private readonly RankGetter _rankGetter = null;

        public static List<PlatformPlayer> cachedPlayerList = new List<PlatformPlayer>();

        [AffinityPostfix]
        [AffinityPatch(typeof(MpPlayerManager), "HandlePlayerData")]
        internal void Postfix(IConnectedPlayer player)
        {
            if (player.isMe || !_config.EnableRankInfo)
                return;

            if (!cachedPlayerList.Exists(x => x.Id == player.userId))
            {
                MpPlayerData playerData;
                if (!_mpPlayerManager.TryGetPlayer(player.userId, out playerData))
                {
                    _log.Info(player.userName + "'s MultiplayerCore data not found");
                    return;
                }

                PlatformPlayer platformPlayer = new PlatformPlayer(player.userId, player.userName, playerData.PlatformId);
                _log.Info("Added " + platformPlayer.Name + " (" + platformPlayer.PlatformId + ") to cached list");
                cachedPlayerList.Add(platformPlayer);
            }

            int index = cachedPlayerList.FindIndex(x => x.Id == player.userId);

            if (cachedPlayerList[index].SSRank == -1)
            {
                cachedPlayerList[index].SSRank = 0;
                _rankGetter.GetScoreSaberRank(index, cachedPlayerList[index].PlatformId);
            }

            if (cachedPlayerList[index].BLRank == -1)
            {
                cachedPlayerList[index].BLRank = 0;
                _rankGetter.GetBeatLeaderRank(index, cachedPlayerList[index].PlatformId);
            }
        }
    }
}