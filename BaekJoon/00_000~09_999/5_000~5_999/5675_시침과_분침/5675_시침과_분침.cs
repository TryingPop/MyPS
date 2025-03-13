using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 12
이름 : 배성훈
내용 : 시침과 분침
    문제번호 : 5675번

    수학, 구현, 시뮬레이션, 애드 혹 문제다.
    조건대로 구현하고 시뮬레이션 돌려 가능한 결과들을 기록했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1398
    {

        static void Main1398(string[] args)
        {

            string Y = "Y\n";
            string N = "N\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            bool[] chk = new bool[181];
            int h = 0;
            int m = 0;
            for (int i = 0; i < 720; i++)
            {

                m++;
                if (m % 12 == 0) h++;

                if (m >= 60) m -= 60;

                int angle = Math.Abs(h - m) * 6;
                if (angle > 180) angle = 360 - angle;
                chk[angle] = true;
            }

            string input;
            while (!string.IsNullOrEmpty((input = sr.ReadLine())))
            {

                int n = int.Parse(input);
                sw.Write(chk[n] ? Y : N);
            }
        }
    }
}
