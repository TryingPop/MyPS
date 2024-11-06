using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 13
이름 : 배성훈
내용 : 평범한 배낭
    문제번호 : 12865번
*/
namespace BaekJoon._14
{

    internal class _14_16
    {

        static void Main16(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[][] inputs = new int[info[0]][];

            int[] memo = new int[info[1] + 1];

            for (int i =0;i < info[0]; i++)
            {

                inputs[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            }


            int count = 1;
            for (int i = 0; i < info[0]; i++)
            {

                for (int j = count - 1; j > 0; j--)
                {


                    // 비어있거나 인덱스 초과면 제외
                    if (memo[j] == 0 || j + inputs[i][0] > info[1])
                    {

                        continue;
                    }

                    // 기존 값 갱신이 필요하면 갱신 자기자신은 이후에 갱신한다
                    if (memo[j + inputs[i][0]] < inputs[i][1] + memo[j])
                    {

                        memo[j + inputs[i][0]] = memo[j] + inputs[i][1];
                    }

                }

                // 자기자신 갱신, 넣을 수 있는지 무게 검사
                if (inputs[i][0] < memo.Length)
                {

                    // 값 갱신이 필요한지 확인
                    if (memo[inputs[i][0]] == 0 || memo[inputs[i][0]] < inputs[i][1])
                    {

                        memo[inputs[i][0]] = inputs[i][1];
                    }
                }

                // for문 중 j를 이용하는 곳에서 for문을 적게 돌리기 위해 넣은 구문 count = memo.Length로 해도 상관 없다
                if (count < memo.Length) count = count + inputs[i][0] < memo.Length ? count + inputs[i][0] : memo.Length;

                /*
                // 다른사람들 풀이는
                int weight = inputs[i][0];
                int value = inputs[i][1];

                // 여기서 만들어지는 memo의 최대값이나 구하는 메모값이나 같다
                for (int j = memo.Length - 1; j >= weight; j--)
                {
                    
                    memo[weight] = Math.Max(memo[weight], memo[j - weight] + value);
                }
                */
            }

            int result = 0;
            for (int i = 0; i < memo.Length; i++)
            {

                if (result < memo[i]) result = memo[i];
            }

            Console.WriteLine(result);
        }
    }
}
