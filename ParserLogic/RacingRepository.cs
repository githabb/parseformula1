using System;
using System.Collections.Generic;
using System.Linq;
using Data.ParsingRaceStorage;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Data;

namespace ParserLogic
{
    public class RacingRepository : IRacingRepository
    {
        private readonly ParsingRaceContext _context;

        public RacingRepository(ParsingRaceContext context)
        {
            _context = context;
        }

        public RacingInformation Convert(RacingResult[] racingResult)
        {
            RacingInformation result = new RacingInformation();
            result.RaceData = new Race() { RaceName = "2017 FORMULA 1 PIRELLI BELGIAN GRAND PRIX" };

            result.Teams = new List<Team>();

            foreach (var item in racingResult)
            {
                Team team = result.Teams.FirstOrDefault(x => string.Equals(x.Car, item.Car, StringComparison.InvariantCultureIgnoreCase));
                if (team == null)
                {
                    team = new Team() { Car = item.Car };
                    result.Teams.Add(team);
                }

                Pilot pilot = team.Pilot.FirstOrDefault(x => x.Number == item.No);
                if (pilot == null)
                {
                    pilot = new Pilot() {FirstName = item.DriverFirstName, LastName = item.DriverLastName, Number = item.No, ShortName = item.DriverShortName };
                    var oneResult = new Result() { Laps = item.Laps, Pos = item.Pos, Pts = item.Pts, Retired = item.Retired, Time = item.Time, Race = result.RaceData };
                    pilot.Result = new List<Result>() {oneResult};

                    team.Pilot.Add(pilot);
                }

            }
            return result;
        }

        public async Task<bool> Save(RacingInformation info)
        {
            _context.Race.Attach(info.RaceData);

            _context.Team.AttachRange(info.Teams);

            var count = await _context.SaveChangesAsync();

            return count > 0;
        }
    }
}
