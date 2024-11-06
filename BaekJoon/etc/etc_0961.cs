using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 11
이름 : 배성훈
내용 : 롤러코스터
    문제번호 : 2873번

    구현, 그리디, 해 구성하기 문제다
    우선 row 또는 col이 홀수면 ㄹ 형태를 적절히 뒤집으면서 이동하면 
    모든 칸을 지나게 할 수 있어 자명하다

    반면 row와 col이 짝수인 경우 특정 1칸을
    못지나게 경로를 만들 수 있다
    
    여기서 1칸의 행의 위치를 r, 열의 위치를 c라하면
    r + c가 홀수만 가능함을 보여야한다
    즉 짝수이면 1칸 제외는 불가능함을 보여야한다

    먼저 홀수인 경우에 가능한지 보자
    row, col의 시작 인덱스를 0부터 시작한다고 하고,
    r이 홀수인 경우만 보여도 충분하다
    c의 경우는 r과 c를 뒤집어 적용하면 같기 때문이다

    오른쪽으로 끝까지 가고, 한칸 아래로 이동,
    왼쪽끝까지 이동 다시 아래로 한칸 이동하면
    위에 두 행을 제거하면서 이동할 수 있다

    이를 r - 1 행이 제거 될 때까지 이를 진행한다
    그리고 r - 1에서는 밑 오른쪽 위 오른쪽 ... ㄹ을 뒤집은 형태로 진행한다
    여기서 다음부분이 건너뛰어야 하는 점이면 
    전체 맵의 크기가 짝수, 짝수이고 r이 홀수 이므로 
    오른쪽이 있으면 오른쪽 연산을 
    끝부분이라 오른쪽이 없으면 아래로 내려가면 된다

    그러면 행은 r + 1이고 오른쪽 끝에 도달할 수 있다
    이후 왼쪽끝, 아래, 오른쪽끝 아래 형식으로 이동하면
    row - 1, col - 1에서 종료되게 할 수 있다
    이렇게 방법이 존재하므로 홀수인 경우는 가능하다

    이제 r + c 가 짝수이면 불가능함을 보여야 한다
    약식으로 확인해보자

    왼쪽 끝에서 오른쪽 끝으로 가는데
    시작지점과 끝 점을 포함해 홀수개의 칸을 지나야한다

    상하좌우로 이동하므로 홀수 칸을 이동하면 (짝수, 짝수) 좌표가 되기 때문이다
    이후 짝수 좌표라 할것이다

    반면 짝수칸을 이동한 경우는 (홀수, 짝수) 또는 (짝수, 홀수) 좌표가된다
    이후 해당 좌표를 홀짝 좌표라 할것이다

    2칸이동마다 짝수 좌표 1개, 홀짝 좌표 1개씩 이동한다
    
    이러한 이동방법에서 카운팅으로 들어가면 짝수 좌표를 하나 이동 못하면
    홀짝좌표 하나도 덩달아 이동 못함을 알 수 있다
    이로 짝수 좌표 1개만 제외할 수 없고 짝수 칸에서는 반드시 1개를 제외해야 함과
    그리고 홀짝좌표 1개가 추가로 제외되기에 홀짝보다 최대점수도 낮아짐을 알 수 있다

    모든 수가 양수이므로 홀짝 좌표 1개를 제외하는게 최대임을 알 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0961
    {

        static void Main961(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int row, col;
            int[][] map;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                if ((row & 1) == 1) Type1();
                else if ((col & 1) == 1) Type2();
                else Type3();

                sw.Close();
            }

            void Type1()
            {

                for (int r = 0; r < row; r++)
                {

                    if (r > 0) sw.Write('D');

                    char key = (r & 1) == 0 ? 'R' : 'L';
                    for (int c = 1; c < col; c++)
                    {

                        sw.Write(key);
                    }
                }
            }

            void Type2()
            {

                for (int c = 0; c < col; c++)
                {

                    if (c > 0) sw.Write('R');

                    char key = (c & 1) == 0 ? 'D' : 'U';
                    for (int r = 1; r < row; r++)
                    {

                        sw.Write(key);
                    }
                }
            }

            void Type3()
            {

                (int r, int c) min = GetMin();

                int u = (min.r >> 1) << 1;
                for (int r = 0; r < u; r++)
                {

                    char key = (r & 1) == 0 ? 'R' : 'L';

                    for (int c = 1; c < col; c++)
                    {

                        sw.Write(key);
                    }

                    sw.Write('D');
                }

                for (int c = 0; c < min.c; c++)
                {

                    char key = (c & 1) == 0 ? 'D' : 'U';

                    sw.Write(key);
                    sw.Write('R');
                }

                for (int c = min.c + 1; c < col; c++)
                {

                    char key = (c & 1) == 1 ? 'D' : 'U';
                    sw.Write('R');
                    sw.Write(key);
                }

                for (int r = u + 2; r < row; r++)
                {

                    sw.Write('D');
                    char key = (r & 1) == 0 ? 'L' : 'R';
                    for (int c = 1; c < col; c++)
                    {

                        sw.Write(key);
                    }
                }
            }

            (int r, int c) GetMin()
            {

                int min = 1234;
                (int r, int c) ret = (0, 1);
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (((r + c) & 1) == 0 || min < map[r][c]) continue;
                        min = map[r][c];
                        ret = (r, c);
                    }
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                map = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    map[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        map[r][c] = ReadInt();
                    }
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using System.Text;

public class Program
{
    static void Main()
    {
        string[] rc = Console.ReadLine().Split(' ');
        int r = int.Parse(rc[0]), c = int.Parse(rc[1]);
        StringBuilder sb = new();
        string rs = new('R', c - 1), ls = new('L', c - 1), ds = new('D', r - 1), us = new('U', r - 1);
        if ((r & 1) == 1)
        {
            for (int i = 1; i <= r; i++)
            {
                if ((i & 1) == 1)
                    sb.Append(rs);
                else
                    sb.Append(ls);
                if (i < r)
                    sb.Append('D');
            }
            Console.Write(sb.ToString());
            return;
        }
        else if ((c & 1) == 1)
        {
            for (int i = 1; i <= c; i++)
            {
                if ((i & 1) == 1)
                    sb.Append(ds);
                else
                    sb.Append(us);
                if (i < c)
                    sb.Append('R');
            }
            Console.Write(sb.ToString());
            return;
        }
        int[,] joy = new int[r + 1, c + 1];
        (int x, int y) ignore = (0, 0);
        int min = int.MaxValue;
        for (int i = 1; i <= r; i++)
        {
            int[] row = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            for (int j = 1; j <= c; j++)
            {
                joy[i, j] = row[j - 1];
                if (((i & 1) == 1 && (j & 1) == 0 || (i & 1) == 0 && (j & 1) == 1) && joy[i, j] < min)
                {
                    min = joy[i, j];
                    ignore.x = j;
                    ignore.y = i;
                }
            }
        }
        bool ignored = false;
        for (int i = 1; i < r; i += 2)
        {
            if (ignore.y == i || ignore.y == i + 1)
            {
                int x = 1, y = i, dir = 0;
                for (int j = 0; j < c * 2 - 2; j++)
                {
                    if (dir == 0)
                    {
                        if (ignore.x == x && ignore.y == y + 1)
                        {
                            sb.Append('R');
                            x++;
                            dir--;
                        }
                        else
                        {
                            sb.Append('D');
                            y++;
                        }
                    }
                    else if (dir == 2)
                    {
                        if (ignore.x == x && ignore.y == y - 1)
                        {
                            sb.Append('R');
                            x++;
                            dir--;
                        }
                        else
                        {
                            sb.Append('U');
                            y--;
                        }
                    }
                    else
                    {
                        sb.Append('R');
                        x++;
                    }
                    dir = (dir + 1) & 3;
                }
                ignored = true;
            }
            else
            {
                if (ignored)
                {
                    sb.Append(ls);
                    sb.Append('D');
                    sb.Append(rs);
                }
                else
                {
                    sb.Append(rs);
                    sb.Append('D');
                    sb.Append(ls);
                }
            }
            if (i < r - 2)
                sb.Append('D');
        }
        Console.Write(sb.ToString());
    }
}
#elif other2
using System;
using System.IO;
using System.Text;

namespace acmicpc.net.Problems.Greedy
{
    public class P002873
    {
        private static StringBuilder ans;
        private static char R = 'R';
        private static char L = 'L';
        private static char U = 'U';
        private static char D = 'D';
        private static int[,] m;
        private static int Row;
        private static int Col;
        private static string nr;
        private static string nl;
        private static string nd;
        private static string nu;

        public static void Main(string[] args)
        {
            ans = new StringBuilder(1_100_000);

            int[] n = Array.ConvertAll(Console.ReadLine().Split(), x => int.Parse(x));
            Row = n[0];
            Col = n[1];
            m = new int[Row + 1, Col + 1];

            for (int i = 1; i <= Row; i++)
            {
                var value = Array.ConvertAll(Console.ReadLine().Split(), x => int.Parse(x));
                int length = value.Length;
                for (int j = 0; j < length; j++)
                {
                    m[i, j + 1] = value[j];
                }
            }

            nr = new string(R, Col - 1);
            nl = new string(L, Col - 1);
            nd = new string(D, Row - 1);
            nu = new string(U, Row - 1);

            if (Row % 2 == 1)
            {
                NormalRepeat(nr, nl, Row, D);
            }
            else if (Col % 2 == 1)
            {
                NormalRepeat(nd, nu, Col, R);
            }
            else
            {
                SpecialRepeat();
            }

            using (var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {
                sw.WriteLine(ans);
            }
        }

        private static void NormalRepeat(string odd, string even, int count, char move)
        {
            for (int i = 1; i <= count; i++)
            {
                if (i % 2 == 1)
                    ans.Append(odd);
                else
                    ans.Append(even);

                if (i < count)
                    ans.Append(move);
            }
        }

        private static void SpecialRepeat()
        {
            var min = Min();
            string odd = nr;
            string even = nl;

            for (int i = 1; i <= Row; i++)
            {
                if (i == min.r || (i % 2 == 1 && i + 1 == min.r))
                {
                    Avoid(min, i);
                    odd = nl;
                    even = nr;
                    i++;
                    continue;
                }

                ans.Append(i % 2 == 1 ? odd : even);

                if (i < Row)
                    ans.Append(D);
            }
        }

        private static void Avoid((int r, int c) min, int r)
        {
            int odd = 1;

            for (int i = 1; i <= Col; i++)
            {
                int add = i % 2 == 1 ? odd : -odd;

                if (i == min.c && r + add == min.r)
                {
                    odd *= -1;
                }
                else
                {
                    ans.Append(add > 0 ? D : U);
                    r += add;
                }

                if (i != Col)
                    ans.Append(R);
            }

            if (r < Row)
                ans.Append(D);
        }

        private static (int r, int c) Min()
        {
            int min = int.MaxValue;
            int minR = 0;
            int minC = 0;

            for (int i = 1; i <= Row; i++)
            {
                for (int j = 1; j <= Col; j++)
                {
                    if ((i + j) % 2 == 0)
                        continue;

                    int value = m[i, j];
                    if (value < min)
                    {
                        min = value;
                        minR = i;
                        minC = j;
                    }
                }
            }

            return (minR, minC);
        }
    }
}
#elif other3
// #include <iostream>
// #include <vector>

using namespace std;

namespace fio
{
	const int SIZE = 1 << 20;
	int nReadIndex = SIZE;
	char arRBuffer[SIZE]{};

	inline char ReadChar()
	{
		if (nReadIndex == SIZE)
		{
			fread(arRBuffer, 1, SIZE, stdin);
			nReadIndex = 0;
		}

		return arRBuffer[nReadIndex++];
	}

	inline int ReadInt()
	{
		char cRead = ReadChar();

		while ((cRead < 48 || cRead > 57) && cRead != '-')
			cRead = ReadChar();

		int nRet = 0;
		bool bNeg = (cRead == '-');

		if (bNeg)
			cRead = ReadChar();

		while (cRead >= 48 && cRead <= 57)
		{
			nRet = nRet * 10 + cRead - 48;
			cRead = ReadChar();
		}

		return bNeg ? -nRet : nRet;
	}

	int nWriteIndex;
	char arWBuffer[SIZE]{};

	inline int GetSize(int nWrite)
	{
		int nSize = 1;

		if (nWrite < 0)
			nWrite = -nWrite;

		while (nWrite >= 10)
		{
			nWrite /= 10;
			nSize++;
		}

		return nSize;
	}

	inline void Flush()
	{
		fwrite(arWBuffer, 1, nWriteIndex, stdout);
		nWriteIndex = 0;
	}

	inline void WriteChar(char cWrite)
	{
		if (nWriteIndex >= SIZE)
			Flush();

		arWBuffer[nWriteIndex++] = cWrite;
	}

	inline void WriteInt(int nWrite)
	{
		int nSize = GetSize(nWrite);

		if (nWriteIndex + nSize >= SIZE)
			Flush();

		if (nWrite < 0)
		{
			nWrite = -nWrite;
			arWBuffer[nWriteIndex++] = '-';
		}

		int nNext = nSize + nWriteIndex;

		while (nSize--)
		{
			arWBuffer[nWriteIndex + nSize] = nWrite % 10 + 48;
			nWrite /= 10;
		}

		nWriteIndex = nNext;
		WriteChar('\n');
	}
}

bool Solve();
int GetMax(int nData1, int nData2);
int GetMin(int nData1, int nData2);
int GetAbs(int nData);

int main()
{
	ios::sync_with_stdio(false);
	cin.tie(NULL);
	cout.tie(NULL);

	Solve();
}

bool Solve()
{
	bool bReturn = false;
	int R, C, nRow, nCol, nData, nMin, nRowIndex, nColIndex;
	char cPrint;

	do
	{
		R = fio::ReadInt();
		C = fio::ReadInt();

		nMin = 2000000000;

		for (int i = 0; i < R; i++)
		{
			for (int j = 0; j < C; j++)
			{
				nData = fio::ReadInt();

				if ((i + j) % 2 == 1)
				{
					if (nData < nMin)
					{
						nRow = i;
						nCol = j;
						nMin = nData;
					}
				}
			}
		}

		if (R % 2 == 1)
		{
			for (int i = 0; i < R; i++)
			{
				if (i % 2 == 0)
					cPrint = 'R';
				else
					cPrint = 'L';

				for (int j = 0; j < C - 1; j++)
					fio::WriteChar(cPrint);

				if (i != R - 1)
					fio::WriteChar('D');
			}
		}
		else if (C % 2 == 1)
		{
			for (int i = 0; i < C; i++)
			{
				if (i % 2 == 0)
					cPrint = 'D';
				else
					cPrint = 'U';

				for (int j = 0; j < R - 1; j++)
					fio::WriteChar(cPrint);

				if (i != C - 1)
					fio::WriteChar('R');
			}
		}
		else
		{
			nRowIndex = 0;

			while (nRowIndex + 2 <= nRow)
			{
				cPrint = 'R';

				for (int i = 0; i < C - 1; i++)
					fio::WriteChar(cPrint);

				fio::WriteChar('D');
				cPrint = 'L';

				for (int i = 0; i < C - 1; i++)
					fio::WriteChar(cPrint);

				fio::WriteChar('D');
				nRowIndex += 2;
			}

			if (nRow % 2 == 0)
			{
				nColIndex = 0;

				while (nColIndex + 2 < nCol)
				{
					fio::WriteChar('D');
					fio::WriteChar('R');
					fio::WriteChar('U');
					fio::WriteChar('R');
					
					nColIndex += 2;
				}

				fio::WriteChar('D');
				fio::WriteChar('R');

				nColIndex++;

				while (nColIndex + 2 < C)
				{
					fio::WriteChar('R');
					fio::WriteChar('U');
					fio::WriteChar('R');
					fio::WriteChar('D');

					nColIndex += 2;
				}

				nRowIndex += 2;

				while (nRowIndex < R)
				{
					fio::WriteChar('D');

					for (int i = 0; i < C - 1; i++)
						fio::WriteChar('L');

					fio::WriteChar('D');

					for (int i = 0; i < C - 1; i++)
						fio::WriteChar('R');

					nRowIndex += 2;
				}
			}
			else
			{
				nColIndex = 0;

				while (nColIndex + 2 <= nCol)
				{
					fio::WriteChar('D');
					fio::WriteChar('R');
					fio::WriteChar('U');
					fio::WriteChar('R');
					
					nColIndex += 2;
				}

				fio::WriteChar('R');
				fio::WriteChar('D');

				nColIndex++;

				while (nColIndex + 2 < C)
				{
					fio::WriteChar('R');
					fio::WriteChar('U');
					fio::WriteChar('R');
					fio::WriteChar('D');
					
					nColIndex += 2;
				}

				nRowIndex += 2;

				while (nRowIndex < R)
				{
					fio::WriteChar('D');

					for (int i = 0; i < C - 1; i++)
						fio::WriteChar('L');

					fio::WriteChar('D');

					for (int i = 0; i < C - 1; i++)
						fio::WriteChar('R');

					nRowIndex += 2;
				}
			}
		}

		fio::Flush();
			
		bReturn = true;
	} while (false);

	return bReturn;
}

int GetMax(int nData1, int nData2)
{
	return nData1 < nData2 ? nData2 : nData1;
}

int GetMin(int nData1, int nData2)
{
	return nData1 > nData2 ? nData2 : nData1;
}

int GetAbs(int nData)
{
	return nData < 0 ? -nData : nData;
}

#endif
}
