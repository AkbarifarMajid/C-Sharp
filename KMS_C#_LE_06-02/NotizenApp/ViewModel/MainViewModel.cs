using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NotizenApp.Model;
using NotizenApp.Helper;

namespace NotizenApp.ViewModel
{
   
    // ViewModel class that connects the View and the Model
    public class MainViewModel : INotifyPropertyChanged
    {


        // Visible list of notes that is connected to the UI
        public ObservableCollection<Note> Notes { get; set; }


        // Private variable to hold the text of the new note
        private string newNoteText;


    
        // Public property for accessing the new note text
        public string NewNoteText
        {
            get => newNoteText;
            set
            {
                newNoteText = value;
                OnPropertyChanged();  // Notify (Call)the UI that the value has changed

            }
        }




        // Selected note (for clicking on list item)
        private Note selectedNote;
        public Note SelectedNote
        {
            get => selectedNote;
            set
            {
                selectedNote = value;
                OnPropertyChanged();// Notify(say) the UI that the value has changed
                OnPropertyChanged(nameof(SelectedNoteContent)); // The value of the selected text has also changed
            }
        }





        
        // Command for adding a new note
        public ICommand AddNoteCommand { get; }


       
        // Command for deleting a note
        public ICommand DeleteNoteCommand { get; }


        
        // Constructor: Initializes commands and note list
        public MainViewModel()
        {
            Notes = new ObservableCollection<Note>(); // Starting with an empty list
            AddNoteCommand = new RelayCommand(AddNote, CanAddNote);     // command Add
            DeleteNoteCommand = new RelayCommand<Note>(DeleteNote);    //Delete command with Note parameter
        }




    
        // Method to add a new note to the list
        private void AddNote()
        {
            Notes.Add(new Note(NewNoteText)); // A new note is created and added
            NewNoteText = string.Empty;       // TextBox is empty
        }



       
        // Checks if a new note can be added (must not be empty)
        private bool CanAddNote()
        {
            return !string.IsNullOrWhiteSpace(NewNoteText);
        }



  
        // Method to delete a selected note
        private void DeleteNote(Note note)
        {
            if (note != null)
                Notes.Remove(note);
        }



        // Returns only the content of the selected note
        // If SelectedNote exists, return its Content.
        // If SelectedNote is null or Content is null, return an empty string (""). A ?? B
        public string SelectedNoteContent
        {
            get
            {
                return SelectedNote?.Content ?? string.Empty;
            }
        }



        // Event to say the UI when the value of a property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to call PropertyChanged when the property changes
        private void OnPropertyChanged([CallerMemberName] string propertyName = "") // auto gets the caller's property a special attribute in C#.the compiler automatically passes the name of the method or property that called the method to that parameter.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
