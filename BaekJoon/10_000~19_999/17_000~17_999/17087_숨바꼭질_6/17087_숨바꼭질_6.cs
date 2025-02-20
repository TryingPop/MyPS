using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 10
이름 : 배성훈
내용 : 숨바꼭질 6
    문제번호 : 17087번

    수학 문제다
    최대 점프 간격은 동생위치 - 수빈이의 위치들의 GCD가
    최대 점프 간격과 같다
        예를들어 동생들 2, 4, 6, 10이고 수빈이의 위치가 8이면
        8 - 2, 8 - 4, 8 - 6, 10 - 8 들의 GCD
        6, 4, 2, 2 의 GCD 2가 정답

    해당 방법 말고도 수빈이의 위치를 넣고 좌표들을 정렬해서
    인접한 좌표들의 간격들의 GCD로 구해도 상관없다
        예를들어 동생들 2, 4, 6, 10 이고 수빈이의 초기 위치 8이라하면
        2, 4, 6, 10, 8 배열을 정렬해 2, 4, 6, 8, 10으로 만들고
        인접한 좌표들의 간격차 2, 2, 2, 2 의 GCD로 구해도 이상없다
    GCD 계산 과정에 의해 동형인 풀이다;
*/

namespace BaekJoon.etc
{
    internal class etc_0176
    {

        static void Main176(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int s = ReadInt(sr);

            int[] pos = new int[len];

            for (int i = 0; i < len; i++)
            {

                int p = ReadInt(sr);
                p = p < s ? s - p : p - s;
                pos[i] = p;
            }

            sr.Close();

            int ret = pos[0];

            for (int i = 1; i < len; i++)
            {

                ret = GCD(ret, pos[i]);
            }

            Console.WriteLine(ret);
        }

        static int GCD(int _a, int _b)
        {

            if (_a < _b)
            {

                int temp = _a;
                _a = _b;
                _b = temp;
            }

            while(_b > 0)
            {

                int temp = _a % _b;
                _a = _b;
                _b = temp;
            }

            return _a;
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
