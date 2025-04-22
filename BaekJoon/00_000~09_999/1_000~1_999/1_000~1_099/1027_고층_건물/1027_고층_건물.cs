using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 고층 건물
    문제번호 : 1027번

    수학, 브루트포스, 기하학 문제다
    기울기만 비교했다 기울기 비교방법은 CCW 알고리즘 방식과 같다
    다만, 높이차이 부등호를 잘못 설정하여, 1번틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0321
    {

        static void Main321(string[] args)
        {


            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            int n = ReadInt(sr);

            int[] heights = new int[n];
            for (int i = 0; i < n; i++)
            {

                heights[i] = ReadInt(sr);
            }

            long[] diffY = new long[n];

            int ret = 0;

            for (int i = 0; i < n; i++)
            {

                int find = 0;
                for (int j = 0; j < n; j++)
                {

                    // 높이차 구하기
                    diffY[j] = heights[j] - heights[i];
                    if (j < i) diffY[j] = -diffY[j];
                }

                for (int j = 0; j < i; j++)
                {

                    bool chk = true;
                    for (int k = j + 1; k < i; k++)
                    {

                        // 왼쪽의 경우는 자기보다 기울기가 낮은게 존재하면 안보인다
                        if (diffY[j] * (i - k) >= diffY[k] * (i - j))
                        {

                            chk = false;
                            break;
                        }
                    }

                    if (chk) find++;
                }

                for (int j = i + 1; j < n; j++)
                {

                    bool chk = true;
                    for (int k = i + 1; k < j; k++)
                    {

                        // 오른쪽의 경우는 중간에 기울기가 높은게 있으면 안보인다
                        if (diffY[j] * (k - i) <= diffY[k] * (j - i))
                        {

                            chk = false;
                            break;
                        }
                    }

                    if (chk) find++;
                }

                if (ret < find) ret = find;
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
