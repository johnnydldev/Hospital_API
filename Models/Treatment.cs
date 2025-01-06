using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Treatment
    {
        //Declaring of variables
        //Declaring og getters and setters

        public int idTreatment {get; set;}
        public string recommendTreatment {get; set;}
        public string startedDate {get; set;}
        public Medicament medicament { get; set; }


    }//End treatment class
}//End namespace
