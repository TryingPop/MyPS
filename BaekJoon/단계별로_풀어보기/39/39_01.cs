using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 19
이름 : 배성훈
내용 : 트리와 쿼리
    문제번호 : 15681번

    기존 루트가 있는 트리에서 해당 정점을 루트로하는 서브 트리(쿼리)의 개수를 찾는 문제이다
*/

namespace BaekJoon._39
{
    internal class _39_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            List<int>[] lines = new List<int>[info[0] + 1];

            for (int i = 1; i < info[0] + 1; i++)
            {

                lines[i] = new();
            }

            for (int i = 0; i < info[0] - 1; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                lines[temp[0]].Add(temp[1]);
                lines[temp[1]].Add(temp[0]);
            }

            int[] chk = new int[info[2]];
            for (int i = 0; i < info[2]; i++)
            {

                chk[i] = int.Parse(sr.ReadLine());
            }

            sr.Close();

            int[] dp = new int[info[0] + 1];
            bool[] visit = new bool[info[0] + 1];

            DFS(lines, info[1], dp, visit);


            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < info[2]; i++)
                {

                    sw.WriteLine(dp[chk[i]]);
                }
            }
        }

        static int DFS(List<int>[] _lines, int _start, int[] _dp, bool[] _visit)
        {

            int result = 1;

            _visit[_start] = true;

            for (int i = 0; i < _lines[_start].Count; i++)
            {

                int next = _lines[_start][i];
                if (_visit[next]) continue;

                result += DFS(_lines, next, _dp, _visit);
            }

            _dp[_start] = result;
            return result;
        }
    }
}
