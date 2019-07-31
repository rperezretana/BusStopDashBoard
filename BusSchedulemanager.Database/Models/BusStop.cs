using System;
using System.Collections.Generic;
using System.Text;

namespace BusSchedulemanager.Database.Models
{
    /// <summary>
    /// The intention of this class is to keep track of the bus stops individually.
    /// Each will have a time span expected to be served, is a simple design for the simple example provided.
    /// A more complex design will be probably better elaborated.
    /// </summary>
    public class BusStop
    {
        /// <summary>
        /// Generic ID, for database. Prefer Guid normally but I will do number for visibility.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// A name to be identifiable by the user or developer. ie: Route 1, Route 2, etc.
        /// </summary>
        public string Name { get; set; }
    }
}
