using System;

namespace ParserLogic
{
    public class RacingResult
    {
        public string Pos { get; set; }

        public int No { get; set; }

        public string DriverFirstName { get; set; }

        public string DriverLastName { get; set; }

        public string DriverShortName { get; set; }

        public string Car { get; set; }

        public int Laps { get; set; }

        public TimeSpan? Time { get; set; }

        public TimeSpan Retired { get; set; }

        public int Pts { get; set; }



        public override string ToString()
        {
            return $"Pos: {Pos}, No: {No},  DriverFirstName: {DriverFirstName}, DriverLastName: {DriverLastName}, DriverShortName: {DriverShortName}, Car: {Car}, Laps: {Laps}, Time: {Time}, Retired: {Retired}, Pts: {Pts}";
        }
    }

}
