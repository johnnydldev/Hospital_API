using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Diagnostic
    {
        //Declaring of variables
        //Declaring of getters and setters

        public int idDiagnostic {get; set;}
        public string medicalCondition {get; set;}
        public string registerDate  {get; set;}
        public Treatment treatment  {get; set;}
        public Doctor doctor  {get; set;}
        public Patient patient {get; set;}
        public LaboratoryResult labResult { get; set; }

       
    }//End diagnostic
}//End namespace
