using System    ;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SystemEx
{
    public static class AssemblyExtensions
    {
        private static readonly byte[][] SystemPublicKeyTokens = new[]
        {
            new byte[] { 0x31, 0xbf, 0x38, 0x56, 0xad, 0x36, 0x4e, 0x35 },
            new byte[] { 0xb0, 0x3f, 0x5f, 0x7f, 0x11, 0xd5, 0x0a, 0x3a },
            new byte[] { 0xb7, 0x7a, 0x5c, 0x56, 0x19, 0x34, 0xe0, 0x89 }
        };

        public static bool IsEcmaAssembly(Assembly asm)
        {
            byte[] publicKey = asm.GetName().GetPublicKey();

            // ECMA key is special, as it is only 4 bytes long
            if (publicKey != null && publicKey.Length == 16 && publicKey[8] == 0x4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsSystemAssembly(Assembly asm)
        {
            byte[] publicKeyToken = asm.GetName().GetPublicKeyToken();

            if (publicKeyToken != null)
            {
                foreach (var systemPublicKeyToken in SystemPublicKeyTokens)
                {
                    if (systemPublicKeyToken.Length != publicKeyToken.Length)
                        continue;

                    bool matched = true;

                    for (int i = 0; i < SystemPublicKeyTokens.Length; ++i)
                    {
                        matched = matched && systemPublicKeyToken[i] == publicKeyToken[i];
                    }

                    if (matched)
                        return true;
                }
            }

            return false;
        }
    }
}
