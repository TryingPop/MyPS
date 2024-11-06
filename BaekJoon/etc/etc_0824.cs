using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 17
이름 : 배성훈
내용 : 토끼의 이동
    문제번호 : 3101번

    수학, 구현 문제다
    보드 좌표로 바로 값을 구하는 함수를 만든 뒤
    시뮬레이션 돌리면서 값을 더해갔다
*/

namespace BaekJoon.etc
{
    internal class etc_0824
    {

        static void Main824(string[] args)
        {

            int UP = 'U';
            int DOWN = 'D';
            int LEFT = 'L';
            int RIGHT = 'R';

            StreamReader sr;

            long size;

            long[] sum;
            long ret;
            int x, y;


            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                ret = 1L;
                x = 0;
                y = 0;
                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int op = sr.Read();

                    if (op == UP) y--;
                    else if (op == DOWN) y++;
                    else if (op == LEFT) x--;
                    else if (op == RIGHT) x++;
                    else continue;

                    ret += PosToVal();
                }

                Console.Write(ret);
            }

            long PosToVal()
            {

                long dia = x + y;
                long ret = sum[dia];

                if (dia < size)
                {

                    if ((dia & 1) == 1) ret += y + 1;
                    else ret += x + 1;
                }
                else
                {

                    if ((dia & 1) == 1) ret += size - x;
                    else ret += size - y;
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);
                size = ReadInt();

                sum = new long[2 * size];

                int idx;
                for (idx = 1; idx <= size; idx++)
                {

                    sum[idx] = sum[idx - 1] + idx;
                }

                for (long i = size - 1; i >= 1; i--)
                {

                    sum[idx] = sum[idx - 1] + i;
                    idx++;
                }
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
// #include <cstdio>
using namespace std;

long long Sigma(int N)
{

    return 1LL * N * (N + 1LL) / 2LL;
}

int main()
{

    int N, K, R = 0, C = 0;
    scanf("%d %d\n", &N, &K);
    long long result = 1;
    for (int i = 0; i < K; i++)
    {

        switch (getchar())
        {

        case 'U':
            --R;
            break;

        case 'D':
            ++R;
            break;

        case 'L':
            --C;
            break;

        default:
            ++C;
        }

        // 좌표를 이용해서 값을 얻어낼 수 있다.
        long long first, dist;
        if (R + C < N)
        {

            // 각 줄은 1의 등차수열이다.
            // 첫번째 값을 얻어내고
            first = 1LL + Sigma(R + C);
            // 첫번쨰 값의 좌표로부터 얼마나 떨어져 있는지를 계산한다.
            dist = ((R + C) % 2 ? R : C);
        } 

        // 여기를 계산할 때 주의해야 한다.
        else
        {

            first = 1LL + Sigma(N) + Sigma(N - 1) - Sigma(2 * N - (R + C + 1));
            dist = ((R + C) % 2 ? R : C) - ((long long)R + C - N + 1);
        }

        result += (first + dist);
    }

    printf("%lld\n", result);
}
#endif
}
