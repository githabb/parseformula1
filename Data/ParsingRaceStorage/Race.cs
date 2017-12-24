using System;
using System.Collections.Generic;

namespace Data.ParsingRaceStorage
{
    public partial class Race
    {
        public Race()
        {
            Result = new HashSet<Result>();
        }

        public int Id { get; set; }
        public string RaceName { get; set; }

        public ICollection<Result> Result { get; set; }
    }
}
