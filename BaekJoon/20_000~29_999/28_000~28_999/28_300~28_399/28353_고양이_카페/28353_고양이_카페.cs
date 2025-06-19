using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 12
이름 : 배성훈
내용 : 고양이 카페
    문제번호 : 28353번

    정렬, 그리디, 두 포인터 알고리즘 문제다
    아이디어는 다음과 같다

    정렬한 뒤, 작은 고양이와 무거운 고양이를 짝으로 지을 수 있는지 확인한다
    그리고 짝이 되면, 1명 행복해진다

    무게를 초과하는 경우
    무거운 애를 제외한다
    무거운 애랑 남아있는 애와 엮으면 항상 무게를 초과하기 때문이다!
*/

namespace BaekJoon.etc
{
    internal class etc_0205
    {

        static void Main205(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int neko = ReadInt(sr);
            int max = ReadInt(sr);

            int[] weights = new int[neko];
            for (int i = 0; i < neko; i++)
            {

                weights[i] = ReadInt(sr);
            }

            sr.Close();

            Array.Sort(weights);

            int left = 0;
            int right = neko - 1;

            int ret = 0;
            while(left < right)
            {

                int calc = weights[left] + weights[right];

                if (calc <= max)
                {

                    ret++;
                    left++;
                    right--;
                }
                else
                {

                    right--;
                }
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
