using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 19
이름 : 배성훈
내용 : 추월
    문제번호 : 2002번

    해시 문제다.
    나올 당시 뒤에 자기보다 먼저들어온 사람이 있다면 추월한 것이다.
    역방향인 경우 작은 번호만 알면 된다.
    반면 정방향인 경우 꺼낸 것을 기록하는 배열을 둬서 풀 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1779
    {

        static void Main1779(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());
            Dictionary<string, int> dic = new(n);

            for (int i = 0; i < n; i++)
            {

                string str = sr.ReadLine();
                dic[str] = i;
            }

            int[] pop = new int[n];
            for (int i = 0; i < n; i++)
            {

                pop[i] = dic[sr.ReadLine()];
            }

            int min = n;
            int ret = 0;
            for (int i = n - 1; i >= 0; i--)
            {

                if (min < pop[i]) ret++;
                else min = pop[i];
            }

            Console.Write(ret);
        }
    }
}
