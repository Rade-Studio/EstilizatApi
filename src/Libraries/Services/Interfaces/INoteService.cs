using System;
using Models.DbEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface INoteService
    {
        Note InsertNote(Note note);
        Note GetNoteById(Guid id);
        List<Note> GetNotesByCategory(string category);
        List<Note> GetAllMyNotes();
        Task<List<Note>> GetAllMyNotesAsync();
        bool DeleteNote(Guid id);
    }
}