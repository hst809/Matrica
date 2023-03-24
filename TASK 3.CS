using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace ConsoleApp1
{
    class DataClass
    {
        public DateTime _Date { get; set; }
        public double _value { get; set; }
        public override string ToString()
        {
            return string.Format("{0},{1}", _Date.ToString("dd/MM/yyyy HH:mm"), _value.ToString());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<DataClass> shifts = new List<DataClass>();
            List<DataClass> data = new List<DataClass>();
            try
            {
                string fileName = Environment.CurrentDirectory + @"\Data.txt";
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string lines = sr.ReadLine();
                    while (lines != "[SHIFTS]")
                    {
                        lines = sr.ReadLine();
                    }
                    lines = sr.ReadLine();
                    while (lines != "[DATA]")
                    {
                        if (lines.Trim() != "")
                        {
                            string[] st = lines.Split(',');
                            shifts.Add(new DataClass { _Date=DateTime.Parse(st[0]),_value= double.Parse(st[1]) });
                            lines = sr.ReadLine();
                        }
                        else
                        {
                            lines = sr.ReadLine();
                        }
                    }
                    lines = sr.ReadLine();
                    while (lines.Trim() != "")
                    {
                        string[] st = lines.Split(',');

                        data.Add(new DataClass { _Date = DateTime.Parse(st[0]), _value = double.Parse(st[1]) });
                        lines = sr.ReadLine();
                    }
                    foreach (DataClass item in data)
                    {                       
                        if (shifts.Exists(x => x._Date == item._Date))
                        {
                            DataClass temp = new DataClass();
                            temp = shifts.Find(x => x._Date == item._Date);
                            TimeSpan dur = new TimeSpan(Convert.ToInt32(temp._value), 0, 0);
                            item._Date = temp._Date.Add(dur);
                        }                        
                    }
                    data.ForEach(i => Console.WriteLine(i));                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file: " + e.Message);
            }
            Console.ReadLine();
        }
    }
}
