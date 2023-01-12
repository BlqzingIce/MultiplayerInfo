# MultiplayerInfo

Extra score info in multiplayer, and a little more!

## Features
- **Results Info**  
Customize what you want to see on the multiplayer results table!  
Pick and choose from: Rank, Combo, Misses, Bombs Hit, Score, Percent, and Accuracy.  
(Note: Enabling all the options may cause overlap with player's usernames.)

- **Nicknames**  
Customize the usernames of players in your lobby as you see fit!  
(Note: Nicknames are only visible to you, changing your own nickname does not change how other people see your username.)  
Some username displays make take a while to change after setting a nickname.

## How To Install
- Simply download MultiplayerInfo.dll from [releases](https://github.com/BlqzingIce/MultiplayerInfo/releases) and put it in your Plugins folder!
- Requires BSIPA, BSML, and SiraUtil (most likely already installed lol)
- Made for 1.25.0, works on 1.21.0 - 1.25.0
- Should be compatible with both BeatTogther, MultiplayerCore, and ServerBrowser

## Config File
- The config file can be found at Beat Saber/UserData/MultiplayerInfo.json
- `EnableNicknames`: Whether custom nicknames set by the player should be used
- `NicknamesList` formatting example:
```
"NicknamesList": [
  {
    "PlayerID": "userid",
    "PlayerName": "username",
    "NickName": "nickname (what you should actually change)"
  },
  {
    "PlayerID": "userid2",
    "PlayerName": "username2",
    "NickName": "nickname2"
  }
]
```
- `ShowRank`: If you finished the level, display your rank (F for failed and NF for no fail will still be shown unless everything else is disabled)
- `ShowCombo`: Show max combo, and a golden FC if you fc'd
- `ShowMisses`: Show total misses, unless you fc'd
- `ShowBombs`: Show total bombs hit + walls hit, unless you fc'd
- `ShowScore`: Self explanatory
- `ShowPercent`: Show percent of total possible score
- `ShowAccuracy`: Show player's average cut accuracy (out of 115)
- `DetailedAcc`: If show accuracy is enabled, also display average score from acc and average score from swing

## Credits
This started as a port of some features from Quest's ScorePercentage, so big thanks to metal_marmott (aka Polly) as well as Zephyr who's ui code I borrowed from. And as always, thanks to the modding community as a whole.