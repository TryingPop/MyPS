using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 24
이름 : 배성훈
내용 : XOR
    문제번호 : 10464번

    수학 문제다
    앞에서 etc_0880 연속 XOR 에서 본 다른 사람 아이디어를 
    그대로 채택해 풀었다

    0 ~ a까지 비트연산을 하는데,
    4단위로 정답이 나뉜다
*/
namespace BaekJoon.etc
{
    internal class etc_0907
    {

        static void Main907(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    int ret = RangeXOR(f - 1) ^ RangeXOR(b);

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            int RangeXOR(int _n)
            {

                switch(_n % 4)
                {

                    case 0:
                        return _n;

                    case 1:
                        return 1;

                    case 2:
                        return _n + 1;

                    default:
                        return 0;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
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
// #include<cstdio>
int f(int x) { return x - x % 2 * x + (x / 2 & 1 ^ x & 1); }
int t,x,y;
int main() {
	scanf("%d", &t);
	while (t--) scanf("%d %d", &x, &y), printf("%d\n", f(x - 1) ^ f(y));
	return 0;
}
#endif
}
