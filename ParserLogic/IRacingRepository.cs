using Data;
using System.Threading.Tasks;

namespace ParserLogic
{
    public interface IRacingRepository
    {
        RacingInformation Convert(RacingResult[] racingResult);

        Task<bool> Save(RacingInformation info);
    }

}
