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
            Tank tankForFirstCar = new Tank(66, "Benzine ali baba");
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
            #endregion
            #region Electric cars
            string model2 = "Model 3";
            int yearOfMakeForSecondCar = 2017;
            double maxSpeedForSecondCar = 233.0;
            double weightForSecondCar = 1611.2;
            double loadCapacityForSecondCar = 93;
            Battery batteryForSecondCar = new Battery(50, "Lithium-ion ali express");
            double travelDistanceKoefForSecondCar = 2.7;
            ElectricCarData mySecondCarWithStockBattery = new ElectricCarData
                (
                model2,
                CarModel.Manufacture.Tesla,
                yearOfMakeForSecondCar,
                maxSpeedForSecondCar,
                weightForSecondCar,
                loadCapacityForSecondCar,
                new Battery(),
                travelDistanceKoefForSecondCar
                );
            ElectricCarData mySecondCarWithCustomBattery = new ElectricCarData
                (
                model2,
                CarModel.Manufacture.Tesla,
                yearOfMakeForSecondCar,
                maxSpeedForSecondCar,
                weightForSecondCar,
                loadCapacityForSecondCar,
                batteryForSecondCar,
                travelDistanceKoefForSecondCar
                );
            #endregion
            #region Output for fuel cars
            Console.WriteLine($"Information about my first car:\nModel: " +
                $"{myFirstCarWithStockTank.Model},\nYearOfMake: " +
                $"{myFirstCarWithStockTank.StartYearOfModel},\nMaxSpeed: " +
                $"{myFirstCarWithStockTank.MaxSpeedKmPh}km/h,\nWeight: " +
                $"{myFirstCarWithStockTank.Weight}kg,\nLoad capacity: " +
                $"{myFirstCarWithStockTank.LoadCappacity}kg.\n\nAdditional information about tank:\nStock tank\nTravel distance coefficient with stock tank: " +
                $"{myFirstCarWithStockTank.TravelDistanceKoef}%, \nMax travel distance: " +
                $"{myFirstCarWithStockTank.MaxTravelDistance}km/h.\n\nCustom tank\nTravel distance coefficient with stock tank: " +
                $"{myFirstCarWithCustomTank.TravelDistanceKoef}% \nMax travel distance: " +
                $"{myFirstCarWithCustomTank.MaxTravelDistance}km/h.\n");
            Console.WriteLine("Write a car model and enter values for travel time and distance");
            double travelTime = double.Parse(Console.ReadLine()!);
            double distance = double.Parse(Console.ReadLine()!);
            TravelInfo travelInfo = new TravelInfo(travelTime, distance, myFirstCarWithStockTank.Model!);
            Console.WriteLine();
            Console.WriteLine("Travel with stock tank: \n" + myFirstCarWithStockTank.Travel(distance));
            Console.WriteLine("\nTravel with custom tank: \n" + myFirstCarWithCustomTank.Travel(distance));
            Console.WriteLine();
            travelInfo.ToString(); //���� �� ������!
            #endregion
            #region Output for electric cars
            Console.WriteLine($"Information about my first car:\nModel: " +
                $"{mySecondCarWithStockBattery.Model},\nYearOfMake: " +
                $"{mySecondCarWithStockBattery.StartYearOfModel},\nMaxSpeed: " +
                $"{mySecondCarWithStockBattery.MaxSpeedKmPh}km/h,\nWeight: " +
                $"{mySecondCarWithStockBattery.Weight}kg,\nLoad capacity: " +
                $"{mySecondCarWithStockBattery.LoadCappacity}kg.\n\nAdditional information about tank:\nStock tank\nTravel distance coefficient with stock tank: " +
                $"{mySecondCarWithStockBattery.TravelDistanceKoef}%, \nMax travel distance: " +
                $"{mySecondCarWithStockBattery.MaxTravelDistance}km/h.\n\nCustom tank\nTravel distance coefficient with stock tank: " +
                $"{mySecondCarWithCustomBattery.TravelDistanceKoef}% \nMax travel distance: " +
                $"{mySecondCarWithCustomBattery.MaxTravelDistance}km/h.\n");
            Console.WriteLine("Write a car model and enter values for travel time and distance");
            double travelTime2 = double.Parse(Console.ReadLine()!);
            double distance2 = double.Parse(Console.ReadLine()!);
            travelInfo = new TravelInfo(travelTime, distance, mySecondCarWithStockBattery.Model!);
            Console.WriteLine();
            Console.WriteLine("Travel with stock tank: \n" + mySecondCarWithStockBattery.Travel(distance));
            Console.WriteLine("\nTravel with custom tank: \n" + mySecondCarWithCustomBattery.Travel(distance));
            Console.WriteLine();
            travelInfo.ToString(); //���� �� ������!
            #endregion
        }
    }
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
        private double capacity;
        private Battery Battery { get; set; }
        public double TravelDistanceKoef { get; protected set; } = 120;
        public double MaxTravelDistance
        {
            get { return capacity; }
            set { value = TravelDistanceKoef * Battery.MaxCapacity; capacity = value; }
        }

        public ElectricCarData(string Model, Manufacture manufacture, int YearOfMake, double MaxSpeed, double Weight, double LoadCapacity, Battery battery, double TravelDistanceKoef) : base()
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
            Battery.RemainingCapacity = Battery.MaxCapacity;
        }
        public void Refuel() { throw new Exception("Cannot refuel electric car!"); }
        public TravelInfo Travel(double distance)
        {
            double remainingCapacity = distance / TravelDistanceKoef;
            if (distance < MaxTravelDistance)
            {
                if (remainingCapacity > Battery.RemainingCapacity)
                {
                    return new TravelInfo(GetHoursForTravel(distance), distance, Model!);
                }
                else
                {
                    Console.WriteLine("Recharge needed..." + null);
                    return new TravelInfo(GetHoursForTravel(distance), distance, Model!);
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
        public string Type { get; set; }//protected set
        public int LifeCycles { get; set; } = 100000;
        public Battery()
        {
            MaxCapacity = 50;
            RemainingCapacity = 50;
            Type = "Lithium-ion";
        }
        public Battery(double MaxCapacity, string Type)
        {
            RemainingCapacity = MaxCapacity;
            this.Type = Type;
        }
        public void Recharge() { RemainingCapacity = MaxCapacity; LifeCycles -= 1; if (LifeCycles == 0) { RemainingCapacity = 0; } }
    }
    public class FuelCarData : CarModel, ITravelable
    {
        private double tankCapacity;
        private Tank Tank { get; set; }
        public double TravelDistanceKoef { get; protected set; } = 10;
        public double MaxTravelDistance
        =>tankCapacity;
        //�� ������� �������� � �������� ��������!!!

        public FuelCarData(string Model, Manufacture manufacture, int YearOfMake, double MaxSpeed, double Weight, double LoadCapacity, double TravelDistanceKoef, Tank tank = null) : base()
        {
            base.Model = manufacture.ToString() + " " + Model;
            base.StartYearOfModel = YearOfMake;
            base.MaxSpeedKmPh = MaxSpeed;
            base.Weight = Weight;
            base.LoadCappacity = LoadCapacity;
            this.Tank = tank;
            this.TravelDistanceKoef = TravelDistanceKoef;
            tankCapacity = Tank.MaxCapacity * this.TravelDistanceKoef;
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
            double remainingCapacity = distance / TravelDistanceKoef;
            if (distance < MaxTravelDistance)
            {
                if (remainingCapacity > Tank.RemainingCapacity)
                {
                    return new TravelInfo(GetHoursForTravel(distance), distance, Model!);
                }
                else
                {
                    Console.WriteLine("Recharge needed..." + null);
                    return new TravelInfo(GetHoursForTravel(distance), distance, Model!);
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
            RemainingCapacity = MaxCapacity;
            //this.MaxCapacity
            this.Type = Type;
        }
    }
    public class TravelInfo
    {
        public double TravelTime { get; set; }
        public double Distance { get; protected set; }
        public string ModelCar { get; protected set; }
        public TravelInfo(double TravelTime, double Distance, string ModelCar)
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
}