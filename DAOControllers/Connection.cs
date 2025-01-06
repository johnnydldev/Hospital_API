using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
//using Microsoft.Extensions.Configuration;

namespace DAOControllers
{
    public class Connection
    {

        //private readonly IConfiguration _configuration;
        public static string hospitalConnection = ConfigurationManager.ConnectionStrings["hospitalConnection"].ConnectionString;


    }//End connection class
}//End namespace 
