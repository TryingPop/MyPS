using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 7
이름 : 배성훈
내용 : 최소 성적
    문제번호 : 29753번

    부동소수점 오차 문제로 여러 번 틀렸다
    처음엔 decimal로 안바꾸고 되겠지 해서
    학점을 2 배 연산해서 int로 만든 뒤 총합 연산을해도
    해결을 못 했다
    그래서 결국 decimal로 자료형을 잡으니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0161
    {

        static void Main161(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            Dictionary<string, double> gTos = new(9);
            
            gTos["F"] = 0;
            gTos["D0"] = 1;
            gTos["D+"] = 1.5;
            gTos["C0"] = 2;
            gTos["C+"] = 2.5;
            gTos["B0"] = 3;
            gTos["B+"] = 3.5;
            gTos["A0"] = 4;
            gTos["A+"] = 4.5;

            int len = ReadInt(sr);
            decimal want = decimal.Parse(sr.ReadLine());
            decimal cur = 0;
            decimal totalTime = 0;

            for (int i = 0; i < len - 1; i++)
            {

                decimal t = ReadInt(sr);
                double s = gTos[sr.ReadLine()];

                totalTime += t;
                cur += (decimal)s * t;
            }

            int lastTime = ReadInt(sr);
            totalTime += lastTime;
            sr.Close();

            string ret = "impossible";
            foreach (var item in gTos)
            {

                decimal chk = cur + (decimal)(lastTime * item.Value);
                chk /= totalTime;
                chk = Math.Truncate(chk * 100) / 100;

                if (chk > want)
                {

                    ret = item.Key;
                    break;
                }
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
