using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 3
이름 : 배성훈
내용 : 진법 변환
    문제번호 : 1112번

    수학, 구현, 정수론 문제다
    경우를 5개로 나눠 풀었다
    나누는 값이 음, 양, 0인 경우
    그리고 진법 값이 음 양인 경우다

    진법 값이 양수면 나누는 값이 음수 양수 차이는 앞의 음수 부호다
    반면 진법이 음수면 나눠서 찾았다 
    -13, -3  => 1222 
    13, -3 => 221 
    으로 규칙을 찾았다
*/


namespace BaekJoon.etc
{
    internal class etc_1021
    {

        static void Main1021(string[] args)
        {

            StreamReader sr;
            char[] ret;
            int len;
            long x, b;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                ret = new char[100];
                len = 0;
                if (b > 0) Calc1();
                else Calc2();

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (int i = 0; i < len; i++)
                    {

                        sw.Write(ret[i]);
                    }
                }
            }

            void Calc2()
            {

                if (x == 0)
                {

                    len++;
                    ret[0] = '0';
                    return;
                }

                while (x != 0)
                {

                    long chk = x % b;
                    if (chk < 0) chk += -b;
                    x -= chk;

                    ret[len++] = (char)(chk + '0');
                    x /= b;
                }

                for (int i = 0; i < len; i++)
                {

                    int j = len - 1 - i;
                    if (j < i) break;
                    var temp = ret[i];
                    ret[i] = ret[j];
                    ret[j] = temp;
                }
            }

            void Calc1()
            {

                if (x < 0) 
                {

                    x = -x;
                    ret[len++] = '-';
                }
                else if (x == 0)
                {

                    len++;
                    ret[0] = '0';
                    return;
                }

                long mul = b;

                while (mul <= x) { mul *= b; }

                mul /= b;
                while (mul > 0)
                {

                    ret[len++] = (char)((x / mul) + '0');
                    x %= mul;
                    mul /= b;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                x = ReadInt();
                b = ReadInt();

                sr.Close();
            }

            int ReadInt()
            {


                int c = sr.Read();
                bool positive = c != '-';
                int ret = positive ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }

#if other
// #pragma warning disable CS8604, CS8602, CS8600

using System;

public class Program
{
    public static void Main(string[] args)
    {
        long[] input = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
        long n = input[0], b = input[1];

        if (n == 0)
        {
            Console.WriteLine("0");
            return;
        }

        string ans = "";
        bool minusSign = false;

        if (n < 0 && b > 0)
        {
            n *= -1;
            minusSign = true; ;
        }

        while (n != 0)
        {
            (long q, long r) = DivMod(n, b);
            n = q;
            ans = r + ans;
        }

        if (minusSign) { ans = "-" + ans; }
        Console.WriteLine(ans);
    }

    public static (long, long) DivMod(long a, long b)
    {
        long q = a / b;
        long r = a % b;

        if (r < 0)
        {
            q++;
            r += Math.Abs(b);
        }
        return (q, r);
    }
}
#elif other2
// #include <cstdio>
// #include <cmath>

int mod(int, int);

int main(){
    int x, b, i = 32, n, t;
    char d[33];
    d[32] = 0;

    scanf("%d %d", &x, &b);
    n = b > 0 ? abs(x) : x;

    while (n) {
        t = mod(n, b);
        n = (n - t) / b;
        d[--i] = t + '0';
    }

    if (!x) d[--i] = '0';

    if (x < 0 && b > 0) d[--i] = '-';

    printf("%s", &d[i]);

    return 0;
}

int mod(int n, int b) {
    n %= abs(b);
    return n < 0 ? n + abs(b) : n;
}
#elif other3
// #include<cstdio>
int N,b,ab;
void print(){
	if(!N)return;
	int c=N%ab;
	N/=b;
	if(c<0){
		c+=ab;
		N++;
	}
	print();
	printf("%d",c);
	return;
}
int main(){
	scanf("%d%d",&N,&b);
	ab=b<0?-b:b;
	if(N<0 and b>0){
		printf("-");
		N=-N;
	}
	if(N)print();
	else printf("0");
	return 0;
}
#endif
}
