using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 적의 적
    문제번호 : 12893번

    그래프 관련 구현 문제이다 그냥 관계 조사하면 된다
    처음에는 그룹 수가 짝수면 되는거 아닌가 싶어 유니온 파인드로 접근했지만
    중간에 1 - 2 - 3 - 4인 상태에서 1 - 4가 주어지는 반례가 떠올라 
    유니온 파인드 접근 방법을 중단했다

    그리디하게 그냥 노드들의 관계를 일일히 탐색하는 방법으로 접근했다
    아래는 노드들의 관계를 조사한 코드이다
*/

namespace BaekJoon.etc
{
    internal class etc_0194
    {

        static void Main194(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            List<int>[] relation = new List<int>[n + 1];
            for (int i = 1; i <= n; i++)
            {

                relation[i] = new();
            }

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                relation[f].Add(b);
                relation[b].Add(f);
            }

            sr.Close();

            Queue<int> q = new Queue<int>();
            int[] team = new int[n + 1];
            bool impossible = false;
            for (int i = 1; i <= n; i++)
            {

                // 이미 조사한 노드
                if (team[i] != 0) continue;
                // 처음 노드는 그냥 1로 설정
                // 처음 그룹이거나 이전과 분리된 그룹 조사할 때 새로 진입한다
                team[i] = 1;

                q.Enqueue(i);

                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    for (int j = 0; j < relation[node].Count; j++)
                    {

                        int next = relation[node][j];
                        if (team[next] == 0)
                        {

                            team[next] = -team[node];
                            q.Enqueue(next);
                        }
                        else if (team[next] == team[node])
                        {

                            // 중복되는 경우 발견
                            impossible = true;
                            q.Clear();
                            break;
                        }

                    }
                }

                // 중복되면 더 조사안하고 탈출
                if (impossible) break;
            }

            if (impossible) Console.WriteLine(0);
            else Console.WriteLine(1);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
