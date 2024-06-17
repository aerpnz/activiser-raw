using System.Runtime.InteropServices;

namespace activiser.Library.Gps
{
    [StructLayout(LayoutKind.Sequential)] //, Size=12)]
    unsafe internal struct SatelliteArray
    {
        private const int GPS_MAX_SATELLITES = 12;

        fixed int data[GPS_MAX_SATELLITES];

        public int Count
        {
            get { return GPS_MAX_SATELLITES; }
        }

        public int this[int value]
        {
            get
            {
                if (!(value < GPS_MAX_SATELLITES)) throw new System.ArgumentOutOfRangeException("value", "value must be less than the maximum number of satellites");
                fixed (int* result = data)
                {
                    return result[value];
                }
            }
        }
    }
}