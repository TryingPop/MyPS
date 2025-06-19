using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 15
이름 : 배성훈
내용 : 회장님께 바치는 합성함수
    문제번호 : 28418번

    문제에서 설명을 다해줬다;
    그래서 그냥 코드로 바꾼 것 밖에 없다
*/

namespace BaekJoon.etc
{
    internal class etc_0038
    {

        static void Main38(string[] args)
        {

            string INF = "Nice";
            string TWO = "Go ahead";
            string ONE = "Remember my character";
            string ZERO = "Head on";

            // f - 2차 다항함수, g - 1차 다항함수
            int[] f = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[] g = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            // 합성함수
            int[] fg = new int[3];
            int[] gf = new int[3];

            // 합성함수 연산
            fg[0] = f[0] * g[0] * g[0];
            fg[1] = (2 * f[0] * g[0] * g[1]) + f[1] * g[0];
            fg[2] = f[0] * g[1] * g[1] + f[1] * g[1] + f[2];

            gf[0] = f[0] * g[0];
            gf[1] = g[0] * f[1];
            gf[2] = f[2] * g[0] + g[1];

            // 빼준 식
            int[] d = new int[3];
            d[0] = fg[0] - gf[0];
            d[1] = fg[1] - gf[1];
            d[2] = fg[2] - gf[2];

            // 결과
            string ret;

            if (d[0] != 0)
            {

                // 판별식
                int det = d[1] * d[1] - 4 * d[0] * d[2];

                // 0보다 작으면 없다
                if (det < 0) ret = ZERO;
                // 0보다 크면 2개
                else if (det > 0) ret = TWO;
                // 0이면 1개
                else ret = ONE;
            }
            else if (d[1] == 0)
            {

                // 직선인데, -형태
                // x축과 일치하는 경우
                if (d[2] == 0) ret = INF;
                // x축 위에 붕뜬 경우
                else ret = ZERO;
            }
            // -이외의 직선이므로 오로지 1개
            else ret = ONE;

            Console.WriteLine(ret);
        }
    }
}
