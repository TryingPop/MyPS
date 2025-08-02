using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 19
이름 : 배성훈
내용 : 킥보드로 등교하기
    문제번호 : 27977번

    매개 변수 탐색 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1558
    {

        static void Main1558(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int l = ReadInt();
            int n = ReadInt();
            int k = ReadInt();

            int[] arr = new int[n + 2];
            arr[n + 1] = l;
            for (int i = 1; i <= n; i++)
            {

                arr[i] = ReadInt();
            }

            int left = 1;
            int right = l;

            while (left <= right)
            {

                int mid = (left + right) >> 1;

                if (Chk(mid)) right = mid - 1;
                else left = mid + 1;
            }

            Console.Write(right + 1);

            bool Chk(int _dis)
            {

                int remain = _dis;
                int use = 0;

                for (int i = 0; i <= n; i++)
                {

                    int curDis = arr[i + 1] - arr[i];
                    if (curDis > _dis) return false;

                    if (remain < curDis)
                    {

                        use++;
                        remain = _dis;
                    }

                    remain -= curDis;
                }

                return use <= k;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {
                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

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
