using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 13
이름 : 배성훈
내용 : 로봇 조종하기
    문제번호 : 2169번

    처음에는, sum을 해서 가로 세로를 확인하려고 했다;
    예를들어
        0   1   2   3   4       <- idx
        A   B   C   D   E       <- 현재 
        a   b   c   d   e       <- 다음

    
    라인의 총합을 저장한 dp를 써서
    dp
        0       1           2               3                   4
        a       a + b       a + b + c       a + b + c + d       a + b + c + d + e
    를 저장하고

    다음 0 자리에 올 최대 가치는 
        A + a, 
        B + b + a, 
        C + c + b + a, 
        D + d + c + b + a, 
        E + e + d + c + b + a 
    중 가장 큰 값을 비교하는 방식으로 말이다
    해당 방법은 width * width * height의 시간(N^3)을 소모한다
    이게 아닌거 같아 고민했고

    그런데, 좌 우로만 이동 가능하기에, 우로 가는거 1번, 좌로 가는거 1번
    둘 중 최대값을 담으면.. N ^ 2 * C(상수) 에 해결이 가능하다.

    그리고 앞의 경우는 딱히 필요없기에 갱신하면서 했다
    다만, (N, M)으로 표현했기에 직교 좌표계를 생각해서
    처음에는 N이 width인줄 알았다, 그런데 해당 문제는 행렬을 표현했다 N이 height 혹은 column이다
*/

namespace BaekJoon._48
{
    internal class _48_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // .. 해당 순서다;
            int height = ReadInt(sr);
            int width = ReadInt(sr);

            // 먼저 한 번 가로로 읽기!
            int[] cur = new int[width];
            int[] cost = new int[width];
            for (int i = 0; i < width; i++)
            {

                cost[i] = ReadInt(sr);
            }

            cur[0] = cost[0];
            for (int i = 1; i < width; i++)
            {

                cur[i] = cur[i - 1] + cost[i];
            }

            int[] rightDp = new int[width];
            int[] leftDp = new int[width];
            for (int i = 0; i < height - 1; i++)
            {

                for (int j = 0; j < width; j++)
                { 

                    cost[j] = ReadInt(sr);
                }

                // 먼저 우로 이동 최대 값을 담는다
                rightDp[0] = cur[0] + cost[0];
                for (int j = 1; j < width; j++)
                {

                    int r = rightDp[j - 1] + cost[j];
                    int d = cur[j] + cost[j];
                    rightDp[j] = d < r ? r : d;
                }

                // 좌로 이동 최대값을 담는다
                leftDp[width - 1] = cur[width - 1] + cost[width - 1];
                for (int j = width - 2; j >= 0; j--)
                {

                    int l = leftDp[j + 1] + cost[j];
                    int d = cur[j] + cost[j];
                    leftDp[j] = l < d ? d : l;
                }

                // 둘을 비교해서 최대가 되는 값을 담는다!
                for (int j = 0; j < width; j++)
                {

                    cur[j] = leftDp[j] < rightDp[j] ? rightDp[j] : leftDp[j];
                }
            }

            sr.Close();

            Console.WriteLine(cur[width - 1]);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            bool plus = true;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                
                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }
                ret *= 10;
                ret += c - '0';
            }

            ret = plus ? ret : -ret;
            return ret;
        }
    }
}
