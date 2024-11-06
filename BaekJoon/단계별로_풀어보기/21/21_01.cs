using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 19
이름 : 배성훈
내용 : 숫자 카드
    문제번호 : 10815번

    집합과 맵에 관련된 문제
    다른 사람들 풀이를 보니깐 HashSet을 이용했다
    
    그런데 내가 푼 방법은 2000만 크기의 배열로 담아서 다른 사람들이 푼 방법보다 메모리가 1.5배 정도 많이 쓰고
    HashSet보다 성능도 느리다!
*/

namespace BaekJoon._21
{
    internal class _21_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StringBuilder sb = new StringBuilder();

            int[] nums = new int[2000 * 10000 + 1];
            int cardNum = int.Parse(sr.ReadLine());

            int[] myCard = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            for (int i = 0; i < cardNum; i++)
            {

                nums[myCard[i] + 10000000] = 1;
            }

            int findNum = int.Parse(sr.ReadLine());

            int[] findCard = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            sr.Close();

            for (int i = 0; i < findNum; i++)
            {

                if (nums[findCard[i] + 10000000] == 1)
                {

                    sb.Append("1");
                }
                else
                {

                    sb.Append("0");
                }
                
                if (i == findNum - 1)
                {

                    break;
                }

                sb.Append(" ");
            }


            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
                sw.WriteLine(sb);

        }
    }
}
