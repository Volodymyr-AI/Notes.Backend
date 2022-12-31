using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Common
{
    public class NotesContextClass
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();    
        public static Guid NoteIdForUpdate = Guid.NewGuid();

        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new NotesDbContext(options);  
            context.Database.EnsureCreated();
            context.Notes.AddRange(
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details1",
                    EditDate = null,
                    Id = Guid.Parse("C3510481-B8B3-4C01-BB14-7AB21E6FF696"),
                    Title = "Title1",
                    UserId = UserAId
                },
                new Note
                {
                    CreationDate = DateTime.Today.AddDays(-1),
                    Details = "Details2",
                    EditDate = DateTime.Today,


                }
            );
        }
    }
}
