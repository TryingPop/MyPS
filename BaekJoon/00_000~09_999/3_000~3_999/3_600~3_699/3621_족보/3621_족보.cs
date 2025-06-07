using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 족보
    문제번호 : 3621번

    수학, 그리디 알고리즘 문제다
    예제에서 0이 있어 0번부터 n -1 번까지 인줄 알았으나,
    1번부터 시작이라 인덱스 에러로 1번 틀렸다
    이후에는 i번째 보고 1번부터 시작임을 인지하고 
    n까지로 범위를 넓히니 이상없이 통과했다

    경우의 수 찾아보고, 언제 늘려야하는지 확인해서
    이를 수식으로 옮겨 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0330
    {

        static void Main330(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int r = ReadInt();

            int[] cnt = new int[n + 1];
            for (int i = 0; i < n; i++)
            {

                int idx = ReadInt();
                cnt[idx]++;
            }

            sr.Close();

            int ret = 0;
            for (int i = 0; i <= n; i++)
            {

                if (cnt[i] <= r) continue;
                int calc = cnt[i] - (r + 1);
                calc /= (r - 1);
                ret += calc + 1;
            }

            Console.WriteLine(ret);

            int ReadInt()
            {

                int c, ret = 0;

                while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }

    }
}
