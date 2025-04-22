using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 23
이름 : 배성훈
내용 : 사탕 게임
    문제번호 : 3085번

    구현, 브루트포스 문제다
    코드가 길어져, 조건 하나를 체크 못해 계속해서 틀렸다
    이후 코드를 수정하는 중, 오류를 발견하고 수정하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0332
    {

        static void Main332(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());
            char[][] board = new char[n][];

            for (int i = 0; i < n; i++)
            {

                board[i] = sr.ReadLine().Trim().ToCharArray();
            }

            sr.Close();

            // 행의 최대 같은거 개수
            int[] rMax = new int[n];
            // 열의 최대 같은 개수
            int[] cMax = new int[n];
            for (int i = 0; i < n; i++)
            {

                int rChk = 1;
                int cChk = 1;
                for (int j = 0; j < n - 1; j++)
                {

                    if (board[i][j] == board[i][j + 1]) cChk++;
                    else
                    {

                        rMax[i] = rMax[i] < cChk ? cChk : rMax[i];
                        cChk = 1;
                    }

                    if (board[j][i] == board[j + 1][i]) rChk++;
                    else
                    {

                        cMax[i] = cMax[i] < rChk ? rChk : cMax[i];
                        rChk = 1;
                    }
                }

                if (cChk > 1) rMax[i] = rMax[i] < cChk ? cChk : rMax[i];
                if (rChk > 1) cMax[i] = cMax[i] < rChk ? rChk : cMax[i];
            }

            int ret = 1;
            bool notChange = true;
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n - 1; j++)
                {

                    int calc1, calc2;
                    if (board[i][j] != board[i][j + 1])
                    {

                        // 변환된 열과 행 조사
                        notChange = false;
                        Swap(i, j, i, j + 1);

                        calc1 = ChkLine(i, j);
                        calc2 = ChkLine(i, j + 1);

                        ret = ret < calc1 ? calc1 : ret;
                        ret = ret < calc2 ? calc2 : ret;

                        // 변환 안된 곳에서 최대값도 확인
                        for (int k = 0; k < n; k++)
                        {

                            if (k != i) ret = ret < rMax[k] ? rMax[k] : ret;
                            if (k != j && k != j + 1) ret = ret < cMax[k] ? cMax[k] : ret;
                        }
                        Swap(i, j, i, j + 1);
                    }

                    if (board[j][i] != board[j + 1][i])
                    {

                        // 변환된 행 부분 최대값 조사
                        notChange = false;
                        Swap(j, i, j + 1, i);

                        calc1 = ChkLine(j, i);
                        calc2 = ChkLine(j + 1, i);

                        ret = ret < calc1 ? calc1 : ret;
                        ret = ret < calc2 ? calc2 : ret;

                        // 변한 완된 부분 중 최대값 조사
                        for (int k = 0; k < n; k++)
                        {

                            if (k != i) ret = ret < cMax[k] ? cMax[k] : ret;
                            if (k != j && k != j + 1) ret = ret < rMax[k] ? rMax[k] : ret;
                        }
                        Swap(j, i, j + 1, i);
                    }
                }
            }

            if (notChange)
            {

                for (int i = 0; i < n; i++)
                {

                    ret = ret < rMax[i] ? rMax[i] : ret;
                    ret = ret < cMax[i] ? cMax[i] : ret;
                }
            }
            Console.WriteLine(ret);

            int ChkLine(int _r, int _c)
            {

                int chk1 = 1;
                int chk2 = 1;
                int ret = 1;
                for (int i = 0; i < n - 1; i++)
                {

                    if (board[_r][i] == board[_r][i + 1]) chk1++;
                    else
                    {

                        ret = ret < chk1 ? chk1 : ret;
                        chk1 = 1;
                    }

                    if (board[i][_c] == board[i + 1][_c]) chk2++;
                    else
                    {

                        ret = ret < chk2 ? chk2 : ret;
                        chk2 = 1;
                    }
                }

                if (chk1 > 1) ret = ret < chk1 ? chk1 : ret;
                if (chk2 > 1) ret = ret < chk2 ? chk2 : ret;

                return ret;
            }
            
            void Swap(int _r1, int _c1, int _r2, int _c2)
            {

                char temp = board[_r1][_c1];
                board[_r1][_c1] = board[_r2][_c2];
                board[_r2][_c2] = temp;
            }
        }
    }

#if other
using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

var n = ScanInt();
var map = new char[n, n];
for (int i = 0; i < n; i++)
{
	for (int j = 0; j < n; j++)
	{
		map[i, j] = (char)sr.Read();
	}
	sr.Read();
}

var max = 0;
for (int i = 0; i < n; i++)
{
	for (int j = 0; j < n; j++)
	{
		if (i < n - 1)
		{
			Swap(i, j, i + 1, j);
			UpdateMax(i, j);
			UpdateMax(i + 1, j);
			Swap(i, j, i + 1, j);
		}
		if (j < n - 1)
		{
			Swap(i, j, i, j + 1);
			UpdateMax(i, j);
			UpdateMax(i, j + 1);
			Swap(i, j, i, j + 1);
		}
	}
}
Console.Write(max);

void Swap(int r1, int c1, int r2, int c2)
{
	ref var a = ref map[r1, c1]; ref var b = ref map[r2, c2];
	(a, b) = (b, a);
}

void UpdateMax(int r, int c)
{
	var item = map[r, c];
	int start, end; //rowLeft item과 동일한 가장 왼쪽 셀 
	for (start = r; start >= 1 && map[start - 1, c] == item; start--) ;
	for (end = r + 1; end < n && map[end, c] == item; end++) ;
	max = Math.Max(end - start, max);

	for (start = c; start >= 1 && map[r, start - 1] == item; start--) ;
	for (end = c + 1; end < n && map[r, end] == item; end++) ;
	max = Math.Max(end - start, max);
}

int ScanInt()
{
	int c, n = 0;
	while (!((c = sr.Read()) is ' ' or '\n'))
	{
		if (c == '\r')
		{
			sr.Read();
			break;
		}
		n = 10 * n + c - '0';
	}
	return n;
}
#endif
}
