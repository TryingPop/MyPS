using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 6
이름 : 배성훈
내용 : Football
    문제번호 : 9530번

    그리디, 정렬 문제다.
    먼저 특정 경기의 골이 다른 경기의 골에 영향을 주지 못한다.
    비기는 경우 2개보다 이기는 경우 1개가 점수가 더 높다.
    골을 가장 적게 구매해서 이길 수 있는게 제일 좋다.
    그래서 1점차이로 이기게 하는게 좋다.

    그래서 점수차를 저장하고 0이상에 대해 골을 1이 될때까지 구매한다.
    만약 1을 못만드는 경우 0을 만들 수 있으면 0이라도 만들면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1379
    {

        static void Main1379(string[] args)
        {

            int n, g;
            int[] arr;

            Input();

            GetRet();

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();
                n = int.Parse(temp[0]);
                g = int.Parse(temp[1]);

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    temp = sr.ReadLine().Split();
                    int a = int.Parse(temp[0]);
                    int b = int.Parse(temp[1]);
                    int need = b - a;
                    if (need < 0) need = -1;
                    arr[i] = need;
                }
            }

            void GetRet()
            {

                Array.Sort(arr);

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (arr[i] < 0) ret += 3;
                    else if (arr[i] == 0)
                    {

                        ret++;
                        if (g == 0) continue;
                        ret += 2;
                        g--;
                    }
                    else
                    {

                        if (g < arr[i]) break;
                        g -= arr[i];
                        ret++;
                        if (g == 0) break;
                        g--;
                        ret += 2;
                    }
                }

                Console.Write(ret);
            }
        }
    }
}
