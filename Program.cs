using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace menuto
{
    class Program
    {
        static void Main(string[] args)
        {
            var shapes = new List<IShape>();
            int command;
            do
            {
                Console.WriteLine("\n1. Add circle");
                Console.WriteLine("2. Add Rectangle");
                Console.WriteLine("3. Removal of areas");
                Console.WriteLine("4. Record to fail");
                Console.WriteLine("5. Loading in fail");
                Console.WriteLine("0. Exit");
                command = int.Parse(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        Console.Write("R=");
                        double r = double.Parse(Console.ReadLine());
                        shapes.Add(new Circle(r));
                        break;
                    case 2:
                        Console.Write("Width=");
                        double w = double.Parse(Console.ReadLine());
                        Console.Write("Heigh=");
                        double h = double.Parse(Console.ReadLine());
                        shapes.Add(new Rectangle(w, h));
                        break;
                    case 3:
                        int i = 1;
                        foreach (var shape in shapes)
                            Console.WriteLine("{0}: {2} area = {1}",
                                i++, shape.Area(), shape.Name);
                        break;
                    case 4:
                        using (var fs = new FileStream("shapes.bin",
                                            FileMode.Create))
                        {
                            foreach (var s in shapes)
                            {
                                var shBytes = s.Serialize();
                                fs.Write(shBytes, 0, shBytes.Length);
                            }
                        }
                        break;
                    case 5:
                        var ds = new TypeDeserializer();
                        ds.RegisterDeserializer(
                            new Circle.Deserializer());
                        ds.RegisterDeserializer(
                            new Rectangle.Deserializer());

                        var bytes = File.ReadAllBytes("shapes.bin");
                        var readIndex = 0;

                        while (readIndex < bytes.Length)
                        {
                            var s = ds.Deserialize(bytes,
                                                ref readIndex);
                            if (s is IShape)
                                shapes.Add(s as IShape);
                        }
                        break;
                }
            }
            while (command != 0);
        }
    }
}
            
            

            



           
        

            
    

