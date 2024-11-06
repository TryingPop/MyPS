using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 18
이름 : 배성훈
내용 : 마법의 돌 장난감
    문제번호 : 21554번

    배열을 바꿔서 저장하는 방법만 구현할 수 있으면 쉽게 풀린다
    배열의 최대 길이가 100이므로 N^3의 방법으로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0062
    {

        static void Main62(string[] args)
        {

            StreamReader sr = new StreamReader(Console.OpenStandardInput());

            int len = ReadInt(sr);

            int[] arr = new int[len + 1];

            for (int i = 1; i <= len; i++)
            {

                arr[i] = ReadInt(sr);
            }

            sr.Close();

            int[] from = new int[len + 1];
            int[] to = new int[len + 1];

            int cur = 0;

            for (int i = 1; i <= len; i++)
            {

                // 자기자리에 있으면 넘긴다
                if (i == arr[i]) continue;

                // 현재자리에 맞는 주인을 찾는다
                int changeIdx = 0;
                for (int j = i + 1; j <= len; j++)
                {

                    if (arr[j] == i) 
                    {

                        changeIdx = j;
                        break; 
                    }
                }

                if (changeIdx == 0) 
                {

                    // 여기로 올 일은 없다!
                    cur = -1;
                    break; 
                }

                // i 부터 changeIdx까지 바꾼다
                int mid = (i + changeIdx) / 2;

                for (int j = i; j <= mid; j++)
                {

                    int temp = arr[j];
                    arr[j] = arr[changeIdx - j + i];
                    arr[changeIdx - j + i] = temp;
                }

                from[cur] = i;
                to[cur] = changeIdx;
                cur++;
            }

            if (cur > 100) cur = -1;
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(cur);
                for (int i = 0; i < cur; i++)
                {

                    sw.Write($"{from[i]} {to[i]}\n");
                }
            }
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
