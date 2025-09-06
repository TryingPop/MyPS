using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 5
이름 : 배성훈
내용 : Ezlulu
    문제번호 : 26492번

    그리디, 해구성하기, 덱 문제다.
    먼저 그리디로 가치가 큰 것으로 최대한 깨는게 최대임을 보장한다.
    그래서 가치가 큰 것에 대해 뒤에 남아 있는 것을 모두 깬다.
    
    이렇게 진행하면 최대값을 찾을 수 있다.
    이제 깨는 순서를 보면 마지막에 넣은 그릇이 남는다.
    이를 다음 가치가 높은 그릇으로 깨야한다.
    그래서 덱형식으로 저장하는데 깨져야 할것은 앞에 넣는다.
    반면 깨는 그릇은 뒤에 넣으면 최대값이 유지된다.

    다만 등식을 잘못 설정해 1번 틀렸고, 
        5
        1 2 3 4 5
    디버깅으로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1868
    {

        static void Main1868(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            int n = ReadInt();

            long[] arr = new long[n];
            int[] ord = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
                ord[i] = i;
            }

            Array.Sort(ord, (x, y) => arr[y].CompareTo(arr[x]));

            int e = n - 1;
            long ret1 = 0;
            int[] ret2 = new int[n];

            int idxF = n;
            int idxT = -1;

            for (int i = 0; i < n; i++)
            {

                if (e < ord[i]) continue;

                long cnt = e - ord[i] + (e == n - 1 ? 0 : 1);
                ret1 += cnt * arr[ord[i]];
                
                for (int j = e; j > ord[i]; j--)
                {

                    ret2[--idxF] = j;
                }
                ret2[++idxT] = ord[i];
                e = ord[i] - 1;
            }

            sw.Write(ret1);
            sw.Write('\n');

            for (int i = idxF; i < n; i++)
            {

                sw.Write($"{ret2[i] + 1} ");
            }

            for (int i = 0; i <= idxT; i++)
            {

                sw.Write($"{ret2[i] + 1} ");
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

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
