using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 2
이름 : 배성훈
내용 : 기타줄
    문제번호 : 1049번

    그리디로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0145
    {

        static void Main145(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt(sr);
            int m = ReadInt(sr);

            int six = 10_001;       // 6개짜리 가장 싼 값
            int one = 10_001;       // 1개짜리 가장 싼 값
            for (int i = 0; i < m; i++)
            {

                int curSix = ReadInt(sr);
                if (six > curSix) six = curSix;

                int curOne = ReadInt(sr);
                if (one > curOne) one = curOne;
            }
            sr.Close();

            int ret;
            // 1개짜리 가격 * 6 < 6개짜리 1개 가격인 경우
            // 그냥 1개짜리로 원하는 수량만큼 구하면 된다
            if (one * 6 <= six) ret = n * one;
            else
            {

                // 6개짜리가 싼 경우 가능한 만큼 6개짜리를 산다!
                // 그리고 나머지 부분은 싼쪽으로 비교해줘야한다
                ret = (n / 6) * six;
                n = n % 6;
                int chk = one * n;
                chk = chk < six ? chk : six;
                ret += chk;
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
