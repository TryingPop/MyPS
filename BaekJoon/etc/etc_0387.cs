using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 29
이름 : 배성훈
내용 : 피자 굽기
    문제번호 : 1756번

    구현, 이분탐색 문제다
    보고 바로 안떠올랐다 이중 포문이 제일 먼저 떠올랐고, 
    입력값의 범위를 보고 해당 방법은 바로 보류했다
    그리고 문제를 읽으며 다른 방법이 있지 않을가 고민했고
    오븐 입력 배열의 변형가능함을 알았다

    오븐에 놓는걸 보면, 깊이가 깊어질수록 위층의 지름보다 큰 것은 못들어가기에 의미가 없다
    예를들어 1층의 지름이 5이고 2층의 지름이 6이라하자
    그러면 2층에 가기 위해서는 지름 5이하여야한다
    공간이 6이 있지만 실제로 들어올 수 있는건 5이다
    그래서 위층의 지름보다 큰 경우 위 층의 지름들의 최소값이 되게 사이즈를 조절했다
    그러면 오븐 배열은 내림차순 정렬 집합이 된다
    이제 피자를 넣을 때 이분탐색으로 층을 쌓아가면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0387
    {

        static void Main387(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();

            int[] arr = new int[n];
            int min = 1_000_000_000;
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();

                // 사이즈 조절
                if (cur < min) min = cur;
                else cur = min;

                arr[i] = cur;
            }

            int r = n - 1;
            for (int i = 0; i < m; i++)
            {

                // 이분 탐색
                int l = 0;
                int cur = ReadInt();

                while (l <= r)
                {

                    int mid = (l + r) / 2;

                    if (cur <= arr[mid]) l = mid + 1;
                    else r = mid - 1;
                }

                // 들어간 인덱스는 l - 1번째이다
                // 이후 l - 1번쨰까지 사용 되었기에
                // l - 2가되어야한다
                // 다 쌓인경우 l > r이므로 포문 없이 r = 0 - 2 = -2이고 반복된다
                r = l - 2;
            }

            // 제일 위에 있는 층은 앞번의 l의 위치이다!
            // 다 쌓인 경우 -2를 가리키는데, 이는 0을 출력하라는 조건과 부합
            Console.WriteLine(r + 2);
            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
#if other
namespace boj_1756
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int answer = 0;

            string[] input = Console.ReadLine().Split();
            int D = int.Parse(input[0]);
            int N = int.Parse(input[1]);

            int[] ovenR = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            for (int i = 0; i < D - 1; i++)
            {
                if (ovenR[i + 1] > ovenR[i])
                    ovenR[i + 1] = ovenR[i];
            }

            Queue<int> pizzaR = new Queue<int>(Array.ConvertAll(Console.ReadLine().Split(), int.Parse));

            for (int i = D - 1; i > -1; i--)
            {
                if (ovenR[i] >= pizzaR.Peek())
                {
                    pizzaR.Dequeue();
                    answer = i + 1;
                }

                if (pizzaR.Count == 0)
                    break;
            }

            if (pizzaR.Count > 0)
                Console.WriteLine(0);

            else
                Console.WriteLine(answer);
        }
    }
}
#elif other2
int[] vs = Console.ReadLine().Split().Select(x=>int.Parse(x)).ToArray();
int t = vs[0] + 1;
int[] oven = Console.ReadLine().Split().Select(x=>int.Parse(x)).ToArray();
//R, F
Stack<(int, int)> array = new Stack<(int, int)> {};
int m = int.MaxValue;
for (int i = 0; i < vs[0]; i++)
{
    if (oven[i] < m)
    {
        array.Push((oven[i], i + 1));
        m = oven[i];
    }
}

oven = Console.ReadLine().Split().Select(x=>int.Parse(x)).ToArray();
var (R, F) = array.Pop();
for (int i = 0; i < vs[1]; i++)
{
    while (oven[i] > R)
    {
        if (array.Count == 0)
        {
            Console.WriteLine("0");
            return;
        }
        var (r, f) = array.Peek();
        if(oven[i] <= r)
        {
            t = Math.Min(t, F);
            break;
        }
        (R, F) = array.Pop();
    }
    t--;
}

Console.WriteLine(Math.Max(t, 0));
#endif
}
