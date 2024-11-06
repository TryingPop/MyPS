using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 9
이름 : 배성훈
내용 : 꿀벌
    문제번호 : 9733번

    해시, 문자열 문제다
    ... ""도 split하면 ""으로 있어 이를 세어 여러 번 틀렸다;
    처음에는 ""도 읽는건가 싶어 계속했고;
    이후에 예제 디버깅을 하니 ""은 무시하는 것이었다;

    그래서 이를 알고 수정하니 이상없이 통과했다;
*/

namespace BaekJoon.etc
{
    internal class etc_0683
    {

        static void Main683(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            Dictionary<string, int> dic = new(7);

            string[] order = { "Re", "Pt", "Cc", "Ea", "Tb", "Cm", "Ex" };
            for (int i = 0; i < order.Length; i++)
            {

                dic[order[i]] = 0;
            }

            double len = 0;
            while (true)
            {

                string temp = sr.ReadLine();
                if (temp == null) break;
                if (temp == string.Empty) continue;
                
                string[] input = temp.Split(' ');
                for (int i = 0; i < input.Length; i++)
                {

                    len++;
                    if (dic.ContainsKey(input[i])) dic[input[i]]++;
                    else dic[input[i]] = 1;
                }
            }

            sr.Close();
            
            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                for (int i = 0; i < order.Length; i++)
                {

                    sw.Write($"{order[i]} {dic[order[i]]} {(dic[order[i]] / len):0.00}\n");
                }

                sw.Write($"Total {(int)len} 1.00\n");
            }
        }
    }

#if other
// cs9733 - rby
// 2023-05-12 00:40:05
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace cs9733
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            string line;
            string[] cmd;
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("Re", 0);
            dict.Add("Pt", 0);
            dict.Add("Cc", 0);
            dict.Add("Ea", 0);
            dict.Add("Tb", 0);
            dict.Add("Cm", 0);
            dict.Add("Ex", 0);

            int count = 0;
            while (true)
            {
                line = sr.ReadLine();
                if (line == "")
                    continue;
                if (line == null || line[0] < 9)
                    break;

                cmd = line.Trim().Split(' ');
                foreach(var item in cmd)
                {
                    if (dict.ContainsKey(item))
                        dict[item]++;

                    if (item != "")
                        count++;
                }
            }
            
            foreach(var item in dict)
            {
                sb.AppendFormat("{0} {1} {2:0.00}\n", item.Key, item.Value,
                    Math.Round((double)item.Value / count, 2));
            }
            sb.AppendFormat("Total {0} 1.00", count);
            sw.WriteLine(sb);

            sw.Close();
            sr.Close();
        }
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace 콘솔테스트
{
    class Program
    {
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StringBuilder sb = new StringBuilder();
            Dictionary<string, double> Data = new Dictionary<string, double>(7);
            Data.Add("Re", 0); Data.Add("Pt", 0); Data.Add("Cc", 0); Data.Add("Ea", 0);
            Data.Add("Tb", 0); Data.Add("Cm", 0); Data.Add("Ex", 0);
            int t = 0;
            string str;
            while ((str = sr.ReadLine()) != null)
            {
                foreach (var item in str.Split())
                {
                    if (String.IsNullOrEmpty(item))
                        continue;
                    t++;
                    if (Data.ContainsKey(item))
                        Data[item]++;
                }    
            }
            foreach (var item in Data)
                sb.AppendLine($"{item.Key} {item.Value} {String.Format("{0:0.00}", item.Value / t)}");
            sb.Append($"Total {t} 1.00");
            Console.Write(sb);
        }
    }
}
#endif
}
