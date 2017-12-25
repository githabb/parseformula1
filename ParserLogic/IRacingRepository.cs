using Data;
using System.Threading.Tasks;

namespace ParserLogic
{
    public interface IRacingRepository
    {
        RacingInformation Convert(RaceModel racingResult);

        Task<bool> Save(RacingInformation info);
    }

}
