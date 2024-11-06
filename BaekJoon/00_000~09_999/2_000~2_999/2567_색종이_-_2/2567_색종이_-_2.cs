using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. -
이름 : 배성훈
내용 : 색종이 2
    문제번호 : 2567번

    둘레를 구하는 로직이 따로 있는가 고민했으나, 못찾았다;
    범위가 0 ~ 100까지 밖에 색종이가 있는 칸을 true로 해서
    길이를 일일히 확인했다

    길이 재는 법은 true -> false이거나 
    false -> true로 바뀌는 부분을 카운트 했다

    마지막 가장자리에 겹쳐질 수도 있으니 판을 1씩 확장했고
    처음 부분은 false로 둬서 해결
*/

namespace BaekJoon.etc
{
    internal class etc_0117
    {

        static void Main117(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            // 오른쪽 끝 때문에 101도 포함시켰다
            bool[][] map = new bool[102][];

            for (int i = 0; i < 102; i++)
            {

                map[i] = new bool[102];
            }

            // 입력 많아야 100 * 100 = 10_000번 
            for (int i = 0; i < n; i++)
            {

                int l = ReadInt(sr);
                int b = ReadInt(sr);

                for (int x = 0; x < 10; x++)
                {

                    for (int y = 0; y < 10; y++)
                    {

                        map[l + x][b + y] = true;
                    }
                }
            }

            sr.Close();

            int ret = 0;
            bool before = false;

            // 왼쪽 끝 확인
            for (int i = 0; i < 102; i++)
            {

                bool cur = map[0][i];
                if (before != cur) ret++;
                if (cur) ret++;

                before = cur;
            }

            // 이전과 비교하면서 확인 가능!
            for (int i = 1; i < 102; i++)
            {

                before = false;
                for (int j =0; j < 102; j++)
                {

                    bool cur = map[i][j];
                    if (before != cur) ret++;
                    before = cur;

                    if (map[i - 1][j] != cur) ret++;
                }
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
