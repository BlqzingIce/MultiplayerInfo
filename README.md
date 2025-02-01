# MultiplayerInfo

Extra score info in multiplayer, and a little more!

## Features
- **Results Info**  
Customize what you want to see on the multiplayer results table!  
Pick and choose from: Placement, Rank, Combo, Misses, Bombs Hit, Score, Percent, and Accuracy.  
(Note: Enabling all the options may cause overlap with players' usernames.)

- **Nicknames**  
Customize the usernames of players in your lobby as you see fit!  
(Note: Nicknames are only visible to you, changing your own nickname does not change how other people see your username.)  
Some username displays make take a while to change after setting a nickname.

- **Global Rank Info**  
Check the global rank of players in your lobby (if they have a ScoreSaber or BeatLeader account that is)!

## How To Install
- Simply download MultiplayerInfo.dll from [releases](https://github.com/BlqzingIce/MultiplayerInfo/releases) and put it in your Plugins folder!
- Requires BSIPA, BSML, SiraUtil and BS_Utils. Rank related features require MultiplayerCore
- Compatible with BeatTogther, MultiplayerCore, and ServerBrowser. Does not change anything in BS+'s Multiplayer+
- SongPlayHistory's Multiplayer Results Percentage might override or conflict with Results Info if both are enabled!

## Config File
- The config file can be found at Beat Saber/UserData/MultiplayerInfo.json
- `EnableScoreInfo`: Whether to change what is displayed on the multiplayer results table or not
- `ShowOrder`: Show the placement order next to your name
- `ShowRank`: If you finished the level, display your rank (F for failed and NF for no-fail will be shown regardless)
- `ShowCombo`: Show max combo, and a golden FC if you fc'd
- `ShowMisses`: Show total misses, unless you fc'd
- `ShowBombs`: Show total bombs hit + walls hit, unless you fc'd
- `ShowScore`: Self explanatory
- `ShowPercent`: Show percent of total possible score
- `ShowEstimatedPercent`: Show estimated score percent based on completion results, useful when using per-player difficulties but breaks on maps with non-standard scoring elements (like chains)
- `ShowAccuracy`: Show accuracy based on the option set below
- `AccDisplay`: Chooses how your accuracy is displayed:  
  `0` = `Number`: As a number out of 115  
  `1` = `Split`: As two numbers, swing acc out of 100 and cut acc out of 15  
  `2` = `Percent`: As a percent  
  `3` = `PercentAndNumber`: Percent | Number  
  `4` = `PercentAndSplit`: Percent | Split  
  `5` = `SaberSurgeon`: Only cut acc, but as a percent
- `EnableNicknames`: Whether custom nicknames set by the player should be used or not
- `Nicknames` formatting example:
```
"Nicknames": [
  {
    "PlayerId": "userid",
    "Name": "username",
    "Nick": "nickname (what you should actually change)"
  },
  {
    "PlayerId": "userid2",
    "Name": "username2",
    "Nicke": "nickname2"
  }
]
```
- `EnableRankInfo`: Whether to query the rank of players in your lobby or not

## Credits
This started as a port of some features from Quest's ScorePercentage, so big thanks to metal_marmott (aka Polly) for the inspiration. Additional thanks to Roy de Jong, who's code in ServerBrowser I referenced for interfacing with MultiplayerCore.  
Any list based UI code was originally from JDFixer, but has been edited to fit the needs of this mod.
