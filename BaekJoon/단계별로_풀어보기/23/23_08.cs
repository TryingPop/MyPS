using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 너의 평점은
    문제번호 : 25206번
*/

namespace BaekJoon._23
{
    internal class _23_08
    {

        static void Main8(string[] args)
        {
            const int len = 20;

            int sumGrades = 0;      // 총 시간
            int sumScores = 0;      // 총 점수

            for (int i = 0; i < len; i++)
            {

                int grades, scores;
                {

                    string[] input = Console.ReadLine().Split(' ');
                    grades = input[1][0] - '0';
                    scores = GetScore(input[2]);
                    if (scores == -1) continue;
                }

                sumGrades += grades;
                sumScores += scores * grades;
            }

            // 평균 점수 계산
            float result = sumScores / (float)(sumGrades * 2);
            Console.WriteLine(result);
        }

        // 학점을 점수로 바꾸어 주는 메소드
        // 점수는 통상아는거에 2배
        static int GetScore(string str)
        {

            switch (str)
            {

                case "A+":
                    return 9;
                case "A0":
                    return 8;
                case "B+":
                    return 7;
                case "B0":
                    return 6;
                case "C+":
                    return 5;
                case "C0":
                    return 4;
                case "D+":
                    return 3;
                case "D0":
                    return 2;
                case "F":
                    return 0;
                default:
                    return -1;
            }
        }
    }
}
