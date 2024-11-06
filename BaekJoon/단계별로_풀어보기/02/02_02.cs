using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 2단계 2번 문제
 * 
 * 시험 성적
 * 시험 점수를 입력받아 학점으로 출력하기
 */

namespace BaekJoon._02
{
    internal class _02_02
    {
        static void Main2(string[] args)
        {
            int score = int.Parse(Console.ReadLine());
            char grade;

            if (score >= 90)
            {
                grade = 'A';
            }
            else if (score >= 80)
            {
                grade = 'B';
            }
            else if (score >= 70)
            {
                grade = 'C';
            }
            else if (score >= 60)
            {
                grade = 'D';
            }
            else
            {
                grade = 'F';
            }

            Console.WriteLine(grade);
        }
        

    }
}
