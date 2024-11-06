using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 25
이름 : 배성훈
내용 : 세 수
    문제번호 : 10817번

    구현, 정렬 문제다
*/
namespace BaekJoon.etc
{
    internal class etc_0836
    {

        static void Main836(string[] args)
        {

            int[] arr = Console.ReadLine().Split().Select(int.Parse).OrderBy(x => x).ToArray();
            Console.Write(arr[1]);
        }
    }
}
