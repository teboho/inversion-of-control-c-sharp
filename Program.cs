

using System;
using Microsoft.Extensions.DependencyInjection;

namespace UnderstandingIoC
{    public class Engine
    {
        public virtual void Start()
        {
            Console.WriteLine("Engine is running");
        }
    }

    public class ElectricEngine : Engine
    {
        public override void Start()
        {
            Console.WriteLine("Electric* engine is running");
        }
    }

    public class Car
    {
        private Engine _engine;
        // dependency injection, i.e. the dependency comes from outside and we are injecting it into this class that will need it :)
        public Car(Engine engine)
        {
            _engine = engine;
        }

        public void Drive()
        {
            _engine.Start();
            Console.WriteLine("The car is driving");
        }
    }

    public class HelloWorld
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<Engine>() // Register the dependencies whose object you might need later
                .AddTransient<Car>() // Register the Car class so you can ask for its object later :)
                .BuildServiceProvider();

            // because we have registered the Car class we can ask for its object now
            var car = serviceProvider.GetService<Car>();
            // the thing is that the Car class needs an Engine object to be passed to it,
            // so the service provider will look for the Engine object from it's other transient and pass it to the Car object aptly
            car.Drive();
        }
    }
}
