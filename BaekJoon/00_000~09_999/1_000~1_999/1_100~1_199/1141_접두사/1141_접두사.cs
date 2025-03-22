using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 18
이름 : 배성훈
내용 : 접두사
    문제번호 : 1141번

    그리디, 문자열, 정렬 문제다.
    먼저 a와 b가 겹치는 경우 a와 b 중 하나만 카운팅하면 된다.
    이는 a, b, c가 겹치는 경우
    a, b가 겹치고 b, c가 겹치고 마찬가지로 a, c가 겹치기 때문이다.
    그리고 다른 것과 겹치는 것을 확인하기 위해서는 
    그리디로 가장 긴 것만 남기면 됨을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1283
    {

        static void Main1283(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());
            string[] strs = new string[n];
            for (int i = 0; i < n; i++)
            {

                strs[i] = sr.ReadLine();
            }

            Array.Sort(strs, (x, y) => y.Length.CompareTo(x.Length));
            HashSet<string> use = new(2500);
            int ret = 0;

            for (int i = 0; i < n; i++)
            {

                string temp = strs[i];
                if (use.Contains(temp)) continue;
                for (int j = 0; j < temp.Length; j++)
                {

                    use.Add(temp.Substring(0, temp.Length - j));
                }

                ret++;
            }

            Console.Write(ret);
        }
    }
}
