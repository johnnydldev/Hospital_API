using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOControllers
{
    public class DAOTreatment
    {
        //Declaring of variables
        //Declaring og getters and setters

        public int idTreatment {get; set;}
        public string recommendTreatment {get; set;}
        public string startedDate {get; set;}
        public DAOMedicament medicament { get; set; }


    }//End treatment class
}//End namespace
