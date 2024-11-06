using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 11
이름 : 배성훈
내용 : RGB거리
    문제번호 : 1149번
*/
namespace BaekJoon._14
{
    internal class _14_06
    {

        static void Main6(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int length = int.Parse(sr.ReadLine());

            int[] prices = Array.ConvertAll(sr.ReadLine().Split(' '), item => int.Parse(item));

            for (int i = 1; i < length; i++)
            {

                int[] input = Array.ConvertAll(sr.ReadLine().Split(' '), item => int.Parse(item));

                int temp1 = prices[0];
                int temp2 = prices[1];

                prices[0] = prices[1] <= prices[2] ? prices[1] + input[0] : prices[2] + input[0];
                prices[1] = temp1 <= prices[2] ? temp1 + input[1] : prices[2] + input[1];
                prices[2] = temp1 <= temp2 ? temp1 + input[2] : temp2 + input[2];

                /*
                // 문제를 잘못 읽었다 >>> i-1번과 i+1번이 같을 수 없는 경우 사용가능
                switch (i % 3) 
                {

                    case 0:
                        prices[0] += input[0];
                        prices[1] += input[0];
                        prices[2] += input[1];
                        prices[3] += input[1];
                        prices[4] += input[2];
                        prices[5] += input[2];
                        break;
                        
                    case 1:
                        prices[0] += input[1];
                        prices[1] += input[2];
                        prices[2] += input[0];
                        prices[3] += input[2];
                        prices[4] += input[0];
                        prices[5] += input[1];
                        break;

                    case 2:
                        prices[0] += input[2];
                        prices[1] += input[1];
                        prices[2] += input[2];
                        prices[3] += input[0];
                        prices[4] += input[1];
                        prices[5] += input[0];
                        break;
                }
                */
            }

            sr.Close();
            int result = prices.Min();

            Console.WriteLine(result);
        }
    }
}
