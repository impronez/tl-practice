using CarFactory.Bodyworks;
using CarFactory.Cars;
using CarFactory.Colors;
using CarFactory.Engines;
using CarFactory.Transmissons;
using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Controls.Menus.MenuItems;

namespace CarFactory.Services;

public static class ConsoleCarConfigurator
{
    public static ICar Configure()
    {
        var engines = new List<(IEngine engine, string name)>
        {
            (new AtmosphericEngine(), "Atmospheric Engine"),
            (new RotaryEngine(), "Rotary Engine"),
            (new TurbochargedEngine(), "Turbocharged Engine"),
        };
        var engine = GetConfigureParam("engine", engines);
        
        var transmissions = new List<(ITransmission, string)>
        {
            (new AutomaticTransmission(), "Automatic Transmission"),
            (new AutomatedManualTransmission(), "Automated Manual Transmission"),
            (new ContinuouslyVariableTransmission(), "Continuously Variable Transmission"),
            (new ManualTransmission(), "Manual Transmission")
        };
        var transmission = GetConfigureParam("transmission", transmissions);
        
        var bodyworks = new List<(IBodywork, string)>
        {
            (new Hatchback(), "Hatchback"),
            (new Sedan(), "Sedan"),
            (new StationWagon(), "Station Wagon")
        };
        var bodywork = GetConfigureParam("bodywork", bodyworks);
        
        var colors = Enum.GetValues<Color>()
            .Select(c => (c: c, c.ToString()))
            .ToList();
        var color = GetConfigureParam("color", colors);

        return CarFactory.Create(engine, transmission, bodywork, color);
    }

    private static T GetConfigureParam<T>(string parameterName, List<(T option, string name)> options)
    {
        Console.WriteLine($"Select {parameterName}:");

        var menu = new ScrollMenu { EraseAfterClose = true };

        foreach (var value in options)
        {
            menu.AddItem(new LabelMenuItem { Text = value.name });
        }
        
        menu.Display();
        if (menu.SelectedIndex is null ||
            menu.SelectedIndex.Value >= options.Count ||
            menu.SelectedIndex.Value < 0)
        {
            throw new InvalidOperationException($"Invalid selection for {parameterName}");
        }

        return options[menu.SelectedIndex!.Value].option;
    }
}
