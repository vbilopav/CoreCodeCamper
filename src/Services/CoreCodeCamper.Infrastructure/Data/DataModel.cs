using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CoreCodeCamper.Infrastructure.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }

        public static void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.FirstName).HasMaxLength(64).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(128).IsRequired();
            builder.Property(e => e.Image).HasMaxLength(256).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(512).IsRequired();
            builder.Property(e => e.Bio).HasMaxLength(1024).IsRequired();
        }
    }

    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(64).IsRequired();
            builder.HasIndex(e => e.Name).IsUnique();
        }
    }

    public class TimeSlot
    {
        public DateTime Start { get; set; }
        public int DurationMinutes { get; set; }

        public static void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.HasKey(e => e.Start);
            builder.Property(e => e.DurationMinutes)
                .IsRequired()
                .HasDefaultValue(Core.Model.TimeSlot.DefaultDurationMinutes);
        }
    }

    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(64).IsRequired();
            builder.HasIndex(e => e.Name).IsUnique();
        }
    }

    public class Session
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public Track Track { get; set; }
        public Room Room { get; set; }
        public Person Speaker { get; set; }

        public static void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).HasMaxLength(32);
            builder.Property(e => e.Title).HasMaxLength(64).IsRequired();
            builder.HasIndex(e => e.Title).IsUnique();
            builder.Property(e => e.Title).HasMaxLength(256).IsRequired();
        }
    }
}
