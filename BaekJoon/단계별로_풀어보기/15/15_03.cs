using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 14
이름 : 배성훈
내용 : 세로읽기
    문제번호 : 10798번
*/

namespace BaekJoon._15
{
    internal class _15_03
    {

        static void Main3(string[] args)
        {

            const int strlen = 15;      // 최대 글자 길이
            const int num = 5;          // 입력 받는 글자 수
            char[,] str = new char[strlen, num];

            // 세로기록
            for (int i = 0; i < num; i++)
            {

                string inputs = Console.ReadLine();

                for (int j = 0; j < inputs.Length; j++)
                {
                    
                    str[j, i] = inputs[j];
                }
            }

            // 가로 읽기
            for (int i = 0; i < strlen; i++)
            {

                for (int j = 0; j < num; j++)
                {

                    if (str[i, j] != 0)
                    {

                        Console.Write(str[i, j]);
                    }
                }
            }
        }
    }
}
