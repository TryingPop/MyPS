using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 14
이름 : 배성훈
내용 : Barbells
    문제번호 : 13751번

    브루트포스 문제다.
    탐색 경우는 3^14 x p = 400만 x 14 = 5600만이다.
    브루트포스가 유효하다 판단했다.

    가능한 정답의 수를 보면 왼쪽에 넣는 것을 기준으로 2^14 x p < 2만 x p = 28만 갯수만큼 된다.
    배열로 접근하는 경우 정렬 후 중복처리 코드를 추가해줘야 한다.
    해시셋으로 할 경우 정렬만 해주면 되기에 해시셋에 저장했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1702
    {

        static void Main1702(string[] args)
        {

            int b, p;
            int[] bar, plates;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                HashSet<int> ret = new((1 << 14) * b);  // 왼쪽에 포함되는건 (2^14) * b

                DFS(0, 0, 0);

                foreach (int item in ret.OrderBy(x => x))
                {

                    sw.Write(item);
                    sw.Write('\n');
                }

                void DFS(int _dep, int _l, int _r)
                {

                    if (_dep == p)
                    {

                        if (_l != _r) return;

                        for (int i = 0; i < b; i++)
                        {

                            ret.Add(bar[i] + _l + _r);
                        }

                        return;
                    }

                    DFS(_dep + 1, _l, _r);
                    DFS(_dep + 1, _l + plates[_dep], _r);
                    DFS(_dep + 1, _l, _r + plates[_dep]);
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                b = ReadInt();
                p = ReadInt();

                bar = new int[b];
                for (int i = 0; i < b; i++)
                {

                    bar[i] = ReadInt();
                }

                plates = new int[p];
                for (int i = 0; i < p; i++)
                {

                    plates[i] = ReadInt();
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
}
