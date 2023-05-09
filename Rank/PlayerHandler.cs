using MultiplayerInfo.Models;
using System;
using System.Collections.Generic;
using Zenject;

namespace MultiplayerInfo.Rank
{
    public class PlayerHandler : IInitializable, IDisposable
    {
        [Inject] private readonly PluginConfig _config = null!;
        [Inject] private readonly IMultiplayerSessionManager _multiplayerSession = null!;
        [Inject] private readonly RankGetter _rankGetter = null!;
        [Inject] private readonly IPlatformUserModel _platformUserModel;
        private UserInfo _localPlayerInfo = null!;

        public static List<BasicPlayer> currentPlayerList = new List<BasicPlayer>();

        public async void Initialize()
        {
            _multiplayerSession.connectedEvent += HandleSessionConnected;
            _multiplayerSession.disconnectedEvent += HandleSessionDisconnected;
            _multiplayerSession.playerConnectedEvent += HandlePlayerConnected;
            _multiplayerSession.playerDisconnectedEvent += HandlePlayerDisconnected;

            _localPlayerInfo = await _platformUserModel.GetUserInfo();
        }

        public void Dispose()
        {
            _multiplayerSession.connectedEvent -= HandleSessionConnected;
            _multiplayerSession.disconnectedEvent -= HandleSessionDisconnected;
            _multiplayerSession.playerConnectedEvent -= HandlePlayerConnected;
            _multiplayerSession.playerDisconnectedEvent -= HandlePlayerDisconnected;
        }

        private void HandleSessionConnected()
        {
            HandlePlayerConnected(_multiplayerSession.localPlayer);

            foreach (var connectedPlayer in _multiplayerSession.connectedPlayers)
            {
                if (!connectedPlayer.isConnected || connectedPlayer.isKicked)
                    continue;

                HandlePlayerConnected(connectedPlayer);
            }

            if (!_config.EnableRankInfo)
                return;

            if (!MpPlayerPatch.cachedPlayerList.Exists(x => x.Id == _multiplayerSession.localPlayer.userId))
            {
                MIPlayer miPlayer = new MIPlayer(_multiplayerSession.localPlayer.userId, _multiplayerSession.localPlayer.userName, _localPlayerInfo.platformUserId);
                MpPlayerPatch.cachedPlayerList.Add(miPlayer);
            }

            int index = MpPlayerPatch.cachedPlayerList.FindIndex(x => x.Id == _multiplayerSession.localPlayer.userId);

            if (MpPlayerPatch.cachedPlayerList[index].SSRank == -1)
            {
                _rankGetter.GetScoreSaberRank(index, MpPlayerPatch.cachedPlayerList[index].PlatformId);
            }

            if (MpPlayerPatch.cachedPlayerList[index].BLRank == -1)
            {
                _rankGetter.GetBeatLeaderRank(index, MpPlayerPatch.cachedPlayerList[index].PlatformId);
            }
        }

        private void HandleSessionDisconnected(DisconnectedReason reason)
        {
            currentPlayerList.Clear();
        }

        private void HandlePlayerConnected(IConnectedPlayer player)
        {
            if (currentPlayerList.Exists(x => x.Id == player.userId))
                return;

            currentPlayerList.Add(new(player.userId, player.userName));
        }

        private void HandlePlayerDisconnected(IConnectedPlayer player)
        {
            currentPlayerList.RemoveAll(x => x.Id == player.userId);
        }
    }
}