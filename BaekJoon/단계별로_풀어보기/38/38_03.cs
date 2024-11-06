using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 18
이름 : 배성훈
내용 : 별자리 만들기
    문제번호 : 4386번

    크루스칼 알고리즘으로 풀었다
*/

namespace BaekJoon._38
{
    internal class _38_03
    {

        static void Main3(string[] args)
        {

            PriorityQueue<(int po1, int po2, float dis), float> q = new PriorityQueue<(int po1, int po2, float dis), float>();
            int[] group = null;
            using (StreamReader sr = new StreamReader(Console.OpenStandardInput()))
            {

                int len = int.Parse(sr.ReadLine());
                float[][] pos = new float[len][];

                // 좌표 입력 받기
                for (int i = 0; i < len; i++)
                {

                    pos[i] = Array.ConvertAll(sr.ReadLine().Split(' '), float.Parse);
                }

                // 거리 연산 및 우선순위 큐에 입력
                for (int i = 0; i < len; i++)
                {

                    for (int j = 0; j < i; j++)
                    {

                        float dis = (float)Dis(pos[i][0] - pos[j][0], pos[i][1] - pos[j][1]);
                        q.Enqueue((i, j, dis), dis);
                    }
                }

                group = new int[len];
                for (int i = 1; i < len; i++)
                {

                    group[i] = i;
                }
            }


            // Kruskal 알고리즘
            Stack<int> s = new();
            float result = 0.0f;
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                int pos1 = Find(group, node.po1, s);
                int pos2 = Find(group, node.po2, s);

                if (pos1 == pos2) continue;

                result += node.dis;

                if (pos1 < pos2) group[pos2] = pos1;
                else group[pos1] = pos2;
            }

            // 문자열 보간으로 반올림한다!
            // Math.Round(result, 2);랑 같은 의미!
            Console.WriteLine($"{result:0.00}");
        }

        static int Find(int[] _group, int _chk, Stack<int> _calc)
        {

            int chk = _chk;

            while (chk != _group[chk])
            {

                _calc.Push(chk);
                chk = _group[chk];
            }

            while(_calc.Count > 0)
            {

                _group[_calc.Pop()] = chk;
            }

            return chk;
        }

        static double Dis(float _x, float _y)
        {

            float x = _x * _x;
            float y = _y * _y;

            return Math.Sqrt(x + y);
        }
    }
}
