using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 삼각형 만들기
    문제번호 : 1448번

    정렬, 그리디 문제다.
    정렬한 뒤 인접한 3개가 삼각형이 되는지 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1446
    {

        static void Main(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int ret = -1;

#if first
            int n = int.Parse(sr.ReadLine());

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
                arr[i] = int.Parse(sr.ReadLine());

            Array.Sort(arr);


            for (int i = 0; i < n - 2; i++)
            {

                if (arr[i + 2] < arr[i] + arr[i + 1]) ret = arr[i] + arr[i + 1] + arr[i + 2];
            }
#elif second

            // 두 포인터
            int n = int.Parse(sr.ReadLine());

            int[] arr = new int[1_000_001];
            for (int i = 0; i < n; i++)
            {

                int idx = int.Parse(sr.ReadLine());
                arr[idx]++;
            }

            int a = 0, b = 0, c = -1;
            arr[0]++;
            Init();
            void Init()
            {

                int idx = 1_000_000;
                while (c == -1)
                {

                    if (arr[idx] == 0)
                    {

                        idx--;
                        continue;
                    }

                    arr[idx]--;
                    if (a == 0) a = idx;
                    else if (b == 0) b = idx;
                    else c = idx;
                }
            }

            while (c != 0)
            {

                if (a < b + c)
                {

                    ret = a + b + c;
                    break;
                }

                Next();
            }

            void Next()
            {

                
                for (int i = c; i >= 0; i--)
                {

                    if (arr[i] == 0) continue;
                    arr[i]--;
                    a = i;
                    break;
                }

                int temp = a;
                a = b;
                b = c;
                c = temp;
            }
#endif
            Console.Write(ret);
        }
    }
}
