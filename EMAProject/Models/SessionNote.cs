using System.ComponentModel.DataAnnotations.Schema;

namespace EMAProject.Models
{
    public class SessionNote
    {
        [ForeignKey("Session")]
        public int SessionNoteID { get; set; }

        public byte[] NoteFile { get; set; }

    }
}
