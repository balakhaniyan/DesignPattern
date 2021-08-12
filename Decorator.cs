using System;
using System.Globalization;

namespace DesignPatterns
{
    public abstract class Beverage
    {
        protected string Description;
        public virtual string GetDescription => Description;
        protected decimal Cost;
        public decimal GetCost => Cost;

        public override string ToString()
        {
            return $"{GetDescription}: {GetCost.ToString(CultureInfo.InvariantCulture)}$";
        }
    }

    public class Espresso : Beverage
    {
        public Espresso()
        {
            Description = "Espresso";
            Cost = 1.99m;
        }
    }

    public class HouseBlend : Beverage
    {
        public HouseBlend()
        {
            Description = "House Blend Coffee";
            Cost = .89m;
        }
    }

    public class DarkRoast : Beverage
    {
        public DarkRoast()
        {
            Description = "Dark Roast Coffee";
            Cost = 1.1m;
        }
    }

    public class Decaf : Beverage
    {
        public Decaf()
        {
            Description = "Decaf Coffee";
            Cost = 0.55m;
        }
    }


    public class Mocha : Beverage
    {
        public Mocha(Beverage beverage)
        {
            Cost = beverage.GetCost + 0.35m;
            Description = beverage.GetDescription + ", Mocha";
        }
    }

    public class Whip : Beverage
    {
        public Whip(Beverage beverage)
        {
            Cost = beverage.GetCost + 0.15m;
            Description = beverage.GetDescription + ", Whip";
        }
    }

    public class Soy : Beverage
    {
        public Soy(Beverage beverage)
        {
            Cost = beverage.GetCost + 0.07m;
            Description = beverage.GetDescription + ", Soy";
        }
    }

    public static class Decorator
    {
        public static bool Run()
        {
            Beverage beverage = new Espresso();
            beverage = new Mocha(beverage);
            beverage = new Whip(beverage);
            Console.WriteLine(beverage);
            return false;
        }
    }
}