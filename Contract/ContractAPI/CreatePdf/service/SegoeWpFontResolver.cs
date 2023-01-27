using PdfSharpCore.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.CreatePdf.service
{
    /// <summary>
    /// Maps font requests for a SegoeWP font to a bunch of 6 specific font files.
    /// These 6 fonts are embedded as resources in the WPFonts assembly.
    /// </summary>
    public class SegoeWpFontResolver : IFontResolver
    {
        public string DefaultFontName => "";
         
        /// <summary>
        /// The font family names that can be used in the constructor of XFont.
        /// Used in the first parameter of ResolveTypeface.
        /// Family names are given in lower case because the implementation of SegoeWpFontResolver ignores case.
        /// </summary>
          
        /// <summary>
        /// The internal names that uniquely identify a font's type faces (i.e. a physical font file).
        /// Used in the first parameter of the FontResolverInfo constructor.
        /// </summary>
        static class FaceNames
        {
            /// Used in the first parameter of the FontResolverInfo constructor.
            public const string SegoeWPLight = "SegoeWPLight";
            public const string SegoeWPSemilight = "SegoeWPSemilight";
            public const string SegoeWP = "SegoeWP";
            public const string SegoeWPSemibold = "SegoeWPSemibold";
            public const string SegoeWPBold = "SegoeWPBold";
            public const string SegoeWPBlack = "SegoeWPBlack";
        }

        // ReSharper restore InconsistentNaming

        public /*override*/ FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Ignore case of font names.
            var name = familyName.ToLower();

            // Deal with the fonts we know.
            switch (name)
            {
                case "ubuntu":
                    if (isBold)
                    {
                        if (isItalic)
                            return new FontResolverInfo("Ubuntu#bi");
                        return new FontResolverInfo("Ubuntu#b");
                    }
                    if (isItalic)
                        return new FontResolverInfo("Ubuntu#i");
                    return new FontResolverInfo("Ubuntu#"); 
            }

            // We pass all other font requests to the default handler.
            // When running on a web server without sufficient permission, you can return a default font at this stage.
            return PlatformFontResolver.ResolveTypeface(familyName, isBold, isItalic);
            //return base.ResolveTypeface(familyName, isBold, isItalic);
        }

        /// <summary>
        /// Gets the bytes of a physical font with specified typeface name.
        /// </summary>
        /// <param name="faceName">A face name previously retrieved by ResolveTypeface.</param>
        /// <returns>
        /// The bytes of the font.
        /// </returns>
        public byte[] GetFont(string faceName)
        {
            switch (faceName)
            {
                case "Ubuntu#":
                    return FontDataHelper.Ubuntu;

                case "Ubuntu#b":
                    return FontDataHelper.UbuntuBold;

                case "Ubuntu#i":
                    return FontDataHelper.UbuntuItalic;

                case "Ubuntu#bi":
                    return FontDataHelper.UbuntuBoldItalic;
            }
            // PDFsharp never calls GetFont with a face name that was not returned by ResolveTypeface.
            throw new ArgumentException(String.Format("Invalid typeface name '{0}'", faceName));
        }
    }
}
