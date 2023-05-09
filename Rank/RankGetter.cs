using Newtonsoft.Json;
using SiraUtil.Logging;
using SiraUtil.Web;
using Zenject;

namespace MultiplayerInfo.Rank
{
    public class RankGetter
    {
        [Inject] private readonly IHttpService _httpService = null!;

        public async void GetScoreSaberRank(int index, string platformId)
        {
            IHttpResponse web = await _httpService.GetAsync("https://scoresaber.com/api/player/" + platformId + "/basic");
            if (web.Successful)
            {
                string result = await web.ReadAsStringAsync();
                ScoreSaberPlayer json = JsonConvert.DeserializeObject<ScoreSaberPlayer>(result);
                MpPlayerPatch.cachedPlayerList[index].SSRank = json.rank;
            }
        }

        public async void GetBeatLeaderRank(int index, string platformId)
        {
            IHttpResponse web = await _httpService.GetAsync("https://api.beatleader.xyz/player/" + platformId + "?stats=false");
            if (web.Successful)
            {
                string result = await web.ReadAsStringAsync();
                BeatLeaderPlayer json = JsonConvert.DeserializeObject<BeatLeaderPlayer>(result);
                MpPlayerPatch.cachedPlayerList[index].BLRank = json.rank;
            }
        }
    }

    class ScoreSaberPlayer
    {
        public string id { get; set; }
        public string name { get; set; }
        public string profilePicture { get; set; }
        public string bio { get; set; }
        public string country { get; set; }
        public double pp { get; set; }
        public int rank { get; set; }
        public int countryRank { get; set; }
        public object role { get; set; }
        public object badges { get; set; }
        public string histories { get; set; }
        public int permissions { get; set; }
        public bool banned { get; set; }
        public bool inactive { get; set; }
        public object scoreStats { get; set; }
    }

    class ProfileSettings
    {
        public int id { get; set; }
        public object bio { get; set; }
        public object message { get; set; }
        public string effectName { get; set; }
        public string profileAppearance { get; set; }
        public object hue { get; set; }
        public object saturation { get; set; }
        public object leftSaberColor { get; set; }
        public object rightSaberColor { get; set; }
        public object profileCover { get; set; }
        public string starredFriends { get; set; }
    }

    class BeatLeaderPlayer
    {
        public int mapperId { get; set; }
        public bool banned { get; set; }
        public bool inactive { get; set; }
        public object banDescription { get; set; }
        public string externalProfileUrl { get; set; }
        public object history { get; set; }
        public object badges { get; set; }
        public object pinnedScores { get; set; }
        public object changes { get; set; }
        public double accPp { get; set; }
        public double passPp { get; set; }
        public double techPp { get; set; }
        public object scoreStats { get; set; }
        public double lastWeekPp { get; set; }
        public int lastWeekRank { get; set; }
        public int lastWeekCountryRank { get; set; }
        public object eventsParticipating { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string platform { get; set; }
        public string avatar { get; set; }
        public string country { get; set; }
        public double pp { get; set; }
        public int rank { get; set; }
        public int countryRank { get; set; }
        public string role { get; set; }
        public object socials { get; set; }
        public object patreonFeatures { get; set; }
        public ProfileSettings profileSettings { get; set; }
        public object clans { get; set; }
    }
}