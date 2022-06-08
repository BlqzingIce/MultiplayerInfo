namespace MultiplayerInfo.Models
{
    internal class Nicknames
    {
        public string PlayerID { get; set; }
        public string PlayerName { get; set; }
        public string NickName { get; set; }

        public Nicknames()
        {
            
        }

        public Nicknames(string playerID, string playerName, string nickName)
        {
            PlayerID = playerID;
            PlayerName = playerName;
            NickName = nickName;
        }
    }

    internal class Player
    {
        public string PlayerID { get; set; }
        public string PlayerName { get; set; }

        public Player(string playerID, string playerName)
        {
            PlayerID = playerID;
            PlayerName = playerName;
        }
    }
}