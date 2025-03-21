using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 21
이름 : 배성훈
내용 : 팀 이름 정하기
    문제번호 : 1296번

    구현, 정렬 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1434
    {

        static void Main1434(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            string name = sr.ReadLine();

            int n = int.Parse(sr.ReadLine());
            (string name, int score)[] arr = new (string name, int score)[n];

            int L = 0;
            int O = 0;
            int V = 0;
            int E = 0;
            for (int i = 0; i < name.Length; i++)
            {

                if (name[i] == 'L') L++;
                else if (name[i] == 'O') O++;
                else if (name[i] == 'V') V++;
                else if (name[i] == 'E') E++;
            }

            int MOD = 100;
            for (int i = 0; i < n; i++)
            {

                arr[i].name = sr.ReadLine();

                int l = L;
                int o = O;
                int v = V;
                int e = E;
                for (int j = 0; j < arr[i].name.Length; j++)
                {

                    if (arr[i].name[j] == 'L') l++;
                    else if (arr[i].name[j] == 'O') o++;
                    else if (arr[i].name[j] == 'V') v++;
                    else if (arr[i].name[j] == 'E') e++;
                }

                arr[i].score = ((l + o) * (l + v)) % MOD;
                arr[i].score = (arr[i].score * (l + e)) % MOD;
                arr[i].score = (arr[i].score * (o + v)) % MOD;
                arr[i].score = (arr[i].score * (o + e)) % MOD;
                arr[i].score = (arr[i].score * (v + e)) % MOD;
            }

            Array.Sort(arr, (x, y) =>
            {

                int ret = y.score.CompareTo(x.score);
                if (ret == 0) ret = x.name.CompareTo(y.name);

                return ret;
            });

            Console.Write(arr[0].name);
        }
    }
}
