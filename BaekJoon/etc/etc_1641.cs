using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 23
이름 : 배성훈
내용 : Drought
    문제번호 : 24496번

    그리디 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1641
    {

        static void Main1641(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t, n;
            int[] arr = new int[100_001];

            t = ReadInt();

            while (t-- > 0)
            {

                Input();

                sw.Write($"{GetRet()}\n");
            }

            long GetRet()
            {

                long ret = 0;

                if (n == 1) return ret;

                // 인덱스가 증가하는 방향으로 인접한거 같게 만들게 시도
                for (int i = 1; i < n - 1; i++)
                {

                    if (arr[i] > arr[i - 1])
                    {

                        int diff = arr[i] - arr[i - 1];
                        ret += 2 * diff;
                        arr[i + 1] -= diff;
                        arr[i] = arr[i - 1];
                    }
                }

                if (arr[n - 1] > arr[n - 2]) return -1;

                // 인덱스가 감소하는 방향으로 인접한거 같게 만들기 시도
                for (int i = n - 2; i >= 1; i--)
                {

                    if (arr[i] > arr[i + 1])
                    {

                        int diff = arr[i] - arr[i + 1];
                        ret += 2 * diff;
                        arr[i - 1] -= diff;
                        arr[i] = arr[i + 1];
                    }
                }

                if (arr[0] > arr[1]) return -1;

                // 음수면 불가능
                return arr[n - 1] < 0 ? -1 : ret;
            }

            void Input()
            {

                n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
