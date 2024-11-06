using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 25
이름 : 배성훈
내용 : 히스토그램
    문제번호 : 1725번

    앞에서 사용한 스택 방법을 그대로 다시 이용해본다!
    27_09는 내년 초에 분할정복에 맞춰서 다시 풀어보기!

    len = 7
    2, 4, 2, 5, 6, 5, 3         // 15가 잘 나오는지 확인
                                // 5 * 3 = 15

    len = 6
    3, 2, 4, 1, 2, 1            // 6 잘 나오는지 확인
                                // 2 * 3 = 6
*/

namespace BaekJoon._31
{
    internal class _31_04
    {

        static void Main4(string[] args)
        {

            int[] heights;
            int len;
            int maxArea = 0;

            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            {

                len = int.Parse(sr.ReadLine());

                heights = new int[len + 1];
                

                // 입력 받기
                for (int i = 0; i < len; i++)
                {

                    
                    heights[i] = int.Parse(sr.ReadLine());
                }

                // 마지막에 -1 추가
                heights[len++] = -1;
            }

            Stack<int> stack = new Stack<int>();

            // 연산
            for (int i = 0; i < len; i++)
            {

                int curHeight = heights[i];

                // 증가하는 경우면 넘어간다
                if (stack.Count == 0
                    || heights[stack.Peek()] < curHeight)
                {

                    stack.Push(i);
                }
                else
                {

                    // 감소하는 경우 꺼내서 넓이 확인
                    // 마지막의 값이 -1이므로 마지막에 항상 들린다!
                    while (stack.Count > 0)
                    {

                        // 높이 설정
                        int height = heights[stack.Peek()];

                        // 스택에 있는 높이가 현재 높이보다 작은 경우 탈출
                        // 마지막은 -1이기에 탈출 못한다
                        if (height < curHeight) break;

                        stack.Pop();
                        int width;

                        // 가로길이 설정
                        // i - 1은 오른쪽 끝값, stack.Peek()은 왼쪽 끝값이 된다
                        if (stack.Count != 0) width = i - 1 - stack.Peek();
                        // 스택에 없는 경우 가장 작은 값이므로 현재까지의 길이 i가 가로가 된다
                        else width = i;

                        // 넓이 계산
                        int area = width * height;
                        // 기존의 최고 넓이보다 현재 넓이가 큰 경우 값 저장
                        if (maxArea < area) maxArea = area;
                    }

                    // 해당 인덱스 스택에 넣기
                    stack.Push(i);
                }
            }

            
            Console.WriteLine(maxArea);
        }
    }
}
