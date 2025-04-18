using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 14
이름 : 배성훈
내용 : 이동하기 5
    문제번호 : 20035번

    그리디 문제다.
    우선 행의 사탕이 많은게 제일 중요하다.
    열의 갯수를 어떻게 더해도 행의 값이 1 증가하는게 더 크다.
    그래서 행이 가장 큰 것을 기준으로 열로 이동을 진행한다.
    해당 상황에서 열의 최대를 찾아야 한다.

    해당 부분을 보면 가장 큰 행이 두 개 이상일 때,
    해당 사이에 열이 가장 큰 곳을 최대한 내려오는게 좋음을 알 수 있다.
    그리고 가장 큰 행의 최소 행과 최대 행을 찾아주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1543
    {

        static void Main1543(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int row = ReadInt();
            int col = ReadInt();

            int[] chkRowMin = new int[10];
            int[] chkRowMax = new int[10];

            int rowSum = 0;
            Array.Fill(chkRowMin, 1_000_000);
            Array.Fill(chkRowMax, -1);
            for (int i = 0; i < row; i++)
            {

                int cur = ReadInt();

                chkRowMin[cur] = Math.Min(chkRowMin[cur], i);
                chkRowMax[cur] = Math.Max(chkRowMax[cur], i);
                rowSum += cur;
            }

            int rowMax = 0;
            for (int i = 9; i > 0; i--)
            {

                if (chkRowMax[i] == -1) continue;
                rowMax = i;
                rowSum += i * (col - 1);
                break;
            }

            int colSum = 0;
            int colMax = 0;
            for (int i = 0; i < col; i++) 
            {

                int cur = ReadInt();
                if (i == 0) colSum = cur * (chkRowMin[rowMax] + 1);
                else if (i == col - 1) colSum += cur * (row - chkRowMax[rowMax]);
                else colSum += cur;


                colMax = Math.Max(colMax, cur);
            }

            colSum += colMax * (chkRowMax[rowMax] - chkRowMin[rowMax]);

            long ret = 1_000_000_000L * rowSum + colSum;

            Console.Write(ret);

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;
                    ret = c - '0';
                        
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
