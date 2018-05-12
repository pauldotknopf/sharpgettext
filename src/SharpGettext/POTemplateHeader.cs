using System;

namespace SharpGettext
{
    public class POTemplateHeader
    {
        public POTemplateHeader()
        {
            CreationDate = DateTime.Now;
        }
        
        public string PackageVersion { get; set; }
        
        public string ReportMsgidBugsTo { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public string LastTranslator { get; set; }
        
        public string LanguageTeam { get; set; }
        
        public string Language { get; set; }
    }
}