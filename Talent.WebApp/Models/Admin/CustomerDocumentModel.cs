using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Talent.WebApp.Models.Admin
{
    public class CustomerDocumentModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string DocumentType { get; set; }
    }
}