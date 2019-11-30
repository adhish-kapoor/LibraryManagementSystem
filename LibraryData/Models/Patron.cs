using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
   public class Patron
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephonenumber { get; set; }

        //virtual keyword is used to lazy load that property's data. 
        //Lazy loading loads a collection from the database the first time it is accessed.
        public virtual LibraryCard LibraryCard { get; set; }
        public virtual LibraryBranch HomeLibraryBranch { get; set; }
    }
}
