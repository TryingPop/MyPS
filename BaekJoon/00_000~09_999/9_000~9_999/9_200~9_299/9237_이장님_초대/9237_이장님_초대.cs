using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 이장님 초대
    문제번호 : 9237번

    그리디, 정렬 문제다
    그리디하게 접근해서 가장 큰거부터 먼저 심는다
    그리고 완성되는 날짜를 찾는다
    구매할 때 1일, 심는데 1일이기에 + 2가된다
    그리고 완성되는 날짜 중 제일 뒤가 이장님 초대날짜가 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0378
    {

        static void Main378(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            sr.Close();

            Array.Sort(arr, (x, y) => y.CompareTo(x));

            int ret = 1;
            for (int i =0; i < n; i++)
            {

                int cur = i + 2 + arr[i];
                ret = ret < cur ? cur : ret;
            }

            Console.WriteLine(ret);
            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
