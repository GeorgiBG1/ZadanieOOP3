using CarsOOP3;
using System.Security.Principal;

namespace CarsOOP3
{
    class Program
    {
        static void Main(string[] args)
        {
            //ElectricCarData carModel1 = new ElectricCarData("Model 3", CarModel.Manufacture.Tesla, 2017, 265.8, 3000, 350, new Battery(230, "Lithium-ion"), 34);
            //TravelInfo travelInfo = new TravelInfo(53, 200, "Model 3");
            //Tank tank = new Tank(100, "Benzine");
            //FuelCarData carModel2 = new FuelCarData("Focus", CarModel.Manufacture.Ford, 1990, 269, 1200, 200, new Tank(340, "Disel"), 34);

            //carModel1.Recharge();
            //carModel2.Refuel();

            dynamic a = 1;
            Console.WriteLine(a);
            a = "A";
            Console.WriteLine(a);
            dynamic b = new Tank();
            Console.WriteLine(b);
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
            if(distance < MaxTravelDistance)
            {
                if(remainingCapacity > Battery.RemainingCapacity)
                {
                    return new TravelInfo(GetHoursForTravel(distance), distance, Model);
                }
                else
                {
                    Console.WriteLine("Recharge needed..." + null);
                    return new TravelInfo(GetHoursForTravel(distance), distance, Model);
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
        private Tank Tank { get => Tank; set => Tank = value; }

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
                    return new TravelInfo(GetHoursForTravel(distance), distance, Model);
                }
                else
                {
                    Console.WriteLine("Recharge needed..." + null);
                    return new TravelInfo(GetHoursForTravel(distance), distance, Model);
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