using kocsi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kocsi.Models;

namespace kocsi.Persistence
{
    public interface IDataAccess
    {
        // Mentés
        Task Save(string path, List<Car> cars);

        // Betöltés
        Task<List<Car>> Load(string path);
    }
}