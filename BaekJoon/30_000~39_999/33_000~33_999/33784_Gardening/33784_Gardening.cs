using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 15
이름 : 배성훈
내용 : Gardening
    문제번호 : 33784번

    기하학, 다각형의 넓이
    가질 수 있는 좌표의 범위가 1000 x 1000이다.

    점과 선으로 구분지어 좌표를 2배해도 
    2000 x 2000 = 400만이므로 브루트포스 방법이 유효하다.

    먼저 테두리로 도형을 잇는다.
    이후 명백히 외부에 점을 1개 찍는다.
    이로 BFS탐색을 해서 외부를 모두 확인한다.

    마지막으로 내부의 점의 갯수를 세어 풀었다.
    다만 끝부분에 탐지를 제대로 못해 여러번 틀렸다.

    다른 사람의 풀이를보니 신발끈 공식으로 접근했다.
    입력이 한붓긋기로 이어져 있어 쓸 수 있다...
    해당 방법이 더 나아보인다.
*/

namespace BaekJoon.etc
{
    internal class etc_1628
    {

#if GPT

        // 시간 초과 나는 코드!
        static void Main()
        {

            int m = int.Parse(Console.ReadLine());
            List<(int, int)> points = new List<(int, int)>();

            for (int i = 0; i < m; i++)
            {
                var input = Console.ReadLine().Split();
                int x = int.Parse(input[0]);
                int y = int.Parse(input[1]);
                points.Add((x, y));
            }

            // 격자 범위 정의
            int n = 1000;
            int count = 0;

            // 다각형 내부 판별 함수
            bool IsInsidePolygon(int x, int y)
            {
                bool inside = false;
                int j = m - 1;

                for (int i = 0; i < m; i++)
                {
                    int xi = points[i].Item1, yi = points[i].Item2;
                    int xj = points[j].Item1, yj = points[j].Item2;

                    bool intersect = ((yi > y) != (yj > y)) &&
                                     (x < (xj - xi) * (y - yi) / (yj - yi) + xi);
                    if (intersect) inside = !inside;

                    j = i;
                }

                return inside;
            }

            // 격자 탐색
            for (int x = 1; x <= n; x++)
            {
                for (int y = 1; y <= n; y++)
                {
                    if (IsInsidePolygon(x, y))
                        count++;
                }
            }

            Console.WriteLine(count);
        }

#else
        static void Main1628(string[] args)
        {

            int MAX = 2_007;
            int OUT = -1234;

            int n;
            int[][] pos;

            Input();

            SetFrame();

            BFS();

            GetRet();

            void GetRet()
            {

                int ret = 0;

                // 안쪽인지 바깥쪽인지 확인
                for (int i = 0; i <= MAX; i += 2)
                {

                    for (int j = 0; j <= MAX; j += 2)
                    {

                        if (pos[i][j] == OUT) continue;
                        ret++;
                    }
                }

                Console.WriteLine(ret);
            }

            void BFS()
            {

                // 위상으로 보면 내부, 외부로 나뉜다.
                // 그래서 명확히 가질 수 없는 점 외부에 점을 찍고 BFS탐색
                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };

                Queue<(int r, int c)> q = new(MAX << 3);
                pos[0][0] = OUT;
                q.Enqueue((0, 0));

                while (q.Count > 0)
                {

                    var node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || pos[nR][nC] != 0) continue;
                        pos[nR][nC] = OUT;
                        q.Enqueue((nR, nC));
                    }
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r > MAX || _c > MAX;
            }

            void SetFrame()
            {

                // 테두리 잇기
                for (int i = 0; i <= MAX; i++)
                {

                    bool flag = false;
                    for (int j = 0; j <= MAX; j++)
                    {

                        if (pos[i][j] == 1) flag = !flag;
                        else if (flag) pos[i][j] = 2;
                    }
                }

                for (int j = 0; j <= MAX; j++)
                {

                    bool flag = false;
                    for (int i = 0; i <= MAX; i++)
                    {

                        if (pos[i][j] == 1) flag = !flag;
                        else if (flag) pos[i][j] = 2;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                pos = new int[MAX + 2][];

                for (int i = 0; i < pos.Length; i++)
                {

                    pos[i] = new int[MAX + 2];
                }

                for (int i = 0; i < n; i++)
                {

                    int x = ReadInt() * 2 + 1;
                    int y = ReadInt() * 2 + 1;

                    pos[x][y] = 1;
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
#endif
    }

#if other

// #include <iostream>
using namespace std;

int N;
int P[1001][2];
int ans;

int main() {
    ios::sync_with_stdio(0);
    cin.tie(0);

    cin >> N;
    for (int i = 0; i < N; i++) cin >> P[i][0] >> P[i][1];
    P[N][0] = P[0][0], P[N][1] = P[0][1];
    for (int i = 0; i < N; i++) ans += P[i][0] * P[i + 1][1] - P[i + 1][0] * P[i][1];
    cout << abs(ans / 2);
}
#endif
}
