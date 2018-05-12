using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks.Dataflow;
using Xunit;

namespace SharpGettext.Tests
{
    public class HeaderTests
    {
        [Fact]
        public void Can_generate_header()
        {
            string header = null;
            using (var stringWriter = new StringWriter())
            {
                Internal.SharpGettextWriter.WriteHeader(new POTemplateHeader
                {
                    CreationDate = new DateTime(2011, 1, 1, 12, 12, 12, DateTimeKind.Utc)
                }, stringWriter);
                stringWriter.Flush();
                header = stringWriter.ToString();
            }
            
            var content = new StringBuilder();

            content.AppendLine("#, fuzzy");
            content.AppendLine("msgid \"\"");
            content.AppendLine("msgstr \"\"");
            content.AppendLine("\"Project-Id-Version: \\n\"");
            content.AppendLine("\"Report-Msgid-Bugs-To: \\n\"");
            content.AppendLine("\"POT-Creation-Date: 2011-01-01 12:12+00:00\\n\"");
            content.AppendLine("\"PO-Revision-Date: YEAR-MO-DA HO:MI+ZONE\\n\"");
            content.AppendLine("\"Last-Translator: \\n\"");
            content.AppendLine("\"Language-Team: \\n\"");
            content.AppendLine("\"Language: \\n\"");
            content.AppendLine("\"MIME-Version: 1.0\\n\"");
            content.AppendLine("\"Content-Type: text/plain; charset=UTF-8\\n\"");
            content.AppendLine("\"Content-Transfer-Encoding: 8bit\\n\"");

            Assert.Equal(content.ToString(), header);
        }
    }
}