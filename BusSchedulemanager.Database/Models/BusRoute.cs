using System;
using System.Collections.Generic;
using System.Text;

namespace BusSchedulemanager.Database.Models
{
    /// <summary>
    /// This wills tore the different routs, assuming we want to test more or less routs.
    /// </summary>
    public class BusRoute
    {
        /// <summary>
        /// Generic Guid for DB id (for the example will be a number)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Friendly user name.
        /// </summary>
        public string Name { get; set; }

        public List<int> NextStop { get; set; }
        public int ForStop { get; set; }
    }
}
