using System;
using System.Collections.Generic;
using System.Text;

namespace activiser.Library.Gps
{
    public class Satellite
    {
        public Satellite() { }
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
