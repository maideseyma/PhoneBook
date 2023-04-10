using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBookEntityLayer.Entities;

namespace PhoneBookDataLayer
{

    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            // ctora parameter verdik
            // generic bir parametre verdik
            // böylece connectionstring özelliğimizi opsiyon oalrak program.cs üzerinden ayarlayacağız

        } // ctor bitti

        // tabloların DBSet propertylerini yazmamız gerekiyor

        public DbSet<Member> MemberTable { get; set; }

        public DbSet<MemberPhone> Phonetable { get; set; }

        public DbSet<PhoneType> PhoneTypes { get; set; }
    }
}
