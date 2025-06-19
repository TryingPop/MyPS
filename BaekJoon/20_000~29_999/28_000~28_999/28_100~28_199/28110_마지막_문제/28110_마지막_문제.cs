using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 22
이름 : 배성훈
내용 : 마지막 문제
    문제번호 : 28110번

    입력된 문제들의 범위 안에 있어야한다
    그리고 다른 난이도들과 문제 난이도 차이가 제일 작아야하는 것 중에서 
    큰 값이 오게 새로운 문제 난이도를 설정해야한다
    만약 여러 개면 낮은 난이도로 선택한다
    이미 출제한 난이도는 나오면 안된다

    아이디어는 다음과 같다
    난이도 차가 작은 것중 가장 큰 것은 중앙 값을 비교했다
    먼저 중앙값을 찾기 위해 정렬을 했다

    여러 개 있으면 낮은 난이도를 택하라고 했으므로 양의 정수에서 내림 연산을 해주는
    정수 나눗셈을 이용했다
    불가능한 경우 -1을 반환하라고 했으므로,
    왼쪽에 위치한 중앙값을 찾고 이중에서 차이가 가장 큰 것을 찾는다
    그리고 가장 큰 것을 반환한다

    만약 차이가 0이라면, 1칸 범위이고 이는 범위 안에서 찾을 수 없다는 말과 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0079
    {

        static void Main79(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            int[] rank = new int[len];

            for (int i = 0; i < len; i++)
            {

                rank[i] = ReadInt(sr);
            }

            sr.Close();

            // 정렬
            Array.Sort(rank);


            // 중앙값과 가장 큰 차이 나게 하기
            int diff = 0;
            int maxIdx = 0;
            for (int i = 0; i < len - 1; i++)
            {

                int mid = (rank[i] + rank[i + 1]) / 2;
                // 내림 연산이므로 왼쪽 값이 차가 최소가 된다
                int max = mid - rank[i];

                // 차가 최소가 되는 것 중 최대값!
                if (max > diff) 
                { 
                    
                    diff = max;
                    maxIdx = i;
                }
            }

            int ret = -1;
            // 범위 안에 존재하는 경우 해당 값 출력
            if (diff != 0) ret = rank[maxIdx] + diff;


            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;
            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
