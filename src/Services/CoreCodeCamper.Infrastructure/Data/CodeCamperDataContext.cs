using Microsoft.EntityFrameworkCore;

namespace CoreCodeCamper.Infrastructure.Data
{

    public class TestModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
    }


    public class CodeCamperDataContext : DbContext
    {
        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Person>(Person.Configure);
            builder.Entity<Room>(Room.Configure);
            builder.Entity<TimeSlot>(TimeSlot.Configure);
            builder.Entity<Track>(Track.Configure);
            builder.Entity<Session>(Session.Configure);
        }
        */
                                                      /*
        public DbSet<Person> Baskets { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Session> Sessions { get; set; }
                                                    */
        public DbSet<TestModel> TestModel { get; set; }

        public CodeCamperDataContext(DbContextOptions<CodeCamperDataContext> options) : base(options) { }

        public CodeCamperDataContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(@"Data Source=VEDRAN-PC\SQLEXPRESS2017;Initial Catalog=CoreCodeCamper;Integrated Security=True;");
        }
        
    }
}
