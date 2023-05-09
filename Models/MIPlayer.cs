namespace MultiplayerInfo.Models
{
    public class MIPlayer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PlatformId { get; set; }
        public int SSRank { get; set; }
        public int BLRank { get; set; }

        public MIPlayer(string id, string name, string platformId)
        {
            Id = id;
            Name = name;
            PlatformId = platformId;
            SSRank = -1;
            BLRank = -1;
        }

        public MIPlayer(BasicPlayer basicPlayer, string platformId)
        {
            Id = basicPlayer.Id;
            Name = basicPlayer.Name;
            PlatformId = platformId;
            SSRank = -1;
            BLRank = -1;
        }
    }
}
