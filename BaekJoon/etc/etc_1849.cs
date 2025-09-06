using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 29
이름 : 배성훈
내용 : 가장높은탑쌓기
    문제번호 : 2655번

    dp, 정렬, 역추적 문제다.
    dp[i][j] = val를
    i번째 탑을 쌓았고 가장 위에 있는 것이 j인 가장 높은 높이 val를 담기게한다.

    블록의 무게가 같은게 없고 면적도 같은게 없다. 
    그리고 쌓는 규칙에 의해 쌓을 수 있다면 쌓아가는게 그리디로 전체에서 최대가 보장된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1849
    {

        static void Main1849(string[] args)
        {

            int n;
            (int a, int h, int w)[] bricks;

            Input();

            GetRet();

            void GetRet()
            {

                int[][] dp = new int[n][];
                int[][] back = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    dp[i] = new int[n];
                    back[i] = new int[n];
                }

                int max = 0;
                for (int cur = 0; cur < n; cur++)
                {

                    dp[0][cur] = bricks[cur].h;
                    max = Math.Max(dp[0][cur], max);
                }

                for (int h = 1; h < n; h++)
                {

                    for (int bot = 0; bot < n; bot++)
                    {

                        if (dp[h - 1][bot] == 0) continue;
                        for (int top = 0; top < n; top++)
                        {

                            if (Chk(bot, top)) continue;

                            int chk = dp[h - 1][bot] + bricks[top].h;
                            if (dp[h][top] < chk)
                            {

                                dp[h][top] = chk;
                                max = Math.Max(chk, max);
                                back[h][top] = bot;
                            }
                        }
                    }
                }

                int ret1 = FindHeight(max);

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write(ret1 + 1);
                sw.Write('\n');

                int s = FindTop(max, ret1);
                sw.Write(s + 1);
                sw.Write('\n');
                while (ret1 > 0)
                {

                    s = back[ret1--][s];
                    sw.Write(s + 1);
                    sw.Write('\n');
                }

                int FindTop(int _max, int _h)
                {

                    for (int top = 0; top < n; top++)
                    {

                        if (dp[_h][top] == _max) return top;
                    }

                    return -1;
                }

                int FindHeight(int _max)
                {

                    for (int h = n - 1; h >= 0; h--)
                    {

                        for (int top = 0; top < n; top++)
                        {

                            if (dp[h][top] == _max) return h;
                        }
                    }

                    return 0;
                }


                bool Chk(int _bot, int _top)
                {

                    return bricks[_bot].a <= bricks[_top].a 
                        | bricks[_bot].w <= bricks[_top].w;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                bricks = new (int a, int h, int w)[n];
                for (int i = 0; i < n; i++)
                {

                    bricks[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
