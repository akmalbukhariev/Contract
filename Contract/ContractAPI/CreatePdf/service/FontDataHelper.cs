using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ContractAPI.CreatePdf.service
{
    /// <summary>
    /// Helper class that returns fonts from embedded resources as byte arrays.
    /// </summary>
    public static class FontDataHelper
    { 
        // Tip: I used JetBrains dotPeek to find the names of the resources (just look how dots in folder names are encoded).
        // Make sure the fonts have compile type "Embedded Resource". Names are case-sensitive.
        public static byte[] Ubuntu
        {
            get { return LoadFontData("ContractAPI.Fonts.Ubuntu-Regular.ttf"); }
        }

        public static byte[] UbuntuBold
        {
            get { return LoadFontData("ContractAPI.Fonts.Ubuntu-Bold.ttf"); }
        }

        public static byte[] UbuntuItalic
        {
            get { return LoadFontData("ContractAPI.Fonts.Ubuntu-Italic.ttf"); }
        }

        public static byte[] UbuntuBoldItalic
        {
            get { return LoadFontData("ContractAPI.Fonts.Ubuntu-BoldItalic.ttf"); }
        }

        /// <summary>
        /// Returns the specified font from an embedded resource.
        /// </summary>
        static byte[] LoadFontData(string name)
        {
            //string[] names = Assembly.GetCallingAssembly().GetManifestResourceNames();

            Assembly assembly = typeof(FontDataHelper).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                    throw new ArgumentException("No resource with name " + name);

                var count = (int)stream.Length;
                var data = new byte[count];
                stream.Read(data, 0, count);
                return data;
            }
        }
    }
}
