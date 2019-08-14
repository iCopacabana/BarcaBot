using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Barcabot.Common.DataModels.Dto.FootballDataApi;
using Match = Barcabot.Common.DataModels.Match;
using Scorer = Barcabot.Common.DataModels.Scorer;

namespace Barcabot.Web
{
    public class FootballDataRetriever : IFootballDataRetriever
    {
        private readonly FootballDataApiRetrievalService _service;

        public FootballDataRetriever(FootballDataApiRetrievalService service)
        {
            _service = service;
        }

        public async Task<List<Scorer>> GetScorers(string competitionId)
        {
            var root = await _service.RetrieveData<ScorersRoot>(FootballDataUrls.TopScorers(competitionId));
            var playersList = root.Scorers;
            var playersListFirst5 = playersList.Take(5).ToList();
            var commonScorersList = new List<Scorer>();

            for (var i = 0; i < playersListFirst5.Count; i++)
            {
                var scorer = new Scorer
                {
                    ScorerId = i,
                    ScorerName = playersListFirst5[i].Player.Name,
                    ScorerTeam = playersListFirst5[i].Team.Name,
                    ScorerGoals = playersListFirst5[i].NumberOfGoals
                };

                commonScorersList.Add(scorer);
            }

            return commonScorersList;
        }

        public async Task<List<Match>> GetScheduledBarcaMatches()
        {
            var scheduledBarcaMatches = await _service.RetrieveData<TeamMatchesRoot>(FootballDataUrls.ScheduledMatches("81"));
            var nextMatchData = await _service.RetrieveData<MatchRoot>(FootballDataUrls.Match(scheduledBarcaMatches.Matches[0].Id.ToString()));
            var scheduledMatchesData = scheduledBarcaMatches.Matches;

            var matchesList = new List<Match>();
            
            //add next match
            var nextMatch = new Match
            {
                MatchId = 0,
                MatchCompetition = nextMatchData.Match.Competition.Name,
                MatchDate = nextMatchData.Match.UtcDate,
                MatchStadium = nextMatchData.Match.Venue,
                MatchHomeTeam = nextMatchData.Match.HomeTeam.Name,
                MatchAwayTeam = nextMatchData.Match.AwayTeam.Name,
                MatchTotalMatches = int.Parse(nextMatchData.Head2Head.NumberOfMatches),
                MatchTotalGoals = nextMatchData.Head2Head.TotalGoals
            };

            if (nextMatchData.Match.HomeTeam.Name == "FC Barcelona")
            {
                nextMatch.MatchWins = nextMatchData.Head2Head.HomeTeam.Wins;
                nextMatch.MatchDraws = nextMatchData.Head2Head.HomeTeam.Draws;
                nextMatch.MatchLosses = nextMatchData.Head2Head.HomeTeam.Losses;
            }
            else
            {
                nextMatch.MatchWins = nextMatchData.Head2Head.AwayTeam.Wins;
                nextMatch.MatchDraws = nextMatchData.Head2Head.AwayTeam.Draws;
                nextMatch.MatchLosses = nextMatchData.Head2Head.AwayTeam.Losses;
            }

            matchesList.Add(nextMatch);

            //add the other matches (less detailed)
            for (var i = 1; i < scheduledMatchesData.Count; i++)
            {
                var match = new Match
                {
                    MatchId = i,
                    MatchCompetition = scheduledMatchesData[i].Competition.Name,
                    MatchDate = scheduledMatchesData[i].UtcDate,
                    MatchHomeTeam = scheduledMatchesData[i].HomeTeam.Name,
                    MatchAwayTeam = scheduledMatchesData[i].AwayTeam.Name
                };

                matchesList.Add(match);
            }

            return matchesList;
        }
    }
}