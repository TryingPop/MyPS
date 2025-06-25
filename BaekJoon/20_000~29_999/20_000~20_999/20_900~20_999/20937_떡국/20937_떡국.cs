using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 12
이름 : 배성훈
내용 : 떡국
    문제번호 : 20937번

    그리디로 풀었다
    아이디어는 다음과 같다

    높이 갯수로 저장한다
    그러면 1 이상인 높이에 대해 1개씩 빼며 탑을 하나 만든다
    그리고 높이 갯수가 모두 0개가 아니라면 앞과 같이 탑을 하나 만들고 1개 뺀다
    이렇게 탑을 만들어가면 최소값이 된다
    그래서 높이가 가장 많은 개수를 정답으로 해서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0202
    {

        static void Main202(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 높이 개수 센다
            // 최대 1 ~ 50만
            int[] cnt = new int[500_001];
            int len = ReadInt(sr);

            for (int i = 0; i < len; i++)
            {

                int idx = ReadInt(sr);
                cnt[idx]++;
            }

            sr.Close();

            int ret = 0;
            for (int i = 0; i < cnt.Length; i++)
            {

                // 가장 많은 높이개수가 탑의 개수랑 동형이다
                if (ret < cnt[i]) ret = cnt[i];
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
