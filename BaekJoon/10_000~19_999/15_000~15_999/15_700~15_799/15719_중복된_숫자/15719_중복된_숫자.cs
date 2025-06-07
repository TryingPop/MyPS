using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 29
이름 : 배성훈
내용 : 중복된 숫자
    문제번호 : 15719번

    수학, 구현 문제다
    총합 계산을 잘못해서 한 번 틀렸다

    처음에는 1000만 불 배열을 할당해 중복 확인을 해서 풀었다
    해당 경우 800ms라 느렸다

    그래서 이후 1부터 n까지이므로,
    총합을 구한 뒤 입력 값을 빼는 형식으로 바꿨다
    그러면 중복 수가 음수로 표현된다

    여기서 1 ~ N - 1 까지 총합을 해야하는데, 1 ~ N까지 총합을 해서 한 번 틀렸다
    이후엔 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0381
    {

        static void Main381(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 1024);


            int n = ReadInt();

#if Slow
            bool[] cnt = new bool[n];
#else
            long sum = (n - 1);
            sum *= n;
            sum /= 2;
#endif
            long ret = 0;
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
#if Slow
                if (cnt[cur])
                {

                    ret = cur;
                    break;
                }

                cnt[cur] = true;
#else
                sum -= cur;
#endif
            }

            ret = -sum;

            sr.Close();
            Console.WriteLine(ret);

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
}
