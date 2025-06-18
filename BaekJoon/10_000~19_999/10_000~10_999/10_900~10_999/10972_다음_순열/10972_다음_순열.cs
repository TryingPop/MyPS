using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 8
이름 : 배성훈
내용 : 다음 순열
    문제번호 : 10972번

    수학, 조합론 문제다.
    다음 순열 규칙성을 찾아 풀었다.

    그러니 끝에서부터 인접한게 증가하는 순간
    왼쪽에 있는 값을 1올리고 나머진 오름차순 정렬하면 됨을 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1384
    {

        static void Main1384(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int chk = 0;
                int[] cnt = new int[n + 1];
                for (chk = n - 2; chk >= 0; chk--)
                {

                    cnt[arr[chk + 1]]++;
                    if (arr[chk] < arr[chk + 1]) break;
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                
                if (chk == -1)
                {

                    sw.Write(-1);
                    return;
                }

                cnt[arr[chk]]++;
                for (int i = 0; i < chk; i++)
                {

                    sw.Write($"{arr[i]} ");
                }

                for (int i = arr[chk] + 1; i <= n; i++)
                {

                    if (cnt[i] == 0) continue;
                    cnt[i]--;
                    sw.Write($"{i} ");
                    break;
                }

                for (int i = 1; i <= n; i++)
                {

                    if (cnt[i] == 0) continue;
                    sw.Write($"{i} ");
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                arr = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            }
        }
    }
}
