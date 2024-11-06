using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 7
이름 : 배성훈
내용 : 배부른 마라토너
    문제번호 : 10546번

    해시 문제다
    동명이인도 있고, 해서 그냥 카운트하고 2로 나눠 나머지가 1인지만 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_0471
    {

        static void Main471(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 4);
            int n = int.Parse(sr.ReadLine());

            Dictionary<string, int> dic = new(n);

            int len = 2 * n - 1;
            for (int i = 0; i < len; i++)
            {

                string temp = sr.ReadLine();
                if (dic.ContainsKey(temp)) dic[temp]++;
                else dic[temp] = 1;
            }

            sr.Close();

            foreach (var item in dic)
            {

                if (item.Value % 2 == 0) continue;
                Console.WriteLine(item.Key);
                break;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    internal class Program
    {
        static void Main()
        {
            Console.ReadLine();
            HashSet<string> N = new HashSet<string>();

            string str;
            while (!String.IsNullOrEmpty((str = Console.ReadLine())))
            {
                if (N.Contains(str))
                    N.Remove(str);
                else
                    N.Add(str);
            }

            Console.Write(N.First());
        }
    }
}
#endif
}
