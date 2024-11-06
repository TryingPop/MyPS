using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 3
이름 : 배성훈
내용 : TV Show Game
    문제번호 : 16367번

    2 - SAT 문제처럼 해결했다
    3개 중 적어도 2개가 맞아야한다
    그래서 1개가 틀리다면 다른 2개는 반드시 맞아야한다!

    이는 2 - SAT문제의 2개 중 적어도 1개가 맞아야한다는 걸 감안하면
    간선을 두 개로 해서 생성했다

    그리고 코사라주 알고리즘을 돌고 2 - SAT와 같은 검증을 했다
    풀면서 아직 2 - SAT에 대한 이해가 부족해서 이게 맞나 의문이 들었고, 
    2 - SAT에 관련된 자료들을 찾아봐야겠다

    먼저 방문한 것을 false로 이후에 true로 하면, 참 -> 거짓으로 방문하는 경우가 없어진다고 한다
    그래서 위상정렬 뒤에 것을 true로 하는 것 같다
*/

namespace BaekJoon._45
{
    internal class _45_08
    {

        static void Main8(string[] args)
        {

            string R = "R";
            string B = "B";
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            // 빨강을 앞에, 파랑을 뒤에
            // 예를들어 전체 노드는 2
            // 빨강 1번은 1
            // 빨강 2번은 2
            // 파랑 1번은 1 + 2 = 3
            // 파랑 2번은 2 + 2 = 4
            int MAX = info[0] * 2;
            List<int>[] lines = new List<int>[2 * info[0] + 1];
            List<int>[] reverseLines = new List<int>[2 * info[0] + 1];

            for (int i = 1; i <= MAX; i++)
            {

                lines[i] = new();
                reverseLines[i] = new();
            }

            for (int i = 0; i < info[1]; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int a = int.Parse(temp[0]);
                if (temp[1] == B) a += info[0];

                int b = int.Parse(temp[2]);
                if (temp[3] == B) b += info[0];

                int c = int.Parse(temp[4]);
                if (temp[5] == B) c += info[0];

                int notA = Not(a, info[0]);
                int notB = Not(b, info[0]);
                int notC = Not(c, info[0]);

                // A 가 거짓이면 B, C가 참이 되어야 한다
                lines[notA].Add(b);
                lines[notA].Add(c);

                // 마찬가지 B가 거짓이면 A, C가 참이 되어야한다
                lines[notB].Add(a);
                lines[notB].Add(c);

                // C가 거짓이면 A, B가 참이 되어야한다!
                lines[notC].Add(a);
                lines[notC].Add(b);

                reverseLines[a].Add(notB);
                reverseLines[a].Add(notC);

                reverseLines[b].Add(notA);
                reverseLines[b].Add(notC);

                reverseLines[c].Add(notA);
                reverseLines[c].Add(notB);
            }

            bool[] visit = new bool[MAX + 1];
            Stack<int> s = new Stack<int>(MAX);

            for (int i = 1; i <= MAX; i++)
            {

                if (visit[i]) continue;
                BeforeDFS(lines, visit, i, s);
            }

            int[] groups = new int[MAX + 1];
            int groupId = 0;
            while (s.Count > 0)
            {

                int node = s.Pop();

                if (!visit[node]) continue;

                groupId++;
                AfterDFS(reverseLines, visit, node, groups, ref groupId);
            }

            // 2 - SAT 처럼 이제 검사
            bool failed = false;
            bool[] solve = new bool[info[0] + 1];
            for (int i = 1; i <= info[0]; i++)
            {

                int f = groups[i];
                int b = groups[Not(i, info[0])];
                if (f == b)
                {

                    failed = true;
                    break;
                }

                if (f > b) solve[i] = true;
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                if (failed) sw.Write(-1);
                else
                {

                    for (int i = 1; i <= info[0]; i++)
                    {

                        if (solve[i]) sw.Write(R);
                        else sw.Write(B);
                    }
                }
            }

            
        }

        static int Not(int _n, int _add)
        {

            if (_n > _add) _n -= _add;
            else _n += _add;

            return _n;
        }

        static void BeforeDFS(List<int>[] _lines, bool[] _visit, int _cur, Stack<int> _calc)
        {

            _visit[_cur] = true;

            for (int i = 0; i < _lines[_cur].Count; i++)
            {

                int next = _lines[_cur][i];
                if (_visit[next]) continue;

                BeforeDFS(_lines, _visit, next, _calc);
            }

            _calc.Push(_cur);
        }

        static void AfterDFS(List<int>[] _reverseLines, bool[] _visit, int _cur, int[] _groups, ref int _groupId)
        {

            _visit[_cur] = false;
            _groups[_cur] = _groupId;

            for (int i = 0; i < _reverseLines[_cur].Count; i++)
            {

                int next = _reverseLines[_cur][i];
                if (!_visit[next]) continue;

                AfterDFS(_reverseLines, _visit, next, _groups, ref _groupId);
            }
        }
    }
}
