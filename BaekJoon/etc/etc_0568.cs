using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 18
이름 : 배성훈
내용 : 신을 모시는 사당
    문제번호 : 27210번

    dp, 누적합 문제다
    마지막 값을 누락시켜 한 번 틀렸다
    연속된 최대? 최소? 합이므로 그리디로 접근해서 풀었다

    아이디어는 다음과 같다
    입력과 동시에 왼쪽을 보는 연속한 돌들을 최대한 그룹화 했다, 
    마찬가지로 오른쪽을 보는 돌들도 연속해서 그룹화 했다 (누적합)
    (1;1 교환이기에 왼쪽은 + 1, 오른쪽은 - 1로 계산했다)

    그리고 그룹을 단위로 그리디하게 탐색을 진행했다
    왼쪽을 보는 기준으로 가장 큰 경우와 오른쪽을 보는 기준으로 가장 큰 경우를 탐색했다
    누적해 나가는데 해당 그룹을 포함할 때 이전 값과 더한게 현재 그룹값보다 큰 경우면
    이전값과 더해서 큰 경우 값을 계승하고 아니면, 현재 값만 받아온다
    그리고 매 시간 최대 값을 확인한다

    이러면 마지막에 남는게 최대값이 보장된다
    왜냐하면 이전 값을 이어받는 경우는 왼쪽에서 크게해주는 값들을 찾는 것이고,
    끊는것은 오른쪽을 끊어주는 역할과 왼쪽 끝을 갱신하는 역할을 하기 때문이다

    경우를 예를들고 이어가는 식으로 최대값인 부분만 보자

        5, -3을 보자
    5인 경우가 최대값이다
    처음 연산에서 5를 찾아내고,
    두번째 항에서 
    5 + -3 > -3 이므로 끝났을 때 2의 값이된다
    2 < 5이므로 매번 최대값 갱신이 필요하다
    
        5, -3, 7을 보면
    여기서는 9가 최대값이다
    2가 살아서 2 + 7 > 7이 되어 
    끝날 때 최대값은 전부 합한 9이다
    살리는건 이러한 이유로 쓰인다

        5   -3  7  -100 1000
    이라하면
    최대 값은 1000만 계산하는 것이다

    1000 왼쪽에 연속된 부분은 -100 이므로 어떻게 더하던 음수이다!
    5, -3, 7까지는 누적해서 이어가다가 -100 부분에서 -93으로 이어가다가
    -93 + 1000 < 1000에서 끊긴다
    앞의 부분을 어떻게 더하던 음수이기에 끊는 것이다
    이러한 아이디어로 아래 코드를 작성했다

    이렇게 제출하니 64ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0568
    {

        static void Main568(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 2);
            
            int[] arr;
            int len;

            Input();
            Solve();

            sr.Close();

            void Solve()
            {

                // 결과 출력
                int min = 100_000;
                int max = -100_000;

                int minCalc = 0;
                int maxCalc = 0;
                for (int i = 0; i < len; i++)
                {

                    if (minCalc + arr[i] < arr[i]) minCalc += arr[i];
                    else minCalc = arr[i];

                    if (maxCalc + arr[i] > arr[i]) maxCalc += arr[i];
                    else maxCalc = arr[i];

                    min = minCalc < min ? minCalc : min;
                    max = max < maxCalc ? maxCalc : max;
                }

                int ret = -min < max ? max : -min;

                Console.WriteLine(ret);
            }

            void Input()
            {

                // 그룹별로 입력
                int n = ReadInt();
                arr = new int[n];

                len = 0;
                int before = ReadInt();
                int val = before == 1 ? 1 : -1;
                for (int i = 1; i < n; i++)
                {

                    int cur = ReadInt();
                    if (before != cur)
                    {

                        before = cur;
                        arr[len++] = val;
                        val = cur == 1 ? 1 : -1;
                    }
                    else val += cur == 1 ? 1 : -1;
                }

                arr[len++] = val;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c!= ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #nullable disable

using System;
using System.IO;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var isLeft = sr.ReadLine().Split(' ').Select(v => v[0] == '1').ToArray();

        var sum = new int[1 + n];

        for (var idx = 0; idx < n; idx++)
            sum[idx + 1] = sum[idx] + (isLeft[idx] ? 1 : -1);

        var mindp = new int[1 + n];
        var maxdp = new int[1 + n];

        for (var idx = 1; idx <= n; idx++)
        {
            mindp[idx] = sum[idx] < sum[mindp[idx - 1]] ? idx : mindp[idx - 1];
            maxdp[idx] = sum[idx] > sum[maxdp[idx - 1]] ? idx : maxdp[idx - 1];
        }

        var max = 0;
        for (var e = 1; e <= n; e++)
        {
            var ls1 = sum[e] - sum[mindp[e]];
            max = Math.Max(max, Math.Abs(ls1));

            var ls2 = sum[e] - sum[maxdp[e]];
            max = Math.Max(max, Math.Abs(ls2));
        }

        sw.WriteLine(max);
    }
}

#endif
}
