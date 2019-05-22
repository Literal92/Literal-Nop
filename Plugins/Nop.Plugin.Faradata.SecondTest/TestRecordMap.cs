using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.Test.Domain
{
   public class TestRecordMap : entityTypeConfiguration<TestRecord>
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PName { get; set; }
        public int PId { get; set; }
        public string DownloadDate { get; set; }
        public bool OnMailingList { get; set; }
    }
}
