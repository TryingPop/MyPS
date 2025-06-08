using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 27
이름 : 배성훈
내용 : 배열 돌리기 1, 배열 돌리기 2
    문제번호 : 16926번, 16927번

    N * M 사각형을 끝부분으로 나눠서 풀었다
    6 * 6 의 경우를 보면
            0 0 0 0 0 0
            0 1 1 1 1 0
            0 1 2 2 1 0
            0 1 2 2 1 0
            0 1 1 1 1 0
            0 0 0 0 0 0
    다음 숫자로 0, 1, 2인 3개의 테두리로 나뉜다

    그리고 0, 1, 2를 기준으로 회전 시켰다
    1000회인 경우 1000회를 전부 회전 시킬 필요가 없다
    그룹에 속한 원소의 개수만큼 회전시키면 처음위치로 오기에
    그룹에 속한 원소의 개수로 나눈 나머지 만큼만 회전 시켰다
        해당부분 처음에 코드를 잘못짜서 1번 틀렸다
        몇 개 예제들어보면 8씩 차이나는 등차수열이됨을 알 수 있다
        위의 0은 20, 1은 12, 2는 4

    그리고 회전은 위쪽, 왼쪽, 아래쪽, 오른쪽으로 구역을 나눠서 했다
    재귀로 이동시켰다 많아야 4번 재귀한다

    그래서 제출하니 이상없이 통과했다

    2도 회전 범위만 10억까지 늘어났을 뿐, 같은 속도로 풀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0114
    {

        static void Main114(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            int rotate = ReadInt(sr);

            int[,] board = new int[n, m];
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m; j++)
                {

                    board[i, j] = ReadInt(sr);
                }
            }

            sr.Close();
            // 회전한 결과를 담을 배열
            int[,] ret = new int[n, m];
            // 그룹의 개수 min
            int len = n < m ? n : m;
            len /= 2;

            // 그룹별로 회전 실시
            for (int i = 1; i <= len; i++)
            {

                int ringLen = 2 * (n + m) + 4 - 8 * i;
                int curRot = rotate % ringLen;

                int row = n + 2 - 2 * i;
                int col = m + 2 - 2 * i;

                for (int r = i - 1; r < i - 1 + row; r++)
                {

                    int nextR = 0;
                    int nextC = 0;
                    if (r == i - 1 || r == i - 2 + row)
                    {

                        // 처음과 끝행인 경우
                        // 해당 행 전부 회전 시켜줘야하기에 남은 부분 회전
                        for (int c = i; c < i - 2 + col; c++)
                        {

                            GetPos(r, c, row, col, i, curRot, ref nextR, ref nextC);
                            ret[nextR, nextC] = board[r, c];
                        }
                    }

                    // 좌 우만 회전
                    GetPos(r, i - 1, row, col, i, curRot, ref nextR, ref nextC);
                    ret[nextR, nextC] = board[r, i - 1];
                    GetPos(r, col + i - 2, row, col, i, curRot, ref nextR, ref nextC);
                    ret[nextR, nextC] = board[r, col + i - 2];
                }
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < m; j++)
                    {

                        sw.Write(ret[i, j]);
                        sw.Write(' ');
                    }
                    sw.Write('\n');
                }
            }
        }

        static void GetPos(int _r, int _c, int _row, int _col, int _i, int _rotate, ref int _getR, ref int _getC)
        {

            if (_r == _i - 1 && _c != _i - 1)
            {

                // 윗 라인 이동
                _rotate -= _c - (_i - 1);
                _c = _i - 1;
                if (_rotate < 0)
                {

                    _c += -_rotate;
                    _rotate = 0;
                }
            }
            else if (_r == _row + _i - 2 && _c != _col + _i - 2)
            {

                // 밑 라인 이동
                _rotate -= _col + _i - 2 - _c;
                _c = _col + _i - 2;

                if (_rotate < 0)
                {

                    _c += _rotate;
                    _rotate = 0;
                }
            }
            else if (_c == _i - 1 && _r != _row + _i - 2)
            {

                // 왼쪽 이동
                _rotate -= _row + _i - 2 - _r;
                _r = _row + _i - 2;

                if (_rotate < 0)
                {

                    _r += _rotate;
                    _rotate = 0;
                }
            }
            else
            {

                // 오른쪽 이동
                _rotate -= _r - (_i - 1);
                _r = _i - 1;

                if (_rotate < 0)
                {

                    _r += -_rotate;
                    _rotate = 0;
                }
            }

            if (_rotate == 0)
            {

                // 전부 회전한 경우
                _getR = _r;
                _getC = _c;
            }
            // 아직 회전이 남은 경우
            else GetPos(_r, _c, _row, _col, _i, _rotate, ref _getR, ref _getC);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            StringBuilder sb = new StringBuilder();
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '),int.Parse);
            int n = arr[0]; int m = arr[1]; int r = arr[2];
            int[,] map = new int[n, m];
            for(int i = 0; i < n; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                for(int j = 0; j < m; j++)
                    map[i,j] = temp[j];
            }
            int max = Math.Min(n, m) / 2;
            spine(map, n - 1, m - 1, r, max);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    sb.Append($"{map[i, j]} "); 
                sb.Append('\n');
            }

            output.WriteLine(sb);

            input.Close();
            output.Close();
        }
        static void spine(int[,] map,int n,int m,int r,int max)
        {
            int start = 0;
            while(start < max)
            {
                List<int> temp = new List<int>();
                for(int i = start; i < n; i++)
                    temp.Add(map[i,start]);
                for (int i = start; i < m; i++)
                    temp.Add(map[n, i]);
                for (int i = n; i > start; i--)
                    temp.Add(map[i, m]);
                for (int i = m; i > start; i--)
                    temp.Add(map[start, i]);
                int[] t = new int[temp.Count];
                for (int i = 0; i < temp.Count; i++)
                    t[(i + r) % temp.Count] = temp[i];
                int index = 0;
                for (int i = start; i < n; i++)
                    map[i, start] = t[index++];
                for (int i = start; i < m; i++)
                    map[n, i] = t[index++];
                for (int i = n; i > start; i--)
                    map[i, m] = t[index++];
                for (int i = m; i > start; i--)
                    map[start, i] = t[index++];
                n--;
                m--;
                start++;
            }
        }
    }
}
#endif
}
