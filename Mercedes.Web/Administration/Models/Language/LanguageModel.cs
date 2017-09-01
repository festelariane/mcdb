using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes.Admin.Models.Language
{
    public class LanguageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Culture { get; set; }
        public bool Active { get; set; }
        public bool IsDefault { get; set; }
    }
}