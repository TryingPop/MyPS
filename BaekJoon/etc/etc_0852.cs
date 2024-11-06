using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 31
이름 : 배성훈
내용 : 벼락치기
    문제번호 : 23739번

    수학, 구현 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0852
    {

        static void Main852(string[] args)
        {

            StreamReader sr;

            Solve();
            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                int n = ReadInt();
                int ret = 0;
                int curTime = 0;
                for (int i = 0; i < n; i++)
                {

                    int total = ReadInt();
                    curTime += total;

                    if (curTime < 30) ret++;
                    else
                    {

                        // 남은시간
                        int r = curTime - 30;

                        // 남은시간 <= 공부한 시간 ==> 절반이상 공부함
                        if (r <= total - r) ret++;

                        curTime = 0;
                    }
                }

                Console.Write(ret);
            }

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
#if other
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int n = int.Parse(sr.ReadLine());

int ans = 0;
int timer = 30;
for(int i=0; i<n; i++)
{
    int val = int.Parse(sr.ReadLine());
    if (timer >= val)
    {
        ans++;
        timer -= val;
    }
    else
    {
        if (val <= timer * 2)
            ans++;
        timer = 0;
    }
    if (timer == 0)
        timer = 30;
}
sw.WriteLine(ans);

sr.Close();
sw.Close();
#endif
}
