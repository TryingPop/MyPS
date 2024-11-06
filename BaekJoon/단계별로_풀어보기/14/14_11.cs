using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 12
이름 : 배성훈
내용 : 포도주 시식
    문제번호 : 2156번
*/

namespace BaekJoon._14
{
    internal class _14_11
    {

        static void Main11(string[] args)
        {

            
            int length = int.Parse(Console.ReadLine());
            
            // 다른 사람껄 참고해서 작성한 코드
            int[] bestScores = new int[length];
            int[] inputs = new int[length];

            for (int i = 0; i < length; i++)
            {

                inputs[i] = int.Parse(Console.ReadLine());

                switch (i)
                {

                    case 0:
                        bestScores[0] = inputs[0];
                        break;
                    case 1:
                        bestScores[1] = bestScores[0] + inputs[1];
                        break;
                    case 2:
                        bestScores[2] = Math.Max(bestScores[i - 1], 
                            Math.Max(inputs[0], inputs[1]) + inputs[2]);
                        break;
                    default:
                        bestScores[i] = Math.Max(bestScores[i - 1], 
                            Math.Max(bestScores[i - 3] + inputs[i - 1], bestScores[i - 2]) + inputs[i]);
                        break;
                }
            }

            Console.WriteLine(bestScores[length - 1]);

            /*
            
            // 제출한 코드
            // 따로 기록을 안하고 최소한의 변수로 돌려막기 했다
            // int[] bestScore = new int[3] { 0, 0, 0 };        // 배열의 인덱스 i가 의미하는 것은 아래와 같다
                                                                // 현재를 k번째 와인이 제시된 상황이라 가정한다 
                                                                // i == 0 인 경우, k번째 와인을 마시지 않았다
                                                                // k-1, k-2번째 마셨는지는 모른다
                                                                // i == 1 인 경우, k번째에 와인을 마셨고, 
                                                                // k-1번째 와인을 마시지 않았다
                                                                // i == 2 인 경우, k번째와 k-1번째 와인을 마셨다
                                                                // 그리고 배열의 값은 해당 상황에서 가장 많이 마신 와인의 양이다

            // bestScore[1] = int.Parse(Console.ReadLine());    // for문 아래 규칙 때문에 처음은 대입 해줘야한다
                                                                // length >= 1이므로 if문 없이 진입 가능


            for (int n = 1; n < length; n++)                    // length가 1보다 큰 경우만 진입
            {

                int input = int.Parse(Console.ReadLine());      // n - 1번째 와인의 양
                int temp = bestScore.Max();                     // n - 2번째 최고 많이 마신 와인의 양이된다
                    
                // 스코어 갱신
                bestScore[2] = bestScore[1] + input;            // bestSore[1]이 아직 갱신 안되었으므로 
                                                                // n-2번째 와인을 마셨고, n-3번째 와인을 마시지 않은 경우에
                                                                // 최고 많이 마신 와인양을 의미한다
                                                                // 그래서 여기에 input을 더하면 
                                                                // n-2번째와 n-1번째 와인을 마신 최고 많은 양을 의미한다

                bestScore[1] = bestScore[0] + input;            // 마찬가지로 n-2번째에 와인을 마시지 않았고
                                                                // n - 1번째 와인을 마신 최대 값이 된다

                bestScore[0] = temp;                            // 바로 앞의 최고 점수가 와인을 안마셨을 때 가질 수 있는 최대값
            }
            */

            // Console.WriteLine(bestScore.Max());              // length개 주어졌을 때 최고 많이 마신 와인의 양 출력

        }
    }
}
