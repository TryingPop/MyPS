using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 17
이름 : 배성훈
내용 : 친구 네트워크
    문제번호 : 4195번

    입력 받을 자료 구조 선정이 중요한 문제인거 같다
    먼저 A와 B가 친구라는 것은 B -> A 친구, A -> B 친구라고 볼 수 있어서 유니온 파인드 알고리즘을 쓸 수 있다

    먼저 문제 수를 입력받고,
    이후 문제마다
            문제에 입력되는 연결 수,
            연결 수 만큼
                이름 1, 이름 2
            를 입력받는다

    조건을 보면 입력 받은 이름이 다시 나올 수 있으므로 이름들을 메모리에 보관해야한다
    또한 이름으로 어떤 그룹에 속했는지 알아야하고, 이름으로 몇 명이 속했는지도 알 수 있어야한다
    그래서 이걸로 만든다면, 이름으로 그룹을 검색할 수 있어야하고, 그룹에는 그룹에 몇명이 있는지 정보를 포함해야한다
    단순하게 그룹을 이름들의 리스트라 하면 해당 조건을 바로 만족할 수 있다

    그런데 그룹마다 얼마나 들어올지도 몰라서 매번 리스트들을 갱신하는 것처럼 에러사항이 많이 있어 보여 중간에 그룹명을 확인하는 다리를 놓았다
    이름으로 -> 그룹명을 확인하고, 그룹명으로 -> 유니온 파인드 알고리즘을 하고, 유니언 파인드 알고리즘에 쓰는 보드에는 인원수도 저장되게 변수를 추가했다

    이름으로 그룹명을 확인하는 것은 해시를 써서 빠르게 찾아주는 Dictionary 자료구조를 이용했고,
    보드는 그룹명과 멤버 수를 보관하는 튜플로 배열을 만들었다

    그리고 앞과 같이 문제를 풀었다.
    차트를 갱신하는 부등식(j < len 이어야 하는데 j <= len으로 해서)을 잘못넣어 한번 인덱스 에러가 떴다 (10만 경우의 수를 확인하는 케이스가 있다!)
    이후 무난하게 통과했다 시간은 176ms로 통과했다
*/

namespace BaekJoon._37
{
    internal class _37_03
    {

        static void Main3(string[] args)
        {

            const int MAX_GROUP = 100_000;

            // 유니온 파인드 알고리즘을 저장할 배열
            (int group, int mem)[] board = new (int group, int mem)[MAX_GROUP];

            // 이름을 그룹 번호로 변경해주기 위해 Dictionary 자료구조 사용
            Dictionary<string, int> nameToGroup = new Dictionary<string, int>(MAX_GROUP * 2);

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int cases = int.Parse(sr.ReadLine());
            // 재귀 함수 호출하기 싫어서 재활용 스택!
            Stack<int> s = new Stack<int>();
            for (int i = 0; i < cases; i++)
            {

                int len = int.Parse(sr.ReadLine());

                // 먼저 쓸 만큼만 차트 갱신
                for (int j = 0; j < len; j++)
                {

                    // j는 그룹이름과 초기 멤버 수 
                    board[j] = (j, 2);
                }

                for (int j = 0; j < len; j++)
                {

                    string[] temp = sr.ReadLine().Split();

                    // 그룹 번호
                    // front, back;
                    int f = j;
                    int b = j;

                    // 이미 이름이 존재하는 경우
                    if (nameToGroup.ContainsKey(temp[0]))
                    {

                        // 해당 그룹번호를 가져온다
                        f = Find(board, nameToGroup[temp[0]], s);
                        // f != j이므로 j의 멤버는 1명 줄어든다!
                        board[j].mem--;
                    }
                    // 없는 경우면 해당 이름을 그룹으로 가게 한다!
                    else nameToGroup.Add(temp[0], j);

                    // 마찬가지!
                    if (nameToGroup.ContainsKey(temp[1]))
                    {

                        b = Find(board, nameToGroup[temp[1]], s);
                        board[j].mem--;
                    }
                    else nameToGroup.Add(temp[1], j);

                    // 그룹이 다르면 연결되었으므로 합쳐져야한다!
                    if (f != b) Union(board, f, b);

                    sw.WriteLine(board[f].mem);
                }

                // 이름 초기화!
                nameToGroup.Clear();
                // 출력 버퍼를 중간에 비운다! 
                // 즉 입력된 것들 출력!
                sw.Flush();
            }

            sr.Close();
            sw.Close();
        }

        static void Union((int group, int mem)[] _board, int _groupA, int _groupB)
        {

            // groupA, groupB는 파인드로 찾은 그룹 번호이다!
            // 두 그룹이 합쳐졌고 A쪽으로 이어줄 것이므로 A의 멤버에 B 멤버 추가!
            _board[_groupA].mem += _board[_groupB].mem;
            // 그리고 B멤버는 A 멤버를 향하게 한다
            _board[_groupB].group = _groupA;
        }

        static int Find((int group, int mem)[] _board, int _chk, Stack<int> _calc)
        {

            // 기존 파인드와 같다
            int chk = _chk;

            // 루트? 까지 검색
            while(chk != _board[chk].group)
            {

                _calc.Push(chk);
                chk = _board[chk].group;
            }

            // 같은 부모를 향하게 한다!
            while(_calc.Count > 0)
            {

                _board[_calc.Pop()].group = chk;
            }

            return chk;
        }
    }
}
