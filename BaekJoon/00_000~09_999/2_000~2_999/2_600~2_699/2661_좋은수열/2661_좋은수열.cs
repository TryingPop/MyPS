using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 7
이름 : 배성훈
내용 : 좋은수열
    문제번호 : 2661번

    백트래킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1751
    {

        static void Main1751(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int[] ret = new int[n];

            DFS();

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            for (int i = 0; i < n; i++)
            {

                sw.Write($"{ret[i]}");
            }

            bool DFS(int _dep = 0, int _prev = 0)
            {

                if (_dep == n) return true;

                for (int i = 1; i <= 3; i++)
                {

                    // 길이 1짜리는 _prev 변수를 넘김으로써 제거
                    if (i == _prev) continue;
                    ret[_dep] = i;

                    // 나쁜수열 인지 확인
                    if (ChkValid()) continue;
                    // 다음 깊이 확인
                    else if (DFS(_dep + 1, i)) return true;
                }

                return false;

                bool ChkValid()
                {

                    // 앞과 같은지 확인
                    // 길이 2부터 확인한다.
                    for (int len = 2; len < _dep; len++)
                    {

                        int idx1 = _dep;
                        int idx2 = _dep - len;

                        // 판별 불가능한 길이인 경우 -1, 
                        // 같은 경우 0,
                        // 다른 경우 1
                        int type = 0;

                        // 같은지 확인
                        for (int j = 0; j < len; idx1--, idx2--, j++)
                        {

                            if (idx2 >= 0 && ret[idx1] == ret[idx2]) continue;
                            type = idx2 < 0 ? -1 : 1;
                            break;
                        }

                        if (type == 0) return true;
                        else if (type == -1) break;
                    }

                    return false;
                }
            }
        }
    }
}
