using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Doctor
    {
        //Declaring of variables
        //Declaring of getters and setters

        public int idDoctor  {get; set;}
        public string nameDoctor {get; set;}
        public string middleName {get; set;}
        public string lastName  {get; set;}
        public string birthDate {get; set;}
        public string telephone {get; set;}
        public Address address { get; set;}


    }//End Doctor class
}//End namespace
