using System;

namespace activiser.Library
{
    public static class Base32 {
        /*
         * 
         * 5 x Bytes = 8 x 5Bits
         *
         *       |3 3 3 3 3 3 3 3 3 3 2 2 2 2 2 2 2 2 2 2 1 1 1 1 1 1 1 1 1 1 0 0 0 0 0 0 0 0 0 0|
         *       |9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0|
         * Bytes |               |               |               |               |               |
         * 5Bits |         |         |         |         |         |         |         |         |
         *
         * 0-7, 8-15, 16-23, 24-31, 32-39
         * 0-4, 5-9, 10-14, 15-19, 20-24, 25-29, 30-34, 35-39
         *
         *
         *	Official Base32 Alphabet (contained in resource file)
         *  0:A, 1:B, 2:C, 3:D, 4:E, 5:F, 6:G, 7:H
         *  8:I, 9:J, 10:K, 11:L, 12:M, 13:N, 14:O, 15:P
         *  16:Q, 17:R, 18:S, 19:T, 20:U, 21:V, 22:W, 23:X, 
         *  24:Y, 25:Z, 26:2, 27:3, 28:4, 29:5, 30:6, 31:7
         *  pad: =
        */

        private static char[] defaultLookupTable = Properties.Resources.RFCTable.ToCharArray();

        public static string Encode(byte[] source)
        {
            return Encode(source, defaultLookupTable);
        }

        /// <summary>
        /// Base32Encoder - convert a byte array to a base32 string
        /// </summary>
        public static string Encode(byte[] source, char[] lookupTable)
        {
            if (lookupTable.Length != 32) throw new ArgumentOutOfRangeException("lookupTable","Lookup table must contain exactly 32 characters");
            int length = source.Length;
            int paddedLength = length;
            int blockCount;
            int paddingCount;
            long lb;
            char[] result;
            int si, ri, x;

            if( (length % 5) == 0) {
                paddingCount = 0;
            }
            else {
                paddingCount = 5 - (length % 5); //need to add padding
                paddedLength += paddingCount ;
            }
            blockCount = paddedLength / 5;

            byte[] paddedSource = new byte[paddedLength];

            // copy data 
            for (x = 0; x < length; x++) {
                paddedSource[x] = source[x];
            }
            // insert padding
            for (; x < paddedLength - 1; x++) {
                paddedSource[x] = 0;
            }					   
      
            byte[] b = new byte[8]; 
            result=new char[blockCount * 8];

            for ( x = 0; x < blockCount; x++) {
                si = x * 5;
                ri = x * 8;
                b[0]=paddedSource[si];
                b[1]=paddedSource[si+1];
                b[2]=paddedSource[si+2];
                b[3]=paddedSource[si+3];
                b[4]=paddedSource[si+4];

                lb = System.BitConverter.ToInt64(b, 0);
					
                result[ri+0]=lookupTable[(lb & 0x1f)];
                result[ri+1]=lookupTable[(lb & 0x3e0) >> 5];
                result[ri+2]=lookupTable[(lb & 0x7c00) >> 10];
                result[ri+3]=lookupTable[(lb & 0xf8000) >> 15];
                result[ri+4]=lookupTable[(lb & 0x1F00000) >> 20];
                result[ri+5]=lookupTable[(lb & 0x3E000000) >> 25];
                result[ri+6]=lookupTable[(lb & 0x7C0000000) >> 30];
                result[ri+7]=lookupTable[(lb & 0xF800000000) >> 35];
            }

            //do padding
            switch(paddingCount) {
                case 0 : break;
                case 1 : paddingCount = 1; break;
                case 2 : paddingCount = 3; break;
                case 3 : paddingCount = 4; break;
                case 4 : paddingCount = 6; break;
            }
            int rub = result.GetUpperBound(0);
            for ( x = 0; x < paddingCount; x++) {
                result[rub - x] = '=';
            }
            return new string(result);

        }
        public static byte[] Decode(string source)
        {
            return Decode(source, defaultLookupTable);
        }

        /// <summary>
        /// Base32Decoder.
        /// </summary>		
        public static byte[] Decode(string source, char[] lookupTable)
        {
            if (lookupTable.Length != 32) throw new ArgumentOutOfRangeException("lookupTable", "Lookup table must contain exactly 32 characters");
            byte[] result;
            int paddedLength = source.Length;
            int resultLength;
            int paddedResultLength; 
            int blockCount;//, lastBlock;
            int paddingCount;
            long lb = 0;
            int si=0, ri=0, x;

            if(paddedLength % 8 != 0) {
                return null;
            }

            blockCount = paddedLength / 8;
            paddedResultLength = blockCount * 5;

            paddingCount = 0;
            for (x = source.Length - 1; source[x] == '='; x-- ) {
                paddingCount++ ;            
            }

            switch(paddingCount) {
                case 3 : paddingCount = 2; break;
                case 4 : paddingCount = 3; break;
                case 6 : paddingCount = 4; break;
                default : break;
            }

            resultLength = paddedResultLength - paddingCount;
            result = new byte[resultLength];

            byte[] rb = new byte[8];

            int padBlock; // test for padding
            if (paddingCount != 0) 
                padBlock = blockCount - 1 ;
            else
                padBlock = blockCount ;

            //lastBlock = blockCount - 1; // we'll do the last block separately
            for (x = 0; x < blockCount; x++, padBlock--, ri+=5, si+=8) {
                
                for(int i = 0; i < 8; i++) {
                    lb |= (long) char2fivebit(source[si+i], lookupTable) << i * 5;
                }
	
                rb = System.BitConverter.GetBytes(lb);

                if(padBlock == 0) {
                    for(int y = 0; y < paddingCount; y++) {
                        result[ri + y] = rb[y];
                    }
                }
                else {
                    for(int i = 0; i < 5; i++) {
                        result[ri+i] = rb[i];
                    }
                }
            }            
            return result;
        }

        private static byte char2fivebit(char c, char[] lookupTable)
        {
            if(c=='=')
                return 0;
            else {
                for (int x = 0 ; x < 32 ; x++) {
                    if ( lookupTable[x] == c)
                        return (byte) x;
                }
                throw new ArgumentException("Character not found in lookup table", "c");
            }
        }
    }
}
