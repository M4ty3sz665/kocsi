using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text; // Ez kell az Encoding-hoz!
using System.Threading.Tasks;
using kocsi.Models;
using kocsi.Models;
using kocsi.Persistence;

namespace kocsi.Persistence
{
    public class TextDataAccess : IDataAccess
    {
        public async Task<List<Car>> Load(string path)
        {
            List<Car> cars = new List<Car>();

            // Itt hozzáadjuk az Encoding.UTF8 paramétert
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                try
                {
                    string numberOfCars = await reader.ReadLineAsync();
                    if (string.IsNullOrEmpty(numberOfCars)) return cars;

                    for (int i = 0; i < int.Parse(numberOfCars); i++)
                    {
                        string line = await reader.ReadLineAsync();
                        if (line == null) break;

                        string[] data = line.Split(";");
                        Car car = new Car(data[0], data[1], int.Parse(data[2]), data[3]);
                        cars.Add(car);
                    }
                }
                catch (Exception) { /* Hiba kezelése */ }
            }
            return cars;
        }

        public async Task Save(string path, List<Car> cars)
        {
            // Itt is hozzáadjuk az Encoding.UTF8-at
            // A 'false' paraméter azt jelenti, hogy ne hozzáfűzzön a fájlhoz, hanem írja felül
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                try
                {
                    string num = cars.Count.ToString();
                    await writer.WriteLineAsync(num);
                    for (int i = 0; i < cars.Count; i++)
                    {
                        string line = cars[i].ToString();
                        await writer.WriteLineAsync(line);
                    }
                }
                catch (Exception) { /* Hiba kezelése */ }
            }
        }
    }
}