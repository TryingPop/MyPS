using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024 .6. 20
이름 : 배성훈
내용 : 속도 위반
    문제번호 : 11971번

    구현 문제다
    투 포인터와 경우를 나눠서 해결했다

    먼저 속도제한 구간과 이동한 구간이 일치하는 경우
    다음으로 속도 제한 구간이 이동간 구간의 여러겹에 걸치는 경우 예를들어 확인했다

    0 ~ 40 속도제한이 30, 40 ~ 60 속도 제한이 10인경우
    현재 0 ~ 10에 50 속도로, 10 ~ 35에 20의 속도로, 
    35 ~ 55에 60속도로가는경우를 보면

    0 ~ 10은 0 ~ 40 구간만 확인, 10 ~ 35도 마찬가지
    35 ~ 55는 0 ~ 40과 40 ~ 60을 확인했다

    그래서 이를 코드로 구현하니 52ms? 에 통과되었다
*/

namespace BaekJoon.etc
{
    internal class etc_0763
    {

        static void Main763(string[] args)
        {

            StreamReader sr;

            int n, m;
            int[] dis;
            int[] max;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                dis = new int[n];
                max = new int[n];

                dis[0] = ReadInt();
                max[0] = ReadInt();
                for (int i = 1; i < n; i++)
                {

                    dis[i] = ReadInt() + dis[i - 1];
                    max[i] = ReadInt();
                }


                int sum = 0;
                int curIdx = 0;
                int ret = 0;
                for (int i = 0; i < m; i++)
                {

                    sum += ReadInt();
                    int spd = ReadInt();

                    while (dis[curIdx] < sum)
                    {

                        ret = Math.Max(ret, spd - max[curIdx]);
                        curIdx++;
                    }

                    ret = Math.Max(ret, spd - max[curIdx]);
                    if (dis[curIdx] == sum) curIdx++;
                }

                sr.Close();
                Console.Write(ret);
            }

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
// cs11971 - rby
// 2023-04-11 20:58:32
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace cs11971
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int[] line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int N = line[0];
            int M = line[1];

            int[] road = new int[101];
            int[] speed = new int[101];

            int cur = 1;
            for (int i = 0; i < N; i++)
            {
                line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                for (int j = 0; j < line[0]; j++)
                    road[cur++] = line[1];
            }

            cur = 1;
            for (int i = 0; i < M; i++)
            {
                line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                for (int j = 0; j < line[0]; j++)
                    speed[cur++] = line[1];
            }

            int max = 0;
            for(int i = 1; i <= 100; i++)
            {
                if (max < speed[i] - road[i])
                    max = speed[i] - road[i];
            }
            sw.WriteLine(max);

            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
