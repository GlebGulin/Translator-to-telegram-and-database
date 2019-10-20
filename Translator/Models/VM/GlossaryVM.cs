using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Translator.Models.Data;

namespace Translator.Models.VM
{
    public class GlossaryVM
    {
        public GlossaryVM()
        {

        }
        public GlossaryVM(Glossary glossary)
        {
            Id = glossary.Id;
            English = glossary.English;
            Russian = glossary.Russian;
            
        }
        public int Id { get; set; }
        public string English { get; set; }
        public string Russian { get; set; }
        
    }
}
