using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpGettext.Internal
{
    public static class SharpGettextWriter
    {
        // Some escaped values to make things easier to read.
        private const string Quote = "\"";
        private const string NewLine = "\\n";
        
        public static void WriteHeader(POTemplateHeader header, TextWriter writer)
        {
            void WriteQuotedValue(string value)
            {
                writer.WriteLine($"{Quote}{value}{NewLine}{Quote}");
            }
            
            writer.WriteLine("#, fuzzy");
            writer.WriteLine($"msgid {Quote}{Quote}");
            writer.WriteLine($"msgstr {Quote}{Quote}");
            WriteQuotedValue($"Project-Id-Version: {header.PackageVersion}");
            WriteQuotedValue($"Report-Msgid-Bugs-To: {header.ReportMsgidBugsTo}");
            WriteQuotedValue($"POT-Creation-Date: {ConvertDateTimeToString(header.CreationDate)}");
            WriteQuotedValue("PO-Revision-Date: YEAR-MO-DA HO:MI+ZONE");
            WriteQuotedValue($"Last-Translator: {header.LastTranslator}");
            WriteQuotedValue($"Language-Team: {header.LanguageTeam}");
            WriteQuotedValue($"Language: {header.Language}");
            WriteQuotedValue("MIME-Version: 1.0");
            WriteQuotedValue("Content-Type: text/plain; charset=UTF-8");
            WriteQuotedValue("Content-Transfer-Encoding: 8bit");
        }

        public static void WriteTranslation(POTranslation translation, TextWriter writer)
        {
            if (translation.Comments != null)
            {
                foreach (var comment in translation.Comments)
                {
                    writer.WriteLine($"#. {comment}");
                }
            }

            if (translation.References != null && translation.References.Count > 0)
            {
                writer.WriteLine($"#: {string.Join("  ", translation.References.Select(x => $"{x.File}:{x.LineNumber}").ToArray())}");
            }
            
            WriteString(writer, "msgid", Escape(translation.Text));
            if (translation.IsPlural)
            {
                WriteString(writer, "msgid_plural", Escape(translation.Plural));
                WriteString(writer, "msgstr[0]", string.Empty);
                WriteString(writer, "msgstr[1]", string.Empty);
            }
            else
            {
                WriteString(writer, "msgstr", string.Empty);
            }
        }
        
        private static string Escape(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? null : s.Replace("\"", "\\\"");
        }

        private static void WriteString(TextWriter writer, string type, string value)
        {
            // Logic for outputting multi-line msgid.
            //
            // IN : a<LF>b
            // OUT: msgid ""
            //      "a\n"
            //      "b"
            //
            // IN : a<LF>b<LF>
            // OUT: msgid ""
            // OUT: "a\n"
            //      "b\n"
            //
            value = value ?? "";
            value = value.Replace("\r\n", "\n");
            var sb = new StringBuilder(100);
            // If multi-line
            if (value.Contains('\n'))
            {
                // · msgid ""
                sb.AppendFormat("{0} \"\"\r\n", type);
                // · following lines
                sb.Append("\"");
                var s1 = value.Replace("\n", "\\n\"\r\n\"");
                sb.Append(s1);
                sb.Append("\"");
            }
            // If single-line
            else
            {
                sb.AppendFormat("{0} \"{1}\"", type, value);
            }
            
            writer.WriteLine(sb.ToString());
        }

        private static string ConvertDateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mmzzzz");
        }
    }
}