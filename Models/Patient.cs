using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Patient
    {
        //Declaring of variables according to sql database
        //Declaring of getters and setters

        public int idPatient {get; set;}
        public string namePatient {get; set;}
        public string middleName {get; set;}
        public string lastName {get; set;}
        public string birthDate {get; set;}
        public string telephone {get; set;}
        public Address address { get; set; }


    }//End Patient class
}//End namespace
