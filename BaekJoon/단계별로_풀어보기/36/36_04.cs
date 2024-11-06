using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 15
이름 : 배성훈
내용 : 트리 순회
    문제번호 : 1991번

    전위, 중위, 후위 순회
    전위는 루트 -> 왼쪽자식 -> 오른쪽자식 순서로 출력한다
    
                        1
                2               3
            4       5       6       7

    1에서 전위 탐색이라 하면
        먼저 루트 1을 탐색 한다
                1
        루트 탐색 했으니 왼쪽 자식 2을 탐색한다     
                1 -> 2
        2에도 왼쪽 자식이 있으므로 4를 탐색한다     
                1 -> 2 -> 4
        4에 자식이 없으므로 부모 2로 가고 2의 오른쪽 탐색
                1 -> 2 -> 4 -> 5
        5에 자식이 없으므로 부모 2로 가고 2의 탐색이 끝났으므로 부모 1로 간다 그리고 오른쪽 3 탐색 시작
                1 -> 2 -> 4 -> 5 -> 3
        3에 왼쪽 자식이 있으므로 6으로 가고
                1 -> 2 -> 4 -> 5 -> 3 -> 6
        6에 자식이 없으므로 부모 3으로 가고 오른쪽 7을 탐색
                1 -> 2 -> 4 -> 5 -> 3 -> 6 -> 7
        더 이상 탐색할게 없으므로 종료

    중위는 왼쪽 -> 루트 -> 오른쪽 순서 탐색이다

                        1
                2               3
            4       5       6       7
    1에서 중위 탐색이라 하면
        왼쪽 순서대로 탐색이라 보면 된다
        왼쪽 순으로 나열하면
            4 -> 2 -> 5 -> 1 -> 6 -> 3 -> 7
    
    후위 탐색은 왼쪽 -> 오른쪽 -> 루트 순서

                        1
                2               3
            4       5       6       7
    1에서 후위 탐색을 하면
        먼저 맨 왼쪽에 있는 4에서 탐색을 시작한다
            4
        4에 자식이 없으므로 부모 2로 가고 오른쪽 자식 5을 조사한다 5에 자식이 없으므로
            4 -> 5
        2의 모든 자식이 탐색완료되었고, 2로 간다
            4 -> 5 -> 2
        루트의 왼쪽 탐색이 끝났다
        이제 오른쪽 3의 노드에서 가장 왼쪽에 있는 원소 6을 탐색하러 간다
            4 -> 5 -> 2 -> 6
        6에 자식이 없으므로 부모 3으로 가고 3의 오른쪽 자식 7로 간다 7에 자식이 없으므로
            4 -> 5 -> 2 -> 6 -> 7
        7의 탐색이 끝나고 부모 3으로 간다
            4 -> 5 -> 2 -> 6 -> 7 -> 3
        오른쪽 탐색도 끝났으므로 이제 루트로 간다
            4 -> 5 -> 2 -> 6 -> 7 -> 3 -> 1

    이를 코드로 짜는 것이다
    이렇게 예시로보면 전위, 중위, 후위 관계에 대해 안와닿았다
    DFS로 왼쪽 탐색 -> 오른쪽 탐색되게 작성하고
    이제 전위 기록을 왼쪽 탐색 전에 즉,
        전위 기록 -> 왼쪽 탐색 -> 오른쪽 탐색
    순으로 하면 전위 탐색이 되고,

    중위 탐색은 
        왼쪽 탐색 -> 중위 기록 -> 오른쪽 탐색
    순으로 하니 중위 탐색이 되고,

    후위는 기록의 위치에 따라 전위, 중위, 후위로 맞춘게 아닌가하는 추론으로
        왼쪽 탐색 -> 오른쪽 탐색 -> 후위 기록
    순서로 하니 위 규칙에 맞춰 나왔다

    그리고 제출하니 정답처리 되었다
*/

namespace BaekJoon._36
{
    internal class _36_04
    {

        static void Main4(string[] args)
        {

            int len = int.Parse(Console.ReadLine());

            // 해당 방법이 아닌 Left Right로 저장해야할듯?
            (int parent, int left, int right)[] root = new (int parent, int left, int right)[len];

            for (int i = 0; i < len; i++)
            {

                root[i] = (-1, -1, -1);
            }

            for (int i = 0; i < len; i++)
            {

                char[] temp = Array.ConvertAll(Console.ReadLine().Split(' '), char.Parse);

                int p = temp[0] - 'A';
                for (int j = 1; j < 3; j++)
                {

                    int chk = temp[j] - 'A';
                    if (chk < 0) continue;

                    root[chk].parent = p;

                    if (j == 1) root[p].left = chk;
                    else root[p].right = chk;
                }
            }

            // string으로 다음과 같이 선언해도 상관없다!
            // string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            StringBuilder fst = new StringBuilder(len);
            StringBuilder sec = new StringBuilder(len);
            StringBuilder trd = new StringBuilder(len);
            DFS(root, 0, alphabet, fst, sec, trd);

            Console.WriteLine(fst);
            Console.WriteLine(sec);
            Console.WriteLine(trd);
        }

        static void DFS((int parent, int left, int right)[] _root, int _start, char[] _alphabet, StringBuilder _first, StringBuilder _second, StringBuilder _third)
        {

            _first.Append(_alphabet[_start]);

            int chk = _root[_start].left;
            if (chk > -1) DFS(_root, chk, _alphabet, _first, _second, _third);

            _second.Append(_alphabet[_start]);

            chk = _root[_start].right;
            if (chk > -1) DFS(_root, chk, _alphabet, _first, _second, _third);

            _third.Append(_alphabet[_start]);
        }
    }
}
