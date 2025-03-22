using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 12
이름 : 배성훈
내용 : 짐 챙기는 숌
    문제번호 : 1817번

    그리디 문제다.
    순서대로만 넣을 수 있기에 그리디로
    최대한 가방에 채워넣는게 가방의 최솟값임이 보장된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1330
    {

        static void Main1330(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int m = ReadInt();

            int cur = 0;
            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                int val = ReadInt();
                if (cur + val <= m)
                {

                    cur += val;
                    continue;
                }
                else
                {

                    cur = val;
                    ret++;
                }
            }

            if (cur != 0) ret++;
            Console.Write(ret);

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
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
