using CarsOOP3;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace CarsOOP3
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Fuel cars
            string model1 = "AMG C63";
            int yearOfMakeForFirstCar = 2015;
            double maxSpeedForFirstCar = 333.1;
            double weightForFirstCar = 2003.4;
            double loadCapacityForFirstCar = 123.7;
            Tank tankForFirstCar = new Tank(60, "Benzine ali baba");
            double travelDistanceKoefForFirstCar = 3.5;
            FuelCarData myFirstCarWithStockTank = new FuelCarData
                (
                model1,
                CarModel.Manufacture.Mercedes,
                yearOfMakeForFirstCar,
                maxSpeedForFirstCar,
                weightForFirstCar,
                loadCapacityForFirstCar,
                travelDistanceKoefForFirstCar,
                new Tank()
                );
            FuelCarData myFirstCarWithCustomTank = new FuelCarData
                (
                model1,
                CarModel.Manufacture.Mercedes,
                yearOfMakeForFirstCar,
                maxSpeedForFirstCar,
                weightForFirstCar,
                loadCapacityForFirstCar,
                travelDistanceKoefForFirstCar,
                tankForFirstCar
                );
            myFirstCarWithCustomTank.Refuel();
            #endregion
            #region Electric cars
            string model2 = "Model 3";
            int yearOfMakeForSecondCar = 2017;
            double maxSpeedForSecondCar = 233.0;
            double weightForSecondCar = 1611.2;
            double loadCapacityForSecondCar = 93;
            Battery batteryForSecondCar = new Battery(123, "Lithium-ion ali express");//
            double travelDistanceKoefForSecondCar = 2.7;//2.7
            ElectricCarData mySecondCarWithStockBattery = new ElectricCarData
                (
                model2,
                CarModel.Manufacture.Tesla,
                yearOfMakeForSecondCar,
                maxSpeedForSecondCar,
                weightForSecondCar,
                loadCapacityForSecondCar,
                travelDistanceKoefForSecondCar,
                new Battery()
                );
            ElectricCarData mySecondCarWithCustomBattery = new ElectricCarData
                (
                model2,
                CarModel.Manufacture.Tesla,
                yearOfMakeForSecondCar,
                maxSpeedForSecondCar,
                weightForSecondCar,
                loadCapacityForSecondCar,
                travelDistanceKoefForSecondCar,
                batteryForSecondCar
                );
            mySecondCarWithCustomBattery.Recharge();
            #endregion
            #region Output for fuel cars
            Console.WriteLine($"Information about my first car:\nModel: " +
                $"{myFirstCarWithStockTank.Model},\nYearOfMake: " +
                $"{myFirstCarWithStockTank.StartYearOfModel},\nMaxSpeed: " +
                $"{myFirstCarWithStockTank.MaxSpeedKmPh}km/h,\nWeight: " +
                $"{myFirstCarWithStockTank.Weight}kg,\nLoad capacity: " +
                $"{myFirstCarWithStockTank.LoadCappacity}kg.\n\nAdditional information about tank:\nStock tank\nTravel distance coefficient with stock tank: " +
                $"{myFirstCarWithStockTank.MaxTravelDistanceKoef}%, \nMax travel distance: " +
                $"{myFirstCarWithStockTank.MaxTravelDistance}km.\n\nCustom tank\nTravel distance coefficient with stock tank: " +
                $"{myFirstCarWithCustomTank.MaxTravelDistanceKoef}% \nMax travel distance: " +
                $"{myFirstCarWithCustomTank.MaxTravelDistance}km.\n");
            Console.WriteLine("Write a car model and enter values for travel time and distance");
            double distance = double.Parse(Console.ReadLine()!);
            Console.WriteLine($"Travel: \n{myFirstCarWithCustomTank.Travel(distance)}\n");
            #endregion
            #region Output for electric cars
            Console.WriteLine($"Information about my second car:\nModel: " +
                $"{mySecondCarWithStockBattery.Model},\nYearOfMake: " +
                $"{mySecondCarWithStockBattery.StartYearOfModel},\nMaxSpeed: " +
                $"{mySecondCarWithStockBattery.MaxSpeedKmPh}km/h,\nWeight: " +
                $"{mySecondCarWithStockBattery.Weight}kg,\nLoad capacity: " +
                $"{mySecondCarWithStockBattery.LoadCappacity}kg.\n\nAdditional information about battery:\nStock battery\nTravel distance coefficient with stock battery: " +
                $"{mySecondCarWithStockBattery.TravelDistanceKoef}%, \nMax travel distance: " +
                $"{mySecondCarWithStockBattery.MaxTravelDistance}km.\n\nCustom battery\nTravel distance coefficient with stock battery: " +
                $"{mySecondCarWithCustomBattery.TravelDistanceKoef}% \nMax travel distance: " +
                $"{mySecondCarWithCustomBattery.MaxTravelDistance}km.\n");
            Console.WriteLine("Write a car model and enter values for travel time and distance");
            double distance2 = double.Parse(Console.ReadLine()!);
            Console.WriteLine($"Travel: \n{mySecondCarWithCustomBattery.Travel(distance2)}\nPerhaps the battery needs charging for another run!");
            Console.WriteLine("Do you want to exit? Enter Yes/No");
            string exit = Console.ReadLine()!;
            exit.ToLower();
            while (exit!="yes")
            {
            Console.WriteLine("Write a car model and enter values for travel time and distance");
            double distance3 = double.Parse(Console.ReadLine()!);
            Console.WriteLine($"Travel: \n{mySecondCarWithCustomBattery.Travel(distance3)}\n");
            Console.WriteLine("Do you want to exit? Enter Yes/No");
            exit = Console.ReadLine()!;
            }
            #endregion
        }
    }
    #region Classes
    interface ITravelable
    {
        //double MaxTravelDistance { get; protected set; }
        void Recharge();
        void Refuel();
        TravelInfo Travel(double distance);
    }
    public abstract class CarModel
    {
        public string? Model { get; protected set; }
        public enum Manufacture
        {
            Ford,
            Mercedes,
            BMW,
            Audi,
            Tesla
        }
        public int StartYearOfModel { get; protected set; }
        public double MaxSpeedKmPh { get; protected set; }
        public double Weight { get; protected set; }
        public double LoadCappacity { get; protected set; }
        public double GetHoursForTravel(double distance)
        {
            double hours = distance / (double)MaxSpeedKmPh;
            return hours;
        }
    }
    public class ElectricCarData : CarModel, ITravelable
    {
        private Battery Battery { get; set; }
        public double TravelDistanceKoef { get; protected set; } = 120;
        public double MaxTravelDistance => TravelDistanceKoef * Battery.MaxCapacity;

        public ElectricCarData(string Model, Manufacture manufacture, int YearOfMake, double MaxSpeed, double Weight, double LoadCapacity, double TravelDistanceKoef, Battery battery = null) : base()
        {
            base.Model = manufacture.ToString() + " " + Model;
            base.StartYearOfModel = YearOfMake;
            base.MaxSpeedKmPh = MaxSpeed;
            base.Weight = Weight;
            base.LoadCappacity = LoadCapacity;
            this.Battery = battery;
            this.TravelDistanceKoef = TravelDistanceKoef;
        }
        public void Recharge()
        {
            Battery.Recharge();
        }
        public void Refuel() { throw new Exception("Cannot refuel electric car!"); }
        public TravelInfo Travel(double distance)
        {
            double remainingCapacity = distance / TravelDistanceKoef;
            var time = Math.Floor((GetHoursForTravel(distance)) * 100);
            int hours = (int)time / 100;
            time %= 100;
            hours += (int)time / 60;
            hours *= 100;
            while (time >= 60)
            {
                time -= 60;
            }
            time = time + hours;
            time /= 100;
            if (distance < MaxTravelDistance)
            {
                if (remainingCapacity < Battery.RemainingCapacity)
                {
                    Battery.RemainingCapacity -= remainingCapacity;
                    return new TravelInfo(distance, time, Model!);
                }
                else
                {
                    Console.WriteLine("Recharge needed..." + null);
                    return new TravelInfo(distance, time, Model!);
                }
            }
            else
            {
                throw new ApplicationException("ImpossibleTravel!");
            }
        }
    }
    public class Battery
    {
        public double MaxCapacity { get; protected set; }
        public double RemainingCapacity { get; set; }
        public string Type { get; protected set; }
        public int LifeCycles { get; set; } = 100000;
        public Battery()
        {
            MaxCapacity = 50;
            RemainingCapacity = 50;
            Type = "Lithium-ion";
        }
        public Battery(double MaxCapacity, string Type)
        {
            this.MaxCapacity = MaxCapacity;
            this.Type = Type;
        }
        public void Recharge()
        {
            RemainingCapacity = MaxCapacity; LifeCycles -= 1;
            if (LifeCycles == 0)
            {
                RemainingCapacity = 0; 
            }
            Console.WriteLine($"Remaining battery life cycles: {LifeCycles}"); 
        }
    }
    public class FuelCarData : CarModel, ITravelable
    {
        private Tank Tank { get; set; }
        public double MaxTravelDistanceKoef { get; protected set; } = 10;
        public double MaxTravelDistance => MaxTravelDistanceKoef * Tank.MaxCapacity;

        public FuelCarData(string Model, Manufacture manufacture, int YearOfMake, double MaxSpeed, double Weight, double LoadCapacity, double TravelDistanceKoef, Tank tank = null) : base()
        {
            base.Model = manufacture.ToString() + " " + Model;
            base.StartYearOfModel = YearOfMake;
            base.MaxSpeedKmPh = MaxSpeed;
            base.Weight = Weight;
            base.LoadCappacity = LoadCapacity;
            this.Tank = tank;
            this.MaxTravelDistanceKoef = TravelDistanceKoef;
            if (tank != null)
            {
                this.Tank = tank;
            }
            else
            {
                this.Tank = new Tank();
            }
        }
        public void Refuel()
        {
            Tank.RemainingCapacity = Tank.MaxCapacity;
        }
        public void Recharge() { throw new Exception("Cannot recharge fuel car!"); }
        public TravelInfo Travel(double distance)
        {
            double remainingCapacity = distance / MaxTravelDistanceKoef;
            var time = Math.Floor((GetHoursForTravel(distance))*100);
            int hours= (int)time / 100;
            time %= 100;
            hours += (int)time / 60;
            hours *= 100;
            while (time>=60)
            {
                time -= 60;
            }
            time = time+hours;
            time /= 100;
            if (distance < MaxTravelDistance)
            {
                if (remainingCapacity < Tank.RemainingCapacity)
                {
                    Tank.RemainingCapacity -= remainingCapacity;
                    return new TravelInfo(distance, time, Model!);
                }
                else
                {
                    Console.WriteLine("Recharge needed..." + null);
                    return new TravelInfo(distance, time, Model!);
                }
            }
            else
            {
                throw new ApplicationException("ImpossibleTravel!");
            }
        }
    }
    public class Tank
    {
        public double MaxCapacity { get; set; }
        public double RemainingCapacity { get; set; } = 10;
        public string Type { get; protected set; }
        public Tank()
        {
            MaxCapacity = 50;
            RemainingCapacity = 50;
            Type = "Benzine";
        }
        public Tank(double MaxCapacity, string Type)
        {
            this.MaxCapacity = MaxCapacity;
            this.Type = Type;
        }
    }
    public class TravelInfo
    {
        public double TravelTime { get; set; }
        public double Distance { get; protected set; }
        public string ModelCar { get; protected set; }
        public TravelInfo(double Distance, double TravelTime,  string ModelCar)
        {
            this.TravelTime = TravelTime;
            this.Distance = Distance;
            this.ModelCar = ModelCar;
        }
        public override string ToString()
        {
            return $"{ModelCar}, traveled {Distance} km, for {TravelTime} hours";
        }
    }
    #endregion
}