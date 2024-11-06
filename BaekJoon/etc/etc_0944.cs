using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 5
이름 : 배성훈
내용 : 소방서의 고민
    문제번호 : 2180번

    그리디, 정렬 문제다
    아이디어는 다음과 같다
    합성함수 형태로 연산이 진행된다

    a, b, c, d가 0이 아닐 경우 보자
    (a, b), (c, d)를 t초에서 (a, b) -> (c, d)순과
    (c, d) -> (a, b) 순으로 진행하면 둘의 차이는
    
    전자는 c(at + b + t) + d + at + b + t
    후자는 a(ct + d + t) + b + ct + d + t 이다

    여기서 서로 같은 부분을 없애주면
    전자는 bc, 후자는 ad만 남는다 

    그래서 ad == bc이면 같은 결과를 내어 어느것을 먼저해도 상관없다
    반면 ad > bc인 경우면 (a, b) -> (c, d) 순이 값이 더 작으므로 좋다
    ad < bc인 경우면 (c, d) -> (a, b) 순이 값이 더 작으므로 좋다

    이로 두개의 경우 b / a 값이 작은걸 먼저 실행하는게 좋다
    그리고 다음 e, f에 대해 e 가 양수 이므로
    t1 < t2라면 et1 + f < et2 + f이므로 값이 더 작은 쪽으로 진행하는게 좋다

    이 둘을 이용하면 b / a 의 값이 작은순으로 정렬해 진행하면 된다
    이는 a, b가 0 이 아닐 경우다!

    그리고 a가 0인 경우를 보면, t초에 시작하던, 0초에 시작하던 b시간으로 동일하다
    그래서 a가 0이 아닌 것들은 시간이 계속해서 증가하므로 그리디로 마지막에 진행한다

    이제 b가 0인 경우는 0초에 시작하면 0초가 걸린다
    그래서 b가 0인 경우는 0초에 먼저 다 실행해 끝내는게 좋다!

    이들을 이용해 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0944
    {

        static void Main944(string[] args)
        {

            int MOD = 40_000;

            StreamReader sr;
            int n;
            (int a, int b)[] arr;
            int ret, add, len;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr, 0, len, Comparer<(int a, int b)>.Create((x, y) =>
                {

                    int chk1 = x.a * y.b;
                    int chk2 = x.b * y.a;

                    return chk2.CompareTo(chk1);
                }));

                ret = 0;
                for (int i = 0; i < len; i++)
                {

                    int chk = (ret * arr[i].a + arr[i].b) % MOD;
                    ret = (ret + chk) % MOD;
                }

                ret = (ret + add) % MOD;

                Console.Write(ret);
            }

            void Input()
            {

                sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new (int a, int b)[n];
                add = 0;
                len = 0;
                for (int i = 0; i < n; i++)
                {

                    int a = ReadInt();
                    int b = ReadInt();

                    if (b == 0) continue;
                    else if (a == 0)
                    {

                        add = (add + b) % MOD;
                        continue;
                    }

                    arr[len++] = (a, b);
                }

                sr.Close();
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

public class Program
{
    struct Fire : IComparable<Fire>
    {
        public int a, b;
        public Fire(int a, int b)
        {
            this.a = a; this.b = b;
        }
        public int CompareTo(Fire other)
        {
            int x = b * other.a, y = other.b * a;
            if (x == 0 && y == 0)
                return b - other.b;
            return x - y;
        }
    }
    const int Mod = 40000;
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        Fire[] fires = new Fire[n];
        for (int i = 0; i < n; i++)
        {
            string[] ab = Console.ReadLine().Split(' ');
            int a = int.Parse(ab[0]), b = int.Parse(ab[1]);
            fires[i] = new(a, b);
        }
        Array.Sort(fires);
        int answer = 0;
        foreach (Fire fire in fires)
        {
            answer += fire.a * answer + fire.b;
            answer %= Mod;
        }
        Console.Write(answer);
    }
}
#endif
}
