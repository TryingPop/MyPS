using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 20
이름 : 배성훈
내용 : 건공문자열
    문제번호 : 30823번

    문자열 문제다
    뒤집는 횟수를 고려안해 3번 틀렸다

    아이디어는 다음과 같다
    우선 앞의 K - 1개를 제외하고는, 인덱스가 K - 1 씩 감소한다
    그리고 앞의 K - 1개는 시행횟수마다 뒤집어지고, 안뒤집어진다
    N - K 가 짝수이면 뒤집기를 홀수번 시행하므로 뒤집어져야하고,
    N - K 가 홀수이면 뒤집기를 짝수번 시행하므로 그대로 출력되어야 한다
*/

namespace BaekJoon.etc
{
    internal class etc_0583
    {

        static void Main583(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 8);
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));
            StringBuilder sb;

            int[] info;
            Solve();

            sr.Close();

            void Solve()
            {

                info = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                sb = new(info[0]);

                string str = sr.ReadLine();

                for (int i = info[1] - 1; i < info[0]; i++)
                {

                    sb.Append(str[i]);
                }

                if ((info[0] - info[1]) % 2 == 0)
                {

                    for (int i = info[1] - 2; i >= 0; i--)
                    {

                        sb.Append(str[i]);
                    }
                }
                else
                {

                    for (int i = 0; i < info[1] - 1; i++)
                    {

                        sb.Append(str[i]);
                    }
                }

                Console.Write(sb);
            }
        }
    }
#if other
using System.Text;

var l = Array.ConvertAll(Console.ReadLine().Split(), Int32.Parse);
var s = Console.ReadLine();
int index = l[1] - 1;
StringBuilder sb = new StringBuilder();
if ((l[0] % 2 == 0 && l[1] % 2 == 0) || (l[0] % 2 != 0 && l[1] % 2 != 0))
{
    sb.Append(s[index..]);
    for (int i = index - 1; i >= 0; i--) sb.Append(s[i]);
}
else
{
    sb.Append(s[index..]);
    sb.Append(s[..index]);
}
Console.Write(sb.ToString());
#endif
}
