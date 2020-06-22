using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class SessionNote
    {
        [ForeignKey("Session")]
        public int SessionNoteID { get; set; }

        public byte[] NoteFile { get; set; }
    }
}
