using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SystemEx
{
    public static class AssemblyInformation
    {
        public static string GetProductName(Assembly assembly)
        {
            var attribs = GetAttribute<AssemblyProductAttribute>(assembly);

            return attribs == null ? null : attribs.Product;
        }

        public static string GetProductTitle(Assembly assembly)
        {
            var attribs = GetAttribute<AssemblyTitleAttribute>(assembly);

            return attribs == null ? null : attribs.Title;
        }

        public static string GetProductVersion(Assembly assembly)
        {
            var version = GetAttribute<AssemblyVersionAttribute>(assembly);

            if (version == null)
            {
                var fileVersion = GetAttribute<AssemblyFileVersionAttribute>(assembly);

                if (fileVersion == null)
                {
                    return null;
                }

                return fileVersion.Version;
            }

            return version.Version;
        }

        public static string GetProductCopyright(Assembly assembly)
        {
            var attribs = GetAttribute<AssemblyCopyrightAttribute>(assembly);
            
            return attribs == null ? null : attribs.Copyright;
        }

        public static string GetProductCompany(Assembly assembly)
        {
            var attribs = GetAttribute<AssemblyCompanyAttribute>(assembly);
            
            return attribs == null ? null : attribs.Company;
        }

        private static T GetAttribute<T>(Assembly assembly) where T : Attribute
        {
            Type attrType = typeof(T);

            object[] attrs = assembly.GetCustomAttributes(attrType, false);

            if (attrs.Length > 0)
            {
                return (T)attrs[0];
            }
            else
            {
                return null;
            }
        }
    }
}
