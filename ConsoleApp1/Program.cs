using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Parma.Gasps.XPlat.Firebird;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                context.Add(new Mock() { Name = "Some" });
                context.SaveChanges();
            }
            
            Console.WriteLine("Hello World!");
        }
        
    }

    class Mock
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Context : DbContext
    {
        public Context() : base()
        {
            base.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "somedb.fdb");
            var builder = new ConnectionStringBuilder("somedb.fdb");
            options.UseFirebird(builder.CreateString());
        }
        DbSet<Mock> Mocks { get; set; }
    }
}
