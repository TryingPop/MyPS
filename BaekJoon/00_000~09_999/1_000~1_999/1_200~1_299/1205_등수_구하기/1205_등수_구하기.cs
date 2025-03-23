using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 13
이름 : 배성훈
내용 : 등수 구하기
    문제번호 : 1205번

    입력 값 n이 적어 구현으로도 풀린다
    이분탐색을 이용해 풀었다

    비오름차순 == 내림차순으로 값이 주어지기에,
    right를 항상 미만이 되게 포인터를 설정했다

    그리고, right + 1이 시작되는 인덱스이기에 
    등수는 인덱스에 + 1을 추가해야해서 right + 2 가 몇 등인지 된다

    그리고 보드에 갱신안되면 -1을 출력해야한다
    갱신되는 경우는 랭킹 보드에 빈자리가 있거나,
    빈자리가 없을 경우 가장 작은 값보다는 커야 갱신이 된다

    이는 랭킹 보드의 빈자리를 -1로 채우고 끝값보다 크거나 같은지 체크해서 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_0219
    {

        static void Main219(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int curScore = ReadInt(sr);

            int len = ReadInt(sr);
            int[] rank = new int[len];

            for (int i = 0; i < n; i++)
            {

                rank[i] = ReadInt(sr);
            }

            sr.Close();

            for(int i = n; i <len; i++)
            {

                // 랭킹 보드의 빈자리는 -1로 채운다
                rank[i] = -1;
            }

            // 가장 작은 값보다 현재 점수가 작거나 같으면 랭킹 갱신 X이므로
            // -1을 바로 출력
            if (curScore == rank[len - 1] || rank[len - 1] > curScore) 
            { 
                
                Console.WriteLine(-1);
                return;
            }

            // 이제 이진탐색으로 이하값을 찾는다
            int left = 0;
            int right = n - 1;

            while(left <= right)
            {

                int mid = (left + right) / 2;

                if (curScore < rank[mid]) left = mid + 1;
                else right = mid - 1;
            }

            int ret = right + 2;
            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
