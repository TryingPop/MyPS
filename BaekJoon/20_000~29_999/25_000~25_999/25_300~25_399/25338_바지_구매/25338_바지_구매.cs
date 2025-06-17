using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 19
이름 : 배성훈
내용 : 바지구매
    문제번호 : 25338번

    수학, 사칙연산 문제다
    식대로 처리하면 이상없이 풀린다
    범위도 최대 -10억이라 오버플로우 걱정안해도 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0287
    {

        static void Main287(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] func = new int[] { ReadInt(sr), ReadInt(sr), ReadInt(sr), ReadInt(sr) };

            int n = ReadInt(sr);

            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                int a = ReadInt(sr);
                int q = ReadInt(sr);

                if (GetVal(func, a, q)) ret++;
            }

            sr.Close();

            Console.WriteLine(ret);
        }

        static bool GetVal(int[] _func, int _a, int _q)
        {

            int ret = _q - _func[1];
            ret *= ret;
            ret *= _func[0];
            ret += _func[2];

            ret = ret < _func[3] ? _func[3] : ret;
            return ret == _a;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }

                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }
}
