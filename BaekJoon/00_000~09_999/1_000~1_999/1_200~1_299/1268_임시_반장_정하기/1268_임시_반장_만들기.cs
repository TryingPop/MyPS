using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 12
이름 : 배성훈
내용 : 임시 반장 정하기
    문제번호 : 1268번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1268
    {

        static void Main1268(string[] args)
        {

            StreamReader sr;
            int n;
            int[][] arr;
            int[] cnt;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                cnt = new int[n];
                for (int i = 0; i < n; i++)
                {

                    for (int j = i + 1; j < n; j++)
                    {

                        for (int k = 0; k < 5; k++)
                        {

                            if (arr[i][k] != arr[j][k]) continue;
                            cnt[i]++;
                            cnt[j]++;
                            break;
                        }
                    }
                }

                int max = 0;
                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (max >= cnt[i]) continue;
                    max = cnt[i];
                    ret = i;
                }

                Console.Write(ret + 1);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                arr = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = sr.ReadLine().Split().Select(int.Parse).ToArray();
                }

                sr.Close();
            }
        }
    }
}
