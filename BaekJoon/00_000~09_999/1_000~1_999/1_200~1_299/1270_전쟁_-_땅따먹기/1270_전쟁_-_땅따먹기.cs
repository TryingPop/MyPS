using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 전쟁 - 땅따먹기
    문제번호 : 1270번

    구현, 자료 구조, 해시를 사용한 집합과 맵, 보이어-무어 다수결 투표 문제다.
    정렬을 이용해 해결했다.

    국가 숫자당 병사 1명이다.
    그래서 국가 수를 세어 절반을 넘어가면 되는지 확인하면 되는데
    정렬하면 같은 국가의 병사들은 일렬로 있다. 떨어져 있는 경우는 없다!
    방문처리하며 병사 수를 세어주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1442
    {

        static void Main1442(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            string N = "SYJKGW\n";
            long[] arr = new long[100_000];

            int q = ReadInt();
            while(q-- > 0)
            {

                int n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadLong();
                }

                Array.Sort(arr, 0, n);
                int max = 0;
                int ret = 0;

                for (int i = 0; i < n;)
                {

                    int cnt = 1;
                    for (int j = i + 1; j < n; j++)
                    {

                        if (arr[i] == arr[j]) cnt++;
                        else break;
                    }

                    if (max < cnt)
                    {

                        max = cnt;
                        ret = i;
                    }
                    i += cnt;
                }

                if (n / 2 < max) sw.Write($"{arr[ret]}\n");
                else sw.Write(N);
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadLong()) { }
                return ret;

                bool TryReadLong()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }

            long ReadLong()
            {

                long ret = 0;

                while (TryReadLong()) { }
                return ret;

                bool TryReadLong()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }
}
