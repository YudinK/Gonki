using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication192
{
    class Program
    {
        static void Main(string[] args)
        {
            SportCar car = new SportCar();
            Bus bus = new Bus();
            CargoCar cargoCar = new CargoCar();
            Automobile automobile = new Automobile();
            List<Thread> threads = new List<Thread>()
            {
                new Thread(cargoCar.Ride),
                new Thread(car.Ride),
                new Thread(bus.Ride),
                new Thread(automobile.Ride)
            };

            //---------------------------------▓██▓
            var carsVs = new CarVs[] { new CarVs() { Color = ConsoleColor.Red }, new CarVs() { Color = ConsoleColor.Green } };
            var limit = 80;
            var rnd = new Random();

            while (carsVs.All(carVs => carVs.X < limit))
            {
                for (int i = 0; i < carsVs.Length; i++)
                {
                    var carVs = carsVs[i];
                    //
                    Console.ForegroundColor = carVs.Color;
                    Console.SetCursorPosition((int)carVs.X, 3 + i * 2);
                    Console.Write("    ");
                    //
                    carVs.Speed += (float)rnd.NextDouble() / 10f;
                    carVs.X += carVs.Speed;
                    //
                    Console.SetCursorPosition((int)carVs.X, 3 + i * 2);
                    Console.Write("▓██▓");
                }
                Thread.Sleep(150);
            }
            //---------------------------------


            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Finish");
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (Thread list in threads)
            {

                list.Start();
            }
            Console.ReadKey();
        }
    }
}
//---------------------------------▓██▓
class CarVs
{
    public float X;
    public float Speed = 0.5f;
    public ConsoleColor Color;
}

//----------------------------------
abstract class Car
{
    public delegate void Start();
    public delegate void Message();
    public event Message Display;

    public int CarSpeed(Random rnd, int r1, int r2)
    {
        int speed;
        return speed = rnd.Next(r1, r2 + 1);
    }

    public void Win(string name)
    {
        Console.WriteLine($"{name}");
    }

    public void Go(string name, int speed)
    {
        int distance = 0;
        Display += () => Win(name);
        for (int i = 0; i < 110; i += 10, distance += 10)
        {
            Thread.Sleep(50);
            if (distance == 100)
                Display();
        }
    }
}
class SportCar : Car
{
    public string Name = "Sport car";

    public void Ride()
    {
        Go(Name, CarSpeed(new Random(), 500, 800));
    }
}

class Automobile : Car
{
    public string Name = "Automobile";

    public void Ride()
    {
        Go(Name, CarSpeed(new Random(), 600, 900));
    }
}

class CargoCar : Car
{
    public string Name = "Cargo car";
    public void Ride()
    {
        Go(Name, CarSpeed(new Random(), 700, 1000));
    }
}

class Bus : Car
{
    public string Name = "Bus";
    public void Ride()
    {
        Go(Name, CarSpeed(new Random(), 800, 1200));
    }
}


