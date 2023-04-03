using SiraUtil.Affinity;
using MultiplayerInfo.Models;
using System.Collections.Generic;

namespace MultiplayerInfo.Patches
{
    internal class ServerPlayerTableViewPatch : IAffinity
    {
        public static List<BasicPlayer> playerList = new List<BasicPlayer>();

        [AffinityPostfix]
        [AffinityPatch(typeof(GameServerPlayersTableView), nameof(GameServerPlayersTableView.SetData))]
        private void Postfix(List<IConnectedPlayer> sortedPlayers)
        {
            playerList.Clear();
            foreach (IConnectedPlayer connectedPlayer in sortedPlayers)
            {
                playerList.Add(new BasicPlayer(connectedPlayer.userId, connectedPlayer.userName));
            }
        }
    }
}