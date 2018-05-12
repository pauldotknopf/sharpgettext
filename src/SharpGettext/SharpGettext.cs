using System.Collections.Generic;
using System.IO;

namespace SharpGettext
{
    public static class SharpGettext
    {
        public static void GeneratePOT(POTemplateHeader header, List<POTranslation> translations, TextWriter writer)
        {
            Internal.SharpGettextWriter.WriteHeader(header, writer);

            writer.WriteLine();
            
            foreach (var translation in translations)
            {
                Internal.SharpGettextWriter.WriteTranslation(translation, writer);
                
                writer.WriteLine();
            }
        }

        public static string GeneratePOT(POTemplateHeader header, List<POTranslation> translations)
        {
            using (var stringWriter = new StringWriter())
            {
                GeneratePOT(header, translations, stringWriter);
                stringWriter.Flush();
                return stringWriter.ToString();
            }
        }
    }
}