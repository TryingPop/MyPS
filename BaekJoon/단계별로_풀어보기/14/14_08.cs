using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

/*
날짜 : 2023. 7. 11
이름 : 배성훈
내용 : 계단 오르기
    문제번호 : 2579번
*/

namespace BaekJoon._14
{
    internal class _14_08
    {

        static void Main8(string[] args)
        {

            int length = int.Parse(Console.ReadLine());

            int score = 0;              // 계단 점수

            int oneStepAgo;             // 한 칸 띌 수 있는 한칸 전 최고점수
            int twoStepAgo = 0;         // 두칸 점프할 수 있는 최고점수
            int beforeBestScore = 0;
            int bestScore = 0;

            for (int i = 0; i < length; i++)
            {

                // 현재층에 맞게 변수 값 갱신
                oneStepAgo = twoStepAgo + score;        // 한칸 점프가 가능해야하므로
                                                        // 세칸 전의 최고점수에 앞번 계단 점수를 합한 값이
                                                        // 한 칸전 점프 가능한 최고점수가 된다
                twoStepAgo = beforeBestScore;           // 갱신 전 이전 층의 최고점수
                                                        // 층이 갱신되면서 2칸 전 최고점수가 된다
                score = int.Parse(Console.ReadLine());  // 현재 층 점수 갱신
                beforeBestScore = bestScore;            // 이전 최고 점수 갱신

                bestScore = oneStepAgo > twoStepAgo ?   // 현재 층 최고점수 갱신
                    oneStepAgo + score : twoStepAgo + score;
            }

            Console.WriteLine(bestScore);
        }
    }
}
