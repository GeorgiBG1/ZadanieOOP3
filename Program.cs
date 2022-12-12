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
            Console.WriteLine("Do you want to create a new car? \n Enter Yes/No");
            string input1 = Console.ReadLine()!;
            input1.ToLower();
            while (input1== "yes")
            {
                Console.WriteLine("Choose a fuel car or an electric car \n Enter Fuel/Electric");
                bool fuelOrElectric = false; // false for fuel, true for electric
                bool defaultOrNot = false; // false for default, true for not
                var tank = new Tank();
                var battery = new Battery();
                string type = Console.ReadLine()!;
                type.ToLower();
                if (type == "fuel")
                {
                    Console.WriteLine($"Enter type of the tank and max capacity or continue with default values (Enter Default)\n");
                    type = Console.ReadLine()!;
                    type.ToLower();
                    if (type == "default") { break; }
                    double capacity = int.Parse(Console.ReadLine()!);
                    tank = new Tank(capacity, type!);
                    defaultOrNot = true;
                }
                else if (type == "electric")
                {
                    fuelOrElectric = true;
                    Console.WriteLine($"Enter type of the battery and max capacity or continue with default values (Enter Default)\n");
                    type = Console.ReadLine()!;
                    type.ToLower();
                    if (type == "default") { break; }
                    double capacity = int.Parse(Console.ReadLine()!);
                    battery = new Battery(capacity, type!);
                    defaultOrNot = true;
                }
                Console.WriteLine("Enter model");
                string model = Console.ReadLine()!;
                Console.WriteLine("Enter manifacturer Ford, Mercedec, BMW, Audi, Tesla");
                string manifacturer = Console.ReadLine()!;
                manifacturer.ToLower();
                var manifacturer1 = CarModel.Manufacture.Ford;
                if (manifacturer == "mercedec")
                {
                    manifacturer1 = CarModel.Manufacture.Mercedes;
                }
                else if (manifacturer == "bmw")
                {
                    manifacturer1 = CarModel.Manufacture.BMW;
                }
                else if (manifacturer == "audi")
                {
                    manifacturer1 = CarModel.Manufacture.Audi;
                }
                else if (manifacturer == "tesla")
                {
                    manifacturer1 = CarModel.Manufacture.Tesla;
                }
                Console.WriteLine("Enter year of make");
                int yearOfMake = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter max speed");
                double maxSpeed = double.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter weight and load capacity");
                double weight = double.Parse(Console.ReadLine()!);
                double loadCapacity = double.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter max travel coefficent");
                double maxTravelCoef = double.Parse(Console.ReadLine()!);
                if (fuelOrElectric == true)
                {
                    if (defaultOrNot == false)
                    {
                        Battery battery1 = new Battery();
                        ElectricCarData car = new ElectricCarData(model, manifacturer1, yearOfMake, maxSpeed, weight, loadCapacity, battery1, maxTravelCoef);
                        Console.WriteLine("Test run or recharge \n Enter Test/Recharge");
                        dynamic input2 = "";
                        input2 = Console.ReadLine()!;
                        input2.ToLower();
                        if (input2 == "test")
                        {
                            Console.WriteLine("Enter distance and travel time");
                            input2 = double.Parse(Console.ReadLine()!);
                            double travelTime = double.Parse(Console.ReadLine()!);
                            CarModel carModel = car;
                            car.GetHoursForTravel(input2!);
                            car.Travel(input2!);
                            TravelInfo travelInfo = new TravelInfo(travelTime, input2, car.Model);
                            travelInfo.ToString();
                        }
                        else if (input2 == "recharge")
                        {
                            battery1.Recharge();
                        }
                    }
                    ElectricCarData car1 = new ElectricCarData(model, manifacturer1, yearOfMake, maxSpeed, weight, loadCapacity, battery, maxTravelCoef);
                    Console.WriteLine("Test run or recharge \n Enter Test/Recharge");
                    dynamic input = "";
                    input= Console.ReadLine()!;
                    input.ToLower();
                    if (input == "test")
                    {
                        Console.WriteLine("Enter distance and travel time");
                        input = double.Parse(Console.ReadLine()!);
                        double travelTime = double.Parse(Console.ReadLine()!);
                        CarModel carModel = car1;
                        car1.GetHoursForTravel(input!);
                        car1.Travel(input!);
                        TravelInfo travelInfo = new TravelInfo(travelTime, input, car1.Model);
                        travelInfo.ToString();
                    }
                    else if (input == "recharge")
                    {
                        battery.Recharge();
                    }
                }
                else
                {
                    if (defaultOrNot == false)
                    {
                        Tank tank1 = new Tank();
                        FuelCarData car2 = new FuelCarData(model, manifacturer1, yearOfMake, maxSpeed, weight, loadCapacity, tank1, maxTravelCoef);
                        Console.WriteLine("Test run or recharge \n Enter Test/Recharge");
                        dynamic input2 = "";
                        input2= Console.ReadLine()!;
                        input2.ToLower();
                        if (input2 == "test")
                        {
                            Console.WriteLine("Enter distance and travel time");
                            input2 = double.Parse(Console.ReadLine()!);
                            double travelTime = double.Parse(Console.ReadLine()!);
                            CarModel carModel = car2;
                            car2.GetHoursForTravel(input2!);
                            car2.Travel(input2!);
                            TravelInfo travelInfo = new TravelInfo(travelTime, input2, car2.Model);
                            travelInfo.ToString();
                        }
                        else if (input2 == "recharge")
                        {
                            car2.Refuel();
                        }
                    }
                    FuelCarData car3 = new FuelCarData(model, manifacturer1, yearOfMake, maxSpeed, weight, loadCapacity, tank, maxTravelCoef);
                    Console.WriteLine("Test run or refuel \n Enter Test/Refuel");
                    Console.WriteLine("Test run or recharge \n Enter Test/Recharge");
                    dynamic input = "";
                    input= Console.ReadLine()!;
                    input.ToLower();
                    if (input == "test")
                    {
                        Console.WriteLine("Enter distance and travel time");
                        input = double.Parse(Console.ReadLine()!);
                        double travelTime = double.Parse(Console.ReadLine()!);
                        CarModel carModel = car3;
                        car3.GetHoursForTravel(input!);
                        car3.Travel(input!);
                        TravelInfo travelInfo = new TravelInfo(travelTime, input, car3.Model);
                        travelInfo.ToString();
                    }
                    else if (input == "refuel")
                    {
                        car3.Refuel();
                    }
                }
            }
        }
    }

    interface ITravelable
    {
        double MaxTravelDistance { get; protected set; }
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
        //public double MaxTravelDistance { get { return Battery.MaxCapacity; } private set { Battery.MaxCapacity } } =  ;
        public double MaxTravelDistance
        {
            get { return capacity; }
            set { value = TravelDistanceKoef * Battery.MaxCapacity; capacity = value; }
        }

        public ElectricCarData(string Model, Manufacture manufacture, int YearOfMake, double MaxSpeed, double Weight, double LoadCapacity, Battery battery, double TravelDistanceKoef) : base()
        {
            base.Model = Model;
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
    public class FuelCarData : CarModel, ITravelable
    {
        private double tankCapacity;
        private Tank Tank { get; set; }

        //public double Tank { get; protected set; }
        public double TravelDistanceKoef { get; protected set; } = 10;
        public double MaxTravelDistance
        {
            get { return tankCapacity; }
            set
            {
                value = Tank.MaxCapacity * TravelDistanceKoef;
                tankCapacity = value;
            }
        }
        public FuelCarData(string Model, Manufacture manufacture, int YearOfMake, double MaxSpeed, double Weight, double LoadCapacity, Tank tank, double TravelDistanceKoef) : base()
        {
            base.Model = Model;
            base.StartYearOfModel = YearOfMake;
            base.MaxSpeedKmPh = MaxSpeed;
            base.Weight = Weight;
            base.LoadCappacity = LoadCapacity;
            this.Tank = tank;
            this.TravelDistanceKoef = TravelDistanceKoef;
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