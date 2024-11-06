using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : -2진수
    문제번호 : 2089번

    수학, 정수론 문제다
    처음에는 어떻게 접근할까 고민했으나,
    10진수 숫자를 이진수로 표현할 때 2^?계승을 앞이나 뒤에서부터 확인하며 진행한다
    보통은 앞에서부터 진행해서 최고차항을 찾고, 1이면 해당 수를 빼면서 진행하나
    여기서는 반대로 뒤에서부터 진행했다

    아이디어는 다음과 같다
    만약 해당 4로 나눴을 때 몫이 홀수라면, 해당 자리는 포함되어야한다
    왜냐하면 다음 자리 8에서는 4를 나눌 수 없기 때문이다
    그리고 해당 자리가 포함되면 해당 값을 더해준다
    그리고 진행한다 이렇게 값을 찾는 경우 이진수와 자리 크기가 3자리를 넘지 않는다
    연산 방법에 의해, 어느 순간 2^k가 되고 이 수는 n을 양수로해서 2진수로 표현했을 때, 
    2^p 까지 표현된다면 p + 1 <= k임은 자명하다
    그리고 해당 수를 표현하는데, p + 1 이상 안쓰인다 만약 p가 짝수인 경우 양수이고, 찾는 값이 음수라면
    2^p + 1 - 2^p하면 얻을 수 있어 1자리를 넘지 못한다
    그래서 많아야 3자리 차이를 넘지 못한다
*/

namespace BaekJoon.etc
{
    internal class etc_0481
    {

        static void Main481(string[] args)
        {

            long find = long.Parse(Console.ReadLine());

            int[] ret = new int[35];
            int len = 0;
            for (int i = 0; i < 35; i++)
            {

                long chk = (1L << i);
                if ((find & chk) == 0) continue;
                if ((i & 1) == 1) chk = -chk;
                find -= chk;
                len = i;
                ret[i] = 1;
            }

            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                for (int i = len; i >= 0; i--)
                {

                    sw.Write(ret[i]);
                }
            }
        }
    }

#if other
int n = int.Parse(Console.ReadLine()!);
if (n == 0)
{
    Console.WriteLine(0);
    return;
}
string result = "";
while (n != 0)
{
    if (n % -2 == 0)
    {
        result = "0" + result;
        n /= -2;
    }
    else
    {
        result = "1" + result;
        n = (n - 1) / -2;
    }
}
Console.WriteLine(result);
#endif
}
