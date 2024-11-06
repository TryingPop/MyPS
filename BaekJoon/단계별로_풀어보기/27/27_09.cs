using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 11
이름 : 배성훈
내용 : 히스토그램에서 가장 큰 직사각형
    문제번호 : 6549번

    그냥 이중 포문 돌리면 O(n^2) 이라 시간초과뜬다

    여기서는 스택을 이용해 풀자!
    인덱스가 맞지 않아 자꾸 틀렸다
    
    이건 추후에 해볼 방법
    세그먼트 트리 + 분할 정복으로 푼다고 한다
    세그먼트 트리는 구간합 or 값 변경 시간 복잡도가 n이 log n 으로 바뀐다 
    대신 값 변경할 때 부모노드도 변경해야 하기에 시간 복잡도 1이 log n 으로 바뀐다
*/

namespace BaekJoon._27
{
    internal class _27_09
    {

        static void Main9(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            {

                while (true)
                {

                    int[] inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                    if (inputs[0] == 0) break;

                    Stack<int> idxs = new Stack<int>(inputs[0]);
                    // 가장 큰 값
                    long max = 0;

                    for(int i = 1; i <= inputs[0]; i++)
                    {

                        // 현재 지점 i의 값과 이전 지점 a의 값을 비교하는데,
                        // 이전 지점 a의 값이 현재 지점 i의 값보다 큰 경우만 한번 확인
                        while(idxs.Count != 0 && inputs[idxs.Peek()] > inputs[i])
                        {

                            // 이전 지점 a의 값을 높이로 한다
                            long height = inputs[idxs.Peek()];

                            // 이전 지점 a의 값을 높이로 하는 가장 면적이 큰 사각형을 찾는다
                            // 가질 수 있는 최대 길이
                            long width = i - 1;

                            // 그리고 a 지점은 뺀다
                            idxs.Pop();

                            // a 값보다 작은 값을 갖는 b 지점이 존재하는지 확인
                            if (idxs.Count != 0)
                            {

                                // b 지점이 있는경우 최대 길이는 a - b가 최대 길이가 된다
                                width = i - 1 - idxs.Peek();
                            }

                            // 면적이 최대값인지 확인 
                            if (max < width * height)
                            {

                                max = width * height;
                            }
                        }
                        
                        // 현재 지점 i보다 이전 지점에서의 큰 값들 값을 모두 확인 했다
                        idxs.Push(i);
                    }

                    // 남는 경우 해결
                    // 최소 높이 지점의 판정과 남은 구간들이 여기에 있다
                    // 만약 input이 1번 인덱스부터 증가하는 수열이면 여기서 모든연산이 이뤄진다.
                    while(idxs.Count != 0)
                    {

                        // 앞과 같은 방법 이용
                        long height = inputs[idxs.Peek()];
                        idxs.Pop();
                        // 끝지점이 정해져있기에 여기서 가장 긴 길이는 전체 길이
                        long width = inputs[0];

                        if (idxs.Count != 0)
                        {

                            width = inputs[0] - idxs.Peek();
                        }

                        if (max < width * height)
                        {

                            max = width * height;
                        }
                    }

                    sb.AppendLine(max.ToString());
                }
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }
        }
    }
}
