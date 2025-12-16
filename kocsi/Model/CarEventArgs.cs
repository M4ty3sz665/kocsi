using kocsi.Models;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kocsi.Models
{
    public class CarEventArgs : EventArgs
    {
        public Car Car { get; private set; }

        public CarEventArgs(Car car)
        {
            Car = car;
        }
    }
}