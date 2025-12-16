using kocsi.Models;
using kocsi.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kocsi.Persistence;

namespace kocsi.Models
{
    public class MainModel
    {
        private IDataAccess _dataAccess;

        public event EventHandler<CarEventArgs> CarCreated;

        public List<Car> cars;
        public List<Car> removeCars;

        public MainModel(IDataAccess access)
        {
            cars = new List<Car>();
            removeCars = new List<Car>(); // Inicializálás, hogy ne legyen null reference
            _dataAccess = access;
        }

        public void RemoveCar(Car car)
        {
            // Törlés rendszám alapján (feltételezzük, hogy egyedi)
            foreach (Car c in cars)
            {
                if (c.LicensePlate == car.LicensePlate)
                {
                    cars.Remove(c);
                    break; // Megtaláltuk, kiléphetünk
                }
            }
            removeCars.Add(car);
        }

        public async Task Load(string path)
        {
            List<Car> newCars = await _dataAccess.Load(path);
            foreach (Car c in newCars)
            {
                cars.Add(c);
                CarCreated?.Invoke(this, new CarEventArgs(c));
            }
        }

        public void Save(string path)
        {
            _dataAccess.Save(path, cars);
        }

        public void GenerateCar(int num)
        {
            for (int i = 0; i < num; i++)
            {
                string brand = "Brand" + i;
                string model = "Model" + (i * 100);
                int year = 2000 + i;
                string licensePlate = "ABC-" + (100 + i);

                Car car = new Car(brand, model, year, licensePlate);
                cars.Add(car);
                CarCreated?.Invoke(this, new CarEventArgs(car));
            }
        }
    }
}