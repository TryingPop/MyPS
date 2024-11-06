using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 3단계 11번 문제
 * 
 * X보다 작은 수
 * 정수의 갯수, 기준, 정수들을 입력받아 기준보다 작은 정수들 순서대로 출력하기
 */

namespace BaekJoon._03
{
    internal class _03_11
    {
        static void Main11(string[] args)
        {
            string[] varArr1 = Console.ReadLine().Split(" ");
            string[] varArr2 = Console.ReadLine().Split(" ");
            int len = int.Parse(varArr1[0]);
            int x = int.Parse(varArr1[1]);

            int[] input = Array.ConvertAll(varArr2, int.Parse);

            foreach (int num in input)
            {
                if (num < x)
                {
                    Console.Write(num + " ");
                }
            }
        }
    }
}
