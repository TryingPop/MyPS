using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 선형 회귀는 너무 쉬워 1
    문제번호 : 27295번

    수학, 정수론, 유클리드 호제법 문제다
    sig(axi + b - yi) = a * (x1 + x2 + ... + xn) + b * n - (y1 + y2 + ... + yn) = 0
    sig(xi) != 0인 
    => a = (sig(yi) - n * b) / sig(xi)
    를 얻을 수 있다
    반면 sig(xi) = 0는 a는 무수히 많은 해가 존재한다
*/

namespace BaekJoon.etc
{
    internal class etc_0659
    {

        static void Main659(string[] args)
        {

            StreamReader sr;
            int n;
            long x, b, y;
            Solve();

            void Solve()
            {

                Input();

                long right = y - b;
                long left = x;
                if (left == 0)
                {

                    Console.WriteLine("EZPZ");
                    return;
                }

                long gcd = GetGCD(left, right);
                left /= gcd;
                right /= gcd;
                if (left < 0)
                {

                    left = -left;
                    right = -right;
                }

                if (left == 1) Console.WriteLine($"{right}");
                else Console.WriteLine($"{right}/{left}");
            }

            long GetGCD(long _a, long _b)
            {

                _a = _a < 0 ? -_a : _a;
                _b = _b < 0 ? -_b : _b;

                while(_b > 0)
                {

                    long temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                n = ReadInt();
                b = ReadInt();

                b = n * b;

                x = 0;
                y = 0;

                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt();
                    x += f;
                    int b = ReadInt();
                    y += b;
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if ( c=='-')
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

#if other
// cs27295 - rby
// 2023-06-27 09:50:47
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace cs27295
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int[] line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int N = line[0];
            long B = line[1];

            long sx = 0;
            long sy = 0;
            for (int i = 0; i < N; i++)
            {
                line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                sx += line[0];
                sy += line[1];
            }
            sy -= B * N;

            if (sx == 0)
            {
                sb.AppendLine("EZPZ");
            }
            else
            {
                if (sy % sx == 0)
                {
                    sb.AppendLine((sy / sx).ToString());
                }
                else
                {
                    bool pos = (sx > 0 && sy >= 0) || (sx < 0 && sy <= 0);
                    sx = sx > 0 ? sx : -sx;
                    sy = sy >= 0 ? sy : -sy;
                    long gcd = GCD(sx, sy);
                    if (pos)
                        sb.AppendFormat("{0}/{1}\n", sy / gcd, sx / gcd);
                    else
                        sb.AppendFormat("-{0}/{1}\n", sy / gcd, sx / gcd);
                }
            }


            sw.Write(sb);
            sw.Close();
            sr.Close();
        }

        static long GCD(long A, long B)
        {
            long r;
            if (B > A)
                (A, B) = (B, A);
            while(B> 0)
            {
                r = A % B;
                A = B;
                B = r;
            }
            return A;
        }
    }
}

#endif
}
