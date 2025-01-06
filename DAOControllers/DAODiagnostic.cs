using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOControllers
{
    public class DAODiagnostic
    {
        //Declaring of variables
        //Declaring of getters and setters

        public int idDiagnostic {get; set;}
        public string medicalCondition {get; set;}
        public string registerDate  {get; set;}
        public DAOTreatment treatment  {get; set;}
        public DAODoctor doctor  {get; set;}
        public DAOPatient patient {get; set;}
        public DAOLaboratoryResult labResult { get; set; }

       
    }//End diagnostic
}//End namespace
