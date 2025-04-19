using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 링고와 순열
    문제번호 : 17505번

    그리디 알고리즘, 해 구성하기 문제다
    해를 만드는건 그리디 알고리즘과 두 포인터를 써서 만들었다
    우선 k의 범위가 0 <= k <= (n - 1) * n / 2이면 
    만들 수 있다!

    뒤에서 i번째 원소에서 자기보다 뒤에 있는 원소들을 이용해 
    만들 수 있는 반전은 0 ~ i - 1개 만들 수 있다
    이를 최대로 되게 해서 i - 1 개 만들게 했다
    그러면 i - 1 개를 만들어야 하는가 여부로 원하는 k값을 조절할 수 있다

    앞에서부터 k >= i - 1이면 k -= i - 1을 하고,
    반전의 개수가 i - 1개가 되게 만든다 즉, 현재 쓸 수 있는 가장 큰 값을 배치
    반면 i - 1개를 만들 필요가 없다면 현재 쓸 수 있는 가장 작은 값을 배치했다

    이러한 과정으로 만들면 문제에서 요구하는 해를 만들 수 있고
    아래는 이 아이디어를 코드로 옮긴 것이다

    현재에서 가장 큰 값과 작은 값을 나타내는 용도로 두 포인터 알고리즘을 썼다
    이렇게 제출하니 144ms에 이상없이 통과했다

    k의 입력범위는 100억이라 long 자료형으로 읽었다
*/

namespace BaekJoon.etc
{
    internal class etc_0259
    {

        static void Main259(string[] args)
        {

            long[] info = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            long calc = info[1];

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {
                
                // 두 포인터 알고리즘
                int min = 1;
                int max = (int)info[0];

                // 뒤에서 i번째 원소는 앞에서
                // info[0] - i번째 인덱스가 된다
                // 인덱스 맞춰주기 위해 내려가는 for문 설정
                for (int i = max - 1; i >= 0; i--)
                {

                    if (calc < i)
                    {

                        // i개를 만들 필요가 없는 경우
                        // 현재 가장 작은 값을 쓴다
                        sw.Write($"{min} ");
                        min++;
                        continue;
                    }

                    // i개를 만들어야 한다
                    // 그래서 현재 쓸 수 있는 가장 큰값을 넣는다
                    calc -= i;
                    sw.Write($"{max} ");
                    max--;
                }

            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nk = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
        var n = (int)nk[0];
        var k = nk[1];

        var arr = new int?[n];
        var unused = new Stack<int>();
        var idx = 0;

        for (var val = n; val > 0; val--)
        {
            var maxInversion = val - 1;
            if (k >= maxInversion)
            {
                arr[idx++] = val;
                k -= maxInversion;
            }
            else
            {
                unused.Push(val);
            }
        }

        for (idx = 0; idx < n; idx++)
        {
            if (!arr[idx].HasValue)
                arr[idx] = unused.Pop();
        }

        foreach (var v in arr)
            sw.Write($"{v} ");
    }
}
#elif other2
using System;
using System.Text;

public class Program
{
    static void Main()
    {
        string[] nk = Console.ReadLine().Split(' ');
        int n = int.Parse(nk[0]);
        long k = long.Parse(nk[1]);
        int[] array = new int[n];
        for (int i = 1; i <= n; i++)
        {
            array[i - 1] = i;
        }
        StringBuilder sb = new();
        int left = 0, right = n - 1;
        for (int i = n - 1; i >= 0; i--)
        {
            if (k >= i)
            {
                sb.Append(array[right--]);
                k -= i;
            }
            else
                sb.Append(array[left++]);
            if (i > 0)
                sb.Append(' '); 
        }
        Console.Write(sb.ToString());
    }
}
#endif
}
