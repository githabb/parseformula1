using Data;
using Data.ParsingRaceStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ParserLogic
{
    public class RacingRepository : IRacingRepository
    {
        private readonly ParsingRaceContext _context;

        public RacingRepository(ParsingRaceContext context)
        {
            _context = context;
        }

        public RacingInformation Convert(RaceModel racingResult)
        {
            RacingInformation result = new RacingInformation();
            result.RaceData = new Race() { RaceName = racingResult.RaceName };

            result.Teams = new List<Team>();

            foreach (var item in racingResult.RaceResults)
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
                    pilot = new Pilot() { FirstName = item.DriverFirstName, LastName = item.DriverLastName, Number = item.No, ShortName = item.DriverShortName };
                    var oneResult = new Result() { Laps = item.Laps, Pos = item.Pos, Pts = item.Pts, Retired = item.Retired, Time = item.Time, Race = result.RaceData };
                    pilot.Result.Add( oneResult );

                    team.Pilot.Add(pilot);
                }

            }
            return result;
        }

        public async Task<bool> Save(RacingInformation info)
        {
            
            var raceInDb = await _context.Race.FirstOrDefaultAsync(r => r.RaceName == info.RaceData.RaceName);

            if (raceInDb != null)
            {
                return false;
            }

            _context.Race.Attach(info.RaceData);
            
            foreach (var item in info.Teams)
            {
                var team = await _context.Team.FirstOrDefaultAsync(t => t.Car == item.Car);              

                if (team == null)
                {
                    _context.Team.Attach(item);
                }
                else
                {
                    foreach (var pilot in item.Pilot)
                    {
                        var p = await _context.Pilot.FirstOrDefaultAsync(h => h.Number == pilot.Number);

                        if (p != null)
                        {
                            foreach (var result in pilot.Result)
                            {
                                result.PilotId = p.Id;
                                _context.Result.Attach(result);
                            }
                        }
                        else
                        {
                            pilot.TeamId = team.Id;
                            _context.Pilot.Attach(pilot);
                        }
                    }
                }                                            
            }                     

            var count = await _context.SaveChangesAsync();

            return count > 0;
        }
    }
}
