using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public static class Reader
    {
        public static List<Anketa> ReadFromExcel(string filepath)
        {
            List<Anketa> anketas = new List<Anketa>();
            using(var reader = new StreamReader(filepath))
            {
                string line;
                bool isHeader = false;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (!isHeader)
                    {
                        isHeader = true;
                        continue;
                    }
                    anketas.Add(ParseLine(line));
                }
            }
            return anketas;
        }
        private static Anketa ParseLine(string text)
        {
            string[] rows = text.Split(new char[] { ';' });
            double term = double.Parse(rows[1]);
            List<double> answers = new List<double>();
            for(int i = 2; i < rows.Length; i++)
            {
                answers.Add(double.Parse(rows[i]));
            }
            Anketa anketa = new Anketa();
            anketa.IsSerious = term > 7 ? 1 : 0;
            anketa.Authoritarian = GroupAnswer(answers, "1-4", "33-36", "65-68", "97-100");
            anketa.Selfish = GroupAnswer(answers, "5-8", "37-40", "69-72", "101-104");
            anketa.Aggresive = GroupAnswer(answers, "9-12", "41-44", "73-76", "105-108");
            anketa.Suspicious = GroupAnswer(answers, "13-16", "45-48", "77-80", "109-112");
            anketa.Subordinate = GroupAnswer(answers, "17-20", "49-52", "81-84", "113-116");
            anketa.Dependent = GroupAnswer(answers, "21-24", "53-56", "85-88", "117-120");
            anketa.Friendly = GroupAnswer(answers, "25-28", "57-60", "89-92", "121-124");
            anketa.Altruistic = GroupAnswer(answers, "29-32", "61-64", "93-96", "125-128");
            return anketa;
        }
        private static double[] GroupAnswer(List<double> answers, params string[] groups)
        {
            List<double> groupedAnswer = new List<double>();
            for(int i = 0; i < groups.Length; i++)
            {
                string group = groups[i];
                string[] groupValue = group.Split(new char[] { '-' });
                int start = int.Parse(groupValue[0]);
                int finish = int.Parse(groupValue[1]);
                groupedAnswer.AddRange(answers.Skip(start - 1).Take(finish - start + 1));
            }
            return groupedAnswer.ToArray();
        }

    }
}
