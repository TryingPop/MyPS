using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 27
이름 : 배성훈
내용 : 줄 세우기
    문제번호 : 2252번

    앞선 순서 대로 출력하라는 말인데 힌트로 위상정렬이 있다
    그래서 위상정렬을 찾아보고 위상정렬의 BFS 아이디어로 풀었다

    먼저 뒤에 있는 애들은 앞에 몇명이 있는지 값을 넣는다(degree 배열)
    그리고, 앞에 없는 인원 degree가 0인 애들을 모두 찾아서 큐에 넣는다

    그리고 q에 있는 애들을 하나씩 꺼내서 앞에 세운다 그리고 뒤에 있는 애들의 degree 카운트를 줄인다
    줄이는데 degree 카운트가 0이되면 q에 넣는다

    그러면 주어진 line의 조건에 맞는 줄이 하나 나온다!
*/

namespace BaekJoon._43
{
    internal class _43_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[] degree = new int[info[0] + 1];

            List<int>[] lines = new List<int>[info[0] + 1];
            for (int i = 1; i < lines.Length; i++)
            {

                lines[i] = new();
            }

            for (int i = 0; i < info[1]; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int back = int.Parse(temp[1]);
                lines[int.Parse(temp[0])].Add(back);
                degree[back]++;
            }

            sr.Close();

            Queue<int> q = new Queue<int>();
            for (int i = 1; i < info[0] + 1; i++)
            {

                if (degree[i] == 0) q.Enqueue(i);
            }

            Queue<int> result = new Queue<int>(info[0]);

            while (q.Count > 0)
            {

                var node = q.Dequeue();
                result.Enqueue(node);

                for (int i = 0; i < lines[node].Count; i++)
                {

                    int idx = lines[node][i];
                    degree[idx]--;
                    if (degree[idx] == 0)
                    {

                        q.Enqueue(idx);
                    }
                }
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            while(result.Count > 0)
            {

                sw.Write(result.Dequeue());
                sw.Write(' ');
            }

            sw.Close();
        }
    }
}
