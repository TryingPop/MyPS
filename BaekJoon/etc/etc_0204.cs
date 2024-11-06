using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 12
이름 : 배성훈
내용 : 서든어택 3
    문제번호 : 22993번

    그리디로 풀었다
    정렬한 뒤 가장 약한 적들을 사냥하면서 성장해간다
    만약 상대가 강한 경우 혼자 생존하는 경우는 없다!
*/

namespace BaekJoon.etc
{
    internal class etc_0204
    {

        static void Main204(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            long player = ReadLong(sr);
            long[] enemy = new long[n - 1];
            for (int i = 0; i < n - 1; i++)
            {

                enemy[i] = ReadLong(sr);
            }

            sr.Close();

            Array.Sort(enemy);

            bool can = true;
            for (int i = 0; i < n - 1; i++)
            {

                // enemy보다 약한 적은 없기에 못잡으면
                // 실패다
                if (player <= enemy[i])
                {

                    can = false;
                    break;
                }
                else if (player > 1_000_000_000) break;

                player += enemy[i];
            }

            Console.WriteLine(can ? "Yes" : "No");
        }

        static long ReadLong(StreamReader _sr)
        {

            int c;
            long ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
