using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Translator.Models.Data;

namespace Translator.Models.Context
{
    public class GlossaryContext : DbContext
    {
        public DbSet<Glossary> glossary { get; set; }
        //public GlossaryContext(DbContextOptions<GlossaryContext> options) : base(options)
        //{

        //}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Glossary_db");
        }



    }
}
