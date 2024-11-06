using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 18
이름 : 배성훈
내용 : 양팔 저울
    문제번호 : 2629번

    만들 수 있는 무게를 인덱스에 기록해 놓고
    인덱스값이 참인지 거짓인지 확인하는게 주된 아이디어!
*/

namespace BaekJoon._30
{
    internal class _30_04
    {

        static void Main4(string[] args)
        {

            // 만들 수 있는 최대 무게
            const int MAX = 15_001;

            // 재는 무게 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            int[] weights = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // 만들 수 있는 무게 저장
            // 확인할 수 있는 무게를 오른쪽에 올린다고 가정하자!
            bool[] dp = new bool[MAX];
            // 아무것도 안올리면 0 이므로 0은 잴 수 있다
            dp[0] = true;

            for (int i= 0; i < len; i++)
            {

                int weight = weights[i];

                bool[] temp = new bool[MAX];

                for (int j = 0; j < MAX; j++)
                {

                    // 이전에 만들 수 있는 무게인 경우
                    if (dp[j]) 
                    {

                        // 0보다 작을 수 있다
                        // 왼쪽에 2g, 오른쪽에 1g를 올리면 
                        // 확인할 수 잇는 무게는 (1 - 2)g = -1g이다
                        // 이를 좌우 반전해서
                        // 오른쪽에 2g, 왼쪽에 1g를 올리면
                        // 확인할 수 잇는 무게는 (2 - 1)g = 1g
                        if (j - weight < 0) temp[weight - j] = true;
                        // 이상 없다
                        else temp[j - weight] = true;

                        // 조건에서 최대 추의 무게는 500g이고 최대 개수는 30개
                        // MAX 변경에 따라 그래서 인덱스 초과하는지 확인!
                        // 실상은 if문 자체가 필요없다!
                        if (j + weight < temp.Length) temp[j + weight] = true;

                        // 그리고 자기 자신의 무게는 잴 수 있으니 true!
                        temp[j] = true;
                    }
                }

                // 덮어 씌운다
                dp = temp;
            }
            
            // 확인할 추 입력
            len = int.Parse(sr.ReadLine());
            int[] chks = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            sr.Close();

            // 연산
            for (int i = 0; i < len; i++)
            {

                if (chks[i] >= MAX) Console.Write("N ");
                else if (dp[chks[i]]) Console.Write("Y ");
                else Console.Write("N ");
            }
        }
    }
}
