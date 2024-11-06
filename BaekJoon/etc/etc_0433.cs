using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 3
이름 : 배성훈
내용 : 송이의 카드 게임
    문제번호 : 22951번

    구현, 시뮬레이션 문제다
    정답 사람을 찾는 부분 처리를 잘못해서 3번 틀렸다

    빼는 카드를 선택하면 해당 카드가 적힌 숫자만큼 오른쪽(반시계 방향)으로 이동시킨다
    카드 개수만큼 이동을 다 한 경우 다음 카드를 뺀다
    이렇게 1장 남을 때까지 진행했다 해당 방법은 최악의 경우 10 * N의 탐색을 요구한다
*/

namespace BaekJoon.etc
{
    internal class etc_0433
    {

        static void Main433(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int k = ReadInt();

            int[] arr = new int[n * k];

            for (int i = 0; i < arr.Length; i++)
            {

                arr[i] = ReadInt();
            }

            sr.Close();

            bool[] use = new bool[n * k];
            int s = 0;
            int curIdx = 0;
            int all = n * k;

            while(all > 1)
            {

                all--;
                use[s] = true;
                while (arr[s] > 0)
                {

                    curIdx++;
                    if (curIdx >= n * k) curIdx -= n * k;
                    if (!use[curIdx]) arr[s]--;
                }
                
                s = curIdx;
            }

            Console.WriteLine($"{1 + (curIdx / k)} {arr[curIdx]}");

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
