using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;

namespace Notes.Persistence.EntityTypeConfigurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(note => note.Id); // Id is our key
            builder.HasIndex(note => note.Id).IsUnique(); // id key is unique
            builder.Property(note => note.Title).HasMaxLength(250); // Title is no longer than 250 symbols in length
        }
    }
}
