using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Address
    {

        //Declaring of getters and setters
        public int idAddress { get; set; }
        public string street { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        
    }//End Address class

}//End namespace
