using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 15
이름 : 배성훈
내용 : 이진 검색 트리
    문제번호 : 5639번

    직접 트리를 몇 개 만들어보고, 조건에 맞춰 입력값과 출력값을 적어보았다
    그리고 출력값과 입력값을 비교하니, 규칙이 보였다
    규칙은 다음과 같다
        1. 입력 값이 감소하는 경우 왼쪽 노드에 넣어지기에 출력이 필요없다
        2. 증가하는 경우 오른쪽에 담기는데 이때 상황에 따라 출력이 필요하다!
            30, 24, 5, 28, 25, 26, 27, 29, 45 순서로 입력된 경우를 들어서 보자
            결과로 5, 27, 26, 25, 29, 28, 24, 45, 30이 출력되기를 희망한다

            i)  30, 24, 5 까지는 감소하므로 아무 이상없다

            ii) 30, 24, 5, 28 에서
                28보다 작은 것 중에 가장 큰 24만 남겨놓고 28보다 작은 나머지들은 반대순서로 출력해야한다
                5가 출력된다
                그래서 출력된 것을 제외하면 30, 24, 5, 28은 30, 24, 28로 된다

            iii)28, 25, 26, 27에서
                26, 27은 28보다 작으므로 출력되지 않는다!

            iv) 28, 25, 26, 27, 29에서
                29는 28보다 크므로 27, 26, 25 순서로 출력되어야 한다

            v) 30, 24, 28, 29에서
                29는 30보다 작으므로 24, 28은 출력되지 않는다

            vi) 30, 24, 28, 29, 45에서
                45는 30보다 크므로 29, 28, 24는 출력되어야 한다
            
            vii)모든 값을 순회했으므로 남아있는 것들을 역순으로 출력한다
                30, 45가 남아있다
                나머지가 반대로 출력되어야한다 즉, 45, 30가 출력되어야한다

            i) ~ vii) 순서를 보면 5, 27, 26, 25, 29, 28, 24, 45, 30 순서로 출력되는 것을 확인할 수 있다

            예제를 하나 더 추가해서 50, 30, 24, 5, 45, 48, 98, 52, 60인 경우를 보자
            결과로 5, 24, 48, 45, 30, 60, 52, 98, 50 순서로 출력되기를 원한다
            
            i)  50, 30, 24, 5까지는 감소하므로 이상없다
            
            ii) 50, 30, 24, 5, 45인 경우
                45보다 작은 수 중에 가장 큰 것은 30이고 30미만의 작은 수들은 모두 역순으로 출력되어야한다
                5, 24 순서로 출력
                50, 30, 24, 5, 45는 50, 30, 45가 된다
            
            iii)50, 30, 45, 48인 경우
                앞에서 증가했고, 48이 50보다는 작으므로 출력되는 것은 없다

            iv) 50, 30, 45, 48, 98인 경우
                98은 50보다 크고, 50 미만의 수들은 반대 순서로 모두 출력되어야 한다
                48, 45, 30 순서로 출력
                50, 30, 45, 48, 98은 50, 98이 된다
                
            v)  50, 98, 52인 경우
                작아졌으므로 출력되지 않는다

            vi) 50, 98, 52, 60인 경우
                60은 98보다 작으므로 출력되지 않는다

            vii)모든 값을 순회했으므로 
                남아있는 50, 98, 52, 60를 역순으로 출력한다
                즉, 60, 52, 98, 50 출력

            i) ~ vii) 순서를 보면 5, 24, 48, 45, 30, 60, 52, 98, 50 순서로 출력된다

            이제 이를 일반화하면
            증가하는 경우 증가하는 시작 값 a와 시작 값 a 바로 앞의 b 값을 알아야 한다
            현재의 c가 b보다 큰 경우 b앞까지 모두 반대 순서로 출력하고
            c와 b의 바로 앞 bb를 비교해서 c가 bb보다 작은 경우면 b를 출력하지 않고 끝내고,
            bb보다 큰 경우면 b를 출력하고, bb의 바로 앞 bbb와 c를 비교해서 c가 bbb보다 작은 경우면 bb를 출력하지 않고 끝내고,
            아닌 경우 bbb의 앞 bbbb와 비교하면서 앞과 같은 비교를 계속해 나간다
            만약 b의 앞이 존재하지 않는 경우면 b를 살리고 멈춘다!

    이를 코드로 나타내면 아래와 같다
    역순으로 검색하고 중복 검색을 피하기 위해 자료 구조 Stack을 사용했고,
    현재 값과 바로 앞의 큰 값을 저장해줘야해서 스택에 넣을껀 튜플을 사용했다
    속도는 880ms로 아슬아슬하게 통과했다; 

    다른 사람의 풀이를 보니 직접 노드를 만들어서 푼 사람도 있고,
    전위로 분할 정복하면 후위 탐색이 되는 아이디어를 이용한 사람도 있다

    StreamWriter를 써놓고.. 스택에서 비교하는 과정에서 Console.WriteLine을 불러와서 느렸다;
    해당 부분 수정하니 880ms -> 76ms로 통과
    시간복잡도는 O(N)이다 분할 정복으로 푼 O(N log N) 보다 엄청 느릴 수 없다;


    정석? 분할 정복 아이디어만 따로 적어본다
        후위는 왼 -> 오 -> 루 순이고
        전위는 루 -> 왼 -> 오 순이라서

    전위의 맨 앞의 원소를 후위의 맨 뒤로,
    그리고 루트보다 큰 원소 중 인덱스가 가장 낮은 애를 기준으로 좌우 분할해야한다 해당 원소는 우측에 포함시키면
    우측이 루트보다 큰 원소들의 모임이다, 좌측은 낮은 원소가 된다 즉, 분할된다
    그리고 우측의 처음이 루트가 되고 이를 루트로 채운다 
    이렇게 우측을 다하고 좌측을 하면서 채우면 후위 연산이 된다

    이를 아래 Divide 메서드로 표현한다
    시간 복잡도는 O(N log N)이다
*/              

namespace BaekJoon._36
{
    internal class _36_06
    {

        static void Main6(string[] args)
        {

            const int MAX_NODE = 10_000;
            const int MAX = 1_000_000;
            // 전위 탐색
            int[] preOrder = new int[MAX_NODE];
            int idx = 0;


            // 입력
            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            {

                // 입력 갯수가 주어지지 않았다!
                while (true)
                {

                    string chk = sr.ReadLine();

                    // 다른사람 답을 보니 다음 메서드가 있었다
                    // if (string.IsNullOrEmpty(chk)) break;
                    if (chk == null || chk == "") break;

                    preOrder[idx++] = int.Parse(chk);
                }
            }
#if first
            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                // 계산용 스택
                Stack<(int key, int back)> s = new Stack<(int key, int back)>();
                // 바로앞 큰 수
                int curBack = MAX;

                // 처음은 입력될 수 있는 모든 수보다 큰 값을 넣어버린다!
                s.Push((preOrder[0], curBack));

                for (int i = 1; i < idx; i++)
                {

                    int chk = preOrder[i];

                    if (chk < s.Peek().key)
                    {

                        // 감소하는 경우
                        // curBack의 값이 변한다 그래서 curBack 갱신!
                        curBack = s.Peek().key;
                        s.Push((chk, curBack));
                    }
                    else if (curBack > chk)
                    {

                        // 커지는데 바로 앞원소보다 작은 경우 이상없이 넣는다!
                        s.Push((chk, curBack));
                    }
                    else
                    {

                        // 바로 앞 원소보다 큰 수가 입력된 경우!
                        while(s.Peek().back < chk)
                        {

                            // 바로앞 원소보다 작아질 때까지 
                            sw.WriteLine(s.Pop().key);
                            // ... 이거 때문에 10배 이상 느려졌다!
                            // 어쩐지 스택인데... 왜 늦더라 했다;
                            // Console.WriteLine(s.Pop().key);
                        }

                        // curBack이 변화해서 Peek().back으로 대체
                        curBack = s.Peek().back;

                        s.Push((chk, curBack));
                    }
                }

                // 나머지 출력
                while(s.Count > 0)
                {

                    sw.WriteLine(s.Pop().key);
                }
            }
#else

            // 후위 탐색 결과값
            int[] result = new int[idx];
            int len = idx - 1;

            Divide(preOrder, 0, len, ref len, result);

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < idx; i++)
                {

                    sw.WriteLine(result[i]);
                }
            }
        }
#endif


#if !first
        static void Divide(int[] _preOrder, int _startIdx, int _endIdx, ref int _idx, int[] _result)
        {

            if (_endIdx < _startIdx) return;

            // 정복
            _result[_idx--] = _preOrder[_startIdx];

            // 분할
            int left = _startIdx + 1;
            int right = _endIdx;

            int root = _preOrder[_startIdx];
            
            while (left <= right)
            {

                int mid = (left + right) / 2;

                int chk = _preOrder[mid];
                
                // 중복값은 없지만, left를 root보다 큰 애들 중 가장 작은 원소로 보낼 예정이다!
                if (root >= chk)
                {

                    
                    left = mid + 1;
                }
                else
                {

                    right = mid - 1;
                }
            }


            // left는 root보다 큰 수 중 가장 작은 수가 되었다!
            // right는 root보다 작거나 같은 수 중 가장 큰 수이다!
            Divide(_preOrder, left, _endIdx, ref _idx, _result);
            Divide(_preOrder, _startIdx + 1, left - 1, ref _idx, _result);
        }
#endif
    }
}
