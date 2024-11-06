using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 13
이름 : 배성훈
내용 : 공유기 설치
    문제번호 : 2110번

    공유기 설치간 인접한 최대 간격을 찾는 것인데
    코드는 특정 간격을 이용해 공유기 설치 개수를 확인해 최대 길이를 찾았다
*/

namespace BaekJoon._28
{
    internal class _28_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();


            int[] pos = new int[info[0]];

            for (int i = 0; i < info[0]; i++)
            {

                pos[i] = int.Parse(sr.ReadLine());
            }

            sr.Close();

            pos = pos.OrderBy(x => x).ToArray();

            // 평균값 정리에 의해 최대 거리는 (end - start) / n 보다 작거나 같다
            int start = 0;
            int end = (pos[^1] - pos[0]) / (info[1] - 1);

            while (start < end)
            {

                int mid = (start + end + 1) / 2;

                int cnt = 1;
                int now = pos[0];

                // 설치 가능한 공유기 갯수 조사
                for (int i = 0; i < info[0]; i++)
                {

                    if (pos[i] - now >= mid) 
                    { 
                        
                        cnt++;
                        now = pos[i];
                    }
                }

                // 조건 만족했는지 확인
                if (cnt >= info[1]) start = mid;
                else end = mid - 1;
            }

            Console.WriteLine(start);
        }
    }
}
