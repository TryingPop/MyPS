using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 29
이름 : 배성훈
내용 : 동일한 단어 그룹화하기
    문제번호 : 16499번

    문자열 문제다.
    조건대로 구현하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1956
    {

        static void Main1956(string[] args)
        {

            int n;
            string[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int[] cur = new int[26], calc = new int[26];
                bool[] visit = new bool[n];
                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (visit[i]) continue;
                    visit[i] = true;
                    ret++;
                    Fill(arr[i], cur);

                    for (int j = i + 1; j < n; j++)
                    {

                        if (visit[j]) continue;
                        Fill(arr[j], calc);
                        if (Comp()) visit[j] = true;
                    }
                }

                Console.Write(ret);

                bool Comp()
                {

                    for (int i = 0; i < 26; i++)
                    {

                        if (cur[i] != calc[i]) return false;
                    }

                    return true;
                }

                void Fill(string str, int[] cnt)
                {

                    Array.Fill(cnt, 0);
                    for (int i = 0; i < str.Length; i++)
                    {

                        cnt[str[i] - 'a']++;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = int.Parse(sr.ReadLine());
                arr = new string[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = sr.ReadLine();
                }
            }
        }
    }
}
