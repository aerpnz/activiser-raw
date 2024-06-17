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

#endregion

namespace activiser.Library.Gps
{
    /// <summary>
    /// class that represents a gps coordinate in degrees, minutes, and seconds.  
    /// </summary>
    public class DegreesMinutesSeconds : IFormattable
    {

        bool _isPositive;
        int _degrees;
        int _minutes;
        double _seconds;        
        
        /// <summary>
        /// Returns true if the degrees, minutes and seconds refer to a positive value,
        /// false otherwise.
        /// </summary>
        public bool IsPositive
        {
            get { return _isPositive; }
            set { _isPositive = value; }
        }

        /// <summary>
        /// The degrees unit of the coordinate
        /// </summary>
        public int Degrees
        {
            get { return _degrees; }
            set { _degrees = value; }
        }

        /// <summary>
        /// The minutes unit of the coordinate
        /// </summary>
        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }

        /// <summary>
        /// The seconds unit of the coordinate
        /// </summary>
        public double Seconds
        {
            get { return _seconds; }
            set { _seconds = value; }
        }

        /// <summary>
        /// Constructs a new instance of DegreesMinutesSeconds converting 
        /// from decimal degrees
        /// </summary>
        /// <param name="decimalDegrees">Initial value as decimal degrees</param>
        public DegreesMinutesSeconds(double decimalDegrees)
        {
            _isPositive = (decimalDegrees > 0);
            
            _degrees = (int) Math.Abs(decimalDegrees);
            
            double doubleMinutes = (Math.Abs(decimalDegrees) - Math.Abs((double)_degrees)) * 60.0;
            _minutes = (int) doubleMinutes;

            _seconds = (doubleMinutes - (double)_minutes) * 60.0;
        }

        /// <summary>
        /// Constructs a new instance of DegreesMinutesSeconds
        /// </summary>
        /// <param name="isPositive">True if the coordinates are positive coordinate, false if they
        /// are negative coordinates.</param>
        /// <param name="degrees">Degrees unit of the coordinate</param>
        /// <param name="minutes">Minutes unit of the coordinate</param>
        /// <param name="seconds">Seconds unit of the coordinate. This should be a positive value.</param>
        public DegreesMinutesSeconds(bool isPositive, int degrees, int minutes, double seconds)
        {
            this._isPositive = isPositive;
            this._degrees = degrees;
            this._minutes = minutes;
            this._seconds = seconds;
        }

        /// <summary>
        /// Converts the decimal, minutes, seconds coordinate to 
        /// decimal degrees
        /// </summary>
        /// <returns></returns>
        public double ToDecimalDegrees()
        {
            double val = (double)_degrees + ((double)_minutes / 60.0) + ((double)_seconds / 3600.0);
            val = _isPositive ? val : val * -1;
            return val;
        }

        /// <summary>
        /// Converts the decimal, minutes, seconds coordinate to 
        /// 'ISO' degrees - i.e. floating point number in the format DDMMSS.fff
        /// </summary>
        /// <returns></returns>
        public double ToIso6709()
        {
            double result = (_degrees * 10000) + (_minutes * 100) + _seconds;
            return (!_isPositive) ? result : -result;
        }

        private char signChar
        {
            get { return this.IsPositive ? '+' : '-' ; }
        }
        /// <summary>
        /// Converts the instance to a string in format: D M' S"
        /// </summary>
        /// <returns>string representation of degrees, minutes, seconds</returns>
        public override string ToString()
        {
            return string.Format("{3}{0:0}�{1:00}'{2:00}\"", _degrees, _minutes, _seconds, signChar);
        }

        /// <summary>
        /// Converts the instance to a string in format: D M' S"
        /// </summary>
        /// <returns>string representation of degrees, minutes, seconds</returns>
        public string ToString(int decimalPlaces)
        {
            return string.Format("{3}{0:0}�{1:00}'{2:" + decimalPlaces.ToString() + "}\"", _degrees, _minutes, _seconds, signChar);
        }

        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "z")
                return this.ToString();
            else if (format == "m")
                return string.Format("{3}{0:0}�{1:00}'", _degrees, _minutes, _seconds, signChar);
            else if (format == "s")
                return string.Format("{3}{0:0}�{1:00}'{2:00}\"", _degrees, _minutes, _seconds, signChar);
            else if (format == "f")
                return string.Format("{3}{0:0}�{1:00}'{2:00.00}\"", _degrees, _minutes, _seconds, signChar);
            else if (format == "n" || format == "I") // ISO 6709 format longitude 
                return string.Format("{3}{0:000}{1:00}{2:00.00}", _degrees, _minutes, _seconds, signChar);
            else if (format == "t" || format == "i") // ISO 6709 format latitude
                return string.Format("{3}{0:00}{1:00}{2:00.00}", _degrees, _minutes, _seconds, signChar);
            else if (format == "o" || format == "G") // NMEA longitude
                return string.Format("{0:000}{1:00}.{2:00},{3}", this._degrees, this._minutes, this._seconds, this._isPositive ? 'E' : 'W');
            else if (format == "a" || format == "g") // NMEA latitude
                return string.Format("{0:00}{1:00}.{2:00},{3}", this._degrees, this._minutes, this._seconds, this._isPositive ? 'N' : 'S');

            throw new ArgumentException("format");
        }

        #endregion
    }
}