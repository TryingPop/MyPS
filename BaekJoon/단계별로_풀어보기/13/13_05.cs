using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 10
이름 : 배성훈
내용 : 주유소
    문제번호 : 13305번
*/

namespace BaekJoon._13
{
    internal class _13_05
    {

        static void Main5(string[] args)
        {

            StreamReader sr = new StreamReader(Console.OpenStandardInput());            // 여기 문제에 한해서는 따로 없어도 될거 같다
                                                                                        // 속도 저하만 발생

            int cityNums = int.Parse(sr.ReadLine());

            ulong[] distances = Array.ConvertAll(sr.ReadLine().Split(' '), item => ulong.Parse(item));          // 계산에서 매번 캐스팅을 안하기 위해서 ulong으로 선언
                                                                                                                // nint를 사용했는데 런타임에서 크기를 정하기에 풀이 시간 차이가 확연하게 존재
            ulong[] prices = Array.ConvertAll(sr.ReadLine().Split(' '), item =>  ulong.Parse(item));

            sr.Close();

            ulong price = prices[0];        // 처음에 기름이 0이므로 처음 도시 가격을 넣는다
            ulong result = 0;               // 거리와 기름가격의 범위가 1 ~ 10억 이므로 long

            for (int i = 0; i < cityNums - 1; i++)
            {

                if (price > prices[i])      
                {

                    price = prices[i];
                }

                result += (price * distances[i]);
            }

            Console.WriteLine(result);
        }
    }
}
