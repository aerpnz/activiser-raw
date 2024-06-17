using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using activiser.Library.MinorPlanet.Properties;

namespace activiser.Library.MinorPlanet
{
    public class GpsPosition : IFormattable 
    {
        public bool PositionValid;
        public DateTime Time;
        public double Latitude;
        public double Longitude;
        public bool SpeedValid;
        public double Speed;
        public bool HeadingValid;
        public double Heading;
        public bool MagneticVariationValid;
        public double MagneticVariation;
        public string Mode;

        // these are for compatibility, and aren't used by this Minor Planet GPS Module
        public float EllipsoidAltitude = 0;
        public bool EllipsoidAltitudeValid = true;
        public float SeaLevelAltitude = 0;
        public bool SeaLevelAltitudeValid = true;

        public GpsPosition() { }

        public GpsPosition(string NmeaString)
        {
            try
            {
                Regex rx = new Regex(Resources.NmeaRegex, RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);

                MatchCollection matches = rx.Matches(NmeaString);
                if (matches.Count != 1) NmeaConstructorFail();

                Match match = matches[0];
                GroupCollection fields = match.Groups;

                if (getNmeaChecksum(NmeaString) != fields["chk"].Value) NmeaConstructorFail();
                this.PositionValid = fields["valid"].Value.ToUpper(CultureInfo.InvariantCulture) == "A";

                this.Latitude = float.Parse(fields["xD"].Value, CultureInfo.InvariantCulture) + (float.Parse(fields["xM"].Value, CultureInfo.InvariantCulture) / 60);
                this.Longitude = float.Parse(fields["yD"].Value, CultureInfo.InvariantCulture) + (float.Parse(fields["yM"].Value, CultureInfo.InvariantCulture) / 60);

                if (char.ToUpper(match.Groups["ns"].Value[0], CultureInfo.InvariantCulture) == 'S')
                {
                    this.Latitude = -this.Latitude;
                }
                else if (char.ToUpper(match.Groups["ns"].Value[0], CultureInfo.InvariantCulture) != 'N')
                {
                    NmeaConstructorFail();
                }

                if (char.ToUpper(match.Groups["ew"].Value[0], CultureInfo.InvariantCulture) == 'W')
                {
                    this.Longitude = -this.Longitude;
                }
                else if (char.ToUpper(match.Groups["ew"].Value[0], CultureInfo.InvariantCulture) != 'E')
                {
                    NmeaConstructorFail();
                }

                this.Time = new DateTime(Int32.Parse(fields["yr"].Value, CultureInfo.InvariantCulture) + 2000,
                                  Int32.Parse(fields["mon"].Value, CultureInfo.InvariantCulture),
                                  Int32.Parse(fields["day"].Value, CultureInfo.InvariantCulture),
                                  Int32.Parse(fields["hr"].Value, CultureInfo.InvariantCulture),
                                  Int32.Parse(fields["min"].Value, CultureInfo.InvariantCulture),
                                  Int32.Parse(fields["sec"].Value, CultureInfo.InvariantCulture),
                                  (int)(Double.Parse("0" + fields["ms"].Value, CultureInfo.InvariantCulture) * 1000),
                                  DateTimeKind.Utc);


                if (!string.IsNullOrEmpty(fields["spd"].Value))
                {
                    this.Speed = float.Parse(fields["spd"].Value, CultureInfo.InvariantCulture);
                    this.SpeedValid = true;
                }
                else
                {
                    this.SpeedValid = false;
                }
                if (!string.IsNullOrEmpty(fields["hdg"].Value))
                {
                    this.Heading = float.Parse(fields["hdg"].Value, CultureInfo.InvariantCulture);
                    this.HeadingValid = true;
                }
                else
                {
                    this.HeadingValid = false;
                }
                if (!string.IsNullOrEmpty(fields["mv"].Value))
                {
                    this.MagneticVariation = double.Parse(fields["mv"].Value, CultureInfo.InvariantCulture);
                    if (fields["mvew"].Value.ToUpper(CultureInfo.InvariantCulture) == "W") this.MagneticVariation = -this.MagneticVariation;
                    else if (fields["mvew"].Value.ToUpper(CultureInfo.InvariantCulture) != "E") NmeaConstructorFail();
                    this.MagneticVariationValid = true;
                }
                else
                {
                    this.MagneticVariationValid = false;
                }
               
                this.Mode = fields["mode"].Value.ToUpper(CultureInfo.InvariantCulture);
                return;
            }
            catch (Exception ex)
            {
                NmeaConstructorFail(ex);
            }
        }

        private static void NmeaConstructorFail()
        {
            throw new FormatException(Resources.GpsPositionNmeaConstructorFailMessage);
        }

        private static void NmeaConstructorFail(Exception innerException)
        {
            throw new FormatException(Resources.GpsPositionNmeaConstructorFailMessage, innerException);
        }

        public bool HasMovedFrom(GpsPosition right)
        {
            return right == null || this.Latitude != right.Latitude || this.Longitude != right.Longitude;
        }

        static double _lastLatitude;// = 0;
        static double _isoLatitude;// = 0;
        static DegreesMinutesSeconds _latitudeDms;// = null;

        /// <summary>
        /// Latitude in ISO decimal DDMMSS.ffff.  North is positive
        /// </summary>
        public double IsoLatitude
        {
            get
            {
                if (_lastLatitude != Latitude)
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
            get
            {
                if (_lastLatitude != Latitude || _latitudeDms == null)
                {
                    _latitudeDms = new DegreesMinutesSeconds(Latitude);
                    _lastLatitude = Latitude;
                }
                return _latitudeDms;
            }
        }

        static double _lastLongitude;// = 0;
        static double _isoLongitude;// = 0;
        static DegreesMinutesSeconds _longitudeDms;// = null;

        /// <summary>
        /// Longitude in ISO decimal DDMMSS.ffff.  North is positive
        /// </summary>
        public double IsoLongitude
        {
            get
            {
                if (_lastLongitude != Longitude)
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
                if (_lastLongitude != Longitude || _longitudeDms == null)
                {
                    _longitudeDms = new DegreesMinutesSeconds(Longitude);
                    _lastLongitude = Longitude;
                }
                return _longitudeDms;
            }
        }

        /// <summary>
        /// True if the Longitude property is valid, false if invalid
        /// </summary>
        public bool LongitudeValid
        {
            get { return (Longitude != 720.0) && (!double.IsNaN(Longitude)); }
        }

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
            return checksum.ToString("X2", CultureInfo.InvariantCulture); // Return the checksum formatted as a two-character hexadecimal
        }

        #region IFormattable Members

        /// <summary>
        /// Format location information.
        /// </summary>
        /// <param name="format">valid values are a,i,f,F,l,L,d,m,s,n,g
        ///     notes: x = latitude, y = longitude, z = altitude, t = time (UTC unless otherwise stated)
        ///     <list type="table"><listheader><term>Format Specifier</term><description>Description</description></listheader>
        ///     <item><term>a</term><description>activiser format: x,y,z,t</description></item>
        ///     <item><term>i</term><description>ISO6709 format: +/-xx.xxxx+/-yyy.yyyy+/-zzzz</description></item>
        ///     <item><term>f, g, G</term><description>fx°x'x" N/S, y°y'y"E/W @ t</description></item>
        ///     <item><term>F</term><description>x°x'x" N/S, y°y'y"E/W \n @ t (two-line version of f)</description></item>
        ///     <item><term>l, L</term><description>same as f, F, except time converted to local time</description></item>
        ///     <item><term>d</term><description>xx.xxxx°N/S,yyy.yyyy°E/W,zzzz</description></item>
        ///     <item><term>m</term><description>xx°xx'N/S,yyy°yy'E/W,zzzz</description></item>
        ///     <item><term>s</term><description>xx°xx'xx"N/S,yyy°yy'yy"E/W,zzzz</description></item>
        ///     <item><term>n</term><description>NMEA GPS 'GPRMC' sentence: $GPRMC,t:HHmmss,A,xxxx.xx,N/S,yyy.yyyy,E/W,speed,heading,t:ddMMyy,magnetic variation,E/W,checksum</description></item>
        ///     </list>
        /// </param>
        /// <param name="formatProvider">ignored, required for IFormattable compatibility</param>
        /// <returns>string</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "i":
                    return ToIsoFormatString();

                case "f":
                case "F":
                case "g":
                case "G":
                    return ToFriendlyString();

                case "l":
                case "L":
                    return ToFriendlyLocalTimeString();

                case "n":
                    string result = ToNmeaString();
                    return result;
            }
            throw new ArgumentOutOfRangeException("format");
        }
        #endregion

        private string ToNmeaString()
        {
            StringBuilder result = new StringBuilder(150);
            result.AppendFormat(CultureInfo.InvariantCulture,
                                "$GPRMC,{0:HHmmss},{7},{1},{2},{3},{4},{0:ddMMyy},{5},{6}{8}",
                                    this.Time,
                                    this.LatitudeInDegreesMinutesSeconds.ToString("a", CultureInfo.InvariantCulture),
                                    this.LongitudeInDegreesMinutesSeconds.ToString("o", CultureInfo.InvariantCulture),
                                    this.Speed.ToString(CultureInfo.InvariantCulture),
                                    (!double.IsNaN( this.Heading)) ? this.Heading.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                    this.MagneticVariationValid ? System.Math.Abs(this.MagneticVariation).ToString("000.00", CultureInfo.InvariantCulture) : string.Empty,
                                    this.MagneticVariationValid ? this.MagneticVariation < 0 ? "W" : "E" : "",
                                    this.PositionValid ? 'A' : 'V',
                                    string.IsNullOrEmpty(this.Mode) ? string.Empty : ',' + this.Mode
                                );
            result.Append('*');
            result.Append(getNmeaChecksum(result.ToString()));
            return result.ToString();
        }

        private string ToFriendlyString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                                        "{0:00}°{1:00}'{2:00}\"{3}, {4:000}°{5:00}'{6:00}\"{7} @ {8} ",
                                        this.LatitudeInDegreesMinutesSeconds.Degrees,
                                        this.LatitudeInDegreesMinutesSeconds.Minutes,
                                        this.LatitudeInDegreesMinutesSeconds.Seconds,
                                        this.LatitudeInDegreesMinutesSeconds.IsPositive ? 'N' : 'S',
                                        this.LongitudeInDegreesMinutesSeconds.Degrees,
                                        this.LongitudeInDegreesMinutesSeconds.Minutes,
                                        this.LongitudeInDegreesMinutesSeconds.Seconds,
                                        this.LongitudeInDegreesMinutesSeconds.IsPositive ? 'E' : 'W',
                                        this.Time.ToString("g", CultureInfo.CurrentCulture)
                                );
        }

        private string ToFriendlyLocalTimeString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                                        "{0:00}°{1:00}'{2:00}\"{3}, {4:000}°{5:00}'{6:00}\"{7} @ {8} ",
                                        this.LatitudeInDegreesMinutesSeconds.Degrees,
                                        this.LatitudeInDegreesMinutesSeconds.Minutes,
                                        this.LatitudeInDegreesMinutesSeconds.Seconds,
                                        this.LatitudeInDegreesMinutesSeconds.IsPositive ? 'N' : 'S',
                                        this.LongitudeInDegreesMinutesSeconds.Degrees,
                                        this.LongitudeInDegreesMinutesSeconds.Minutes,
                                        this.LongitudeInDegreesMinutesSeconds.Seconds,
                                        this.LongitudeInDegreesMinutesSeconds.IsPositive ? 'E' : 'W',
                                        this.Time.ToLocalTime().ToString("g", CultureInfo.CurrentCulture)
                                );
        }

        private string ToActiviserFormatString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:00.0000000},{1:000.0000000},{2},{3:u}",
                                           this.Latitude,
                                           this.Longitude,
                                           0, // this.SeaLevelAltitude,
                                           this.Time);
        }

        private string ToIsoFormatString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:+00.0000;-00.0000}{1:+000.0000;-000.0000}{2}",
                       this.Latitude,
                       this.Longitude,
                       string.Empty); // this.SeaLevelAltitudeValid ? this.SeaLevelAltitude.ToString("+0000;-0000", CultureInfo.InvariantCulture) : 
        }

        /// <summary>
        /// Convertes the string representation of a location to its GpsPosition equivalent. A return value indicates whether the conversion succeeded or failed. 
        /// </summary>
        /// <param name="value">A String object containing a location to convert.</param>
        /// <param name="format">valid values are a,i,f,F,l,L,d,m,s,n,g
        ///     notes: x = latitude, y = longitude, z = altitude, t = time (UTC unless otherwise stated)
        ///     <list type="table">
        ///     <item><term>a</term><description>activiser format: x,y,z,t</description></item>
        ///     <item><term>i</term><description>ISO6709 format: +/-xx.xxxx+/-yyy.yyyy+/-zzzz</description></item>
        ///     <item><term>f, g, G</term><description>fx°x'x" N/S, y°y'y"E/W @ t</description></item>
        ///     <item><term>F</term><description>x°x'x" N/S, y°y'y"E/W \n @ t (two-line version of f)</description></item>
        ///     <item><term>l, L</term><description>same as f, F, except time converted to local time</description></item>
        ///     <item><term>d</term><description>xx.xxxx°N/S,yyy.yyyy°E/W,zzzz</description></item>
        ///     <item><term>m</term><description>xx°xx'N/S,yyy°yy'E/W,zzzz</description></item>
        ///     <item><term>s</term><description>xx°xx'xx"N/S,yyy°yy'yy"E/W,zzzz</description></item>
        ///     <item><term>n</term><description>NMEA GPS 'GPRMC' sentence: $GPRMC,t:HHmmss,A,xxxx.xx,N/S,yyy.yyyy,E/W,speed,heading,t:ddMMyy,magnetic variation,E/W,checksum</description></item>
        ///     </list>
        /// </param>
        /// <param name="formatProvider">ignored</param>
        /// <param name="result">output parameter to hold the parsed result if the string value is successfully parsed</param>
        /// <returns>true if value was successfully converted; otherwise false</returns>
        /// <remarks></remarks>
        public static bool TryParse(string value, string format, IFormatProvider formatProvider, out GpsPosition result)
        {
            result = null; // hacky - always set to null.

            if (format == "a") return TryParseActiviser(value, formatProvider, ref result);
            if (format == "i") return TryParseISO(value, ref result);
            if (format == "n") return TryParseNMEA(value, ref result);
            if ("fFgGlL".IndexOfAny(format.ToCharArray()) != -1) return TryParseFriendlyWithTime(value, format, ref result);
            //if ("dms".IndexOfAny(format.ToCharArray()) != -1) return TryParseFriendlyWithAltitude(value, ref result);

            return false;
        }

        private static bool TryParseNMEA(string value, ref GpsPosition result)
        {
            #region NMEA
            try
            {
                Regex rx = new Regex(Properties.Resources.NmeaRegex, RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);

                MatchCollection matches = rx.Matches(value);
                if (matches.Count != 1) return false;

                Match match = matches[0];
                GroupCollection fields = match.Groups;

                if (getNmeaChecksum(value) != fields["chk"].Value) return false;

                result = new GpsPosition();

                result.Latitude = float.Parse(fields["xD"].Value, CultureInfo.InvariantCulture) + (float.Parse(fields["xM"].Value, CultureInfo.InvariantCulture) / 60);
                result.Longitude = float.Parse(fields["yD"].Value, CultureInfo.InvariantCulture) + (float.Parse(fields["yM"].Value, CultureInfo.InvariantCulture) / 60);

                if (char.ToUpper(match.Groups["ns"].Value[0], CultureInfo.InvariantCulture) == 'S')
                {
                    result.Latitude = -result.Latitude;
                }
                else if (char.ToUpper(match.Groups["ns"].Value[0], CultureInfo.InvariantCulture) != 'N')
                {
                    return false;
                }

                if (char.ToUpper(match.Groups["ew"].Value[0], CultureInfo.InvariantCulture) == 'W')
                {
                    result.Longitude = -result.Longitude;
                }
                else if (char.ToUpper(match.Groups["ew"].Value[0], CultureInfo.InvariantCulture) != 'E')
                {
                    return false;
                }

                result.Time = new DateTime(Int32.Parse(fields["yr"].Value, CultureInfo.InvariantCulture) + 2000,
                                  Int32.Parse(fields["mon"].Value, CultureInfo.InvariantCulture),
                                  Int32.Parse(fields["day"].Value, CultureInfo.InvariantCulture),
                                  Int32.Parse(fields["hr"].Value, CultureInfo.InvariantCulture),
                                  Int32.Parse(fields["min"].Value, CultureInfo.InvariantCulture),
                                  Int32.Parse(fields["sec"].Value, CultureInfo.InvariantCulture),
                                  (int)(Double.Parse("0" + fields["ms"].Value, CultureInfo.InvariantCulture) * 1000),
                                  DateTimeKind.Utc);

                try
                {
                    if (!string.IsNullOrEmpty(fields["spd"].Value))
                    {
                        result.Speed = float.Parse(fields["spd"].Value, CultureInfo.InvariantCulture);
                    }
                    if (!string.IsNullOrEmpty(fields["hdg"].Value))
                    {
                        result.Heading = float.Parse(fields["hdg"].Value, CultureInfo.InvariantCulture);
                    }
                    if (!string.IsNullOrEmpty(fields["mv"].Value))
                    {
                        result.MagneticVariation = double.Parse(fields["mv"].Value, CultureInfo.InvariantCulture);
                        if (fields["mvew"].Value.ToUpper(CultureInfo.InvariantCulture) == "W") result.MagneticVariation = -result.MagneticVariation;
                        else if (fields["mvew"].Value.ToUpper(CultureInfo.InvariantCulture) != "E") return false; // throw new ArgumentOutOfRangeException("value", "Magnetic variation hemisphere must be 'E' or 'W'");
                    }
                }
                catch
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

                result.Latitude = float.Parse(xDegrees, CultureInfo.CurrentCulture) + (float.Parse(xMinutes, CultureInfo.CurrentCulture) / 60) + (float.Parse(xSeconds, CultureInfo.CurrentCulture) / 3600);
                result.Longitude = float.Parse(yDegrees, CultureInfo.CurrentCulture) + (float.Parse(yMinutes, CultureInfo.CurrentCulture) / 60) + (float.Parse(ySeconds, CultureInfo.CurrentCulture) / 3600);

                if (match.Groups["ns"].Value.ToUpper(CultureInfo.InvariantCulture) == Properties.Resources.TryParseSouth)
                {
                    result.Latitude = -result.Latitude;
                }
                else if (match.Groups["ns"].Value.ToUpper(CultureInfo.InvariantCulture) != Properties.Resources.TryParseNorth)
                {
                    return false;
                }

                if (match.Groups["ew"].Value.ToUpper(CultureInfo.InvariantCulture) == Properties.Resources.TryParseWest)
                {
                    result.Longitude = -result.Longitude;
                }
                else if (match.Groups["ns"].Value.ToUpper(CultureInfo.InvariantCulture) != Properties.Resources.TryParseEast)
                {
                    return false;
                }

                try
                {
                    if (format.ToLower(CultureInfo.InvariantCulture) == "l") // local time
                    {
                        result.Time = DateTime.SpecifyKind(DateTime.Parse(match.Groups["dt"].Value, CultureInfo.CurrentCulture), DateTimeKind.Local);
                    }
                    else
                    {
                        result.Time = DateTime.SpecifyKind(DateTime.Parse(match.Groups["dt"].Value, CultureInfo.CurrentCulture), DateTimeKind.Utc);
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
                    result.Latitude = float.Parse(match.Groups["xD"].Value, CultureInfo.InvariantCulture);
                    result.Longitude = float.Parse(match.Groups["yD"].Value, CultureInfo.InvariantCulture);
                }
                else if (!string.IsNullOrEmpty(match.Groups["xM"].Value))
                {
                    string xDegrees = match.Groups["xM"].Value.Substring(0, 3);
                    string xMinutes = match.Groups["xM"].Value.Substring(3, 6);
                    string yDegrees = match.Groups["yM"].Value.Substring(0, 4);
                    string yMinutes = match.Groups["yM"].Value.Substring(4, 6);

                    result.Latitude = float.Parse(xDegrees, CultureInfo.InvariantCulture) + (float.Parse(xMinutes, CultureInfo.InvariantCulture) / 60);
                    result.Longitude = float.Parse(yDegrees, CultureInfo.InvariantCulture) + (float.Parse(yMinutes, CultureInfo.InvariantCulture) / 60);
                }
                else if (!string.IsNullOrEmpty(match.Groups["xS"].Value))
                {
                    string xDegrees = match.Groups["xS"].Value.Substring(0, 3);
                    string xMinutes = match.Groups["xS"].Value.Substring(3, 2);
                    string xSeconds = match.Groups["xS"].Value.Substring(5, 5);
                    string yDegrees = match.Groups["yS"].Value.Substring(0, 3);
                    string yMinutes = match.Groups["yS"].Value.Substring(4, 2);
                    string ySeconds = match.Groups["yS"].Value.Substring(6, 5);

                    result.Latitude = float.Parse(xDegrees, CultureInfo.InvariantCulture) + (float.Parse(xMinutes, CultureInfo.InvariantCulture) / 60) + (float.Parse(xSeconds, CultureInfo.InvariantCulture) / 3600);
                    result.Longitude = float.Parse(yDegrees, CultureInfo.InvariantCulture) + (float.Parse(yMinutes, CultureInfo.InvariantCulture) / 60) + (float.Parse(ySeconds, CultureInfo.InvariantCulture) / 3600);
                }
                //if (!string.IsNullOrEmpty(match.Groups["z"].Value))
                //    result.SeaLevelAltitude = float.Parse(match.Groups["z"].Value, CultureInfo.InvariantCulture);

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
            string[] items = value.Split(',');
            if (items.Length != 4) return false; // throw new FormatException("Invalid number of fields for format specifier");
            try
            {
                result = new GpsPosition();
                result.Latitude = double.Parse(items[0], CultureInfo.InvariantCulture);
                result.Longitude = double.Parse(items[1], CultureInfo.InvariantCulture);
                //result.SeaLevelAltitude = float.Parse(items[2], CultureInfo.InvariantCulture);
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
    }
}
