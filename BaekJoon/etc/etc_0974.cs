using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 17
이름 : 배성훈
내용 : Password Problem (Small, Large)
    문제번호 : 12394번, 12395번

    수학, 확률론, 브루트 포스 문제다
    처음에는 백스페이스 누르는 경우 바로 정답으로 입력 시도하는 경우, 엔터 누르는 경우
    나눠서 구했다

    그리고 다른 사람의 풀이를 보니, 백스페이스 누르는 경우에 바로 정답을 누르는 경우가 포함됨을 알고
    또한 누적합을 이용하면 O(1)으로 확률을 곱해줄 필요가 없음을 알았다
    그래서 수정해 제출하니 양쪽 다 이상없이 통과한다
*/

namespace BaekJoon.etc
{

    internal class etc_0974
    {

        static void Main974(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int a, b;
            double[] exp;
            double ret;

            Solve();
            void Solve()
            {

                Init();

                int test = int.Parse(sr.ReadLine());

                for (int t = 1; t <= test; t++)
                {

                    Input();

                    GetRet();

                    sw.Write($"Case #{t}: {ret:0.000000}\n");
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                ret = Enter();
                for (int i = 0; i <= a; i++)
                {

                    double chk = BackSpace(i);
                    ret = Math.Min(ret, chk);
                }
            }

            double BackSpace(int _n)
            {

                double e = a == _n ? 1.0 : exp[a - 1 - _n];
                /*
                for (int i = 0; i < a - _n; i++)
                {

                    e *= exp[i];
                }
                */

                return (b - a + 1 + 2 * _n) + (b + 1) * (1 - e);
            }

            double Enter()
            {

                return b + 2;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                exp = new double[99_999];
            }

            void Input()
            {

                string[] temp = sr.ReadLine().Split();
                a = int.Parse(temp[0]);
                b = int.Parse(temp[1]);

                temp = sr.ReadLine().Split();
                for (int i = 0; i < a; i++)
                {

                    exp[i] = double.Parse(temp[i]);
                }

                for (int i = 1; i < a; i++)
                {

                    exp[i] *= exp[i - 1];
                }
            }
        }
    }

#if other
// #include<cstdio>
double p[9];
double min(double x, double y) { return x < y ? x : y; }
int main() {
    int T, a, b, i, j;
    double r;
    scanf("%d", &T);
    p[0] = 1;
    for (int t = 1;t <= T;t++) {
        scanf("%d%d", &a, &b);
        for (i = 1;i <= a;i++) {
            scanf("%lf", &p[i]);
            p[i] *= p[i - 1];
        }
        r = b + 2;
        for (i = 0;i <= a;i++) {
            r = min(r, (a - i) + p[i] * (b - i + 1) + (1 - p[i]) * (b - i + 1 + b + 1));
        }
        printf("Case #%d: %lf\n", t, r);
    }
    return 0;
}
#endif
}
