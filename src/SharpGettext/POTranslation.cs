using System.Collections.Generic;

namespace SharpGettext
{
    public class POTranslation
    {
        public string Text { get; set; }
        
        public string Plural { get; set; }

        public string Context { get; set; }

        public bool IsPlural { get; set; }
        
        public string File { get; set; }

        public int? LineNumber { get; set; }

        public List<string> Comments { get; set; }
        
        public List<POSourceReference> References { get; set; }
    }
}