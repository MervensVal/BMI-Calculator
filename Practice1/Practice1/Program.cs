using System;
using System.Collections.Generic;
using System.IO;

namespace Practice1
{
    class Program
    {
        static void Main(string[] args)
        {
            Results();
        }

        public static void Results()
        {
            //use loop, array, and text file
            List<BMI> bMIList = new List<BMI>();
            char countinue = 'Y';

            while (countinue == 'Y' || countinue == 'y')
            {
                try
                {
                    BMI.greetings();
                    BMI bMI = new BMI();
                    Console.Write("Please enter name: ");
                    bMI.Name = Console.ReadLine();
                    BMI.greetings(bMI.Name);

                    Console.Write("Please enter birth year: ");
                    int birthYear = Convert.ToInt32(Console.ReadLine());
                    bMI.Age = bMI.calculateAge(birthYear);

                    Console.Write("Weight: ");
                    bMI.Weight = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Height: ");
                    bMI.Height = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("--------------------------------------");

                    Console.WriteLine("Hi " + bMI.Name + ". The results are:  ");
                    bMI.BMINumber = bMI.Calculate();
                    bMI.Decision(bMI.BMINumber);
                    bMIList.Add(bMI);
                    bMI.saveToFile();
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("Type 'Y' to continue");
                    Console.WriteLine("Type any other key to close the program:  ");
                    countinue = Convert.ToChar(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("======================================");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(" Please try again");
                    Console.WriteLine("======================================");
                }
            }
            double sum = 0;
            double sumAge = 0;
            double average;
            double averageAge;
            foreach (BMI bmi in bMIList)
            {
                sum = +bmi.BMINumber;
                sumAge = +bmi.Age;
            }
            average = sum / bMIList.Count;
            double av = Math.Truncate(average * 100) / 100;
            averageAge = Math.Truncate(sumAge / bMIList.Count);
            Console.WriteLine("======================================");
            Console.WriteLine("The average BMI for the " + bMIList.Count + " individuals is " + av);
            Console.WriteLine("The average age of the " + bMIList.Count + " individuals is " + averageAge);
            Console.WriteLine("======================================");
        }
    }

    public interface IBMI {
        public double Calculate();
        public void Decision(double calc);
        public int calculateAge(int birthyear);
    }

    public class BMI : IBMI
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double BMINumber { get; set; }
        public BMI()
        {

        }

        public double Calculate()
        {
            return 703 * (this.Weight / (Math.Pow((this.Height * 12), 2)));
        }

        public void Decision(double calc)
        {
            //double c = Math.Truncate(calc * 100) / 100;
            double c = calc;

            if (c < 18.5)
            {
                Console.WriteLine("Underweight - BMI is " + c);
            }
            else if (c >= 18.5 && c <= 24.9)
            {
                Console.WriteLine("Normal weight - BMI is " + c);
            }
            else if (c >= 25 && c <= 29.9)
            {
                Console.WriteLine("Overweight - BMI is " + c);
            }
            else if (c >= 30)
            {
                Console.WriteLine("Obese - BMI is " + c);
            }
            else 
            {
                Console.WriteLine("Unable to calculate");
            }
        }

        public int calculateAge(int birthyear)
        {
            var today = DateTime.Today.Year;
            return today - birthyear;           
        }

        public static void greetings()
        {
            Console.WriteLine("Hi Welcome to the BMI calculator");
        }
        public static void greetings(string name)
        {
            Console.WriteLine("Lets get started " + name);
        }

        public void saveToFile()
        {
            StreamWriter sw = new StreamWriter("C:\\(Add path to text file here)", true);
            sw.WriteLine("-------------------");
            sw.WriteLine("Name: " + this.Name);
            sw.WriteLine("Age: " + this.Age);
            sw.WriteLine("Weight: " + this.Weight);
            sw.WriteLine("Height: " + this.Height);
            sw.WriteLine("BMI: " + this.BMINumber);
            sw.Close();
        }
    }
}
