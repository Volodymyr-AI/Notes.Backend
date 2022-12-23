
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryValidation : AbstractValidator<GetNoteListQuery>
    {
        public GetNoteListQueryValidation()
        {
            RuleFor(list => list.UserId).NotEqual(Guid.Empty);
            
        }
    }
}
