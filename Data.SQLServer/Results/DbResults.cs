using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Results
{
    public class DbResults : IDbResult
    {
        public string GetModifiedSuccedResult(string element)
        {
            return element + "had modified successfly";
        }

        public string GetSuccedAddedResult(string element)
        {
            return element + "had added successfly"; 
        }

        public string GetSuccedDeleteResult(string element)
        {
            return element + "had deleted successfly";
        }
    }
    public interface IDbResult
    {
        string GetSuccedAddedResult(string element );
        string GetModifiedSuccedResult(string element);
        string GetSuccedDeleteResult(string element);
    }
}
