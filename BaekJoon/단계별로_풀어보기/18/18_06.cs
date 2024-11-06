using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 21
이름 : 배성훈
내용 : 좌표 정렬하기
    문제번호 : 11650번

    논리에 맞춰 작성해본 힙정렬이다
    속도는 5%정도 느리다!
    그런데 메모리 사용량이 10% 정도 적다
*/

namespace BaekJoon._18
{
    internal class _18_06
    {

        static void Main6(string[] args)
        {


            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[][] inputs = new int[len][];

            for (int i = 0; i < len; i++)
            {

                inputs[i] = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
#if false

            foreach(var i in inputs.OrderBy(x => x[0]).ThenBy(x => x[1]))
            {

                sw.WriteLine(string.Format("{0} {1}", i[0], i[1]));
            }

#else 

            StringBuilder sb = new StringBuilder();

            // 스스로 작성해본 힙 정렬 현재 시간 초과 뜬다...
            HeapSort(ref inputs, inputs.Length);

            for (int i = 0; i < inputs.Length; i++)
            {

                sb.AppendLine(string.Format("{0} {1}", inputs[i][0], inputs[i][1]));
            }
            sw.Write(sb);
#endif
            sw.Close();

        }

#if true

        // 힙 정렬 알고리즘 좌표로 적용시켜보기!
        static void HeapSort(ref int[][] arr, int len)
        {

            // 처음 트리 설정
            {
                int chk;
                int[] temp;
                int chkNum;
                // 제일 큰 값 arr[0]의 위치로!
                for (int i = 1; i < len; i++)
                {

                    chkNum = i;
                    chk = i;
                    // 부모 노드보다 x 좌표 값이 크거나,
                    // 부모 노드와 x 좌표 값이 같은데, 부모 노드보다 y좌표 값이 큰 경우 해당 부모 노드와 자리 바꾼다
                    do
                    {

                        chk = ((chk - 1) / 2);

                        if (CompareToXY(arr[chk], arr[chkNum]) == 1)
                        {

                            break;
                        }

                        Swap(ref arr[chkNum], ref arr[chk]);
                        chkNum = chk;
                    }
                    while (chk >= 1);
                }

                Swap(ref arr[0], ref arr[len - 1]);
            }

            // 이제부터 반복이 필요한 과정
            Heap(ref arr, len - 1);
        }

        static void Heap(ref int[][] arr, int len)
        {

            if (len <= 1) return;

            int chkNum = 0;
            int chk1, chk2;
            do
            {

                chk1 = 2 * chkNum + 1;
                chk2 = 2 * chkNum + 2;
                // 2개 존재
                if (chk1 < len && chk2 < len)
                {

                    if (CompareToXY(arr[chk1], arr[chkNum]) == 1)
                    {

                        
                        if (CompareToXY(arr[chk2], arr[chk1]) == 1)
                        {

                            Swap(ref arr[chk2], ref arr[chkNum]);
                            chkNum = chk2;
                            continue;
                        }

                        Swap(ref arr[chk1], ref arr[chkNum]);
                        chkNum = chk1;
                        continue;
                    }
                    else if (CompareToXY(arr[chk2], arr[chkNum]) == 1)
                    {

                        Swap(ref arr[chk2], ref arr[chkNum]);
                        chkNum = chk2;
                        continue;
                    }
                }
                // 1개 존재
                else if (chk1 < len)
                {

                    if (CompareToXY(arr[chk1], arr[chkNum]) == 1)
                    {

                        Swap(ref arr[chk1], ref arr[chkNum]);
                        chkNum = chk1;
                        continue;
                    }
                }

                break;
            } while (true);

            Swap(ref arr[0], ref arr[len - 1]);
            Heap(ref arr, len - 1);
        }

        static void Swap(ref int[] x, ref int[] y)
        {

            int[] temp = x;
            x = y;
            y = temp;
        }

        /// <summary>
        /// x, y 좌표 값 비교 메소드, 앞에께 크면 1, 뒤에께 크면 -1, 같으면 0
        /// </summary>
        /// <param name="x">비교할 좌표 1</param>
        /// <param name="y">비교할 좌표 2</param>
        /// <returns>비교 값</returns>
        static int CompareToXY(int[] x, int[] y)
        {

            if (x[0] > y[0] || (x[0] == y[0] && x[1] > y[1]))
            {

                return 1;
            }
            else if (x[0] == y[0] && x[1] == y[1])
            {

                return 0;
            }

            return -1;
        }
#endif
    }
}
