using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 야구 시즌
    문제번호 : 21760번

    수학, 조합론 문제다
    그냥 나눠서 계산하면 된다
    d에 근접한 연산은 나눗셈 내림으로 찾으면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0412
    {

        static void Main412(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();

            while(test-- > 0)
            {

                int n = ReadInt();
                int m = ReadInt();
                int k = ReadInt();
                int d = ReadInt();

                // 전체 팀 수
                long total = n * m;
                // 1회 실시 할 때,
                // 한 팀에 대해 같은 지역리그 횟수 + 다른리그와 시합하는 횟수를 찾는다
                // 같은 지역 리그 횟수는 자신을 제외한 다른 팀과의 횟수에 k를 곱한게 된다
                total *= k * (m - 1) + m * (n - 1);
                // A - B가 1경기씩 한걸 각각 A, B에서 세면 2번 세어진다 중복 제외!
                total /= 2;

                long chk = d / total;
                if (chk == 0) sw.WriteLine(-1);
                else sw.WriteLine(chk * total);
            }

            sr.Close();
            sw.Close();

            
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
