using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Prescription
    {
        //Declaring of variables according to sql database
        //Declaring of getters and setters

        public int idRecipe {get; set;}
        public string namePatient{get; set;}
        public string medicalCondition{get; set;}
        public string treatment {get; set;}
        public string test {get; set;}
        public string testResult{get; set;}
        public string medicament{get; set;}
        public string doctor{get; set;}
        public string registerDate{get; set;}
        public Patient patient { get; set; }


    }//End Recipe class
}//End namespace
