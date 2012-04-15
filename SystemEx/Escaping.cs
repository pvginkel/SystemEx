using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Diagnostics;

namespace SystemEx
{
    /// <summary>
    /// Contains utility methods for escaping and unescaping strings.
    /// </summary>
    public static class Escaping
    {
        private static readonly int _longestHtmlEntity;

        // HTML entities taken from http://www.w3.org/TR/html4/sgml/entities.html.

        private static readonly Map<string, int> _htmlEntities = new Map<string, int>
        {
            { "Aacute", 193 }, { "aacute", 225 }, { "Acirc", 194 }, { "acirc", 226 }, { "acute", 180 }, { "AElig", 198 },
            { "aelig", 230 }, { "Agrave", 192 }, { "agrave", 224 }, { "alefsym", 8501 }, { "Alpha", 913 }, { "alpha", 945 },
            { "amp", 38 }, { "and", 8743 }, { "ang", 8736 }, { "Aring", 197 }, { "aring", 229 }, { "asymp", 8776 },
            { "Atilde", 195 }, { "atilde", 227 }, { "Auml", 196 }, { "auml", 228 }, { "bdquo", 8222 }, { "Beta", 914 },
            { "beta", 946 }, { "brvbar", 166 }, { "bull", 8226 }, { "cap", 8745 }, { "Ccedil", 199 }, { "ccedil", 231 },
            { "cedil", 184 }, { "cent", 162 }, { "Chi", 935 }, { "chi", 967 }, { "circ", 710 }, { "clubs", 9827 },
            { "cong", 8773 }, { "copy", 169 }, { "crarr", 8629 }, { "cup", 8746 }, { "curren", 164 }, { "dagger", 8224 },
            { "Dagger", 8225 }, { "darr", 8595 }, { "dArr", 8659 }, { "deg", 176 }, { "Delta", 916 }, { "delta", 948 },
            { "diams", 9830 }, { "divide", 247 }, { "Eacute", 201 }, { "eacute", 233 }, { "Ecirc", 202 }, { "ecirc", 234 },
            { "Egrave", 200 }, { "egrave", 232 }, { "empty", 8709 }, { "emsp", 8195 }, { "ensp", 8194 }, { "Epsilon", 917 },
            { "epsilon", 949 }, { "equiv", 8801 }, { "Eta", 919 }, { "eta", 951 }, { "ETH", 208 }, { "eth", 240 },
            { "Euml", 203 }, { "euml", 235 }, { "euro", 8364 }, { "exist", 8707 }, { "fnof", 402 }, { "forall", 8704 },
            { "frac12", 189 }, { "frac14", 188 }, { "frac34", 190 }, { "frasl", 8260 }, { "Gamma", 915 }, { "gamma", 947 },
            { "ge", 8805 }, { "gt", 62 }, { "harr", 8596 }, { "hArr", 8660 }, { "hearts", 9829 }, { "hellip", 8230 },
            { "Iacute", 205 }, { "iacute", 237 }, { "Icirc", 206 }, { "icirc", 238 }, { "iexcl", 161 }, { "Igrave", 204 },
            { "igrave", 236 }, { "image", 8465 }, { "infin", 8734 }, { "int", 8747 }, { "Iota", 921 }, { "iota", 953 },
            { "iquest", 191 }, { "isin", 8712 }, { "Iuml", 207 }, { "iuml", 239 }, { "Kappa", 922 }, { "kappa", 954 },
            { "Lambda", 923 }, { "lambda", 955 }, { "lang", 9001 }, { "laquo", 171 }, { "larr", 8592 }, { "lArr", 8656 },
            { "lceil", 8968 }, { "ldquo", 8220 }, { "le", 8804 }, { "lfloor", 8970 }, { "lowast", 8727 }, { "loz", 9674 },
            { "lrm", 8206 }, { "lsaquo", 8249 }, { "lsquo", 8216 }, { "lt", 60 }, { "macr", 175 }, { "mdash", 8212 },
            { "micro", 181 }, { "middot", 183 }, { "minus", 8722 }, { "Mu", 924 }, { "mu", 956 }, { "nabla", 8711 },
            { "nbsp", 160 }, { "ndash", 8211 }, { "ne", 8800 }, { "ni", 8715 }, { "not", 172 }, { "notin", 8713 },
            { "nsub", 8836 }, { "Ntilde", 209 }, { "ntilde", 241 }, { "Nu", 925 }, { "nu", 957 }, { "Oacute", 211 },
            { "oacute", 243 }, { "Ocirc", 212 }, { "ocirc", 244 }, { "OElig", 338 }, { "oelig", 339 }, { "Ograve", 210 },
            { "ograve", 242 }, { "oline", 8254 }, { "Omega", 937 }, { "omega", 969 }, { "Omicron", 927 }, { "omicron", 959 },
            { "oplus", 8853 }, { "or", 8744 }, { "ordf", 170 }, { "ordm", 186 }, { "Oslash", 216 }, { "oslash", 248 },
            { "Otilde", 213 }, { "otilde", 245 }, { "otimes", 8855 }, { "Ouml", 214 }, { "ouml", 246 }, { "para", 182 },
            { "part", 8706 }, { "permil", 8240 }, { "perp", 8869 }, { "Phi", 934 }, { "phi", 966 }, { "Pi", 928 },
            { "pi", 960 }, { "piv", 982 }, { "plusmn", 177 }, { "pound", 163 }, { "prime", 8242 }, { "Prime", 8243 },
            { "prod", 8719 }, { "prop", 8733 }, { "Psi", 936 }, { "psi", 968 }, { "quot", 34 }, { "radic", 8730 },
            { "rang", 9002 }, { "raquo", 187 }, { "rarr", 8594 }, { "rArr", 8658 }, { "rceil", 8969 }, { "rdquo", 8221 },
            { "real", 8476 }, { "reg", 174 }, { "rfloor", 8971 }, { "Rho", 929 }, { "rho", 961 }, { "rlm", 8207 },
            { "rsaquo", 8250 }, { "rsquo", 8217 }, { "sbquo", 8218 }, { "Scaron", 352 }, { "scaron", 353 }, { "sdot", 8901 },
            { "sect", 167 }, { "shy", 173 }, { "Sigma", 931 }, { "sigma", 963 }, { "sigmaf", 962 }, { "sim", 8764 },
            { "spades", 9824 }, { "sub", 8834 }, { "sube", 8838 }, { "sum", 8721 }, { "sup", 8835 }, { "sup1", 185 },
            { "sup2", 178 }, { "sup3", 179 }, { "supe", 8839 }, { "szlig", 223 }, { "Tau", 932 }, { "tau", 964 },
            { "there4", 8756 }, { "Theta", 920 }, { "theta", 952 }, { "thetasym", 977 }, { "thinsp", 8201 }, { "THORN", 222 },
            { "thorn", 254 }, { "tilde", 732 }, { "times", 215 }, { "trade", 8482 }, { "Uacute", 218 }, { "uacute", 250 },
            { "uarr", 8593 }, { "uArr", 8657 }, { "Ucirc", 219 }, { "ucirc", 251 }, { "Ugrave", 217 }, { "ugrave", 249 },
            { "uml", 168 }, { "upsih", 978 }, { "Upsilon", 933 }, { "upsilon", 965 }, { "Uuml", 220 }, { "uuml", 252 },
            { "weierp", 8472 }, { "Xi", 926 }, { "xi", 958 }, { "Yacute", 221 }, { "yacute", 253 }, { "yen", 165 },
            { "yuml", 255 }, { "Yuml", 376 }, { "Zeta", 918 }, { "zeta", 950 }, { "zwj", 8205 }, { "zwnj", 8204 }
        };

        static Escaping()
        {
            _longestHtmlEntity = 0;

            foreach (var entity in _htmlEntities.Item1Keys)
            {
                _longestHtmlEntity = Math.Max(_longestHtmlEntity, entity.Length);
            }
        }

        /// <summary>
        /// Encodes a string using C encoding with <b>"</b> as the quote char.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>The encoded string.</returns>
        public static string StringEncode(string value)
        {
            return StringEncode(value, '"');
        }

        /// <summary>
        /// Encodes a string using C encoding with <paramref name="quoteChar"/> as the quote char.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <param name="quoteChar">The character used to quote the string. The default
        /// value for this parameter is <c>"</c>.</param>
        /// <returns>The encoded string.</returns>
        public static string StringEncode(string value, char quoteChar)
        {
            return StringEncode(value, quoteChar, true);
        }

        public static string StringEncode(string value, bool enclose)
        {
            return StringEncode(value, '"', enclose);
        }

        /// <summary>
        /// Encodes a string using C encoding with <paramref name="quoteChar"/> as the quote char.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <param name="quoteChar">The character used to quote the string. The default
        /// value for this parameter is <c>"</c>.</param>
        /// <returns>The encoded string.</returns>
        public static string StringEncode(string value, char quoteChar, bool enclose)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (quoteChar != '\'' && quoteChar != '"')
                throw new ArgumentException("Illegal quote");

            var sb = new StringBuilder();

            if (enclose)
                sb.Append(quoteChar);

            for (int i = 0; i < value.Length; i++)
            {
                if (Char.IsControl(value[i]))
                {
                    sb.Append("\\u");
                    sb.Append(((int)value[i]).ToString("X4"));
                }
                else if (value[i] == '\'' || value[i] == '"')
                {
                    sb.Append('\\');
                    sb.Append(value[i]);
                }
                else
                {
                    sb.Append(value[i]);
                }
            }

            if (enclose)
                sb.Append(quoteChar);

            return sb.ToString();
        }

        public static string StringDecode(string value)
        {
            if (String.IsNullOrEmpty(value))
                return value;

            var state = StringDecodeState.None;
            var sb = new StringBuilder();
            var escapeCache = new StringBuilder();

            int i = 0;

            while (i <= value.Length)
            {
                char c;
                bool atEnd = i == value.Length;

                if (i < value.Length)
                    c = value[i];
                else
                    c = '\0';

                switch (state)
                {
                    case StringDecodeState.None:
                        if (!atEnd)
                        {
                            if (c == '\\')
                                state = StringDecodeState.Escape;
                            else
                                sb.Append(c);
                        }
                        break;

                    case StringDecodeState.Escape:
                        if (atEnd)
                        {
                            sb.Append('\\');
                        }
                        else
                        {
                            switch (Char.ToLowerInvariant(c))
                            {
                                case 'a': sb.Append('\a'); break;
                                case 'b': sb.Append('\b'); break;
                                case 't': sb.Append('\t'); break;
                                case 'r': sb.Append('\r'); break;
                                case 'v': sb.Append('\v'); break;
                                case 'f': sb.Append('\f'); break;
                                case 'n': sb.Append('\n'); break;
                                case 'e': sb.Append('\u001B'); break;
                                case '\'': sb.Append('\''); break;
                                case '"': sb.Append('"'); break;
                                case '\\': sb.Append('\\'); break;
                                case 'u': state = StringDecodeState.UnicodeEscape; break;
                                case 'x': state = StringDecodeState.HexEscape; break;
                                default:
                                    if (CharEx.IsOct(c))
                                    {
                                        i--;
                                        state = StringDecodeState.OctEscape;
                                    }
                                    else
                                    {
                                        sb.Append(c);
                                    }
                                    break;
                            }

                            if (state == StringDecodeState.Escape)
                                state = StringDecodeState.None;
                        }
                        break;

                    case StringDecodeState.OctEscape:
                        if (
                            !atEnd &&
                            CharEx.IsOct(c) &&
                            escapeCache.Length < 3
                        )
                        {
                            escapeCache.Append(c);
                        }
                        else
                        {
                            if (escapeCache.Length > 0)
                                sb.Append(CharEx.FromOct(escapeCache.ToString()));

                            if (!atEnd)
                            {
                                state = StringDecodeState.None;

                                escapeCache = new StringBuilder();

                                i--;
                            }
                        }
                        break;

                    case StringDecodeState.HexEscape:
                    case StringDecodeState.UnicodeEscape:
                        if (
                            !atEnd &&
                            CharEx.IsHex(c) &&
                            escapeCache.Length < (state == StringDecodeState.HexEscape ? 2 : 4)
                        )
                        {
                            escapeCache.Append(c);
                        }
                        else
                        {
                            if (escapeCache.Length == 0)
                                sb.Append(state == StringDecodeState.HexEscape ? 'x' : 'u');
                            else
                                sb.Append(CharEx.FromHex(escapeCache.ToString()));

                            if (!atEnd)
                            {
                                state = StringDecodeState.None;

                                escapeCache = new StringBuilder();

                                i--;
                            }
                        }
                        break;

                    default:
                        Debug.Fail("Illegal state");
                        return null;
                }

                i++;
            }

            return sb.ToString();
        }

        private enum StringDecodeState
        {
            None,
            Escape,
            UnicodeEscape,
            HexEscape,
            OctEscape
        }

        /// <summary>
        /// Encodes a string using URI encoding.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>The encoded string.</returns>
        public static string UriEncode(string value)
        {
            return Uri.EscapeDataString(value);
        }

        /// <summary>
        /// Decodes a string using URI encoding.
        /// </summary>
        /// <param name="value">The string to decode.</param>
        /// <returns>The decoded string.</returns>
        public static string UriDecode(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var sb = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                if (
                    value[i] == '%' &&
                    i < value.Length - 2 &&
                    CharEx.IsHex(value[i + 1]) &&
                    CharEx.IsHex(value[i + 2])
                )
                {
                    sb.Append(
                        (char)(CharEx.HexToInt(value[i + 1]) * 16 + CharEx.HexToInt(value[i + 2]))
                    );

                    i += 2;
                }
                else if (value[i] == '+')
                {
                    sb.Append(' ');
                }
                else
                {
                    sb.Append(value[i]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Encodes a string using HTML encoding.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>The encoded string.</returns>
        public static string HtmlEncode(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var sb = new StringBuilder();

            foreach (char c in value)
            {
                string entity;

                if (_htmlEntities.TryGetValueByItem2((int)c, out entity))
                {
                    sb.Append("&");
                    sb.Append(entity);
                    sb.Append(";");
                }
                else if (Char.IsControl(c))
                {
                    sb.Append("&#");
                    sb.Append((int)c);
                    sb.Append(";");
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Decodes a string using HTML encoding.
        /// </summary>
        /// <param name="value">The string to be decoded.</param>
        /// <returns>The decode string.</returns>
        public static string HtmlDecode(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var sb = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '&' && value.Length > i + 2)
                {
                    // Scan for the ;.

                    int maxSearch = Math.Min(value.Length, i + _longestHtmlEntity + 2);
                    int endPosition = -1;

                    for (int j = i + 1; j < maxSearch; j++)
                    {
                        if (value[j] == ';')
                        {
                            endPosition = j;
                            break;
                        }
                    }

                    // If we did not find an end separator, just skip over this
                    // entity and treat is at text.

                    if (endPosition == -1)
                    {
                        sb.Append(value[i]);
                        continue;
                    }

                    // Are we in a numeric separator?

                    if (value[i + 1] == '#')
                    {
                        int offset = 2;

                        bool isHexNumeric = false;

                        if (value[i + 2] == 'x' || value[i + 2] == 'X')
                        {
                            isHexNumeric = true;
                            offset++;
                        }

                        // All parts of the numeric separator must be digits.

                        bool isNumeric = true;

                        for (int j = i + offset; j < endPosition; j++)
                        {
                            if (!(
                                Char.IsDigit(value[j]) ||
                                (isHexNumeric && CharEx.IsHex(value[j]))
                            ))
                            {
                                isNumeric = false;
                                break;
                            }
                        }

                        // If not all numeric, just skip over this
                        // entity and treat is at text.

                        if (!isNumeric)
                        {
                            sb.Append(value[i]);
                            continue;
                        }

                        // Convert the numeric entity to unicode.

                        string numericEntity = value.Substring(i + offset, endPosition - (i + offset));

                        sb.Append((char)int.Parse(numericEntity, isHexNumeric ? NumberStyles.HexNumber : NumberStyles.Integer));

                        i = endPosition;
                    }
                    else
                    {
                        string entity = value.Substring(i + 1, endPosition - (i + 1));

                        int codePoint;

                        if (_htmlEntities.TryGetValueByItem1(entity, out codePoint))
                        {
                            sb.Append((char)codePoint);

                            i = endPosition;
                        }
                        else
                        {
                            // If we don't know the entity, just skip over this
                            // entity and treat is at text.

                            sb.Append(value[i]);
                        }
                    }
                }
                else
                {
                    sb.Append(value[i]);
                }
            }

            return sb.ToString();
        }

        public static string Base64Encode(byte[] data)
        {
            if (data == null)
                return null;

            return Convert.ToBase64String(data);
        }

        public static byte[] Base64Decode(string data)
        {
            if (data == null)
                return null;

            return Convert.FromBase64String(data);
        }

        public static Dictionary<string, string> UrlDecode(string value)
        {
            var result = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(value))
            {
                foreach (string part in value.Split('&'))
                {
                    string[] subParts = part.Split(new char[] { '=' }, 2);

                    result[UriDecode(subParts[0])] = subParts.Length == 2 ? UriDecode(subParts[1]) : null;
                }
            }

            return result;
        }

        public static string UrlEncode(Dictionary<string, string> value)
        {
            if (value == null)
                return null;

            var sb = new StringBuilder();
            bool hadOne = false;

            foreach (var item in value)
            {
                if (hadOne)
                    sb.Append('&');
                else
                    hadOne = true;

                sb.Append(UriEncode(item.Key));

                if (item.Value != null)
                {
                    sb.Append('=');
                    sb.Append(UriEncode(item.Value));
                }
            }

            return sb.ToString();
        }
    }
}
