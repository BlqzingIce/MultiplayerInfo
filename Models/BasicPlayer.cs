namespace MultiplayerInfo.Models
{
    public class BasicPlayer
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public BasicPlayer(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
