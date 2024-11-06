using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 30
이름 : 배성훈
내용 : 뱀
    문제번호 : 10875번

	구현, 시뮬레이션 문제다
	지나온 일일히 칸들을 비교하기에는 크기가 너무 크다(10^8 * 10^8)
	뱀을 선분으로 보고 선분교차 판정을 해야한다

	11번 틀렸는데 처음 2번은 구현을 잘못했나 싶어 틀렸고,
	3번째에 잘못된거 하나가 범위가 int를 초과할 수 있다고 판단했다
	1000 * 10^8 > int.MaxValue 이다

	이후 3번부터는 해당 부분을 수정했고,
	4 ~ 7번째는 입력 형식이 잘못되었는가 의심했다
	그래서 입력 방법을 split해서 읽었다
	그래도 틀리니 구현이 잘못되었음을 느꼈고, 처음부터 다시 구현했다
	
	로직을 뱀을 시간만큼 이동하고 해당 고개 돌리기에 죽었는지 판별했다
	먼저 이동 중에 몸통과 교차하는지 판정했다
	여기서 충돌하면 이동한 후 머리 부분과 충돌지점간의 최대 거리를 찾아야하는데,
	최소거리를 찾아 10번까지 틀렸다;

	이동 후 머리와 충돌지점의 최대 거리를 결과에서 빼주면 충돌한 시간이 되기 때문이다
	앞에서도 말했다싶이 이동하고 충돌 판정을 했다

	그리고 몸통과 충돌하는게 있으면 탈출하고 결과를 출력하고
	몸통과 충돌하지 않았다면 맵 밖에 벗어났는지 판별했다

	여기서 벗어났다면 이동 후 머리부분과 경계밖의 거리를 계산해 넘겨주고 결과에서 빼준다
	이렇게 이동하며 죽었는지 판별했다

	그래도 모든 명령 후 죽지 않는다면
	이제 해당 방향으로만 진행하기에 일렬로 쭉 맵범위를 벗어나게 이동시킨다
	그리고 죽음 판정을 해서 죽는 시간을 구했다
*/

namespace BaekJoon.etc
{
    internal class etc_0851
    {

        static void Main851(string[] args)
        {

            StreamReader sr;

            int l, n;

            long ret;

            (int x, int y)[] pos;
            int dir;
            int[] dirX;
            int[] dirY;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                sr.Close();
                Console.Write(ret);
            }

            void GetRet()
            {

                pos = new (int x, int y)[n + 2];
                int time, sub;


                ret = 0;

                for (int i = 0; i < n; i++)
                {

                    time = ReadInt();

                    pos[i + 1].x = pos[i].x + time * dirX[dir];
                    pos[i + 1].y = pos[i].y + time * dirY[dir];

                    ret += time;
                    if (ChkDead(i, out sub))
                    {

                        ret -= sub;
                        return;
                    }

                    dir += ReadDir();
                    if (dir == -1) dir = 3;
                    else if (dir == 4) dir = 0;
                }

                time = 2 * l + 5;

                pos[n + 1].x = pos[n].x + time * dirX[dir];
                pos[n + 1].y = pos[n].y + time * dirY[dir];

                ret += time;
                if (ChkDead(n, out sub))
                {

                    ret -= sub;
                }
            }

            bool ChkDead(int _idx, out int _sub)
            {

                if (ChkBody(_idx, out _sub)) return true;
                else if (ChkOut(_idx + 1, out _sub)) return true;

                return false;
            }

            bool ChkBody(int _i, out int _sub)
            {

                bool ret = false;

                _sub = -1;
                int iType = _i % 2;
                for (int j = 0; j < _i - 1; j++)
                {

                    int jType = j % 2;

                    if (IsNotCross(_i, j)) continue;
                    ret = true;

                    int chk;

                    if (iType == jType)
                    {

                        if (iType == 0) chk = Math.Max(GetDisX(_i + 1, j), GetDisX(_i + 1, j + 1));
                        else chk = Math.Max(GetDisY(_i + 1, j), GetDisY(_i + 1, j + 1));
                    }
                    else
                    {

                        if (iType == 0) chk = GetDisX(_i + 1, j);
                        else chk = GetDisY(_i + 1, j);
                    }

                    if (_sub < chk) _sub = chk;
                }

                return ret;
            }

            bool IsNotCross(int _i, int _j)
            {

                int iType = _i % 2;
                int jType = _j % 2;

                if (iType == jType)
                {

                    int min1, max1, min2, max2;
                    if (iType == 0)
                    {

                        if (pos[_i].y != pos[_j].y) return true;
                        min1 = Math.Min(pos[_i].x, pos[_i + 1].x);
                        max1 = Math.Max(pos[_i].x, pos[_i + 1].x);

                        min2 = Math.Min(pos[_j].x, pos[_j + 1].x);
                        max2 = Math.Max(pos[_j].x, pos[_j + 1].x);
                    }
                    else
                    {

                        if (pos[_i].x != pos[_j].x) return true;
                        min1 = Math.Min(pos[_i].y, pos[_i + 1].y);
                        max1 = Math.Max(pos[_i].y, pos[_i + 1].y);

                        min2 = Math.Min(pos[_j].y, pos[_j + 1].y);
                        max2 = Math.Max(pos[_j].y, pos[_j + 1].y);
                    }

                    return max2 < min1 || max1 < min2;
                }

                if (iType == 1)
                {

                    int temp = _i;
                    _i = _j;
                    _j = temp;
                }

                int min = Math.Min(pos[_i].x, pos[_i + 1].x);
                int max = Math.Max(pos[_i].x, pos[_i + 1].x);

                if (pos[_j].x < min || pos[_j].x > max) return true;

                min = Math.Min(pos[_j].y, pos[_j + 1].y);
                max = Math.Max(pos[_j].y, pos[_j + 1].y);

                return pos[_i].y < min || pos[_i].y > max;
            }

            bool ChkOut(int _idx, out int _addTime)
            {

                if (pos[_idx].x > l)
                {

                    _addTime = pos[_idx].x - l - 1;
                    return true;
                }
                else if (pos[_idx].x < -l)
                {

                    _addTime = -l - 1 - pos[_idx].x;
                    return true;
                }
                else if (pos[_idx].y > l)
                {

                    _addTime = pos[_idx].y - l - 1;
                    return true;
                }
                else if (pos[_idx].y < -l)
                {

                    _addTime = -l - 1 - pos[_idx].y;
                    return true;
                }

                _addTime = -1;
                return false;
            }

            int GetDisX(int _idx1, int _idx2)
            {

                return Math.Abs(pos[_idx1].x - pos[_idx2].x);
            }

            int GetDisY(int _idx1, int _idx2)
            {

                return Math.Abs(pos[_idx1].y - pos[_idx2].y);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                l = ReadInt();
                n = ReadInt();

                dir = 0;
                dirX = new int[4] { 1, 0, -1, 0 };
                dirY = new int[4] { 0, 1, 0, -1 };
            }

            int ReadDir()
            {

                int ret = sr.Read() == 'L' ? 1 : -1;
                if (sr.Read() == '\r') sr.Read();

                return ret;
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
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CodeTestCS
{
	class Program
	{
		static readonly StreamReader reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
		static readonly StreamWriter writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
		static readonly StringBuilder std = new StringBuilder();

    #region Member
		static long l;
		static long n;
		static readonly List<(long, bool)> turnList = new List<(long, bool)>();

		static long time = 0;
		static (long, long) current = (0, 0);
		static int direction = 0;
		static readonly long[] moveX = new long[] { -1, 1, 0, 0 };
		static readonly long[] moveY = new long[] { 0, 0, 1, -1 };
		static readonly List<((long, long), (long, long), int)> lineList = new List<((long, long), (long, long), int)>();
    #endregion

    #region Function
		static void InputCase()
		{
			var input = Array.ConvertAll(reader.ReadLine().Split(), long.Parse);
			l = input[0];

			input = Array.ConvertAll(reader.ReadLine().Split(), long.Parse);
			n = input[0];

			for(long i = 0; i < n; i++)
			{
				var inputChar = reader.ReadLine().Split();
				if (inputChar[1] == "L")
				{
					turnList.Add((long.Parse(inputChar[0]), true));
				}
				else
				{
					turnList.Add((long.Parse(inputChar[0]), false));
				}
			}
		}

		static bool MakeLine(long endTime)
		{
			// 시작은 한칸 다음
			var start = (current.Item1 + moveX[direction], current.Item2 + moveY[direction]);

			// 방향 전환이 더이상 없음을 의미 맵 끝까지 간다
			if (endTime < 0)
			{
				endTime = l * 2 + 1;
			}

			// 끝은 시간 만큼 진행 방향으로 이동
			var end = (current.Item1 + moveX[direction] * endTime, current.Item2 + moveY[direction] * endTime);

			// 충돌 체크
			var bumpTime = -1L;
			foreach (var line in lineList)
			{
				// 현재 가로 방향 진행
				if (direction < 2)
				{
					// 대상이 가로
					if (line.Item3 == 0)
					{
						// 가로는 Y가 같아야 충돌
						if (start.Item2 == line.Item1.Item2)
						{
							if (direction == 0)
							{
								if (start.Item1 >= line.Item1.Item1 && end.Item1 <= line.Item2.Item1)
								{
									var temp = start.Item1 - line.Item2.Item1 + 1;
									if (temp < 1)
									{
										temp = 1;
									}
									if (bumpTime < 0 || bumpTime > temp)
									{
										bumpTime = temp;
									}
								}
							}
							else
							{
								if (end.Item1 >= line.Item1.Item1 && start.Item1 <= line.Item2.Item1)
								{
									var temp = line.Item1.Item1 - start.Item1 + 1;
									if (temp < 1)
									{
										temp = 1;
									}
									if (bumpTime < 0 || bumpTime > temp)
									{
										bumpTime = temp;
									}
								}
							}
						}
					}
					// 대상이 세로
					else
					{
						// 세로는 Y가 충돌 선분 안에 있어야 함
						if (start.Item2 >= line.Item1.Item2 && start.Item2 <= line.Item2.Item2)
						{
							if (direction == 0)
							{
								if (line.Item1.Item1 >= end.Item1 && line.Item1.Item1 <= start.Item1)
								{
									var temp = start.Item1 - line.Item1.Item1 + 1;
									if (temp < 1)
									{
										temp = 1;
									}
									if (bumpTime < 0 || bumpTime > temp)
									{
										bumpTime = temp;
									}
								}
							}
							else
							{
								if (line.Item1.Item1 >= start.Item1 && line.Item1.Item1 <= end.Item1)
								{
									var temp = line.Item1.Item1 - start.Item1 + 1;
									if (temp < 1)
									{
										temp = 1;
									}
									if (bumpTime < 0 || bumpTime > temp)
									{
										bumpTime = temp;
									}
								}
							}
						}
					}
				}
				// 현재 세로 방향 진행
				else
				{
					// 대상이 세로
					if (line.Item3 == 1)
					{
						// 세로는 X가 같아야 충돌
						if (start.Item1 == line.Item1.Item1)
						{
							if (direction == 2)
							{
								if (end.Item2 >= line.Item1.Item2 && start.Item2 <= line.Item2.Item2)
								{
									var temp = line.Item1.Item2 - start.Item2 + 1;
									if (temp < 1)
									{
										temp = 1;
									}
									if (bumpTime < 0 || bumpTime > temp)
									{
										bumpTime = temp;
									}
								}
							}
							else
							{
								if (start.Item2 >= line.Item1.Item2 && end.Item2 <= line.Item2.Item2)
								{
									var temp = start.Item2 - line.Item2.Item2 + 1;
									if (temp < 1)
									{
										temp = 1;
									}
									if (bumpTime < 0 || bumpTime > temp)
									{
										bumpTime = temp;
									}
								}
							}
						}
					}
					// 대상이 가로
					else
					{
						// 가로는 X가 충돌 선분 안에 있어야 함
						if (start.Item1 >= line.Item1.Item1 && start.Item1 <= line.Item2.Item1)
						{
							if (direction == 2)
							{
								if (line.Item1.Item2 >= start.Item2 && line.Item1.Item2 <= end.Item2)
								{
									var temp = line.Item1.Item2 - start.Item2 + 1;
									if (temp < 1)
									{
										temp = 1;
									}
									if (bumpTime < 0 || bumpTime > temp)
									{
										bumpTime = temp;
									}
								}
							}
							else
							{
								if (line.Item1.Item2 >= end.Item2 && line.Item1.Item2 <= start.Item2)
								{
									var temp = start.Item2 - line.Item1.Item2 + 1;
									if (temp < 1)
									{
										temp = 1;
									}
									if (bumpTime < 0 || bumpTime > temp)
									{
										bumpTime = temp;
									}
								}
							}
						}
					}
				}
			}

			// 충돌 했으면 시간 갱신 후 종료
			if (bumpTime > 0)
			{
				time += bumpTime;
				return true;
			}

			// 시간 증가 및 이동
			current = end;
			time += endTime;

			// 방향 별로 저장 (작은 좌표에서 큰 좌표 순으로 저장)
			switch (direction)
			{
				case 0:
					lineList.Add((end, start, 0));
					break;
				case 1:
					lineList.Add((start, end, 0));
					break;
				case 2:
					lineList.Add((start, end, 1));
					break;
				case 3:
					lineList.Add((end, start, 1));
					break;
			}
			return false;
		}

		static int Turnning(bool wise)
		{
			if (wise)
			{
				switch (direction)
				{
					case 0:
						return 3;
					case 1:
						return 2;
					case 2:
						return 0;
					case 3:
						return 1;
				}
			}
			else
			{
				switch (direction)
				{
					case 0:
						return 2;
					case 1:
						return 3;
					case 2:
						return 1;
					case 3:
						return 0;
				}
			}
			return direction;
		}

		static long FlowTime()
		{
			for (int i = 0; i < n; i++)
			{
				var turn = turnList[i];
				if (MakeLine(turn.Item1))
				{
					return time;
				}
				else
				{
					direction = Turnning(turn.Item2);
				}
			}
			if (MakeLine(-1))
			{
				return time;
			}
			return -1;
		}

		static void AddBaseLine()
		{
			// 시작 방향
			current = (0, 0);
			direction = 1;
			lineList.Clear();

			// 시작 지점 추가
			lineList.Add(((0, 0), (0, 0), 0));

			// 맵 테두리 추가
			lineList.Add(((-l - 1, l + 1), (l + 1, l + 1), 0));
			lineList.Add(((-l - 1, -l - 1), (l + 1, -l - 1), 0));
			lineList.Add(((-l - 1, -l - 1), (-l - 1, l + 1), 1));
			lineList.Add(((l + 1, -l - 1), (l + 1, l + 1), 1));
		}
    #endregion

		static void Main(string[] args)
		{
			InputCase();
			AddBaseLine();
			std.Append(FlowTime());
			writer.Write(std);
			writer.Close();
		}
	}
}
#endif
}
