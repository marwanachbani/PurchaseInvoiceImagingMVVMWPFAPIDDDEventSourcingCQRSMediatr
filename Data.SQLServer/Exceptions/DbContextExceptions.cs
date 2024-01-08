using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Exceptions
{
    public class DbContextExceptions : IDbContextException
    {
       

        public string Hundle(DbUpdateException e)
        {
             return e.Message;
        }
    }
    public interface IDbContextException
    {
        string Hundle(DbUpdateException e);
       
    }
}
