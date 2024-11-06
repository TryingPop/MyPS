using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 29
이름 : 배성훈
내용 : 합성함수와 쿼리
    문제번호 : 17435번

    처음에는 사이클 배열만 주어지고 사이클을 찾아야하나 했다
        사이클을 찾는데 벨만 - 포드 알고리즘처럼 각 노드에 대해 n - 1번 돌릴까 생각했으나,
        노드가 최대 20만개나 주어지므로, 이는 N^2인 해당 방법에서 시간 초과가 뜬다!
        그래서 유니온 파인드로 사이클이 발견되면, 같은 그룹의 애들의 사이클 진입 시기와 주기들을 모두 조사할 생각이였다
        그런데, 이 경우는 n - 1 개가 사이클이 돌면, 마찬가지로 N^2이라 시간초과가 뜰 수 있다
        또한, 사이클이 없는 경우 최대 값을 입력받으면 20만 * 50만 = 100억 역시 시간 초과이다

    그래서 힌트를 보게되었고, 희소배열 알고리즘을 적용시켜야한다
    처음 보는 로직이므로 찾아보니 2^n승 이동 경로를 보관하는 방법이다
    그래서 해당 말대로 보관해서 풀었다 시간은 550ms나왔다

    빠른 사람 풀이를 보니 2^n승 연산을 비트연산으로 바로 했는데, 좋은 방법인거 같다!
*/

namespace BaekJoon._44
{
    internal class _44_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            int log = (int)Math.Log2(500_000) + 2;
            int[,] func = new int[len + 1, log];

            {

                // 함수 입력 받기
                string[] temp = sr.ReadLine().Split(' ');

                for (int i = 0; i < len; i++)
                {

                    func[i + 1, 1] = int.Parse(temp[i]);
                }

                // 2^n승 이동
                for (int b = 2; b < log; b++)
                {

                    // 번호별 이동!
                    for (int f = 1; f < len + 1; f++)
                    {

                        // f에서 2^b승 이동은, f에서 2^b -1 이동한 지점에서 2^ b - 1번 이동한 것을 넣는다 
                        func[f, b] = func[func[f, b - 1], b - 1];
                    }
                }
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());

            for (int t = 0; t < test; t++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                // 합성 횟수
                int cycle = int.Parse(temp[0]);
                // 인자
                int param = int.Parse(temp[1]);

                // 합성 횟수가 0일때까지
                while (cycle != 0)
                {

                    int up = 0;
                    int calc = 1;
                    while(calc <= cycle)
                    {

                        calc *= 2;
                        up++;
                    }

                    param = func[param, up];
                    cycle -= calc / 2;
                }

                sw.WriteLine(param);
            }

            sr.Close();
            sw.Close();
        }
    }

#if other
    using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
    using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

    var m = ScanInt();
    var f = new int[19, m + 1];
    for (int i = 1; i <= m; i++)
        f[0, i] = ScanInt();
    for (int i = 1; i <= 18; i++)
        for (int j = 1; j <= m; j++)
            f[i, j] = f[i - 1, f[i - 1, j]];

    var q = ScanInt();
    for (int i = 0; i < q; i++)
    {
        var n = ScanInt();
        var x = ScanInt();

        var input = x;
        for (int j = 0; j < 19; j++)
        {
            if ((n & (1 << j)) == 0)
                continue;
            input = f[j, input];
        }
        sw.WriteLine(input);
    }

    int ScanInt()
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
#endif
}
