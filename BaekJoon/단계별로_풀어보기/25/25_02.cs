using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 22
이름 : 배성훈
내용 : N과 M (2)
    문제번호 : 15650번

    조합(Combination) 경우의 수 문제
*/

namespace BaekJoon._25
{
    internal class _25_02
    {

        static void Main2(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), (item) => int.Parse(item));
#if first
            int[] board = new int[info[0] + 1];
            // bool[] chk = new bool[info[0] + 1];          // 여기서는 뒤로 돌아오는 경우가 없기에
                                                            // 막을 필요가 없다
            int[] result = new int[info[0]];

            board[0] = info[1];

            for (int i = 1; i < board.Length; i++)
            {

                board[i] = i;
            }


            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            Back(sw, board, result);
#elif true

            // 다른 사람 풀이 로직만!
            int[] arr = new int[info[0] + 1];
            arr[0] = info[1];


            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            DFS(sw, arr);
#endif
            sw.Close();
        }

#if first
        static void Back(StreamWriter sw, int[] board, int[] result, int step = 0, int before = 0)
        {

            if (step >= board[0])
            {

                for (int i = 0; i < step; i++)
                {

                    sw.Write($"{result[i]} ");
                }
                sw.Write('\n');

                return;
            }

            // 앞과 비슷하나 이번에는 오름차순 조건과, 자리 위치 달라도 같은 경우로 인식하기에
            // 이전 값보다 큰 경우만 체크한다
            for (int i = before + 1; i <= board.Length - 1; i++)
            {

                // 이전 값보다 큰 경우
                result[step] = i;
                // 재귀로 진행
                Back(sw, board, result, step + 1, i);
            }

        }
#elif true

        static void DFS(StreamWriter sw, int[] arr, int cur = 1, int dept = 1)
        {

            if (dept == arr[0])
            {

                for (int i = 1; i <= dept; i++) 
                {

                    sw.Write($"{arr[i]} ");
                }
                sw.Write('\n');
                return;
            }

            for (int i = cur; i <= arr.Length - 1; i++)
            {

                arr[dept] = 1;
                DFS(sw, arr, i + 1, dept + 1);
            }
        }
#endif
    }
}
