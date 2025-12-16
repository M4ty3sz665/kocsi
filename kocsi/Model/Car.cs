using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace kocsi.Models
{
    public class Car
    {
        public string Brand { get; private set; }       // Márka (pl. Toyota)
        public string Model { get; private set; }       // Típus (pl. Corolla)
        public int Year { get; private set; }           // Évjárat (pl. 2020)
        public string LicensePlate { get; private set; } // Rendszám (pl. AAA-123)

        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand DescriptionCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public Car(string brand, string model, int year, string licensePlate)
        {
            Brand = brand;
            Model = model;
            Year = year;
            LicensePlate = licensePlate;
        }

        public override string ToString()
        {
            // CSV formátum a mentéshez
            return Brand + ";" + Model + ";" + Year + ";" + LicensePlate;
        }
    }
}