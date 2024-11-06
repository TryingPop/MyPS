using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 19
이름 : 배성훈
내용 : 전력난
    문제번호 : 6497번

    왜 0 0 이 있는지 의문을 가졌다
    ... 여러 케이스로 이루어진 문제이다!

    25%에서 두 문제 이상이 있어 틀렸다

    그리고 모든 거리의 합은 2^31보다 작다라고 명시되어 있어
    출력용으로는 int로 설정했고 여기서는 최대 절약 금액이 얼마인지 묻는거라 빼주는 형식으로 했다

    최대 절약 금액 = 전체 거리 - 최단 거리가 된다;

    매번 빼기 연산을 하는 거 보다 작은 수에 덧셈 연산을 하다가
    마지막에 뺄 셈을 하니 484ms -> 468ms로 단축되었다
*/

namespace BaekJoon._38
{
    internal class _38_05
    {

        static void Main5(string[] args)
        {


            int MAX = 200_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            Stack<int> s = new();
            int[] groups = new int[MAX];
            PriorityQueue<(int pos1, int pos2, int dis), int> q = new PriorityQueue<(int pos1, int pos2, int dis), int>(MAX);
            while (true)
            {

                int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);


                if (info[0] == info[1] && info[0] == 0) break;


                // 길 입력 받기
                for (int i = 0; i < info[1]; i++)
                {

                    int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                    q.Enqueue((temp[0], temp[1], temp[2]), temp[2]);
                }

                for (int i = 0; i < info[0]; i++)
                {

                    groups[i] = i;
                }


                int result = 0;
                while (q.Count > 0)
                {

                    var node = q.Dequeue();

                    int chk1 = Find(groups, node.pos1, s);
                    int chk2 = Find(groups, node.pos2, s);

                    if (chk1 == chk2) 
                    {

                        result += node.dis;
                        continue; 
                    }


                    groups[chk1] = chk2;
                }

                sw.WriteLine(result);
            }

            sw.Close();
            sr.Close();
        }

        static int Find(int[] _groups, int _chk, Stack<int> _calc)
        {

            while(_chk != _groups[_chk])
            {

                _calc.Push(_chk);
                _chk = _groups[_chk];
            }

            while(_calc.Count > 0)
            {

                _groups[_calc.Pop()] = _chk;
            }

            return _chk;
        }
    }
}
