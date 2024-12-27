namespace MultiplayerInfo.Models
{
    public class PlatformPlayer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PlatformId { get; set; }
        public int SSRank { get; set; }
        public int BLRank { get; set; }

        public PlatformPlayer(string id, string name, string platformId)
        {
            Id = id;
            Name = name;
            PlatformId = platformId;
            SSRank = -1;
            BLRank = -1;
        }

        public PlatformPlayer(BasicPlayer basicPlayer, string platformId)
        {
            Id = basicPlayer.Id;
            Name = basicPlayer.Name;
            PlatformId = platformId;
            SSRank = -1;
            BLRank = -1;
        }
    }
}
