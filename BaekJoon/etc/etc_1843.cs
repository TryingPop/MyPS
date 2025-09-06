using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 27
이름 : 배성훈
내용 : Milk Measurement
    문제번호 : 15465번

    구현, 정렬, 시뮬레이션 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1843
    {

        static void Main1843(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            (int day, int name, int add)[] arr = new (int day, int name, int add)[n];
            Dictionary<string, int> use = new(n);
            int idx = 0;

            // 입력데이터
            for (int i = 0; i < n; i++)
            {

                // 날짜, 이름, 추가 정보가 담겨있다.
                string[] temp = sr.ReadLine().Split();
                int day = int.Parse(temp[0]);

                // 등록안된 이름인지 확인
                if (!use.ContainsKey(temp[1])) use[temp[1]] = idx++;

                // 이름에 맞는 번호 
                int name = use[temp[1]];

                // 우유 추가값
                int add = 0;
                for (int j = 1; j < temp[2].Length; j++)
                {

                    add = add * 10 + temp[2][j] - '0';
                }

                add = temp[2][0] == '+' ? add : -add;

                arr[i] = (day, name, add);
            }

            // 날짜별 정렬
            Array.Sort(arr, (x, y) => x.day.CompareTo(y.day));

            int ret = -1;
            int[] milk = new int[idx];
            Array.Fill(milk, 7);
            bool[] max = new bool[idx], prevMax = new bool[idx];
            int prevDay = 0;

            for (int i = 0; i < n; i++)
            {

                if (prevDay != arr[i].day)
                {

                    if (ChkRankingTop()) ret++;
                    prevDay = arr[i].day;
                }

                milk[arr[i].name] += arr[i].add;
            }

            if (ChkRankingTop()) ret++;
            Console.Write(ret);

            // 확인해야한다.
            bool ChkRankingTop()
            {

                int m = -1;
                for (int i = 0; i < idx; i++)
                {

                    m = Math.Max(milk[i], m);
                }

                bool ret = false;
                for (int i = 0; i < idx; i++)
                {

                    max[i] = m == milk[i];
                    if (prevMax[i] != max[i]) ret = true;
                    prevMax[i] = max[i];
                }

                return ret;
            }
        }
    }
}
