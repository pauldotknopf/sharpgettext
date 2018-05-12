using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace SharpGettext.Tests
{
    public class TranslationTests
    {
        [Fact]
        public void Can_write_translation()
        {
            var result = GetTranslation(new POTranslation { Text = "test trans" });

            var expected = new StringBuilder();
            expected.AppendLine("msgid \"test trans\"");
            expected.AppendLine("msgstr \"\"");

            Assert.Equal(expected.ToString(), result);
        }

        [Fact]
        public void Can_write_comment()
        {
            var result = GetTranslation(new POTranslation
            {
                Text = "test trans",
                Comments = new List<string>
                {
                    "comment 1",
                    "comment 2"
                }
            });

            var expected = new StringBuilder();
            expected.AppendLine("#. comment 1");
            expected.AppendLine("#. comment 2");
            expected.AppendLine("msgid \"test trans\"");
            expected.AppendLine("msgstr \"\"");

            Assert.Equal(expected.ToString(), result);
        }

        [Fact]
        public void Can_write_references()
        {
            var result = GetTranslation(new POTranslation
            {
                Text = "test trans",
                References = new List<POSourceReference>
                {
                    new POSourceReference
                    {
                        File = "test1.c"
                    },
                    new POSourceReference
                    {
                        File = "test2.c"
                    }
                }
            });

            var expected = new StringBuilder();
            expected.AppendLine("#: test1.c:0  test2.c:0");
            expected.AppendLine("msgid \"test trans\"");
            expected.AppendLine("msgstr \"\"");

            Assert.Equal(expected.ToString(), result);
        }

        [Fact]
        public void Can_write_plural()
        {
            var result = GetTranslation(new POTranslation
            {
                Text = "test tran",
                Plural = "test trans",
                IsPlural = true
            });

            var expected = new StringBuilder();
            expected.AppendLine("msgid \"test tran\"");
            expected.AppendLine("msgid_plural \"test trans\"");
            expected.AppendLine("msgstr[0] \"\"");
            expected.AppendLine("msgstr[1] \"\"");

            Assert.Equal(expected.ToString(), result);
        }

        private string GetTranslation(POTranslation translation)
        {
            using (var writer = new StringWriter())
            {
                Internal.SharpGettextWriter.WriteTranslation(translation, writer);
                writer.Flush();
                return writer.ToString();
            }
        }
    }
}