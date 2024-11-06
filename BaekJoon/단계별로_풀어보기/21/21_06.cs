using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 19
이름 : 배성훈
내용 : 듣보잡
    문제번호 : 1764번
*/

namespace BaekJoon._21
{
    internal class _21_06
    {

        static void Main6(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            HashSet<string> noListen = new HashSet<string>(info[0]);

            for (int i = 0; i < info[0]; i++)
            {

                noListen.Add(sr.ReadLine());
            }

            HashSet<string> result = new HashSet<string>(info[0]);

            {

                string str = "";

                for (int i = 0; i < info[1]; i++)
                {

                    str = sr.ReadLine();

                    if (noListen.Contains(str))
                    {

                        result.Add(str);
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine((result.Count).ToString());

            foreach (string str in result.OrderBy(item => item))
            {

                sb.AppendLine(str);
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(sb);
            }
        }
    }
}
