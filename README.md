# MultiplayerInfo

Extra score info in multiplayer, and a little more!

## Features
- **Results Info**  
Adds total misses, score as a percentage, and accuracy to the multiplayer results table.

- **Nicknames**  
Customize the usernames of players in your lobby as you see fit!  
(Note: Nicknames are only visible to you, changing your nickname does not change how other people see your username.)  
Some username displays make take a while to change after setting a nickname.

## How To Install
- Simply download MultiplayerInfo.dll from [releases](https://github.com/BlqzingIce/MultiplayerInfo/releases) and put it in your Plugins folder!
- Requires BSIPA, BSML, and SiraUtil (most likely already installed lol)
- Made for 1.23.0, works on 1.21.0 - 1.23.0
- Should be compatible with both BeatTogther, MultiplayerCore, and ServerBrowser

## Config File
- The config file can be found at Beat Saber/UserData/MultiplayerInfo.json
- `ResultsInfo`: Whether or not extra info should be displayed on the multiplayer results table
- `ShowAccuracy`: If `ResultsInfo` is true, also show player's average cut accuracy (out of 115)
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

## Credits
This started as a port of some features from Quest's ScorePercentage, so big thanks to metal_marmott (aka Polly) as well as Zephyr who's ui code I borrowed from, as well as the modding community as a whole.
