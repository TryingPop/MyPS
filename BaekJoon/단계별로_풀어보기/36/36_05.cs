using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 15
이름 : 배성훈
내용 : 트리의 순회
    문제번호 : 2263번

    중위, 후위 탐색으로 전위 탐색 찾기!

    분할 정복으로 먼저 풀었다.
    가장 빠른 사람과 10배 차이난다!

    중위 탐색과 후위 탐색을 보면,
        후위 탐색 끝에 루트가 담긴다
        중위 탐색은 루트의 위치는 왼쪽과 오른쪽을 나눈다
    
    전위 탐색은 루트 > 왼쪽의 루트 > ... > 왼쪽의 루트 
    왼쪽의 루트가 더 이상 없다면, 이후 오른쪽의 루트가 담긴다
    그리고 다시 왼쪽의 루트가 없을 때까지 왼쪽의 루트를 탐색한다
    다시 오른쪽의 루트를 찾는다
    이하 반복된다
    
    그래서, 먼저 루트를 찾고 좌우 분할한다
    중위 탐색에서 만약 0, 1, 2, ...., i에서 루트 값이 발견되었다고 하자
    왼쪽은 루트 앞까지 갯수만 담는다
        중위 탐색과 후위 탐색 모두 왼쪽을 인덱스가 0, 1, 2, ..., i - 1 으로 나눈다
    오른쪽은 루트를 제외하고 넣는다
        중위 탐색의 경우 i + 1, i + 2, .., len - 1까지 하고,
        후위 탐색의 경우 i, i + 1, i + 2, ..., len - 2까지 넣는다

    좌측의 부분을 먼저 연산하게 한다
    그리고 우측을 연산하게 한다

    분할이 끝나면 값을 대입한다
    모든 항이 오른쪽 일렬일 경우에 경우 최악의 경우의 수가 나오고
    시간 복잡도는 O(N^2) (1만개...)이다
    경우의 수가 10만개인 여기서 시간초과 나와야 정상이다

    다른 사람의 풀이를 보니 비슷하게 나눠서 풀었다 그런데 아이디어가 조금 다르다
    내 풀이의 경우 for문을 돌면서 직접 인덱스를 찾았으나 다른 사람의 풀이는 인덱스가 모두 다르다는 점을 이용해서
    처음부터 인덱스를 담고 시작했다 그래서 for문 없이 바로 찾는다!
    for문을 dp를 이용해하니 10배 이상 속도 향상되었다!
    1708ms -> 128ms
*/

namespace BaekJoon._36
{
    internal class _36_05
    {

        static void Main5(string[] args)
        {
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = int.Parse(sr.ReadLine());
            // 중위 탐색
            int[] inOrder = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            // 후위 탐색
            int[] posterOrder = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            sr.Close();

#if !first

            /// 추가
            // 다른 사람의 아이디어를 빌려 해결 완료!
            // index에 관한 배열을 함으로써
            // 매번 for문을 돌 필요가 없어졌다!
            int[] inOrderIdxs = new int[len + 1];
            for (int i = 0; i < len; i++)
            {

                inOrderIdxs[inOrder[i]] = i;
            }
            ///

            int[] result = new int[len];
            int idx = 0;

            // Divide(inOrder, posterOrder, 0, 0, len - 1, ref idx, result);
            Divide(inOrderIdxs, posterOrder, 0, 0, len - 1, ref idx, result);

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < len; i++)
                {

                    sw.Write(result[i]);
                    sw.Write(' ');
                }
            }
#elif second

            int[] indexOfInorder = new int[len + 1];
            for (int i = 0; i < len; i++)
                indexOfInorder[inOrder[i]] = i;
            var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            var stack = new Stack<(int l, int r, int pl, int pr)>();
            stack.Push((0, len - 1, 0, len - 1));
            
            while (stack.Count > 0)
            {
                
                (int l, int r, int pl, int pr) = stack.Pop();
                
                if (l == r)
                {


                    if (l != 0 || r != len - 1) sw.Write(' ');

                    sw.Write(inOrder[r]);
                    
                    continue;
                }

                else if (l > r) continue;
                
                var parent = posterOrder[pr];
                var mid = indexOfInorder[parent];
                
                if (l != 0 || r != len - 1)
                    sw.Write(' ');
                
                sw.Write(parent);

                stack.Push((mid + 1, r, pl + mid - l, pr - 1));
                stack.Push((l, mid - 1, pl, pl + mid - l - 1));
            }
            sw.Close();
#endif
        }

#if !first
        // static void Divide(int[] _inOrder, int[] _posterOrder, int _inStart, int _posterStart, int len, ref int _idx, int[] _result)
        static void Divide(int[] _inOrderIdxs, int[] _posterOrder, int _inStart, int _posterStart, int len, ref int _idx, int[] _result)
        {

            /*
            // 수정
            if (len < 0)
            {

                return;
            }

            int chk = _posterOrder[_posterStart + len];
            for (int i = len; i >= 0; i--)
            {

                if (_inOrder[_inStart + i] == chk)
                {

                    // 정복
                    _result[_idx++] = chk;

                    // 분할
                    Divide(_inOrder, _posterOrder, _inStart, _posterStart, i - 1, ref _idx, _result);
                    Divide(_inOrder, _posterOrder, _inStart + i + 1, _posterStart + i, len - i - 1, ref _idx, _result);
                    break;
                }
            }
            */

            if (len < 0) return;

            // ... 다른 사람의 아이디어를 빌려 수정 완료!
            int chk = _posterOrder[_posterStart + len];
            _result[_idx++] = chk;

            int idx = _inOrderIdxs[chk] - _inStart;
            
            Divide(_inOrderIdxs, _posterOrder, _inStart, _posterStart, idx - 1, ref _idx, _result);
            Divide(_inOrderIdxs, _posterOrder, _inStart + idx + 1, _posterStart + idx, len - idx - 1, ref _idx, _result);
        }
#endif
    }
}
