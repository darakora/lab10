using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab10KP
{
    [Serializable]
    public class Plane
    {
        private string pointBName; //название пункта назначения
        public string PointBName
        {
            get
            {
                return pointBName;
            }
            set
            {
                if(value.Length == 0)
                {
                    throw new Exception("len of str == 0");
                }
                pointBName = value;
            }
        }

        public decimal FlightNumber { get; set; } //шестизначный номер рейса
        public DateTime DepartureTime { get; set; } //время тправления

        public override string ToString()
        {
            return PointBName + "\n" + FlightNumber + "\n" + DepartureTime + "\n";
        }

        //public override bool Equals(object obj)
        //{
        //    return (obj is Plane) && DepartureTime == ((Plane)obj).DepartureTime;
        //}
    }

    class Airport
    {
        public List<Plane> Planes { get; set; } = new List<Plane>();
        public void infAboutPlanes(int ind) // вывод инф. о самолетах
        {
            Console.WriteLine(Planes[ind]);
        }

        public void InfTimeAboutPlanes() // вывод инф. отправл. в теч часа
        {
            foreach (Plane item in Planes)
            {
                if(item.DepartureTime - DateTime.Now < new TimeSpan(1, 0, 0))
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void InfAboutPointBOfPlanes(string pointBName) // вывод инф. о самолетах отпр. в заданный пункт
        {
            foreach (Plane item in Planes)
            {
                if (item.PointBName == pointBName)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void PrintAllPlanes()
        {
            Planes.Sort((a, b) =>
            {
                return a.DepartureTime.CompareTo(b.DepartureTime);
            });
            foreach (Plane item in Planes)
            {
                Console.WriteLine(item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Plane));
            SoapFormatter soap = new SoapFormatter();
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using(FileStream fs = new FileStream("Plane.dat", FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fs, new Plane() { DepartureTime = new DateTime(2019, 11, 29), FlightNumber = 1232, PointBName = "pip2" });
                Console.WriteLine("Object serialized like Binary");
            }

            using (FileStream fs = new FileStream("Plane.soap", FileMode.OpenOrCreate))
            {
                soap.Serialize(fs, new Plane() { DepartureTime = new DateTime(2019, 11, 29), FlightNumber = 122, PointBName = "pip2" });
                Console.WriteLine("Object serialized like soap");
            }

            using (FileStream fs = new FileStream("Plane.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, new Plane() { DepartureTime = new DateTime(2019, 11, 29), FlightNumber = 122, PointBName = "pip2" });
                Console.WriteLine("Object serialized like xml");
            }
            return;



            Airport airport = new Airport();
            airport.Planes.Add(new Plane() { DepartureTime = new DateTime(2019, 11, 27), FlightNumber = 10, PointBName = "pip" });
            airport.Planes.Add(new Plane() { DepartureTime = new DateTime(2019, 11, 28), FlightNumber = 11, PointBName = "pip1" });
            airport.Planes.Add(new Plane() { DepartureTime = new DateTime(2019, 11, 29), FlightNumber = 12, PointBName = "pip2" });

            airport.InfAboutPointBOfPlanes("pip");
            Console.ReadLine();
        }
    }
}
