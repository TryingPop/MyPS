using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 30
이름 : 배성훈
내용 : 선발 명단
    문제번호 : 3980번

    브루트포스, 백트래킹 문제다
    문제에서 능력치가 0인 포지션에 배치될 수 없고
    그리고 반드시 하나 이상의 라인업이 존재한다기에
    0인 경우 배치하지 않는 DFS 탐색을 했다

    만약 0인 경우 배치될 수 없다는 조건이 없다면
    DFS로 탐색하는건 시간이 오래 걸린다
*/

namespace BaekJoon.etc
{
    internal class etc_0395
    {

        static void Main395(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();

            int[,] stat = new int[11, 11];
            int[] calc = new int[11];
            bool[] visit = new bool[11];

            while(test-- > 0)
            {

                for (int i = 0; i < 11; i++)
                {

                    for (int j = 0; j < 11; j++)
                    {

                        stat[i, j] = ReadInt();
                    }
                }

                int ret = DFS(0);
                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();

            int DFS(int _depth)
            {

                int ret = 0;
                if (_depth == 11)
                {

                    for (int i = 0; i < 11; i++)
                    {

                        // 총합 계산
                        ret += stat[calc[i], i];
                    }

                    return ret;
                }

                for (int i = 0; i < 11; i++)
                {

                    // 배치 불가능인 경우나 이미 배치한 경우
                    // 0을 확인 안하면 시간 초과 뜰 것이다
                    if (stat[i, _depth] == 0 || visit[i]) continue;
                    visit[i] = true;
                    calc[_depth] = i;
                    int chk = DFS(_depth + 1);
                    ret = ret < chk ? chk : ret;
                    visit[i] = false;
                }

                return ret;
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
}
