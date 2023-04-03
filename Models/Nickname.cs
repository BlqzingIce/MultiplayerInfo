namespace MultiplayerInfo.Models
{
    public class Nickname
    {
        public string PlayerId { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }

        //needed to avoid this error
        //The type initializer for 'IPA.Config.Stores.Converters.CustomObjectConverter`1' threw an exception. ---> System.ArgumentException: Config type does not have a public parameterless constructor
        public Nickname()
        {
            
        }

        public Nickname(string playerId, string name, string nick)
        {
            PlayerId = playerId;
            Name = name;
            Nick = nick;
        }
    }
}
