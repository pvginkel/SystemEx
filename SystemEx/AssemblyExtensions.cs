using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SystemEx
{
    public static class AssemblyExtensions
    {
        private static readonly byte[] SystemPublicKeyToken = { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };

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

            if (publicKeyToken != null && publicKeyToken.Length == SystemPublicKeyToken.Length)
            {
                for (int i = 0; i < SystemPublicKeyToken.Length; ++i)
                {
                    if (SystemPublicKeyToken[i] != publicKeyToken[i])
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
