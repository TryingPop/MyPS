using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 4
이름 : 배성훈
내용 : 물통
    문제번호 : 2251번

    DFS, BFS 문제다
    조건을 무시해서 여러 번 틀렸다

    아이디어는 다음과 같다
    물 옮기는 경우를 계속해서 시뮬레이션 돌렸다
    앞에서 연산했던 경우면 넘긴다

    물이 200까지라서 단순 계산해서 보면 최악의 경우 800만번 돌아간다
    다차원 배열을 만들고 싶지 않아 int로 계산해서 풀었다
    그리고 해당 경우를 빠르게 기록하기 위해 HashSet을 이용했다
*/

namespace BaekJoon.etc
{
    internal class etc_0407
    {

        static void Main407(string[] args)
        {

            int DIV = 1_000;
            int[] max = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            bool[] ret = new bool[max[2] + 1];
            Queue<int> q = new Queue<int>(1_000);
            HashSet<int> isChk = new(1_000);
            ret[max[2]] = true;
            isChk.Add(max[2]);

            q.Enqueue(EnCodeWater(0, 0, max[2]));
            while(q.Count > 0)
            {

                int decode = q.Dequeue();

                for (int i = 0; i < 3; i++)
                {

                    for (int j = 0; j < 3; j++)
                    {

                        int chk = MoveWater(decode, i, j);
                        if (isChk.Contains(chk)) continue;

                        isChk.Add(chk);
                        q.Enqueue(chk);

                        RecordWater(chk);
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < ret.Length; i++)
                {

                    if (!ret[i]) continue;
                    sw.Write(i);
                    sw.Write(' ');
                }
            }

            // 물 상태 기록
            void RecordWater(int _format)
            {

                DeCodeWater(_format, out int a, out int b, out int c);
                if (a > 0) return;

                ret[c] = true;
            }
            
            // 물 이동
            int MoveWater(int _format, int g, int t)
            {

                if (g == t) return _format;
                DeCodeWater(_format, out int a, out int b, out int c);

                int go;
                int to;

                if (g == 0) go = a;
                else if (g == 1) go = b;
                else go = c;

                if (t == 0) to = a;
                else if (t == 1) to = b;
                else to = c;

                int move = Math.Min(go, max[t] - to);

                if (g == 0) a -= move;
                else if (g == 1) b -= move;
                else c -= move;

                if (t == 0) a += move;
                else if (t == 1) b += move;
                else c += move;

                return EnCodeWater(a, b, c);
            }

            // 물 경우를 숫자로 변환
            int EnCodeWater(int _a, int _b, int _c)
            {

                int ret = _a * DIV + _b;
                return ret * DIV + _c;
            }

            // 숫자를 물의 경우로 해석
            void DeCodeWater(int _format, out int _a, out int _b, out int _c)
            {

                _c = _format % DIV;
                _format /= DIV;

                _b = _format % DIV;
                _a = _format / DIV;
            }
        }
    }
#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.BaekJoon
{
    // 물통

    // 1번째 물통이 비어있는 경우, 3번째 물통에 담겨있을 수 있는 물의 양을 오름차순으로 정렬해 보이면 된다.

    internal class Problem2251Solver
    {
        static int[] Sender = { 0, 0, 1, 1, 2, 2 };
        static int[] Receiver = { 1, 2, 0, 2, 0, 1 };

        static bool[,] visited = new bool[201, 201];
        static bool[] answer = new bool[201];
        static int[] bottle = new int[3]; 

        static public void Main()
        {
            string[] inputs = Console.ReadLine().Split(' ');

            for (int i = 0; i < 3; i++)
                bottle[i] = int.Parse(inputs[i]);

            BFS();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 201; i++)
            {
                if (answer[i])
                    sb.Append($"{i} ");
            }

            Console.Write(sb.ToString());
        }

        static public void BFS()
        {
            Queue<ValueTuple<int, int>> queue = new Queue<ValueTuple<int, int>>();
            queue.Enqueue((0, 0));
            visited[0, 0] = true;
            answer[bottle[2]] = true;

            while (queue.Count > 0)
            {
                var pair_val = queue.Dequeue();
                int A = pair_val.Item1;
                int B = pair_val.Item2;
                int C = bottle[2] - A - B;

                for (int i = 0; i < 6; i++)
                {
                    int[] nxt = { A, B, C };
                    nxt[Receiver[i]] += nxt[Sender[i]];
                    nxt[Sender[i]] = 0;

                    if (nxt[Receiver[i]] > bottle[Receiver[i]])
                    {
                        nxt[Sender[i]] = nxt[Receiver[i]] - bottle[Receiver[i]];
                        nxt[Receiver[i]] = bottle[Receiver[i]];
                    }
                    if (!visited[nxt[0], nxt[1]])
                    {
                        visited[nxt[0], nxt[1]] = true;
                        queue.Enqueue((nxt[0], nxt[1]));
                    }
                    if (nxt[0] == 0)
                    {
                        answer[nxt[2]] = true;
                    }
                }
            }
        }
    }
}

#endif
}
