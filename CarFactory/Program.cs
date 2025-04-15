using CarFactory.Cars;
using CarFactory.Services;

Console.WriteLine("Welcome to a car configurator!");

ICar car = ConsoleCarConfigurator.Configure();

Console.WriteLine(car.GetStringConfiguration());