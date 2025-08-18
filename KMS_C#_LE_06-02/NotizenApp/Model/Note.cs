using System;

namespace NotizenApp.Model
{

    // This class show a note
    public class Note
    {
        
        // The content of the note entered by the user
        public string Content { get; set; }

        

        // The date and time when the note was created
        public DateTime CreatedAt { get; set; }


        // Class constructor - initializes Content and sets CreatedAt when creating a note
        public Note(string content)
        {
            Content = content;
            CreatedAt = DateTime.Now; // The running time is automatically recorded
        }
    }
}
