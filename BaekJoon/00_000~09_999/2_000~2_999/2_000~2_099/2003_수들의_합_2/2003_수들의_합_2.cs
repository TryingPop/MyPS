using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 26
이름 : 배성훈
내용 : 수들의 합 2
    문제번호 : 2003번

    브루트포스, 누적합, 두 포인터 문제다
    처음에는 길이를 n으로해서 두 포인터를 했지만,
    마지막에 읽는 부분 로직을 잘못짜서 한 번 틀렸다

    이후에 그냥 배열의 길이를 1 늘려서 마지막도 이상없이 읽게 하니 68ms로 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0357
    {

        static void Main357(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();

            int[] arr = new int[n + 1];

            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            sr.Close();

            int l = -1;
            int r = -1;

            int ret = 0;
            int cur = 0;
            while(l < n && r < n)
            {

                if (cur == m)
                {

                    r++;
                    cur += arr[r];
                    ret++;
                }
                else if (cur < m)
                {

                    r++;
                    cur += arr[r];
                }
                else 
                { 
                    
                    l++;
                    cur -= arr[l];
                }
            }


            Console.WriteLine(ret);

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
