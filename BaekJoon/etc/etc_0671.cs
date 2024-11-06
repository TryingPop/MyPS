using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 2
이름 : 배성훈
내용 : 감시
    문제번호 : 15683번
 
    구현, 브루트포스, 시뮬레이션, 백트래킹 문제다
    방향을 하나씩 전환하면서 시뮬레이션 돌렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0671
    {

        static void Main671(string[] args)
        {

            StreamReader sr;

            int row, col;
            int[][] board;
            int[][] light;
            (int r, int c, int type, int dir)[] cctv;
            int len = 0;
            int[] dirR = { -1, 0, 1, 0 };
            int[] dirC = { 0, 1, 0, -1 };

            Solve();

            void Solve()
            {

                Input();
                int ret = DFS(0);
                Console.WriteLine(ret);
            }

            // CCTV 방향 전환
            int DFS(int _depth)
            {

                int ret = 100;
                if (_depth == len)
                {

                    ret = Count();
                    return ret;
                }

                for (int i = 0; i < 4; i++)
                {

                    cctv[_depth].dir = i;
                    CCTV(_depth, true);
                    int chk = DFS(_depth + 1);
                    ret = chk < ret ? chk : ret;
                    CCTV(_depth, false);
                }

                return ret;
            }

            // CCTV 작동
            void CCTV(int _idx, bool _isOn)
            {

                int dir = cctv[_idx].dir;
                switch (cctv[_idx].type)
                {

                    case 1:
                        Light(dir, cctv[_idx].r, cctv[_idx].c, _isOn);
                        break;

                    case 2:
                        Light(dir, cctv[_idx].r, cctv[_idx].c, _isOn);
                        Light((dir + 2) % 4, cctv[_idx].r, cctv[_idx].c, _isOn);
                        break;

                    case 3:
                        Light(dir, cctv[_idx].r, cctv[_idx].c, _isOn);
                        Light((dir + 1) % 4, cctv[_idx].r, cctv[_idx].c, _isOn);
                        break;

                    case 4:
                        Light(dir, cctv[_idx].r, cctv[_idx].c, _isOn);
                        Light((dir + 1) % 4, cctv[_idx].r, cctv[_idx].c, _isOn);
                        Light((dir + 3) % 4, cctv[_idx].r, cctv[_idx].c, _isOn);
                        break;

                    case 5:
                        Light(0, cctv[_idx].r, cctv[_idx].c, _isOn);
                        Light(1, cctv[_idx].r, cctv[_idx].c, _isOn);
                        Light(2, cctv[_idx].r, cctv[_idx].c, _isOn);
                        Light(3, cctv[_idx].r, cctv[_idx].c, _isOn);
                        break;
                }
            }

            // 감시 안되는 땅 세기
            int Count()
            {

                int ret = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (light[r][c] == 0) ret++;
                    }
                }

                return ret;
            }

            // 감시되는 곳 확인
            void Light(int _dir, int _r, int _c, bool _isOn = true)
            {

                int nextR, nextC;
                int i = 1;
                while (true)
                {

                    nextR = _r + i * dirR[_dir];
                    nextC = _c + i * dirC[_dir];
                    if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] == -1) break;
                    if (_isOn) light[nextR][nextC]++;
                    else light[nextR][nextC]--;
                    i++;
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                cctv = new (int r, int c, int type, int dir)[8];
                light = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    light[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = ReadInt();
                        if (cur == 6)
                        {

                            board[r][c] = -1;
                            light[r][c] = 1;
                        }
                        else if (cur > 0)
                        {

                            cctv[len++] = (r, c, cur, 0);
                            light[r][c] = 1;
                        }
                    }
                }

                sr.Close();
            }

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

#if other
using System;
using System.Collections.Generic;
using System.IO;
using Point = System.ValueTuple<int, int>;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

int n = ScanInt(), m = ScanInt();
var map = new int[n, m];
var cctvs = new List<CCTV>();
var isObserved = new bool[n, m];
var originBlind = 0;
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < m; j++)
    {
        var item = ScanInt();
        map[i, j] = item;
        if (item != 0)
        {
            isObserved[i, j] = true;
            if (item is >= 1 and <= 5)
                cctvs.Add(new(item, i, j));
        }
        else originBlind++;
    }
}
var dirs = new int[]
{
    default,
    0b0001,
    0b0101,
    0b0011,
    0b0111,
    0b1111,
};

var observedPoints = new List<Point>[cctvs.Count];
for (int i = 0; i < observedPoints.Length; i++)
    observedPoints[i] = new();
var ret = originBlind;
ChooseWhereToWatch(0);
Console.Write(ret);

void ChooseWhereToWatch(int index)
{
    if (index == cctvs.Count)
    {
        int blindSpot = originBlind;
        foreach (var o in observedPoints)
            blindSpot -= o.Count;
        ret = Math.Min(blindSpot, ret);
        return;
    }

    (var type, var row, var col) = cctvs[index];
    var observed = observedPoints[index];
    for (var i = 0; i < 4; i++)
    {
        for (int dir = dirs[type], j = i; dir != 0; dir >>= 1, j++)
        {
            if ((dir & 1) == 0) continue;
            (var wr, var wc) = GetDir(j);
            Observe(wr, wc);
        }
        ChooseWhereToWatch(index + 1);
        foreach ((var r, var c) in observed)
            isObserved[r, c] = false;
        observed.Clear();
    }

    void Observe(int wr, int wc)
    {
        var r = row;
        var c = col;
        while ((r += wr) != -1 && r != n &&
            (c += wc) != -1 && c != m &&
            map[r, c] != 6)
        {
            if (map[r, c] == 0 && !isObserved[r, c])
            {
                isObserved[r, c] = true;
                observed.Add((r, c));
            }
        }
    }
}

Point GetDir(int index)
{
    index = (index + 1) % 4;
    var minus = 1 - ((index >> 1) << 1);
    return (minus * ((index + 1) & 1), minus * (index & 1));
}

int ScanInt()
{
    int c, ret = 0;
    while (!((c = sr.Read()) is '\n' or ' ' or -1))
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        ret = 10 * ret + (c - '0');
    }
    return ret;
}

record struct CCTV(int Type, int Row, int Col);
#elif other2
namespace AlgorithmPractice
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			string[] RC = Console.ReadLine().Split(' ');

			int ROW = int.Parse(RC[0]);
			int COLUMN = int.Parse(RC[1]);

			int[,] maps = new int[ROW, COLUMN];
			List<(int, int, int)> CCTVList = new List<(int, int, int)>();
			// row, colum, cctv종류

			for(int i=0; i< ROW; i++)
			{
				string[] cols = Console.ReadLine().Split(' ');
				for(int k=0; k< COLUMN; k++)
				{
					int num = int.Parse(cols[k]);
					maps[i, k] = num;
					if(num >= 1 && num <= 5)
					{
						CCTVList.Add( (i, k, num) );
					}
				}
			}

			int count = CCTVList.Count;
			int minBlindArea = 500000000;

			BackTracking(0);
			Console.Write(minBlindArea);

			void BackTracking(int n)
			{
				// Base Condition.
				if(n == count)
				{
					int areaCnt = 0;
					for(int i=0; i<ROW; i++)
					{
						for(int k=0; k< COLUMN; k++)
						{
							if (maps[i,k] == 0)
							{
								areaCnt++;
							}
						}
					}
					if(areaCnt < minBlindArea)
					{
						minBlindArea = areaCnt;
					}
					return;
				}

				int x, y, cctv;
				(x, y, cctv) = CCTVList[n];
				MakeRay(x, y, cctv, n);
			}

			void MakeRay(int row, int column, int cctv, int n)
			{
				switch(cctv)
				{
					case 1: First(row, column, n); break;
					case 2: Second(row, column, n); break;
					case 3: Third(row, column, n); break;
					case 4: Fourth(row, column, n); break;
					case 5: Fifth(row, column, n); break;
				}
			}

			void First(int row, int column, int n)
			{
				for (int i = 0; i <4; i++)
				{
					RayCast(row, column, -1, (Direction) i);
					BackTracking(n + 1);
					RayCast(row, column, 1, (Direction) i);
				}
			}

			void Second(int row, int column, int n)
			{
				RayCast(row, column, -1, Direction.Up);
				RayCast(row, column, -1, Direction.Down);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Up);
				RayCast(row, column, 1, Direction.Down);

				RayCast(row, column, -1, Direction.Left);
				RayCast(row, column, -1, Direction.Right);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Left);
				RayCast(row, column, 1, Direction.Right);
			}

			void Third(int row, int column, int n)
			{
				RayCast(row, column, -1, Direction.Up);
				RayCast(row, column, -1, Direction.Right);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Up);
				RayCast(row, column, 1, Direction.Right);

				RayCast(row, column, -1, Direction.Right);
				RayCast(row, column, -1, Direction.Down);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Right);
				RayCast(row, column, 1, Direction.Down);

				RayCast(row, column, -1, Direction.Down);
				RayCast(row, column, -1, Direction.Left);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Down);
				RayCast(row, column, 1, Direction.Left);

				RayCast(row, column, -1, Direction.Left);
				RayCast(row, column, -1, Direction.Up);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Left);
				RayCast(row, column, 1, Direction.Up);
			}

			void Fourth(int row, int column, int n)
			{
				// 상 빼고 다
				RayCast(row, column, -1, Direction.Left);
				RayCast(row, column, -1, Direction.Right);
				RayCast(row, column, -1, Direction.Down);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Left);
				RayCast(row, column, 1, Direction.Right);
				RayCast(row, column, 1, Direction.Down);
				
				// 하 빼고 다
				RayCast(row, column, -1, Direction.Left);
				RayCast(row, column, -1, Direction.Right);
				RayCast(row, column, -1, Direction.Up);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Left);
				RayCast(row, column, 1, Direction.Right);
				RayCast(row, column, 1, Direction.Up);

				// 좌 빼고 다
				RayCast(row, column, -1, Direction.Up);
				RayCast(row, column, -1, Direction.Right);
				RayCast(row, column, -1, Direction.Down);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Up);
				RayCast(row, column, 1, Direction.Right);
				RayCast(row, column, 1, Direction.Down);

				// 우 빼고 다
				RayCast(row, column, -1, Direction.Left);
				RayCast(row, column, -1, Direction.Up);
				RayCast(row, column, -1, Direction.Down);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Left);
				RayCast(row, column, 1, Direction.Up);
				RayCast(row, column, 1, Direction.Down);
			}

			void Fifth(int row, int column, int n)
			{
				RayCast(row, column, -1, Direction.Left);
				RayCast(row, column, -1, Direction.Right);
				RayCast(row, column, -1, Direction.Up);
				RayCast(row, column, -1, Direction.Down);
				BackTracking(n + 1);
				RayCast(row, column, 1, Direction.Left);
				RayCast(row, column, 1, Direction.Right);
				RayCast(row, column, 1, Direction.Up);
				RayCast(row, column, 1, Direction.Down);
			}

			void RayCast(int row, int column, int value, Direction dir)
			{
				// 시작지점에서, 0보다 작은 수를 만나면, value만큼 더한다.
				// 백트래킹 구조상 0이 1이 되는 예외는 없다.
				int x = 0, y = 0;
				switch(dir)
				{
					case Direction.Left:
						(x, y) = (-1, 0);
						break;
					case Direction.Right:
						(x, y) = (1, 0);
						break;
					case Direction.Up:
						(x, y) = (0, 1);
						break;
					case Direction.Down:
						(x, y) = (0, -1);
						break;
				}

				while(true)
				{
					row += x;
					column += y;

					if (row < 0 || column < 0 || row >= ROW || column >= COLUMN || maps[row, column] == 6) return;

					if (maps[row, column] <= 0)
					{
						maps[row, column] += value;
					}
				}
			}
		}
	}

	public enum Direction
	{
		Up,
		Down,
		Left,
		Right
	}
}
#endif
}
