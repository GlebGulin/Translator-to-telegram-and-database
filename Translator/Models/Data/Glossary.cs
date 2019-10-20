using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Translator.Models.Data
{
    [Table("Glossary")]
    public class Glossary
    {
        public int Id { get; set; }
        public string English { get; set; }
        public string Russian { get; set; }
        
    }
}
