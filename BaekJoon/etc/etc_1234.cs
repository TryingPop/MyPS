using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 1 
이름 : 배성훈
내용 : 비부분수열
    문제번호 : 1885번

    그리디 문제다.
    아이디어는 다음과 같다.
    만들 수 있는 가장 긴 부분수열은 다음과 같다.
    모든 숫자가 1개씩 나오면 길이 1인 부분수열을 만들 수 있다.

    이후 2번째는 길이 1인 부분수열을 만들고 다시 카운팅해서
    모두 1개씩 나오면 길이 2인 부분수열을 만들 수 있다.

    예를들어 보자
    1 1 2 2 3 3
    인 경우 1이다.

    3 1을 만들지 못한다.
    1 1 2 2 3 3 1 2 3
    인 경우 1 1 2 2 3 에서 모두 1개씩 나왔다.
    그러면 다시 카운팅해서 뒤의 3 1 2 부분에서 2인 부분수열을 만들 수 있게된다.
    즉, 1 1 2 2 3 3 1 2 에서 길이 2인 모든 부분수열을 만들 수 있다.

    아이디어는 빠르게 찾았으나
    이를 구현하는데 복잡해져서 3번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_1234
    {

        static void Main1234(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();

            int[] cnt = new int[k + 1]; // cnt[idx] = val : idx가 나온 갯수는 val개

            int min = 0;
            int chk = 0;
            int ret = 0;

            for (int i = 0; i < n; i++)
            {

                int num = ReadInt();

                if (min == cnt[num]) 
                { 
                        
                    cnt[num]++; 
                    chk++; 
                }

                if (chk == k)
                {

                    chk = 0;
                    min++;
                    ret++;
                }
            }

            Console.Write(ret + 1);

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();

                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
