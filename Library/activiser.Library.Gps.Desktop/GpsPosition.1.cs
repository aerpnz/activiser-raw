//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//
//
// Use of this sample source code is subject to the terms of the Microsoft
// license agreement under which you licensed this sample source code. If
// you did not accept the terms of the license agreement, you are not
// authorized to use this sample source code. For the terms of the license,
// please see the license agreement between you and Microsoft or, if applicable,
// see the LICENSE.RTF on your install media or the root of your tools installation.
// THE SAMPLE SOURCE CODE IS PROVIDED "AS IS", WITH NO WARRANTIES OR INDEMNITIES.
//
#region Using directives

using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text.RegularExpressions;

#endregion

namespace activiser.Library.Gps
{
    [StructLayout(LayoutKind.Sequential)]
    public class GpsPosition : IFormattable
    {
        internal GpsPosition() { }


        public GpsPosition(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            GpsPosition tmp;
            // don't really like this... try parsing with each of our supported formats....
            if (!TryParse(value, "a", null, out tmp))
                if (!TryParse(value, "i", null, out tmp))
                    if (!TryParse(value, "n", null, out tmp))
                        if (!TryParse(value, "g", null, out tmp))
                            if (!TryParse(value, "d", null, out tmp))
                                throw new FormatException("Unrecognised or invalid location value");

            CopyFromTemp(tmp, DateTime.MinValue);

            tmp = null;
        }

        public GpsPosition(string value, DateTime timeOverride)
        {
            if (value == null) throw new ArgumentNullException("value");
            GpsPosition tmp;
            // don't really like this... try parsing with each of our supported formats....
            if (!TryParse(value, "a", null, out tmp))
                if (!TryParse(value, "i", null, out tmp))
                    if (!TryParse(value, "n", null, out tmp))
                        if (!TryParse(value, "g", null, out tmp))
                            if (!TryParse(value, "d", null, out tmp))
                                throw new FormatException("Unrecognised or invalid location value");

            CopyFromTemp(tmp, timeOverride);

            tmp = null;
        }

        public GpsPosition(string value, string format)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (format == null) throw new ArgumentNullException("format");
            GpsPosition tmp;
            if (!TryParse(value, format, null, out tmp))
                            throw new FormatException("Unrecognised or invalid location value");
            CopyFromTemp(tmp, DateTime.MinValue);
            tmp = null;
        }

        public GpsPosition(string value, string format, DateTime timeOverride)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (format == null) throw new ArgumentNullException("format");
            GpsPosition tmp;
            if (!TryParse(value, format, null, out tmp))
                throw new FormatException("Unrecognised or invalid location value");
            CopyFromTemp(tmp, timeOverride);
            tmp = null;
        }

        private void CopyFromTemp(GpsPosition tmp, DateTime timeOverride)
        {
            this.Latitude = tmp.Latitude;
            this.Longitude = tmp.Longitude;
            this.SeaLevelAltitude = tmp.SeaLevelAltitude;
            this.Speed = tmp.Speed;
            this.Heading = tmp.Heading;
            this.MagneticVariation = tmp.MagneticVariation;
            this.Time = (timeOverride != DateTime.MinValue) ? timeOverride : tmp.Time;
        }
        //public GpsPosition(string value, DateTime timeOverride)
        //{
        //    if (value == null) throw new ArgumentNullException("value");

        //    GpsPosition tmp;
        //    if (TryParse(value, "i", null, out tmp))
        //    {
        //        this.Latitude = tmp.Latitude;
        //        this.Longitude = tmp.Longitude;
        //        this.SeaLevelAltitude = tmp.SeaLevelAltitude;
        //    }
        //    else
        //    {
        //        throw new FormatException("Invalid ISO Location");
        //    }

        //    if (timeOverride != null && timeOverride != DateTime.MinValue)
        //    {
        //        this._utcTime = new SystemTime(timeOverride);
        //        _validFields |= GPS_VALID_UTC_TIME;
        //    }
        //}

        #region ValidityFlags

        internal static int GPS_VALID_UTC_TIME = 0x00000001;
        internal static int GPS_VALID_LATITUDE = 0x00000002;
        internal static int GPS_VALID_LONGITUDE = 0x00000004;
        internal static int GPS_VALID_SPEED = 0x00000008;
        internal static int GPS_VALID_HEADING = 0x00000010;
        internal static int GPS_VALID_MAGNETIC_VARIATION = 0x00000020;
        internal static int GPS_VALID_ALTITUDE_WRT_SEA_LEVEL = 0x00000040;
        internal static int GPS_VALID_ALTITUDE_WRT_ELLIPSOID = 0x00000080;
        internal static int GPS_VALID_POSITION_DILUTION_OF_PRECISION = 0x00000100;
        internal static int GPS_VALID_HORIZONTAL_DILUTION_OF_PRECISION = 0x00000200;
        internal static int GPS_VALID_VERTICAL_DILUTION_OF_PRECISION = 0x00000400;
        internal static int GPS_VALID_SATELLITE_COUNT = 0x00000800;
        internal static int GPS_VALID_SATELLITES_USED_PRNS = 0x00001000;
        internal static int GPS_VALID_SATELLITES_IN_VIEW = 0x00002000;
        internal static int GPS_VALID_SATELLITES_IN_VIEW_PRNS = 0x00004000;
        internal static int GPS_VALID_SATELLITES_IN_VIEW_ELEVATION = 0x00008000;
        internal static int GPS_VALID_SATELLITES_IN_VIEW_AZIMUTH = 0x00010000;
        internal static int GPS_VALID_SATELLITES_IN_VIEW_SIGNAL_TO_NOISE_RATIO = 0x00020000;

        #endregion

        internal int _version = 1;                          // Current version of GPSID client is using.
        internal int _size = 0;                             // sizeof(_GPS_POSITION)

        // Not all fields in the structure below are guaranteed to be valid.  
        // Which fields are valid depend on GPS device being used, how stale the API allows
        // the data to be, and current signal.
        internal int _validFields = 0;                      // Valid fields are specified in dwValidFields, based on GPS_VALID_XXX flags.

        internal int _flags = 0;                            // Additional information about this position structure (GPS_DATA_FLAGS_XXX)

        //** Time related
        internal SystemTime _utcTime = new SystemTime(); 	//  UTC according to GPS clock.

        //** Position + heading related
        internal double _latitude = 0.0;                    // Degrees latitude.  North is positive
        internal double _longitude = 0.0;                   // Degrees longitude.  East is positive
        internal float _speed = 0.0f;                       // Speed in knots
        internal float _heading = 0.0f;                     // Degrees heading (course made good).  True North=0
        internal double _magneticVariation = 0.0;           // Magnetic variation.  East is positive
        internal float _altitudeWRTSeaLevel = 0.0f;         // Altitute with regards to sea level, in meters
        internal float _altitudeWRTEllipsoid = 0.0f;        // Altitude with regards to ellipsoid, in meters

        //** Quality of this fix
        internal GpsFixQuality _fixQuality = GpsFixQuality.Unknown;         // Where did we get fix from? 
        internal GpsFixType _fixType = GpsFixType.Unknown;                  // Is this 2d or 3d fix?  
        internal GpsFixSelection _selectionType = GpsFixSelection.Unknown;  // Auto or manual selection between 2d or 3d mode
        internal float _positionDilutionOfPrecision = 0.0f;                 // Position Dilution Of Precision
        internal float _horizontalDilutionOfPrecision = 0.0f;               // Horizontal Dilution Of Precision
        internal float _verticalDilutionOfPrecision = 0.0f;                 // Vertical Dilution Of Precision

        //** Satellite information
        internal int _satelliteCount = 0;                                                       // Number of satellites used in solution
        internal SatelliteArray _satellitesUsedPRNs = new SatelliteArray();                     // PRN numbers of satellites used in the solution
        internal int _satellitesInView = 0;                      	                            // Number of satellites in view.  From 0-GPS_MAX_SATELLITES
        internal SatelliteArray _satellitesInViewPRNs = new SatelliteArray();                   // PRN numbers of satellites in view                
        internal SatelliteArray _satellitesInViewElevation = new SatelliteArray();              // Elevation of each satellite in view          
        internal SatelliteArray _satellitesInViewAzimuth = new SatelliteArray();                // Azimuth of each satellite in view             
        internal SatelliteArray _satellitesInViewSignalToNoiseRatio = new SatelliteArray();     // Signal to noise ratio of each satellite in view

        /// <summary>
        /// UTC according to GPS clock.
        /// </summary>
        public DateTime Time
        {
            get { return _utcTime.ToDateTime(); }
            set
            {
                if (value != DateTime.MinValue)
                {
                    _utcTime = new SystemTime(value);
                    this._validFields |= GPS_VALID_UTC_TIME;
                }
                else
                {
                    _utcTime = new SystemTime();
                    this._validFields ^= GPS_VALID_UTC_TIME;
                }
            }
        }
        /// <summary>
        /// True if the Time property is valid, false if invalid
        /// </summary>
        public bool TimeValid
        {
            get { return ((_validFields & GPS_VALID_UTC_TIME) != 0) ;} 
        }

        /// <summary>
        /// Satellites used in the solution
        /// </summary>
        /// <returns>Array of Satellites</returns>
        public Satellite[] GetSatellitesInSolution()
        {
            Satellite[] inViewSatellites = GetSatellitesInView();
            ArrayList list = new ArrayList();
            for (int index = 0; index < _satelliteCount; index++)
            {
                Satellite found = null;
                for (int viewIndex = 0; viewIndex < inViewSatellites.Length && found == null; viewIndex++)
                {
                    if (_satellitesUsedPRNs[index] == inViewSatellites[viewIndex].Id)
                    {
                        found = inViewSatellites[viewIndex];
                        list.Add(found);
                    }
                }
            }

            return (Satellite[])list.ToArray(typeof(Satellite));
        }

        /// <summary>
        /// True if the SatellitesInSolution property is valid, false if invalid
        /// </summary>
        public bool SatellitesInSolutionValid
        {
            get { return (_validFields & GPS_VALID_SATELLITES_USED_PRNS) != 0; }
        }

        /// <summary>
        /// Satellites in view
        /// </summary>
        /// <returns>Array of Satellites</returns>
        public Satellite[] GetSatellitesInView()
        {
            Satellite[] satellites = null;
            if (_satellitesInView != 0)
            {
                satellites = new Satellite[_satellitesInView];
                for (int index = 0; index < satellites.Length; index++)
                {
                    satellites[index] = new Satellite();
                    satellites[index].Azimuth = _satellitesInViewAzimuth[index];
                    satellites[index].Elevation = _satellitesInViewElevation[index];
                    satellites[index].Id = _satellitesInViewPRNs[index];
                    satellites[index].SignalStrength = _satellitesInViewSignalToNoiseRatio[index];
                }
            }

            return satellites;
        }

        /// <summary>
        /// True if the SatellitesInView property is valid, false if invalid
        /// </summary>
        public bool SatellitesInViewValid
        {
            get { return (_validFields & GPS_VALID_SATELLITES_IN_VIEW) != 0; }
        }


        /// <summary>
        /// Number of satellites used in solution
        /// </summary>
        public int SatelliteCount
        {
            get { return _satelliteCount; }
        }
        /// <summary>
        /// True if the SatelliteCount property is valid, false if invalid
        /// </summary>
        public bool SatelliteCountValid
        {
            get { return (_validFields & GPS_VALID_SATELLITE_COUNT) != 0 && _satelliteCount != 50; } // sanity check 2008/01/15- $GPGGA seems to be reporting 50 satellites with an invalid fix
        }

        /// <summary>
        /// Number of satellites in view.  
        /// </summary>
        public int SatellitesInViewCount
        {
            get { return _satellitesInView; }
        }
        /// <summary>
        /// True if the SatellitesInViewCount property is valid, false if invalid
        /// </summary>
        public bool SatellitesInViewCountValid
        {
            get { return (_validFields & GPS_VALID_SATELLITES_IN_VIEW) != 0; }
        }

        /// <summary>
        /// Speed in knots
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            private set
            {
                if (!float.IsNaN(value))
                {
                    _speed = value;
                    _validFields |= GPS_VALID_SPEED;
                }
                else
                {
                    _speed = float.NaN;
                    _validFields &= -GPS_VALID_SPEED; // invalidate speed
                }
            }
        }
        /// <summary>
        /// Speed in knots
        /// </summary>
        public float SpeedInKnots
        {
            get { return _speed; }
        }
        /// <summary>
        /// True if the Speed property is valid, false if invalid
        /// </summary>
        public bool SpeedValid
        {
            get { return (_validFields & GPS_VALID_SPEED) != 0; }
        }

        const float KnotsToFPS = 1.687810f;
        const float KnotsToMPH = 1.150779f;
        const float KnotsToKMH = 1.852f;
        const float KnotsToMS = 1.852f / 3600.0f; // metres per second
        const float KnotsToFPM = 101.268591f; // feet per minute

        public float SpeedInKilometresPerHour
        {
            get
            {
                return _speed * KnotsToKMH;
            }
        }
        
        public float SpeedInMilesPerHour
        {
            get
            {
                return _speed * KnotsToMPH;
            }
        }

        public float SpeedInMetresPerSecond
        {
            get
            {
                return _speed * KnotsToMS;
            }
        }

        public float SpeedInFeetPerSecond
        {
            get
            {
                return _speed * KnotsToFPS;
            }
        }

        public float SpeedInFeetPerMinute
        {
            get
            {
                return _speed * KnotsToFPM;
            }
        }
        /// <summary>
        /// Altitude with regards to ellipsoid, in meters
        /// </summary>
        public float EllipsoidAltitude
        {
            get { return _altitudeWRTEllipsoid; }
        }
        /// <summary>
        /// True if the EllipsoidAltitude property is valid, false if invalid
        /// </summary>
        public bool EllipsoidAltitudeValid
        {
            get { return (_validFields & GPS_VALID_ALTITUDE_WRT_ELLIPSOID) != 0; }
        }

        /// <summary>
        /// Altitute with regards to sea level, in meters
        /// </summary>
        public float SeaLevelAltitude
        {
            get { return _altitudeWRTSeaLevel; }
            private set
            {
                if (!float.IsNaN(value))
                {
                    _validFields |= GPS_VALID_ALTITUDE_WRT_SEA_LEVEL;
                    _altitudeWRTSeaLevel = value;
                }
            }
        }
        /// <summary>
        /// True if the SeaLevelAltitude property is valid, false if invalid
        /// </summary>
        public bool SeaLevelAltitudeValid
        {
            get { return ((_validFields & GPS_VALID_ALTITUDE_WRT_SEA_LEVEL) != 0) && (!float.IsNaN(_altitudeWRTSeaLevel)); }
        }

        /// <summary>
        /// Latitude in decimal degrees.  North is positive
        /// </summary>
        public double Latitude
        {
            get { return _latitude; }
            private set
            {
                _latitude = value;
                if (!double.IsNaN(value))
                {
                    _validFields |= GPS_VALID_LATITUDE;
                }
                else
                {
                    _validFields &= ~GPS_VALID_LATITUDE;
                }
            }
        }

        static double _lastLatitude = 0;
        static double _isoLatitude = 0;
        static DegreesMinutesSeconds _latitudeDms = null;
        
        /// <summary>
        /// Latitude in ISO decimal DDMMSS.ffff.  North is positive
        /// </summary>
        public double IsoLatitude
        {
            get {
                if (_lastLatitude != _latitude)
                {
                    _isoLatitude = LatitudeInDegreesMinutesSeconds.ToIso6709();
                }
                return _isoLatitude; 
            }
        }
        
        /// <summary>
        /// Latitude in degrees, minutes, seconds.  North is positive
        /// </summary>

        public DegreesMinutesSeconds LatitudeInDegreesMinutesSeconds
        {
            get {
                if (_lastLatitude != _latitude || _latitudeDms == null) 
                {
                    _latitudeDms = new DegreesMinutesSeconds(_latitude);
                    _lastLatitude = _latitude;
                }
                return _latitudeDms;
            }
        }

        /// <summary>
        /// True if the Latitude property is valid, false if invalid
        /// </summary>
        public bool LatitudeValid
        {
            get { return ((_validFields & GPS_VALID_LATITUDE) != 0) && (_latitude != 360.0) && (!double.IsNaN(_latitude)); }
        }

        /// <summary>
        /// Longitude in decimal degrees.  East is positive
        /// </summary>
        public double Longitude
        {
            get { return _longitude; }
            private set
            {
                _longitude = value;
                if (!double.IsNaN(value))
                {
                    _validFields |= GPS_VALID_LONGITUDE;
                }
                else
                {
                    _validFields &= ~GPS_VALID_LONGITUDE;
                }
            }
        }

        static double _lastLongitude = 0;
        static double _isoLongitude = 0;
        static DegreesMinutesSeconds _longitudeDms = null;

        /// <summary>
        /// Longitude in ISO decimal DDMMSS.ffff.  North is positive
        /// </summary>
        public double IsoLongitude
        {
            get
            {
                if (_lastLongitude != _longitude)
                {
                    _isoLongitude = LongitudeInDegreesMinutesSeconds.ToIso6709();
                }
                return _isoLongitude;
            }
        }
        /// <summary>
        /// Longitude in degrees, minutes, seconds.  East is positive
        /// </summary>
        public DegreesMinutesSeconds LongitudeInDegreesMinutesSeconds
        {
            get
            {
                if (_lastLongitude != _longitude || _longitudeDms == null)
                {
                    _longitudeDms = new DegreesMinutesSeconds(_longitude);
                    _lastLongitude = _longitude;
                }
                return _longitudeDms;
            }
        }

        /// <summary>
        /// True if the Longitude property is valid, false if invalid
        /// </summary>
        public bool LongitudeValid
        {
            get { return ((_validFields & GPS_VALID_LONGITUDE) != 0) && (_longitude != 720.0) && (!double.IsNaN(_longitude)); }
        }

        /// <summary>
        /// Degrees heading (course made good).  True North=0
        /// </summary>
        public float Heading
        {
            get { return _heading; }
            private set
            {
                _heading = value;
                if (!float.IsNaN(value))
                {
                    _validFields |= GPS_VALID_HEADING;
                }
                else
                {
                    _validFields &= ~GPS_VALID_HEADING;
                }
            }
        }

        public double HeadingInRadians
        {
            get
            {
                return (180-(_heading+90)) * Math.PI / 180.0;
            }
        }
        /// <summary>
        /// Heading in degrees, minutes, seconds.  East is positive
        /// </summary>
        public DegreesMinutesSeconds HeadingInDegreesMinutesSeconds
        {
            get { return new DegreesMinutesSeconds(_heading); }
        }

        /// <summary>
        /// True if the Heading property is valid, false if invalid
        /// </summary>
        public bool HeadingValid
        {
            get { return (_validFields & GPS_VALID_HEADING) != 0 && !float.IsNaN(_heading); }
        }

        /// <summary>
        /// Magentic variation in degrees, East is positive
        /// </summary>
        public double MagneticVariation
        {
            get { return _magneticVariation; }
            private set
            {
                _magneticVariation = value;
                if (!double.IsNaN(value))
                {
                    _validFields |= GPS_VALID_MAGNETIC_VARIATION;
                }
                else
                {
                    _validFields &= ~GPS_VALID_MAGNETIC_VARIATION;
                }
            }
        }

        /// <summary>
        /// True if the Heading property is valid, false if invalid
        /// </summary>
        public bool MagneticVariationValid
        {
            get { return (_validFields & GPS_VALID_MAGNETIC_VARIATION) != 0 && !double.IsNaN(_magneticVariation); }
        }

        /// <summary>
        /// Position Dilution Of Precision
        /// </summary>
        public float PositionDilutionOfPrecision
        {
            get { return _positionDilutionOfPrecision; }
        }
        /// <summary>
        /// True if the PositionDilutionOfPrecision property is valid, false if invalid
        /// </summary>
        public bool PositionDilutionOfPrecisionValid
        {
            get { return (_validFields & GPS_VALID_POSITION_DILUTION_OF_PRECISION) != 0; }
        }

        /// <summary>
        /// Horizontal Dilution Of Precision
        /// </summary>
        public float HorizontalDilutionOfPrecision
        {
            get { return _horizontalDilutionOfPrecision; }
        }
        /// <summary>
        /// True if the HorizontalDilutionOfPrecision property is valid, false if invalid
        /// </summary>
        public bool HorizontalDilutionOfPrecisionValid
        {
            get { return (_validFields & GPS_VALID_HORIZONTAL_DILUTION_OF_PRECISION) != 0; }
        }

        /// <summary>
        /// Vertical Dilution Of Precision
        /// </summary>
        public float VerticalDilutionOfPrecision
        {
            get { return _verticalDilutionOfPrecision; }
        }
        /// <summary>
        /// True if the VerticalDilutionOfPrecision property is valid, false if invalid
        /// </summary>
        public bool VerticalDilutionOfPrecisionValid
        {
            get { return (_validFields & GPS_VALID_VERTICAL_DILUTION_OF_PRECISION) != 0; }
        }

        public bool PositionValid
        {
            get
            {
                return this.SatelliteCountValid && this.LatitudeValid && this.LongitudeValid && this.SeaLevelAltitudeValid && this.TimeValid;
            }
        }

        /// <summary>
        /// Format location information.
        /// </summary>
        /// <param name="format">valid values are a,i,f,F,l,L,d,m,s,n,g
        ///     notes: x = latitude, y = longitude, z = altitude, t = time (UTC unless otherwise stated)
        ///     a = 'activiser' format: x,y,z,t
        ///     i = 'ISO6709' format: +/-xx.xxxx+/-yyy.yyyy+/-zzzz
        ///     f, G = x°x'x" N/S, y°y'y"E/W @ t 
        ///     F = x°x'x" N/S, y°y'y"E/W \n @ t 
        ///     l, L = same as f, F, except time converted to local time.
        ///     d = xx.xxxx°N/S,yyy.yyyy°E/W,zzzz
        ///     m = xx°xx'N/S,yyy°yy'E/W,zzzz
        ///     s = xx°xx'xx"N/S,yyy°yy'yy"E/W,zzzz
        ///     n,g = NMEA GPS 'GPRMC' sentence: $GPRMC,t:HHmmss,A,xxxx.xx,N/S,yyy.yyyy,E/W,speed,heading,t:ddMMyy,magnetic variation,E/W,checksum
        /// </param>
        /// <returns>string</returns>
        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        public override string ToString ( ) 
        {
            return this.ToString("G");
        }

        //public string ToIso6709String()
        //{
        //    return this.ToString("i");
        //}

        /// <summary>
        /// Gets an checksum for a NMEA GPS 'sentence'.
        /// this is a stripped-down version that probly won't work outside of this context.
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private static string getNmeaChecksum(string sentence)
        {
            int checksum = Convert.ToByte(sentence[sentence.IndexOf('$') + 1]);
            for (int i = 2; i < sentence.Length; i++) // Loop through all chars to get a checksum
            {
                if (sentence[i] == '*') break;
                checksum ^= Convert.ToByte(sentence[i]); // No. XOR the checksum with this character's value
            }
            return checksum.ToString("X2"); // Return the checksum formatted as a two-character hexadecimal
        }

        #region IFormattable Members

        /// <summary>
        /// Format location information.
        /// </summary>
        /// <param name="format">valid values are a,i,f,F,l,L,d,m,s,n,g
        ///     notes: x = latitude, y = longitude, z = altitude, t = time (UTC unless otherwise stated)
        ///     a = 'activiser' format: x,y,z,t
        ///     i = 'ISO6709' format: +/-xx.xxxx+/-yyy.yyyy+/-zzzz
        ///     f, g, G = x°x'x" N/S, y°y'y"E/W @ t 
        ///     F = x°x'x" N/S, y°y'y"E/W \n @ t (two-line version of f)
        ///     l, L = same as f, F, except time converted to local time.
        ///     d = xx.xxxx°N/S,yyy.yyyy°E/W,zzzz
        ///     m = xx°xx'N/S,yyy°yy'E/W,zzzz
        ///     s = xx°xx'xx"N/S,yyy°yy'yy"E/W,zzzz
        ///     n = NMEA GPS 'GPRMC' sentence: $GPRMC,t:HHmmss,A,xxxx.xx,N/S,yyy.yyyy,E/W,speed,heading,t:ddMMyy,magnetic variation,E/W,checksum
        /// </param>
        /// <param name="formatProvider">ignored</param>
        /// <returns>string</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "a":
                    return string.Format("{0},{1},{2},{3:u}", this.Latitude, this.Longitude, this.SeaLevelAltitude, this.Time);

                case "i":
                    return string.Format(
                            "{0:+00.0000;-00.0000}{1:+000.0000;-000.0000}{2}",
                            this.Latitude, this.Longitude,
                            this.SeaLevelAltitudeValid ? this.SeaLevelAltitude.ToString("+0000;-0000") : string.Empty
                    );

                case "f":
                case "g":
                case "G":
                    return string.Format(
                            "{0:00}°{1:00}'{2:00}\"{3}, {4:000}°{5:00}'{6:00}\"{7} @ {8} ",
                            this.LatitudeInDegreesMinutesSeconds.Degrees,
                            this.LatitudeInDegreesMinutesSeconds.Minutes,
                            this.LatitudeInDegreesMinutesSeconds.Seconds,
                            this.LatitudeInDegreesMinutesSeconds.IsPositive ? 'N' : 'S',
                            this.LongitudeInDegreesMinutesSeconds.Degrees,
                            this.LongitudeInDegreesMinutesSeconds.Minutes,
                            this.LongitudeInDegreesMinutesSeconds.Seconds,
                            this.LongitudeInDegreesMinutesSeconds.IsPositive ? 'E' : 'W',
                            this.Time.ToString("g")
                    );
                case "F":
                    return string.Format(
                            "{0:00}°{1:00}'{2:00}\"{3}, {4:000}°{5:00}'{6:00}\"{7} \n@ {8} ",
                            this.LatitudeInDegreesMinutesSeconds.Degrees,
                            this.LatitudeInDegreesMinutesSeconds.Minutes,
                            this.LatitudeInDegreesMinutesSeconds.Seconds,
                            this.LatitudeInDegreesMinutesSeconds.IsPositive ? 'N' : 'S',
                            this.LongitudeInDegreesMinutesSeconds.Degrees,
                            this.LongitudeInDegreesMinutesSeconds.Minutes,
                            this.LongitudeInDegreesMinutesSeconds.Seconds,
                            this.LongitudeInDegreesMinutesSeconds.IsPositive ? 'E' : 'W',
                            this.Time.ToString("g")
                    );

                case "l":
                    return string.Format(
                            "{0:00}°{1:00}'{2:00}\"{3}, {4:000}°{5:00}'{6:00}\"{7} @ {8} ",
                            this.LatitudeInDegreesMinutesSeconds.Degrees,
                            this.LatitudeInDegreesMinutesSeconds.Minutes,
                            this.LatitudeInDegreesMinutesSeconds.Seconds,
                            this.LatitudeInDegreesMinutesSeconds.IsPositive ? 'N' : 'S',
                            this.LongitudeInDegreesMinutesSeconds.Degrees,
                            this.LongitudeInDegreesMinutesSeconds.Minutes,
                            this.LongitudeInDegreesMinutesSeconds.Seconds,
                            this.LongitudeInDegreesMinutesSeconds.IsPositive ? 'E' : 'W',
                            this.Time.ToLocalTime().ToString("g")
                    );
                case "L":
                    return string.Format(
                            "{0:00}°{1:00}'{2:00}\"{3}, {4:000}°{5:00}'{6:00}\"{7} \n@ {8} ",
                            this.LatitudeInDegreesMinutesSeconds.Degrees,
                            this.LatitudeInDegreesMinutesSeconds.Minutes,
                            this.LatitudeInDegreesMinutesSeconds.Seconds,
                            this.LatitudeInDegreesMinutesSeconds.IsPositive ? 'N' : 'S',
                            this.LongitudeInDegreesMinutesSeconds.Degrees,
                            this.LongitudeInDegreesMinutesSeconds.Minutes,
                            this.LongitudeInDegreesMinutesSeconds.Seconds,
                            this.LongitudeInDegreesMinutesSeconds.IsPositive ? 'E' : 'W',
                            this.Time.ToLocalTime().ToString("g")
                    );
                case "d":
                    return string.Format("{0:00.0000}°{1},{2:000.0000}°{3},{4:+0000;-0000}",
                            this.Latitude, this.Latitude < 0 ? "S" : "N",
                            this.Longitude, this.Longitude < 0 ? "W" : "E",
                            this.SeaLevelAltitude
                    );

                case "m":
                    return string.Format("{0:00}°{1:00}.{2:000}'{3},{4:000}°{5:00}.{6:000}'{7},{8:+0000;-0000}",
                        this.LatitudeInDegreesMinutesSeconds.Degrees,
                        this.LatitudeInDegreesMinutesSeconds.Minutes,
                        this.LatitudeInDegreesMinutesSeconds.Seconds * 1000 / 60,
                        this.LatitudeInDegreesMinutesSeconds.IsPositive ? 'N' : 'S',
                        this.LongitudeInDegreesMinutesSeconds.Degrees,
                        this.LongitudeInDegreesMinutesSeconds.Minutes,
                        this.LongitudeInDegreesMinutesSeconds.Seconds * 1000 / 60,
                        this.LongitudeInDegreesMinutesSeconds.IsPositive ? 'E' : 'W',
                        this.SeaLevelAltitude
                        );

                case "s":
                    return string.Format("{0:00}°{1:00}\'{2:00}\"{3},{4:000}°{5:00}\'{6:00}\"{7},{8:+0000;-0000}",
                        this.LatitudeInDegreesMinutesSeconds.Degrees,
                        this.LatitudeInDegreesMinutesSeconds.Minutes,
                        this.LatitudeInDegreesMinutesSeconds.Seconds,
                        this.LatitudeInDegreesMinutesSeconds.IsPositive ? 'N' : 'S',
                        this.LongitudeInDegreesMinutesSeconds.Degrees,
                        this.LongitudeInDegreesMinutesSeconds.Minutes,
                        this.LongitudeInDegreesMinutesSeconds.Seconds,
                        this.LongitudeInDegreesMinutesSeconds.IsPositive ? 'E' : 'W',
                        this.SeaLevelAltitude
                        );

                case "n":
                    string result = string.Format("$GPRMC,{0:HHmmss},A,{1},{2},{3},{4},{0:ddMMyy},{5},{6}",
                            this.Time,
                            this.LatitudeInDegreesMinutesSeconds.ToString("a"),
                            this.LongitudeInDegreesMinutesSeconds.ToString("o"),
                            this.SpeedValid ? this.Speed.ToString() : string.Empty ,
                            this.HeadingValid ? this.Heading.ToString() : string.Empty,
                            this.MagneticVariationValid ? System.Math.Abs(this._magneticVariation).ToString("000.00") : string.Empty,
                            this.MagneticVariationValid ? this._magneticVariation < 0 ? "W" : "E" : ""
                        );
                    result += '*' + getNmeaChecksum(result);
                    return result;
            }
            throw new ArgumentOutOfRangeException("format");
        }
        #endregion

        #region TryParse
        public static bool TryParse(string value, out GpsPosition result)
        {
            if (TryParse(value, "a", null, out result)) return true;
            if (TryParse(value, "i", null, out result)) return true;
            if (TryParse(value, "n", null, out result)) return true;
            if (TryParse(value, "g", null, out result)) return true;
            if (TryParse(value, "d", null, out result)) return true;
            return false;
        }

        /// <summary>
        /// Convertes the string representation of a location to its GpsPosition equivalent. A return value indicates whether the conversion succeeded or failed. 
        /// </summary>
        /// <param name="value">A String object containing a location to convert.</param>
        /// <param name="format">valid values are a,i,f,F,l,L,d,m,s,n,g
        ///     notes: x = latitude, y = longitude, z = altitude, t = time (UTC unless otherwise stated)
        ///     a = 'activiser' format: x,y,z,t
        ///     i = 'ISO6709' format: +/-xx.xxxx+/-yyy.yyyy+/-zzzz
        ///     f, g, G = x°x'x" N/S, y°y'y"E/W @ t 
        ///     F = x°x'x" N/S, y°y'y"E/W \n @ t (two-line version of f)
        ///     l, L = same as f, F, except time converted to local time.
        ///     d = xx.xxxx°N/S,yyy.yyyy°E/W,zzzz
        ///     m = xx°xx'N/S,yyy°yy'E/W,zzzz
        ///     s = xx°xx'xx"N/S,yyy°yy'yy"E/W,zzzz
        ///     n = NMEA GPS 'GPRMC' sentence: $GPRMC,t:HHmmss,A,xxxx.xx,N/S,yyy.yyyy,E/W,speed,heading,t:ddMMyy,magnetic variation,E/W,checksum
        /// </param>
        /// <param name="formatProvider">ignored</param>
        /// <returns>true if value was successfully converted; otherwise false</returns>
        /// <remarks></remarks>
        public static bool TryParse(string value, string format, IFormatProvider formatProvider, out GpsPosition result)
        {
            result = null; // hacky - always set to null.

            if (format == "a") return TryParseActiviser(value, formatProvider, ref result);
            if (format == "i") return TryParseISO(value, ref result);
            if (format == "n") return TryParseNMEA(value, ref result);
            if ("fFlL".IndexOfAny(format.ToCharArray()) != -1)  return TryParseFriendlyWithTime(value, format, ref result);
            if ("dms".IndexOfAny(format.ToCharArray()) != -1) return TryParseFriendlyWithAltitude(value, ref result);

            return false;
        }

        private static bool TryParseNMEA(string value, ref GpsPosition result)
        {
            #region NMEA
            try
            {
                Regex rx = new Regex(Properties.Resources.TryParseRegexNMEA, RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);

                MatchCollection matches = rx.Matches(value);
                if (matches.Count != 1) return false;

                Match match = matches[0];
                GroupCollection fields = match.Groups;

                if (getNmeaChecksum(value) != fields["chk"].Value) return false;

                result = new GpsPosition();
                result.Latitude = float.Parse(fields["xD"].Value) + (float.Parse(fields["xM"].Value) / 60);
                result.Longitude = float.Parse(fields["yD"].Value) + (float.Parse(fields["yM"].Value) / 60);

                if (char.ToUpper(match.Groups["ns"].Value[0]) == 'S')
                {
                    result.Latitude = -result.Latitude;
                }
                else if (char.ToUpper(match.Groups["ns"].Value[0]) != 'N')
                {
                    return false;
                }

                if (char.ToUpper(match.Groups["ew"].Value[0]) == 'W')
                {
                    result.Longitude = -result.Longitude;
                }
                else if (char.ToUpper(match.Groups["ew"].Value[0]) != 'E')
                {
                    return false;
                }

                result.Time = new DateTime(Int32.Parse(fields["yr"].Value) + 2000, Int32.Parse(fields["mon"].Value), Int32.Parse(fields["day"].Value),
                        Int32.Parse(fields["hr"].Value), Int32.Parse(fields["min"].Value), Int32.Parse(fields["sec"].Value),
                        (int)(Double.Parse("0" + fields["ms"].Value) * 1000), DateTimeKind.Utc);

                try
                {
                    if (!string.IsNullOrEmpty(fields["spd"].Value))
                    {
                        result.Speed = float.Parse(fields["spd"].Value);
                    }
                    if (!string.IsNullOrEmpty(fields["hdg"].Value))
                    {
                        result.Heading = float.Parse(fields["hdg"].Value);
                    }
                    if (!string.IsNullOrEmpty(fields["mv"].Value))
                    {
                        result.MagneticVariation = double.Parse(fields["mv"].Value);
                        if (fields["mvew"].Value.ToUpper() == "W") result.MagneticVariation = -result.MagneticVariation;
                        else if (fields["mvew"].Value.ToUpper() != "E") return false; // throw new ArgumentOutOfRangeException("value", "Magnetic variation hemisphere must be 'E' or 'W'");
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
            #endregion
        }

        private static bool TryParseFriendlyWithAltitude(string value, ref GpsPosition result)
        {
            #region Friendly Formats with Altitude
            try
            {
                Regex rx = new Regex(Properties.Resources.TryParseRegexFriendlyWithAltitude, RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture);

                MatchCollection matches = rx.Matches(value);
                if (matches.Count != 1) return false;

                Match match = matches[0];
                result = new GpsPosition();

                string xDegrees = match.Groups["xD"].Value;
                string xMinutes = match.Groups["xM"].Value;
                string xSeconds = match.Groups["xS"].Value;
                string yDegrees = match.Groups["yD"].Value;
                string yMinutes = match.Groups["yM"].Value;
                string ySeconds = match.Groups["yS"].Value;

                if (!string.IsNullOrEmpty(xSeconds))
                {
                    result.Latitude = float.Parse(xDegrees) + (float.Parse(xMinutes) / 60) + (float.Parse(xSeconds) / 3600);
                    result.Longitude = float.Parse(yDegrees) + (float.Parse(yMinutes) / 60) + (float.Parse(ySeconds) / 3600);
                }
                else if (!string.IsNullOrEmpty(xMinutes))
                {
                    result.Latitude = float.Parse(xDegrees) + (float.Parse(xMinutes) / 60);
                    result.Longitude = float.Parse(yDegrees) + (float.Parse(yMinutes) / 60);
                }
                else
                {
                    result.Latitude = float.Parse(xDegrees);
                    result.Longitude = float.Parse(yDegrees);
                }

                if (char.ToUpper(match.Groups["ns"].Value[0]) == 'S')
                {
                    result.Latitude = -result.Latitude;
                }
                else if (char.ToUpper(match.Groups["ns"].Value[0]) != 'N')
                {
                    return false;
                }

                if (char.ToUpper(match.Groups["ew"].Value[0]) == 'W')
                {
                    result.Longitude = -result.Longitude;
                }
                else if (char.ToUpper(match.Groups["ns"].Value[0]) != 'E')
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
            #endregion
        }

        private static bool TryParseFriendlyWithTime(string value, string format, ref GpsPosition result)
        {
            #region Friendly Formats with Date/Time
            try
            {
                Regex rx = new Regex(Properties.Resources.TryParseRegexFriendlyWithTime, RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture
                );

                MatchCollection matches = rx.Matches(value);
                if (matches.Count != 1) return false;

                Match match = matches[0];
                result = new GpsPosition();

                string xDegrees = match.Groups["xD"].Value;
                string xMinutes = match.Groups["xM"].Value;
                string xSeconds = match.Groups["xS"].Value;
                string yDegrees = match.Groups["yD"].Value;
                string yMinutes = match.Groups["yM"].Value;
                string ySeconds = match.Groups["yS"].Value;

                result.Latitude = float.Parse(xDegrees) + (float.Parse(xMinutes) / 60) + (float.Parse(xSeconds) / 3600);
                result.Longitude = float.Parse(yDegrees) + (float.Parse(yMinutes) / 60) + (float.Parse(ySeconds) / 3600);

                if (match.Groups["ns"].Value.ToUpper() == Properties.Resources.TryParseSouth)
                {
                    result.Latitude = -result.Latitude;
                }
                else if (match.Groups["ns"].Value.ToUpper() != Properties.Resources.TryParseNorth)
                {
                    return false;
                }

                if (match.Groups["ew"].Value.ToUpper() == Properties.Resources.TryParseWest)
                {
                    result.Longitude = -result.Longitude;
                }
                else if (match.Groups["ns"].Value.ToUpper() != Properties.Resources.TryParseEast)
                {
                    return false;
                }

                try
                {
                    if (format.ToLower() == "l") // local time
                    {
                        result.Time = DateTime.SpecifyKind(DateTime.Parse(match.Groups["dt"].Value), DateTimeKind.Local);
                    }
                    else
                    {
                        result.Time = DateTime.SpecifyKind(DateTime.Parse(match.Groups["dt"].Value), DateTimeKind.Utc);
                    }
                }
                catch
                {
                    result.Time = DateTime.MinValue;
                }

                return true;
            }
            catch
            {
                return false;
            }

            #endregion
        }

        private static bool TryParseISO(string value, ref GpsPosition result)
        {
            #region ISO format
            try
            {
                Regex rx = new Regex(Properties.Resources.TryParseRegexISO, RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
                MatchCollection matches = rx.Matches(value);
                if (matches.Count != 1) return false;

                Match match = matches[0];
                result = new GpsPosition();
                if (!string.IsNullOrEmpty(match.Groups["xD"].Value))
                {
                    result.Latitude = float.Parse(match.Groups["xD"].Value);
                    result.Longitude = float.Parse(match.Groups["yD"].Value);
                }
                else if (!string.IsNullOrEmpty(match.Groups["xM"].Value))
                {
                    string xDegrees = match.Groups["xM"].Value.Substring(0, 3);
                    string xMinutes = match.Groups["xM"].Value.Substring(3, 6);
                    string yDegrees = match.Groups["yM"].Value.Substring(0, 4);
                    string yMinutes = match.Groups["yM"].Value.Substring(4, 6);

                    result.Latitude = float.Parse(xDegrees) + (float.Parse(xMinutes) / 60);
                    result.Longitude = float.Parse(yDegrees) + (float.Parse(yMinutes) / 60);
                }
                else if (!string.IsNullOrEmpty(match.Groups["xS"].Value))
                {
                    string xDegrees = match.Groups["xS"].Value.Substring(0, 3);
                    string xMinutes = match.Groups["xS"].Value.Substring(3, 2);
                    string xSeconds = match.Groups["xS"].Value.Substring(5, 5);
                    string yDegrees = match.Groups["yS"].Value.Substring(0, 3);
                    string yMinutes = match.Groups["yS"].Value.Substring(4, 2);
                    string ySeconds = match.Groups["yS"].Value.Substring(6, 5);

                    result.Latitude = float.Parse(xDegrees) + (float.Parse(xMinutes) / 60) + (float.Parse(xSeconds) / 3600);
                    result.Longitude = float.Parse(yDegrees) + (float.Parse(yMinutes) / 60) + (float.Parse(ySeconds) / 3600);
                }
                if (!string.IsNullOrEmpty(match.Groups["z"].Value))
                    result.SeaLevelAltitude = float.Parse(match.Groups["z"].Value);

                return true;
            }
            catch
            {
                return false;
            }
            #endregion
        }

        private static bool TryParseActiviser(string value, IFormatProvider formatProvider, ref GpsPosition result)
        {
            try
            {
                string[] items = value.Split(',');
                if (items.Length != 4) throw new FormatException("Invalid number of fields for format specifier");
                result = new GpsPosition();
                result.Latitude = double.Parse(items[0]);
                result.Longitude = double.Parse(items[1]);
                result.SeaLevelAltitude = float.Parse(items[2]);
                result.Time = DateTime.ParseExact(items[3], "u", formatProvider);
                return true;
            }
            catch // (FormatException fex)
            {
                // invalidate all fields if any are invalid.
                // result = null;
                return false;
            }
        }

        #endregion
    }
}
