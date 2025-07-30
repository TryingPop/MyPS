using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 19
이름 : 배성훈
내용 : 레벨 햄버거
    문제번호 : 16974번

    dp, 분할 정복 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1777
    {

        static void Main1777(string[] args)
        {

            int n;
            long x;
            long[] p, b;    // p : 레벨 idx에 패티 개수, b : 레벨 idx에 번과 패티의 개수

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                Console.Write(DFS(n, x));

                long DFS(int _dep, long _x)
                {

                    if (_dep < 0 || _x <= 0) return 0;
                    // 현재 구간을 모두 덮으면 패티 개수 출력
                    else if (b[_dep] <= _x) return p[_dep];
                    // 왼쪽 구간 확인
                    // 앞의 빵 1개 제거
                    long ret = DFS(_dep - 1, _x - 1);

                    // 오른쪽 구간 확인
                    // 앞에 빵1개 + 레벨이 dep - 1 버거 존재 + 패티 1개 존재
                    // 그래서 -2 는 빵 1개와 패티 1개 그리고 레벨이 dep - 1 버거의 길이 b[dep - 1]제거
                    long r = _x - 2 - b[_dep - 1];
                    // +1은 패티 추가이고 길이가 있는 경우만 탐색한다.
                    if (r >= 0) ret += 1 + DFS(_dep - 1, r);

                    return ret;
                }
            }

            void SetArr()
            {

                b = new Int64[n + 1];
                p = new Int64[n + 1];
                Array.Fill(p, -1);

                CntPatty(n);

                CntBun();

                void CntBun()
                {

                    // 번 + 패티 개수 찾기
                    // 점화식을 이용
                    b[0] = 1;
                    for (int i = 1; i <= n; i++)
                    {

                        b[i] = 2 * b[i - 1] + 3;
                    }
                }

                long CntPatty(int _dep)
                {

                    // 패티 개수 찾기
                    ref long ret = ref p[_dep];
                    if (ret != -1) return ret;
                    else if (_dep == 0) ret = 1;
                    else ret = 2 * CntPatty(_dep - 1) + 1;

                    return ret;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();

                n = int.Parse(temp[0]);
                x = long.Parse(temp[1]);
            }
        }
    }
}
