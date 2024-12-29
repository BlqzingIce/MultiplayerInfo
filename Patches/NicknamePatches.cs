using BeatSaber.AvatarCore;
using HMUI;
using SiraUtil.Affinity;
using System.Collections.Generic;
using TMPro;
using Zenject;

namespace MultiplayerInfo.Patches
{
    internal class ConnectedPlayerNamePatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;

        [AffinityPostfix]
        [AffinityPatch(typeof(ConnectedPlayerName), "Start")]
        private void Postfix(IConnectedPlayer ____connectedPlayer, TMPro.TextMeshProUGUI ____nameText)
        {
            if (!_config.EnableNicknames) return;
            for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
            {
                if (!_config.Nicknames[i].PlayerId.Equals(____connectedPlayer.userId)) continue;
                ____nameText.text = _config.Nicknames[i].Nick;
                break;
            }
        }
    }
    
    internal class MultiplayerLeaderboardPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;

        [AffinityPostfix]
        [AffinityPatch(typeof(MultiplayerLeaderboardPanelItem), nameof(MultiplayerLeaderboardPanelItem.SetData))]
        private void Postfix(string playerName, TextMeshProUGUI ____playerNameText)
        {
            if (!_config.EnableNicknames) return;
            for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
            {
                if (!_config.Nicknames[i].Name.Equals(playerName)) continue;
                ____playerNameText.text = _config.Nicknames[i].Nick;
                break;
            }
        }
    }
    
    internal class ScoreItemPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;

        [AffinityPostfix]
        [AffinityPatch(typeof(MultiplayerScoreItem), nameof(MultiplayerScoreItem.SetName))]
        private void Postfix(string text, TextMeshProUGUI ____nameText)
        {
            for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
            {
                if (!_config.Nicknames[i].Name.Equals(text)) continue;
                ____nameText.text = _config.Nicknames[i].Nick;
                break;
            }
        }
    }
    
    internal class ServerPlayerTableCellPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;

        [AffinityPostfix]
        [AffinityPatch(typeof(GameServerPlayerTableCell), nameof(GameServerPlayerTableCell.SetData))]
        private void Postfix(IConnectedPlayer connectedPlayer, CurvedTextMeshPro ____playerNameText)
        {
            for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
            {
                if (!_config.Nicknames[i].PlayerId.Equals(connectedPlayer.userId)) continue;
                _config.Nicknames[i].Name = connectedPlayer.userName;

                if (!_config.EnableNicknames) continue;
                ____playerNameText.text = _config.Nicknames[i].Nick;
                break;
            }
        }
    }
    
    internal class SpectatingSpotPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;

        [AffinityPostfix]
        [AffinityPatch(typeof(MultiplayerSpectatingSpotPickerViewController), "RefreshSpectatingSpotName")]
        private void Postfix(StepValuePicker ____stepValuePicker)
        {
            if (!_config.EnableNicknames) return;
            for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
            {
                if (!_config.Nicknames[i].Name.Equals(____stepValuePicker.text)) continue;
                ____stepValuePicker.text = _config.Nicknames[i].Nick;
                break;
            }
        }
    }
}