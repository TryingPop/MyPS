using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 25
이름 : 배성훈
내용 : 오아시스 재결합
    문제번호 : 3015번

    먼저 삼중 포문으로 만들어 풀어보자!
    O(Nlog N)으로 줄여야한다
    
    /// 주석이 처음 푼 방법이다
    그런데 중복되는 경우 O(N^2)이라
    마땅히 떠오르는 방법이 없었다

    그래서 다른 사람 풀이를 검색하게 되었고, 
    확인한 결과 stack에 2차원을 넣어주면 된다는 글을 보고 이차원 배열로 만들어 풀었다
    
    그래도 오답이 나왔는데, 
    
    입력값이 500,000개를 간과해서 생긴 문제였다
    정답이 될 수 있는 최대 값은 500,000 C 2 > 2,500,000,000 > 2^31
    이므로 int 자료형을 사용하면 오버플로우가 난다!
*/

namespace BaekJoon._31
{
    internal class _31_05
    {

        static void Main5(string[] args)
        {

            int[] heights;
            int len;
            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            {

                len = int.Parse(sr.ReadLine());

                heights = new int[len]; 
                for (int i = 0; i < len; i++)
                {

                    heights[i] = int.Parse(sr.ReadLine());
                }
            }

            // stack에 담는건 길이, 같은 길이의 인원 수
            // /// 되어 있는 코드에 의해 이차원 식이 나왔다
            Stack<int[]> stack = new Stack<int[]>();

            /// 앞서 계산용
            /// Stack<int> stack = new Stack<int>();


            // 시작점은 먼저 넣어준다
            stack.Push(new int[2] { heights[0], 1});

            /// 처음 왼쪽 값 저장
            /// stack.Push(heights[0]);

            long result = 0;

            // 뒤에 있는 사람 기준으로 센다
            // 가능한 앞의 점을 세면 된다
            for (int i = 1; i < len; i++)
            {


                if (stack.Peek()[0] > heights[i])
                /// if (stackk.Peek() > heights[i])
                {

                    // 감소하는 경우
                    // 인접한 경우만 성립 가능하므로 인접한 경우의 수 추가
                    result++;
                    // 그리고 순 감소하므로 해당 값을 갖는 숫자는 1개이다
                    stack.Push(new int[2] { heights[i], 1 });

                    /// 순 감소하는 경우 해당 값을 넣는다
                    /// stack.Push(heights[i]);
                }
                else
                {

                    // i를 오른쪽 기준 삼아 계산 시작
                    // 인접한 항을 추가한다
                    int[] temp = stack.Pop();

                    /// 인접한 항을 꺼낸다
                    /// int temp = stack.Pop();


                    // arr = 3 3 3
                    // idx = 0 1 2
                    // 인 경우
                    // idx = 1에서 여기에 오고 이 경우 idx = 0만 가능하기에 temp[1]은 1이다
                    // idx = 2에서도 여기에 오고 이 경우 idx = 0, 1모두 3이므로 2개 경우가 추가된다고 보면 된다
                    result += temp[1];

                    /// 인접한 경우이므로 경우의 수 1개 추가
                    /// result++;

                    /// 중복값 대비용 스택
                    /// Stack<int> calc = new Stack<int>();

                    // 이외에도 원소가 있는 경우
                    while (stack.Count > 0)
                    {
                        
                        // 여기서 부터는 stack.Peek()의 값이 heights보다 크다고 할 수 없어 확인해야한다
                        if (stack.Peek()[0] <= heights[i])
                        {
                            
                            // '_/', 'v', '-v' 형태들이 여기에 해당된다
                            temp = stack.Pop();
                            result += temp[1];
                        }
                        else 
                        {

                            // 4, 3, 1, 2라 가정하면
                            // 3 ~ 2는 가능하나
                            // 3 > 2가 4 ~ 2 사이에 있어 는 될 수 없다
                            // 즉 3 ~ 2 일 때 탈출한다고 보면 된다
                            result++;
                            break; 
                        }

                        /// 4 3 2 2 1 0 2를 입력 받아 왔다고 가정하면
                        /// 0 ~ 2의 경우는 시작에서 걸러냈다
                        /// 1 ~ 2, 2 ~ 2, 2 ~ 2, 3 ~ 2를 찾는 과정이다
                        /// 
                        /// if (stack.Peek() < heights[i])
                        /// {
                        ///     
                        ///     /// 작은 경우면 꺼낸다
                        ///     /// '_/', 'v' 의 형태들이 여기서 검출된다
                        ///     /// 1 ~ 2가 포함된다
                        ///     stack.Pop();
                        ///     result++;
                        /// }
                        /// else if (stack.Peek() == heights[i])
                        /// {
                        /// 
                        ///     /// '-v'의 형태들이 여기서 검출된다
                        ///     /// 2 ~ 2, 2 ~ 2가 포함된다
                        ///     result++;
                        ///     /// 같으면 중복 값이므로 임시 보관해야한다
                        ///     calc.Push(stack.Pop());
                        /// }
                        /// else 
                        /// {
                        ///     
                        ///     /// 만들 수 잇는 마지막 값이다
                        ///     /// 3 ~ 2가 된다
                        ///     /// 4 ~ 2는 4 바로 뒤에 3 > 2있어 성립하지 않는다
                        ///     result++;
                        ///     break;
                        /// }
                    }


                    
                    if (temp[0] == heights[i])
                    {

                        // 만약 같은 값이면 다음 연산에도 사용되므로 넣어줘야한다
                        // 앞에서 뺏으므로 경우의 수를 추가해서 다시 넣는다
                        temp[1]++;
                        stack.Push(temp);
                    }
                    // 없는 경우의 수면 새로 만들어 넣는다
                    else stack.Push(new int[2] { heights[i], 1 });


                    /// 중복되는 값이 존재하는 경우
                    /// 여기서 값이 모두 같은 경우 N^2이 되어 쓸 수 없는 방법이다!
                    /// while(calc.Count > 0)
                    /// {
                    ///     
                    ///     // 중복값은 다음에도 활용되기에 다시 넣어줘야한다
                    ///     stack.Push(calc.Pop());
                    /// }

                    /// 해당 값을 넣어준다
                    /// stack.Push(heights[i]);
                }
            }

            
            /*
            // Wrong!

            for (int term = 1; term < heights.Length - 1; term++)
            {

                for (int start = 0; start + term < heights.Length; start++)
                {

                    int end = start + term;

                    result++;
                    for (int chk = 1; chk < term; chk++)
                    {

                        if (heights[start + chk] > heights[start]
                            || heights[start + chk] > heights[end])
                        {

                            result--;
                            break;
                        }

                    }
                }
            }
            */

            Console.WriteLine(result);
        }
    }
}
