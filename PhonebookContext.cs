using System;
using Microsoft.EntityFrameworkCore;

namespace Phonebook{
    // primary database context of ef
    // we write here tables and seeds data too
    public class PhonebookContext : DbContext
    {
        public PhonebookContext(DbContextOptions<PhonebookContext> options)
                : base(options)
        {}
        // our primary tables is Persons
        public DbSet<Person> Persons { get; set; }
        // and Phones for these persons
        public DbSet<Phone> Phones { get; set; }
        // this overrride for system use only, aspnet database path configured at Sturtup class
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=/tmp/phonebook.db");

        // we seed some test data -- no comments
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "An first test name", BirthDay= new DateTime(2008, 5, 1) },
                new Person { Id = 2, Name = "An second test name", BirthDay= new DateTime(2012, 7, 13) }
                );
            modelBuilder.Entity<Phone>().HasData(
                new Phone{ Id = 1, PersonId = 1, Number = "+79999999999" },
                new Phone{ Id = 2, PersonId = 1, Number = "+77777777777" },
                new Phone{ Id = 3, PersonId = 2, Number = "+73333333333" }
            );
            
        }
    }
}