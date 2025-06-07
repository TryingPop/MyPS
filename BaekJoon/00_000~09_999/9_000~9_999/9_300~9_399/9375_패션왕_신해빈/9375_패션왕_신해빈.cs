using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 12
이름 : 배성훈
내용 : 패션왕 신해빈
    문제번호 : 9375번

    수학, 조합론, 해시를 사용한 집합과 맵 문제다
    dictionary 자료구조로 개수를 저장했다
*/

namespace BaekJoon.etc
{
    internal class etc_1055
    {

        static void Main1055(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            StringBuilder sb;

            int n;
            Dictionary<string, int> cloth;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                for (int i = 0; i < t; i++)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                int ret = 1;
                foreach (int cnt in cloth.Values)
                {

                    ret *= cnt;
                }

                sw.Write($"{ret - 1}\n");
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sb = new(40);
                cloth = new(30);
            }

            void Input()
            {

                cloth.Clear();
                n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    string name = GetString();
                    string type = GetString();

                    if (cloth.ContainsKey(type)) cloth[type]++;
                    else cloth[type] = 2;
                }
            }

            string GetString()
            {

                int c;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    sb.Append((char)c);
                }

                string str = sb.ToString();
                sb.Clear();

                return str;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System.Text;

internal class Program2
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder();

        Dictionary<string, int> dressDic = new Dictionary<string, int>();

        int n;
        int.TryParse(Console.ReadLine(), out n);

        for (int i = 0; i < n; i++)
        {
            dressDic.Clear();

            int m = int.Parse(Console.ReadLine());
            int count = 1;

            for (int j = 0; j < m; j++)
            {
                string[] arr = Console.ReadLine().Split(' ');

                if (!dressDic.ContainsKey(arr[1]))
                {
                    dressDic.Add(arr[1], 0);
                }

                dressDic[arr[1]]++;
            }

            foreach (string s in dressDic.Keys)
            {
                count *= dressDic[s] + 1;
            }

            count -= 1;


            sb.AppendLine(count.ToString());
        }

        Console.WriteLine(sb.ToString());
    }
}
#endif
}
