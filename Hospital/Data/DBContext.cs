using Microsoft.EntityFrameworkCore;

namespace Hospital.Data
{
    public class DBContext : DbContext

    {
        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
        }

    }//End context class
}//End namespace
