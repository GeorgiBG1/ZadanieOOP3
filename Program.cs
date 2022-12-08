using CarsOOP3;
using System.Security.Principal;

namespace CarsOOP3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
    interface ITravelable
    {
        public double MaxTravelDistance { get; protected set; }
        public void Refuel();
        public TravelInfo Travel(double distance);
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

        protected double GetHoursForTravel(double distance)
        {
            double hours = distance / (double)MaxSpeedKmPh;
            return hours;
        }
    }


    public class ElectricCarData : CarModel, ITravelable
    {
        private double capacity;
        private Battery Battery { get => Battery; set => Battery = value; }
        public double TravelDistanceKoef { get; protected set; } = 120;
        //public double MaxTravelDistance { get { return Battery.MaxCapacity; } private set { Battery.MaxCapacity } } =  ;
        double ITravelable.MaxTravelDistance { get { return capacity; } set { value = TravelDistanceKoef * Battery.MaxCapacity; capacity = value; }
        }

        public ElectricCarData(string Model,/*enum Manufacturer,*/ int YearOfMake, double MaxSpeed, double Weight, double LoadCapacity, Battery battery, double TravelDistanceKoef) : base()
        {
            base.Model = Model;
            base.StartYearOfModel = YearOfMake;
            base.MaxSpeedKmPh = MaxSpeed;
            base.Weight = Weight;
            base.LoadCappacity = LoadCapacity;
            this.Battery= battery;
            this.TravelDistanceKoef = TravelDistanceKoef;    
        }
        void ITravelable.Refuel()
        {
            Battery.RemainingCapacity = Battery.MaxCapacity;
        }
        public TravelInfo Travel(double distance) 
        {
            GetHoursForTravel(distance);
            //TODO if проверка, защото не разбрах условието, а тук трябва да има if(){}else{}
            throw new ApplicationException("ImpossibleTravel!");
            Console.WriteLine("Recharge needed..." + null);
        }
    }//
    public class FuelCarData : CarModel, ITravelable
    {
        private double tankCapacity;

        public Tank Tank { get; protected set; }
        public double MaxTravelDistanceKoef { get; protected set; } = 10;
        double ITravelable.MaxTravelDistance
        {
            get { return tankCapacity; }
            set { value = Tank.MaxCapacity * MaxTravelDistanceKoef; 
                tankCapacity = value; }
        }
        public FuelCarData(string Model,/*enum Manufacturer,*/ int YearOfMake, double MaxSpeed, double Weight, double LoadCapacity, Tank tank, double TravelDistanceKoef) : base()
        {
            base.Model = Model;
            base.StartYearOfModel = YearOfMake;
            base.MaxSpeedKmPh = MaxSpeed;
            base.Weight = Weight;
            base.LoadCappacity = LoadCapacity;
            this.Tank = tank;
            this.MaxTravelDistanceKoef = TravelDistanceKoef;
        }
        void ITravelable.Refuel()
        {
            throw new NotImplementedException();
        }
    }
    public class Battery
    {
        public double MaxCapacity { get; protected set; }
        public double RemainingCapacity { get; set ; }
        public string Type { get; set; }//protected set
        public int LifeCycles { get; set; } = 100000;
        public Battery()
        {
            
            MaxCapacity = 50;
            RemainingCapacity= 50;
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
        public Tank MaxCapacity { get; set; }
        public double RemainingCapacity { get; protected set; } = 10;
        public string Type { get; protected set; }

        public Tank()
        {
            MaxCapacity = 50;
            RemainingCapacity = 50;
            Type = "Benzine";
        }
        public Tank(Tank MaxCapacity, string Type)
        {
            this.MaxCapacity = MaxCapacity;
            this.Type = Type;
        }
    }
    public class TravelInfo
    {
        public double TravelTime { get; set; }
        public double Distance { get; protected set; }
        private string ModelCar { get; set; }

        public TravelInfo(string ModelCar, double TravelTime, double Distance)
        {
            this.ModelCar = ModelCar;
            this.TravelTime = TravelTime;
            this.Distance = Distance;
        }

        public override string ToString()
        {
            return $"{ModelCar}, traveled {Distance} km, for {TravelTime} hours";
        }
    }
    enum Manufacturer
    {
        Ford,
        Mercedes,
        BMW,
        Audi,
        Tesla
    }
}