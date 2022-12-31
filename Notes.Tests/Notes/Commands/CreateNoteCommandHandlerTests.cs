using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Notes.Commands
{
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            //Arrange
            var handler = new CreateNoteCommandHandler(Context);
            var noteName = "note name";
            var noteDetails = "note details";


            //Act
            var notedId = await handler.Handle(
                new CreateNoteCommand
                {
                    Title = noteName,
                    Detail = noteDetails,
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None);
            //Assert
            Assert.NotNull(
                await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == notedId && note.Title == noteName && note.Details == noteDetails));
        }
    }
}
