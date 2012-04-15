using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    /// <summary>
    /// Contains utility methods to work with characters.
    /// </summary>
    public static class CharEx
    {
        /// <summary>
        /// Test whether a character is a hexadecimal character.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>true when the character is a hexadecimal character; otherwise false.</returns>
        public static bool IsHex(char value)
        {
            switch (value)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Converts a hexadecimal character to its integer value.
        /// </summary>
        /// <param name="value">The character to convert to an integer.</param>
        /// <returns>The integer value of the hexadecimal character.</returns>
        public static int HexToInt(char value)
        {
            switch (value)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return (int)value - (int)'0';

                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                    return ((int)value - (int)'a') + 10;

                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                    return ((int)value - (int)'A') + 10;

                default:
                    throw new ArgumentOutOfRangeException("value");
            }
        }

        public static char FromHex(string value)
        {
            if (String.IsNullOrEmpty(value))
                return '\0';

            ushort result = 0;

            foreach (char c in value)
            {
                if (IsHex(c))
                {
                    result <<= 4;
                    result += (ushort)HexToInt(c);
                }
                else
                {
                    break;
                }
            }

            return (char)result;
        }

        public static bool IsOct(char c)
        {
            return c >= '0' && c <= '7';
        }

        public static char FromOct(string value)
        {
            if (String.IsNullOrEmpty(value))
                return '\0';

            ushort result = 0;

            foreach (char c in value)
            {
                if (IsOct(c))
                {
                    result <<= 3;
                    result += (ushort)(c - '0');
                }
                else
                {
                    break;
                }
            }

            return (char)result;
        }
    }
}
