using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 17
이름 : 배성훈
내용 : 퍼거슨과 사과
    문제번호 : 2942번

    수학, 정수론, 유클리드 호제법 문제다
    r, b의 gcd를 찾으면 gcd의 약수들은 r, b를 모두 나눈다
    그래서 gcd의 약수들을 찾고 경우의 수를 출력하니 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_0819
    {

        static void Main819(string[] args)
        {

            StreamReader sr;

            int r, g;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int gcd = GetGCD(r, g);

                for (int i = 1; i * i <= gcd; i++)
                {

                    if (gcd % i != 0) continue;
                    int other = gcd / i;

                    sw.Write($"{i} {r / i} {g / i}\n");
                    if (i == other) continue;

                    sw.Write($"{other} {r / other} {g / other}\n");
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                r = ReadInt();
                g = ReadInt();

                sr.Close();
            }

            int GetGCD(int _a, int _b)
            {

                while (_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

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

namespace C__study
{
    internal class baekjoon222
    {
        public static int get_gcd(int a, int b)
        {
            while (b != 0)
            {
                int tmp = b;
                b = a % b;
                a = tmp;
            }
            return a;
        }
        public static int[] get_sub(int[]sub, int gcd)
        {
            if(gcd == 1)
            {
                sub[0] = 1;
            }
            else if(gcd > 1 && gcd < 4)
            {
                sub[0] = 1;
                sub[1] = gcd;
            }
            else
            {
                int count = 0;
                for(int i = 1; i<= (int)Math.Sqrt(gcd); i++)
                {
                    if(gcd % i == 0)
                    {
                        sub[count] = i;
                        if(i != gcd/i)
                            sub[count + 1] = gcd / i;
                        count+=2;
                    }
                }
            }

            return sub;
        }
        public static void Main()
        {
            string input = Console.ReadLine();
            string[] inputs = input.Split(' ');
            int red = int.Parse(inputs[0]);
            int green = int.Parse(inputs[1]);
            int gcd = get_gcd(red, green);
            int[] sub = new int[(int)Math.Sqrt(gcd)*2];
            sub = get_sub(sub, gcd);
            Array.Sort(sub);
            for(int i = 0; i < sub.Length; i++)
            {
                if (sub[i] != 0)
                    Console.WriteLine("{0} {1} {2}", sub[i], red / sub[i], green / sub[i]);
            }
        }
    }
}

#endif
}
