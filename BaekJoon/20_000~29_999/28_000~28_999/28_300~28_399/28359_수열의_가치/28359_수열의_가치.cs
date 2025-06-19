using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 28
이름 : 배성훈
내용 : 수열의 가치
    문제번호 : 28359번

    그리디, 정렬, 해 구성하기 문제다.
    아이디어는 다음과 같다.
    오름차순으로 정렬하면 모든 원소를 1번씩 사용할 수 있다.
    그리고 가장 큰 원소들은 2번 사용된다.
    
    실제로 내림차순과 오름차순으로 배치하면 맨 뒤에 원소는 2번 사용할 수 있다.
    그래서 값 * 개수를 해서 가장 큰 원소를 2번 사용하게 하면 커지지 않을까 생각했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1136
    {

        static void Main1136(string[] args)
        {

            int[] cnt;
            int n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int max = 0;
                int ret1 = 0;
                int end = 0;
                for (int i = 1; i <= n; i++)
                {

                    int chk = i * cnt[i];
                    if (max < chk)
                    {

                        max = chk;
                        end = i;
                    }

                    ret1 += chk;
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    sw.Write($"{ret1 + max}\n");
                    for (int i = 0; i < end; i++)
                    {

                        while (cnt[i] > 0)
                        {

                            cnt[i]--;
                            sw.Write($"{i} ");
                        }
                    }

                    for (int i = n; i >= end; i--)
                    {

                        while (cnt[i] > 0)
                        {

                            cnt[i]--;
                            sw.Write($"{i} ");
                        }
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                cnt = new int[n + 1];
                for (int i = 0; i < n; i++)
                {

                    cnt[ReadInt()]++;
                }

                sr.Close();
                int ReadInt()
                {

                    int c, ret = 0;
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }
}
