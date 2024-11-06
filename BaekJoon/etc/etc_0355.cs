using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 26
이름 : 배성훈
내용 : 외계 침략자 윤이
    문제번호 : 26123번

    구현 문제다
    문제에 친절하게 정답이 int 범위를 벗어난다고 되어져 있다
    아이디어는 다음과 같다

    가장 높은 층부터 1층씩 파괴하기에 층의 개수를 저장했다
    그리고 자장 높은 층부터 1층씩 파괴를 시작한다
    그러면 가장 높은층에 있는 빌딩들 값만큼 결과가 추가되고,
    해당 값은 -1 층으로 이동한다
    그리고 0층이되면 중지하기에, max = 0이면 탈출했다

    건물 층이 1층부터라고 명시되어져 있고, 입력되는 건물이 n개수가 1개 이상이기에 
    max = 0으로 시작해도 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0355
    {

        static void Main355(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] h = new int[300_001];

            int n = ReadInt();
            int k = ReadInt();

            int max = 1;
            for (int i = 0; i < n; i++)
            {

                int height = ReadInt();

                h[height]++;
                if (max < height) max = height;
            }

            sr.Close();

            long ret = 0;
            for (int i = 0; i < k; i++)
            {

                int add = h[max];
                h[max] = 0;
                ret += add;
                max--;
                h[max] += add;
                if (max == 0) break;
            }

            Console.WriteLine(ret);

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
