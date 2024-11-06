using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 12
이름 : 배성훈
내용 : 캔디 분배
    문제번호 : 3955번

    수학, 정수론, 확장 유클리드 호제법 문제다
    카잉 달력 풀다가 해당 문제로 넘어왔다

    아이디어는 다음과 같다
    hX + cY = 1인
    양수 Y를 찾는 것이다

    이는 유클리드 알고리즘을 이용하는 것인데,
    해당 방법은 gcd를 찾으면서 가는 방법을 
    역으로 연산하면 합동식을 최대 공약수가 되는 값을 찾을 수 있게 해준다

    문제 조건으로 10억개가 넘으면 취소해야하고,
    최소 1개 이상 구매해야한다(해당 반례 처리를 안해서 2번 틀렸다)
    이에 조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0517
    {

        static void Main517(string[] args)
        {

            string NO = "IMPOSSIBLE\n";
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int test = ReadInt();

            while(test-- > 0)
            {

                int h = ReadInt();
                int c = ReadInt();

                int gcd = GetGCD(h, c, out long ret);
                ret %= h / gcd;
                ret = ret <= 0 ? (h / gcd) + ret : ret;

                // 문제 조건에 맞춰 새롭게 설정
                if (c == 1) ret = h + 1;
                else if (h == 1) ret = 1;


                if (gcd != 1 || ret > 1_000_000_000) sw.Write(NO);
                else sw.Write($"{ret}\n");
            }

            sr.Close();
            sw.Close();

            int GetGCD(int _a, int _b, out long _ret)
            {

                long s1 = 1;
                long s2 = 0;

                long t1 = 0;
                long t2 = 1;

                long q;
                while(_b > 0)
                {

                    int r = _a % _b;
                    q = (_a - r) / _b;

                    _a = _b;
                    _b = r;

                    long temp = -q * s2 + s1;
                    s1 = s2;
                    s2 = temp;

                    temp = -q * t2 + t1;
                    t1 = t2;
                    t2 = temp;
                }

                _ret = t1;
                return _a;
            }

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

#if other
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//----------------

namespace BaekjoonStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            int testCount = int.Parse(Console.ReadLine());
            for (;testCount>0;testCount--) {
                int[] r = Array.ConvertAll(Console.ReadLine().Split(' '),int.Parse);
                if (r[0] == 1 && r[1] == 1)
                {
                    Console.WriteLine(2);
                }
                else if(r[0] == 1){
                    Console.WriteLine(1);
                }
                else if (r[1] == 1)
                {
                    if (r[0] + 1 > 1_000_000_000)
                    {
                        Console.WriteLine("IMPOSSIBLE");
                    }
                    else
                    {
                        Console.WriteLine(r[0] + 1);
                    }
                }
                else
                {
                    int gdc = GDC(r[0], r[1], out int x, out int y);
                    if (gdc == 1)
                    {
                        if (x > 0)
                        {
                            y += r[0] * (y / r[0] + 1);
                        }
                        if (y > 1_000_000_000)
                        {
                            Console.WriteLine("IMPOSSIBLE");
                        }
                        else
                        {
                            Console.WriteLine(y);
                        }
                    }
                    else
                    {
                        Console.WriteLine("IMPOSSIBLE");
                    }
                }
            }
        }
        static int GDC(int a,int b,out int x,out int y) {
            (x, y) = (1, 0);
            var (bx, by) = (0, 1);
            int q;
            while (b!=0) {
                (a, b, q) = (b, a % b, a / b);
                (x, y, bx, by) = (bx, by, x - q * bx, y - q * by);
            }
            return a;
        }
    }
}
#elif other2
var n = int.Parse(Console.ReadLine()!);
while (n-- > 0)
{
    var a = Array.ConvertAll(Console.ReadLine()!.Split(' '), long.Parse);
    if (a[1] == 1)
        Console.WriteLine($"{(a[0] < 1E9 ? (a[0] + 1) : "IMPOSSIBLE")}");
    else if (a[0] == 1) Console.WriteLine(1);
    else if (g(a[0], a[1]) != 1) Console.WriteLine("IMPOSSIBLE");
    else
    {
        long result = e(a[0], a[1]);
        Console.WriteLine($"{(result < 1E9 ? result : "IMPOSSIBLE")}");
    }
}
long g(long a, long b) => a % b == 0 ? b : g(b, a % b);
long e(long m, long a)
{
    long mod = m;
    a %= m;
    long t1 = 0;
    long t2 = 1;
    (t1, t2) = (t2, (t1 - m / a * t2) % mod);
    t2 += t2 < 0 ? mod : 0;
    while (m % a != 0)
    {
        (m, a) = (a, m % a);
        (t1, t2) = (t2, (t1 - m / a * t2) % mod);
        t2 += t2 < 0 ? mod : 0;
    }
    return t1 % mod;
}
#endif
}
