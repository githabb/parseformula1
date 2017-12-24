using System;
using System.Collections.Generic;

namespace Data.ParsingRaceStorage
{
    public partial class Result
    {
        public int Id { get; set; }
        public int Laps { get; set; }
        public TimeSpan? Time { get; set; }
        public TimeSpan Retired { get; set; }
        public int Pts { get; set; }
        public string Pos { get; set; }
        public int PilotId { get; set; }
        public int RaceId { get; set; }

        public Pilot Pilot { get; set; }
        public Race Race { get; set; }
    }
}
