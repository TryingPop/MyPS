using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 11
이름 : 배성훈
내용 : DSLR
    문제번호 : 9019번

    비주얼 스튜디오에서는 
    DSLR_C 함수를 DSLR로 명명해도 컴파일 에러가 안뜨나
    백준은 컴파일 에러가 뜬다

    해당 풀이는 솔직히 좋은 풀이가 절대 아니다!
    string 합연산을 많이 쓸 뿐더러 출력도 모아서 하는게 아닌 바로바로 해서 개선될 부분이 많다
    해당 문제는 시간 제한이 6초라 역추적 없이 그냥 읽기로 해도 풀렸다;

    실제 걸린 시간은 4초이다

    그런데, 다른 사람 풀이를 보니 더 좋은게 보인다
    새로운 자료구조를 만드는데

    struct Node
    {

        public char alphabet;
        public int before;
    }

    before로 이전 idx를 찾아간다
    alphabet은 오는데 쓴다

    이를 이용해 스트링 연산을 줄였다!
    ... 다시 보니 다익스트라에서 자주 쓰던 방법이었다; ...
    튜플을 단지 구조체로 새로 정의해서 썼을뿐; 큰차이는 없었다...

    그리고 Queue, Stack이 아닌 Deque를 활용하면 재활용 할 수 있어서 더 좋아보인다
*/

namespace BaekJoon._35
{
    internal class _35_06
    {
        
        // 0 이상 10000미만 !
        const int MAX = 10_000;
        const int MAX_2 = MAX / 10;


        static void Main6(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

#if first
            string[] dp = new string[MAX];
            Queue<int> q = new Queue<int>();

            // 문제 풀기!
            for (int i = 0; i < len; i++)
            {

                // null로 배열 초기화!
                if (i != 0) Array.Fill(dp, null);

                int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                // 탐색 시작
                q.Enqueue(info[0]);
                dp[info[0]] = "";

                while (q.Count > 0)
                {

                    // 탈출
                    if (dp[info[1]] != null)
                    {

                        q.Clear();
                        break;
                    }

                    int node = q.Dequeue();
                    string cur = dp[node];

                    for (int j = 0; j < 4; j++)
                    {

                        int next = DSLR(node, j);

                        if (dp[next] != null) continue;

                        dp[next] = cur + DSLR_C(j);
                        q.Enqueue(next);
                    }
                }
                Console.WriteLine(dp[info[1]]);
            }
#else 

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            Node[] dp = new Node[MAX];
            Queue<int> q = new Queue<int>();
            Stack<char> s = new Stack<char>();

            for (int i = 0; i < len; i++)
            {

                int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                for (int j = 0; j < MAX; j++)
                {

                    dp[j].before = -1;
                }

                q.Enqueue(info[0]);
                // 끝문자
                dp[info[0]].alphabet = '\0';
                // -2는 시작이라는 의미
                dp[info[0]].before = -2;
                
                while(q.Count > 0)
                {

                    if (dp[info[1]].before != -1)
                    {

                        q.Clear();
                        break;
                    }

                    int node = q.Dequeue();

                    for(int j = 0; j < 4; j++)
                    {

                        int next = DSLR(node, j);

                        // 값 대입 된 경우
                        if (dp[next].before != -1) continue;

                        dp[next].before = node;
                        dp[next].alphabet = DSLR_C(j);

                        q.Enqueue(next);
                    }
                }

                // 추적
                int chk = info[1];
                s.Push('\n');
                while (dp[chk].before != -2)
                {

                    s.Push(dp[chk].alphabet);
                    chk = dp[chk].before;
                }

                // 출력
                while(s.Count > 0)
                {

                    sw.Write(s.Pop());
                }
            }

            sw.Close();
#endif
            sr.Close();

        }

#if !first



        public struct Node
        {

            // 이전 주소
            public int before;

            // 오는 방법
            public char alphabet;
        }

#endif
        static char DSLR_C(int _idx)
        {

            switch (_idx)
            {

                case 0:
                    return 'D';

                case 1:
                    return 'S';

                case 2:
                    return 'L';

                default:
                    return 'R';
            }
        }

        static int DSLR(int _chk, int _idx)
        {

            switch (_idx)
            {

                case 0:
                    return D(_chk);

                case 1:
                    return S(_chk);

                case 2:
                    return L(_chk);

                default:
                    return R(_chk);
            }
        }

        static int D(int _chk)
        {

            int result = _chk * 2;
            result = result < MAX ? result : result - MAX;
            return result;
        }

        static int S(int _chk)
        {

            int result = _chk - 1;
            if (result == -1) result = MAX - 1;
            return result;
        }

        static int L(int _chk)
        {

            if (_chk == 0) return 0;

            int result = 10 * _chk;
            if (result < MAX) return result;

            int calc = result / MAX;
            result = result % MAX + calc;
            return result;
        }

        static int R(int _chk)
        {

            int calc = _chk % 10;
            int result = _chk / 10 + MAX_2 * calc;
            return result;
        }
    }
}
