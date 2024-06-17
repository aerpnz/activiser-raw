using System;

using System.Collections.Generic;
using System.Text;

namespace activiser.Library.MinorPlanet
{
    public class PositionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Event data supplied when the PositionChanged event is raised
        /// </summary>
        /// <param name="position">new GPS position information</param>
        public PositionChangedEventArgs(GpsPosition position)
        {
            this._position = position;
        }

        /// <summary>
        /// Gets the new position when the GPS reports a new position.
        /// </summary>
        public GpsPosition Position
        {
            get
            {
                return _position;
            }
        }

        private GpsPosition _position;

    }
}
