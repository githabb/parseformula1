using System;
using System.Collections.Generic;

namespace Data.ParsingRaceStorage
{
    public partial class Team
    {
        public Team()
        {
            Pilot = new HashSet<Pilot>();
        }

        public int Id { get; set; }
        public string Car { get; set; }

        public ICollection<Pilot> Pilot { get; set; }
    }
}
