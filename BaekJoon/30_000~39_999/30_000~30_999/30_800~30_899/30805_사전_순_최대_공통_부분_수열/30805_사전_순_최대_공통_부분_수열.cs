using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 4
이름 : 배성훈
내용 : 사전 순 최대 공통 부분 수열
    문제번호 : 30805번

    그리디 문제다.
    처음에는 공통 부분 수열이라길래 LCS를 찾는것인 줄 알았다.
    그래서 LCS를 구현하고 잘되는지 예제로 돌려보는데, 가장 긴 것은 의미가 없어 보였다.
    그래서 그리디로 접근해 풀었다.

    아이디어는 다음과 같다.
    일치하는 것중 가장 큰 것을 최우선적으로 사용해 공통 부분 수열을 만들면 된다.
    그래서 내림차순으로 정렬하고 같은 경우 인덱스로 오름차순으로 정렬해주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1150
    {

        static void Main1150(string[] args)
        {

            int len1, len2;
            int[] arr1, arr2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int[] sorted = new int[len1];
                for (int i = 1; i < len1; i++)
                {

                    sorted[i] = i;
                }

                Array.Sort(sorted, (x, y) => 
                {

                    int ret = arr1[y].CompareTo(arr1[x]);
                    if (ret == 0) ret = x.CompareTo(y);
                    return ret;
                });

                int[] ret = new int[len1];
                int len = 0;

                int idx1 = -1;
                int idx2 = -1;

                for (int i = 0; i < len1; i++)
                {

                    int idx = sorted[i];
                    if (idx < idx1) continue;

                    int cur = arr1[idx];
                    for (int j = idx2 + 1; j < len2; j++)
                    {

                        if (cur != arr2[j]) continue;
                        idx1 = idx;
                        idx2 = j;
                        ret[len++] = cur;
                        break;
                    }
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    sw.Write($"{len}\n");
                    for (int i = 0; i < len; i++)
                    {

                        sw.Write($"{ret[i]} ");
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                len1 = ReadInt();
                arr1 = new int[len1];

                for (int i = 0; i < len1; i++)
                {

                    arr1[i] = ReadInt();
                }

                len2 = ReadInt();
                arr2 = new int[len2];

                for (int i = 0; i < len2; i++)
                {

                    arr2[i] = ReadInt();
                }

                sr.Close();
                int ReadInt()
                {

                    int ret = 0;
                    while(TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;
                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
}
