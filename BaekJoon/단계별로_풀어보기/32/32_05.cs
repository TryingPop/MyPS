using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 26
이름 : 배성훈
내용 : 바이러스
    문제번호 : 2606번

    감염 과정을 보니 인접한 애들이 다 감염되고 다시 인접한 애들이 감염되므로 BFS로 풀었다
    완전 탐색이므로 DFS, BFS 차이 없다
*/

namespace BaekJoon._32
{
    internal class _32_05
    {

        static void Main5(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int num = int.Parse(sr.ReadLine());

            List<int>[] conns = new List<int>[num + 1];

            int len = int.Parse(sr.ReadLine());

            for (int i = 0; i < len; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int com1 = int.Parse(temp[0]);
                int com2 = int.Parse(temp[1]);

                conns[com1] = conns[com1] ?? new();
                conns[com2] = conns[com2] ?? new();

                conns[com1].Add(com2);
                conns[com2].Add(com1);
            }

            sr.Close();

            // 감염된 개수 출력
            int result = BFS(conns, num);

            // 출력
            Console.WriteLine(result);
        }

        static int BFS(List<int>[] _conns, int num) 
        {

            bool[] chk = new bool[num + 1];

            // 1번이 감염
            const int start = 1;
            chk[start] = true;

            // 1번을 통해 감염되는 개수
            int broken = 0;

            Queue<List<int>> queue = new Queue<List<int>>();

            if (_conns[start] != null) queue.Enqueue(_conns[start]);

            while(queue.Count > 0)
            {

                List<int> conn = queue.Dequeue();

                for (int i = 0; i < conn.Count; i++)
                {

                    int next = conn[i];
                    if (!chk[next])
                    {

                        chk[next] = true;
                        broken++;
                        if (_conns[next] != null) queue.Enqueue(_conns[next]);
                    }
                }
            }

            return broken;
        }
    }
}
