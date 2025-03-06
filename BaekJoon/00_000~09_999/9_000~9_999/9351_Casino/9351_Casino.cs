using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 6
이름 : 배성훈
내용 : Casino
    문제번호 : 9351번

    브루트포스 문제다.
    조건을 잘못이해해 여러 번 틀렸다.
    오른쪽에서 왼쪽으로 찾아야하며, 1인 경우는 빈 공간으로 해야한다.
    그리고 1줄에 1개씩 출력해야 한다.

    문자의 길이가 1000이므로 각 지점에서 시작해 
    가장 긴 팰리드롬을 찾아도 길어야 50만을 넘지 않는다.
    케이스도 많아야 50이므로 2500만을 넘지 않아 브루트포스로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1317
    {

        static void Main1317(string[] args)
        {

            string input;
            List<int> arr;

            Solve();
            void Solve()
            {

                int MAX = 1_000;
                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                arr = new(MAX + 1);
                int t = int.Parse(sr.ReadLine());
                for (int i = 1; i <= t; i++)
                {

                    sw.Write($"Case #{i}:\n");
                    input = sr.ReadLine().Trim();

                    int len = GetLen();

                    if (len == 1) continue;
                    arr.Sort((x, y) => y.CompareTo(x));
                    for (int j = 0; j < arr.Count; j++)
                    {

                        for (int k = 0; k < len; k++)
                        {

                            sw.Write(input[arr[j] + k]);
                        }

                        sw.Write('\n');
                    }
                }
            }

            int GetLen()
            {

                arr.Clear();
                int max = 1;
                for (int i = 0; i < input.Length; i++)
                {

                    ChkOdd(i);
                    ChkEven(i);
                }

                return max;

                void ChkEven(int _s)
                {

                    int match = 0;
                    int l, r;
                    for (l = _s, r = _s + 1; l >= 0 && r < input.Length; l--, r++, match += 2)
                    {

                        if (input[l] != input[r]) break;
                    }

                    if (max < match)
                    {

                        arr.Clear();
                        arr.Add(l + 1);
                        max = match;
                    }
                    else if (max == match)
                    {

                        arr.Add(l + 1);
                    }
                }

                void ChkOdd(int _s)
                {

                    int match = 1;
                    int l, r;
                    for (l = _s - 1, r = _s + 1; l >= 0 && r < input.Length; l--, r++, match += 2)
                    {

                        if (input[l] != input[r]) break;
                    }

                    if (max < match)
                    {

                        arr.Clear();
                        arr.Add(l + 1);
                        max = match;
                    }
                    else if (max == match)
                    {

                        arr.Add(l + 1);
                    }
                }
            }
        }
    }
}
