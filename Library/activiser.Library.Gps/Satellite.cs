using System;
using System.Collections.Generic;
using System.Text;

namespace activiser.Library.Gps
{
    /// <summary>
    /// Satellite information
    /// </summary>
    public class Satellite
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Satellite() { }

        /// <summary>
        /// Constructor used when rebuilding information from historical data (rather than directly from 
        /// the GPS system.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="elevation"></param>
        /// <param name="azimuth"></param>
        /// <param name="signalStrength"></param>
        public Satellite(int id, int elevation, int azimuth, int signalStrength)
        {
            this.id = id;
            this.elevation = elevation;
            this.azimuth = azimuth;
            this.signalStrength = signalStrength;
        }

        int id;
        /// <summary>
        /// Id of the satellite
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }


        int elevation;
        /// <summary>
        /// Elevation of the satellite
        /// </summary>
        public int Elevation
        {
            get
            {
                return elevation;
            }
            set
            {
                elevation = value;
            }
        }


        int azimuth;
        /// <summary>
        /// Azimuth of the satellite
        /// </summary>
        public int Azimuth
        {
            get
            {
                return azimuth;
            }
            set
            {
                azimuth = value;
            }
        }


        int signalStrength;
        /// <summary>
        /// SignalStrenth of the satellite
        /// </summary>
        public int SignalStrength
        {
            get
            {
                return signalStrength;
            }
            set
            {
                signalStrength = value;
            }
        }

    }
}
