using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using kocsi.Models;

namespace kocsi.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private MainModel _model;

    // INPUTOK
    public string BrandInput { get; set; }
    public string ModelInput { get; set; }
    public int YearInput { get; set; }
    public string LicensePlateInput { get; set; }

    public ObservableCollection<Car> Cars { get; set; }

    public RelayCommand AddCarCommand { get; set; }
    public RelayCommand SaveCommand { get; set; }
    public RelayCommand LoadCommand { get; set; }

    // Navigációs események
    public event EventHandler<CarEventArgs> ChangeView;
    public event EventHandler BackToMainView;

    public event EventHandler SaveEvent;
    public event EventHandler LoadEvent;

    public MainViewModel(MainModel model)
    {
        _model = model;
        _model.CarCreated += _model_CarCreated;
        Cars = new ObservableCollection<Car>();

        AddCarCommand = new RelayCommand(AddCar);

        SaveCommand = new RelayCommand(() => { SaveEvent?.Invoke(this, EventArgs.Empty); });
        LoadCommand = new RelayCommand(() => { LoadEvent?.Invoke(this, EventArgs.Empty); });
    }

    private void _model_CarCreated(object? sender, CarEventArgs e)
    {
        // Létrehozunk egy példányt a View számára, parancsokkal
        Car car = new Car(e.Car.Brand, e.Car.Model, e.Car.Year, e.Car.LicensePlate);

        car.RemoveCommand = new RelayCommand(
            () => { Cars.Remove(car); });

        car.DescriptionCommand = new RelayCommand(
            () => { ChangeView?.Invoke(this, new CarEventArgs(car)); });

        car.BackCommand = new RelayCommand(
           () => { BackToMainView?.Invoke(this, new EventArgs()); });

        Cars.Add(car);
    }

    private void AddCar()
    {
        // Új autó létrehozása az inputokból
        Car car = new Car(BrandInput, ModelInput, YearInput, LicensePlateInput);

        car.RemoveCommand = new RelayCommand(
            () => { Cars.Remove(car); });

        car.DescriptionCommand = new RelayCommand(
            () => { ChangeView?.Invoke(this, new CarEventArgs(car)); });

        car.BackCommand = new RelayCommand(
           () => { BackToMainView?.Invoke(this, new EventArgs()); });

        Cars.Add(car);

        // Opcionális: hozzáadás a modell listájához is, ha szinkronban akarod tartani
        _model.cars.Add(car);
    }
}