using System;

namespace DesignPatterns
{
    public abstract class Duck
    {
        protected IQuack Quack { get; set; }
        protected IFly Fly { get; set; }

        public void Quacking()
        {
            Quack.Quack();
        }

        public void Flying()
        {
            Fly.Fly();
        }

        public void ChangeFly(IFly fly)
        {
            Fly = fly;
        }

        public void ChangeQuack(IQuack quack)
        {
            Quack = quack;
        }
    }

    public class RedHeadDuck : Duck
    {
        public RedHeadDuck()
        {
            Quack = new Quacking();
            Fly = new FlyWithWings();
        }
    }

    public class WoodenDuck : Duck
    {
        public WoodenDuck()
        {
            Quack = new Silent();
            Fly = new FlyNoWay();
        }
    }

    public interface IQuack
    {
        public void Quack();
    }

    public class Quacking : IQuack
    {
        public void Quack()
        {
            Console.WriteLine("Quack");
        }
    }

    public class Squeak : IQuack
    {
        public void Quack()
        {
            Console.WriteLine("Squeak");
        }
    }

    public class Silent : IQuack
    {
        public void Quack()
        {
            Console.WriteLine("Silent");
        }
    }

    public interface IFly
    {
        public void Fly();
    }

    public class FlyWithWings : IFly
    {
        public void Fly()
        {
            Console.WriteLine("Fly");
        }
    }

    public class FlyNoWay : IFly
    {
        public void Fly()
        {
            Console.WriteLine("No Fly");
        }
    }

    public static class Strategy
    {
        public static bool Run()
        {
            var woodenDuck = new WoodenDuck();
            var redHeadDuck = new RedHeadDuck();
            woodenDuck.Flying();
            woodenDuck.ChangeFly(new FlyWithWings());
            woodenDuck.Flying();
            redHeadDuck.Quacking();
            return true;
        }
    }
}