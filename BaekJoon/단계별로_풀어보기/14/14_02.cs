using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 10
이름 : 배성훈
내용 : 신나는 함수 실행
    문제번호 : 9184번

    재귀함수를 메인에서 표현
    함수를 이용한 것과 비교하면 풀이시간이 5배이상 느리다

    풀이 방법이 많이 잘못되었다
*/

namespace BaekJoon._14
{
    internal class _14_02
    {

        /*
        /// <summary>
        /// 해당 함수를 동적 프로그래밍 하기!
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int w(int a, int b, int c)
        {

            int result = 0;

            if (chk[a, b, c])
            {

                result = solves[a, b, c];
            }
            else if (a <= 0 || b <= 0 || c <= 0)
            {

                result = 1;
            }
            else if (a > 20 || b > 20 || c > 20)
            {

                result = w(20, 20, 20);
            }
            else if (a < b && b < c)
            {

                result = w(a, b, c - 1) + w(a, b - 1, c - 1) - w(a, b - 1, c);
            }
            else
            {

                result = w(a - 1, b, c) + w(a - 1, b - 1, c) + w(a - 1, b, c - 1) - w(a - 1, b - 1, c - 1);
            }

            solves[a, b, c] = result;
            chk[a, b, c] = true;
            return result;
        }

        public static int[,,] solves = new int[101, 101, 101];
        public static bool[,,] chk = new bool[101, 101, 101];

        static void Main(string[] args)
        {


            while (true)
            {
                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), item => int.Parse(item));

                if (inputs[0] == -1 && inputs[1] == -1 && inputs[2] == -1)
                {

                    break;
                }

                Console.WriteLine(string.Format("w({0}, {1}, {2}) = {3}", inputs[0], inputs[1], inputs[2], w(inputs[0], inputs[1], inputs[2])));
            }
        }
        */

        static void Main2(string[] args)
        {

            int[,,] solves = new int[21, 21, 21];
            bool[,,] chkSol = new bool[21, 21, 21];

            int[] target = new int[3] { 0, 0, 0 };      // 정답에서 기존 입력 값을 요구하기에 값을 변경해 계산
            int[] calc = new int[3] { 0, 0, 0 };        // 계산용


            while (true)
            {

                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), item => int.Parse(item));
                int result;

                if (inputs[0] == -1 && inputs[1] == -1 && inputs[2] == -1)
                {

                    break;
                }


                // 얕은 복사가 될 수 있으나 숫자 자료형 구조는 값을 복사하므로 주소 복사 걱정 X
                target[0] = inputs[0];
                target[1] = inputs[1];
                target[2] = inputs[2];

                // 입력값 수정
                
                if (inputs.Min() <= 0)
                {

                    if (inputs[0] < 0)
                    {

                        target[0] = 0;
                    }
                    else if (inputs[0] > 20)
                    {

                        target[0] = 20;
                    }

                    if (inputs[1] < 0)
                    {

                        target[1] = 0;
                    }
                    else if (inputs[1] > 20)
                    {

                        target[1] = 20;
                    }

                    if (inputs[2] < 0)
                    {

                        target[2] = 0;
                    }
                    else if (inputs[2] > 20)
                    {

                        target[2] = 20;
                    }
                }
                else if (inputs[0] > 20 || inputs[1] > 20 || inputs[2] > 20)
                {

                    // 재귀함수 w의 두 번째 조건을 여기서 해결
                    target[0] = 20;
                    target[1] = 20;
                    target[2] = 20;
                }

                // target.Clone()이 설명에서 얕은 복사라기에 깊은 복사 연습겸 Array.Copy이용 < 성능에 도움 일절 안된다
                Array.Copy(target, calc, target.Length);

                // 뒤에 실행하는게 먼저 값이 나오는게 재귀이므로 스택 자료구조 이용
                Stack<int[]> beforeCalc = new Stack<int[]>();

                while(true)
                {

                    // 메모장에 저장되어져 있는 경우 먼저 검사
                    // 검사를 처음해야한다 안할경우 값이 존재하는데 다른 방안으로 갈 수 있기 때문이다
                    if (chkSol[calc[0], calc[1], calc[2]])
                    {

                        if (beforeCalc.Count > 0)
                        {

                            // 재귀 해야하는 경우이므로 앞전 값으로 변경
                            calc = beforeCalc.Pop();
                        }
                        else
                        {

                            // 재귀가 없다 즉, target의 값과 calc의 값이 같다
                            // 또한 타겟의 값이 메모장에 있으니 사용
                            result = solves[calc[0], calc[1], calc[2]];
                            break;
                        }
                    }

                    
                    if (calc[0] == 0 || calc[1] == 0 || calc[2] == 0)
                    {

                        // 재귀함수 w의 첫 번째 조건
                        solves[calc[0], calc[1], calc[2]] = 1;
                        chkSol[calc[0], calc[1], calc[2]] = true;
                    }
                    // 재귀함수 w의 두 번째 조건은 앞에서 메모장의 크기를 줄이는 것과 동시에 해결했다
                    // 해결이 안되었을 경우 여기 사이에 조건이 와야한다
                    else if (calc[0] < calc[1] && calc[1] < calc[2])
                    {

                        // 세 번째 조건
                        if (!chkSol[calc[0], calc[1], calc[2] - 1])
                        {

                            // 디버깅을 하다 보면 함수실행 순서를 알 수 있고,
                            // 실행 순서에 의해 w(a, b, c-1) 조건으로 진입
                            // 값이 있는지 판별한다 있으면 w(a, b-1, c-1) 검사
                            // 모두 있는 경우 메모장에 있는 값으로 규칙에 맞춰 새로운 값을 도출하면 되므로 
                            // 적합성 검사부터 했다

                            // 값이 없으므로 이전 값 입력이 필요
                            // 깊은 복사해서 값 채웠다. 여기서는 얕은 복사나 직접 대입해도 된다
                            int[] before = new int[3];
                            Array.Copy(calc, before, calc.Length);
                            beforeCalc.Push(before);

                            // calc의 값을 깎으므로 재귀 표현
                            calc[2] -= 1;
                        }
                        else if (!chkSol[calc[0], calc[1] - 1, calc[2] - 1])
                        {

                            // 이하 동문
                            int[] before = new int[3];
                            Array.Copy(calc, before, calc.Length);
                            beforeCalc.Push(before);
                            calc[1] -= 1;
                            calc[2] -= 1;
                        }
                        else if (!chkSol[calc[0], calc[1]- 1, calc[2]])
                        {

                            int[] before = new int[3];
                            Array.Copy(calc, before, calc.Length);
                            beforeCalc.Push(before);
                            calc[1] -= 1;
                        }
                        else
                        {

                            // 여기는 앞에 메모장에 존재하는지 검사했고 메모장에 해당 값이 존재하므로
                            // 값을 입력 한다
                            solves[calc[0], calc[1], calc[2]] = solves[calc[0], calc[1], calc[2] - 1]
                                                                + solves[calc[0], calc[1] - 1, calc[2] - 1]
                                                                - solves[calc[0], calc[1] - 1, calc[2]];
                            // 그리고 기존 값을 넣어준다
                            chkSol[calc[0], calc[1], calc[2]] = true;
                        }
                    }
                    else
                    {

                        // 재귀함수 w의 마지막 조건
                        // 세 번째와 같아서 생략
                        if (!chkSol[calc[0] - 1, calc[1], calc[2]])
                        {

                            int[] before = new int[3];
                            Array.Copy(calc, before, calc.Length);

                            beforeCalc.Push(before);
                            calc[0] -= 1;
                        }
                        else if (!chkSol[calc[0] - 1, calc[1] - 1, calc[2]])
                        {

                            int[] before = new int[3];
                            Array.Copy(calc, before, calc.Length);
                            beforeCalc.Push(before);

                            calc[0] -= 1;
                            calc[1] -= 1;
                        }
                        else if (!chkSol[calc[0] - 1, calc[1], calc[2] - 1])
                        {

                            int[] before = new int[3];
                            Array.Copy(calc, before, calc.Length);
                            beforeCalc.Push(before);

                            calc[0] -= 1;
                            calc[2] -= 1;
                        }
                        else if (!chkSol[calc[0] - 1, calc[1] - 1, calc[2] - 1])
                        {

                            int[] before = new int[3];
                            Array.Copy(calc, before, calc.Length);
                            beforeCalc.Push(before);

                            calc[0] -= 1;
                            calc[1] -= 1;
                            calc[2] -= 1;
                        }
                        else
                        {

                            solves[calc[0], calc[1], calc[2]] = solves[calc[0] - 1, calc[1], calc[2]]
                                                                + solves[calc[0] - 1, calc[1] - 1, calc[2]]
                                                                + solves[calc[0] - 1, calc[1], calc[2] - 1]
                                                                - solves[calc[0] - 1, calc[1] - 1, calc[2] - 1];
                            chkSol[calc[0], calc[1], calc[2]] = true;
                        }
                    }
                }

                Console.WriteLine($"w({inputs[0]}, {inputs[1]}, {inputs[2]}) = {result}");
            }
        }
    }
}
