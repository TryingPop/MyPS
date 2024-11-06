using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

/*
날짜 : 2024. 3. 27
이름 : 배성훈
내용 : 정보갓 영훈이
    문제번호 : 14843번

    수학, 구현 문제다
    엄밀도 문제로 여러 번 틀렸고, 정답 출력부분에서 여러 번 틀렸다
    마지막에 .이 필요하다;

    그리고 엄밀도 부분은 c#에서는 decimal을 써야한다
*/

namespace BaekJoon.etc
{
    internal class etc_0361
    {

        static void Main361(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt();
            decimal ret = 0;

            for (int i = 0; i < len; i++)
            {

                // 조건에 맞게 점수 계산
                ret += GetScore();
            }

            len = ReadInt();
            int rank = 1;

            for (int i = 0; i < len; i++)
            {

                decimal chk = decimal.Parse(sr.ReadLine());
                if (ret < chk) rank++;
            }

            sr.Close();
            len++;

            // 15%이내인지 확인
            decimal per = rank;
            per /= len;

            // 정답 출력
            if (per <= (decimal)(0.15)) Console.Write($"The total score of Younghoon \"The God\" is {ret:0.00}.");
            else Console.Write($"The total score of Younghoon is {ret:0.00}.");

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }

            decimal GetScore()
            {

                decimal[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), decimal.Parse);

                decimal ret = temp[0] / 4;
                ret *= (1 + (1 / temp[1]));
                ret *= (1 + (temp[3] / temp[2]));
                return ret;
            }
        }
    }
}
