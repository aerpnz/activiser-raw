using System;

namespace activiser.Library.Gps
{
    /// <summary>
    /// Event args used for PositionChanged events.
    /// </summary>
    public class PositionChangedEventArgs: EventArgs
    {
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
