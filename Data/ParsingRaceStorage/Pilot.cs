using System;
using System.Collections.Generic;

namespace Data.ParsingRaceStorage
{
    public partial class Pilot
    {
        public Pilot()
        {
            Result = new HashSet<Result>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public int TeamId { get; set; }

        public Team Team { get; set; }
        public ICollection<Result> Result { get; set; }
    }
}
