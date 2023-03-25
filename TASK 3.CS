using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Globalization;

namespace ConsoleApp1
{
    class DataClass
    {
        public int _Index { get; set; }
        public DateTime _Date { get; set; }
        public double _Value { get; set; }
        public override string ToString()
        {
            return string.Format("{0},{1}", _Date.ToString("dd/MM/yyyy HH:mm"), _Value.ToString());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<DataClass> shifts = new List<DataClass>();
            List<DataClass> data = new List<DataClass>();
            List<DataClass> result = new List<DataClass>();
            try
            {
                string fileName = Environment.CurrentDirectory + @"\Data.txt";
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string lines = sr.ReadLine();
                    while (lines != "[SHIFTS]" && !sr.EndOfStream)
                    {
                        lines = sr.ReadLine();
                    }
                    lines = sr.ReadLine();
                    while (lines != "[DATA]" && !sr.EndOfStream)
                    {
                        if (lines.Trim() != "")
                        {
                            string[] st = lines.Split(',');
                            shifts.Add(new DataClass { _Date = DateTime.ParseExact(st[0], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), _Value = double.Parse(st[1]) });
                            lines = sr.ReadLine();
                        }
                        else
                        {
                            lines = sr.ReadLine();
                        }
                    }
                    lines = sr.ReadLine();
                    int j = 1;
                    while (lines.Trim() != "" && !sr.EndOfStream)
                    {
                        string[] st = lines.Split(',');
                        data.Add(new DataClass { _Index = j++, _Date = DateTime.ParseExact(st[0], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), _Value = double.Parse(st[1]) });
                        lines = sr.ReadLine();
                    }
                }
                int i = 0;
                TimeSpan duration;
                double total = 0;
                data.Where(x => x._Date < shifts[i]._Date).ToList().ForEach(
                          item => result.Add(item)
                          );
                do
                {
                    total += shifts[i]._Value;
                    duration = new TimeSpan(Convert.ToInt32(total), 0, 0);

                    data.Where(x => x._Index > result.Count && x._Date.Add(duration) < shifts[i + 1]._Date).ToList().ForEach(
                        item => result.Add(new DataClass() { _Date = item._Date.Add(duration), _Value = item._Value })
                        );
                    i++;

                } while (i < shifts.Count - 1);
                total += shifts[i]._Value;
                duration = new TimeSpan(Convert.ToInt32(total), 0, 0);

                data.Where(x => x._Index > result.Count).ToList().ForEach(
                    item => result.Add(new DataClass() { _Date = item._Date.Add(duration), _Value = item._Value })
                    );

                result.ForEach(it => Console.WriteLine(it));


                string path = Environment.CurrentDirectory + @"\Result.txt";
                using (StreamWriter sr = new StreamWriter(path))
                {
                    result.ForEach(it => sr.WriteLine(it));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
