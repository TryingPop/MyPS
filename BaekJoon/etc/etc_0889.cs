using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 19 
이름 : 배성훈
내용 : 프렉탈 평면
    문제번호 : 1030번

    구현, 분할정복, 재귀 문제다
    각 크기별 색칠되는 시작지점과 끝점을 찾고,
    해당 구간에 r, c 가 포함되면 1, 
    아니면 다음 크기로 넘어간다
    다음 크기에 해당하는 r, c로 변형한다 이는 다음 전체 크기의 나머지가 된다
    예를들어 전체 크기가 9일 경우 r = 4는 3의 크기로 넘어갈 때 r = 4 % 3 = 1이다
*/

namespace BaekJoon.etc
{
    internal class etc_0889
    {

        static void Main889(string[] args)
        {

            StreamReader sr;

            int s, n, k, r1, r2, c1, c2;

            int[] nSquare;
            int[] start;
            int[] end;

            Solve();
            void Solve()
            {

                Input();

                SetArrs();

                GetRet();
            }

            void GetRet()
            {

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (int r = r1; r <= r2; r++)
                    {

                        for (int c = c1; c <= c2; c++)
                        {

                            if (Chk(r, c, s)) sw.Write('1');
                            else sw.Write('0');
                        }

                        sw.Write('\n');
                    }
                }
            }

            bool Chk(int _r, int _c, int _depth)
            {

                if (_depth == 0) return false;

                if (start[_depth] <= _r && _r <= end[_depth]
                    && start[_depth] <= _c && _c <= end[_depth]) return true;

                _depth--;
                return Chk(_r % nSquare[_depth], _c % nSquare[_depth], _depth);
            }

            void SetArrs()
            {

                nSquare = new int[s + 1];
                nSquare[0] = 1;

                start = new int[s + 1];
                end = new int[s + 1];

                for (int i = 1; i <= s; i++)
                {

                    nSquare[i] = nSquare[i - 1] * n;
                }

                int chk = (n - k) / 2;
                if (s == 0) return;
                start[1] = 1 + chk;
                end[1] = n - chk;

                for (int i = 2; i <= s; i++)
                {

                    start[i] = (start[i - 1] - 1) * n + 1;
                    end[i] = end[i - 1] * n;
                }

                for (int i = 1; i <= s; i++)
                {

                    start[i]--;
                    end[i]--;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                s = ReadInt();
                n = ReadInt();
                k = ReadInt();

                r1 = ReadInt();
                r2 = ReadInt();

                c1 = ReadInt();
                c2 = ReadInt();

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
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

public class Program
{
    public int s, N, K, R1, R2, C1, C2;
    public int[,] map;

    public static void Main(string[] args)
    {
        string input = Console.ReadLine();

        Program program = new Program();
        program.getInput(input.Split(' '));
        program.InitMapSize();
        program.SetFractalMap();
        program.PrintConsole();
    }

    public void getInput(string[] input)
    {
        int idx = 0;
        s = int.Parse(input[idx++]);
        N = int.Parse(input[idx++]);
        K = int.Parse(input[idx++]);
        R1 = int.Parse(input[idx++]);
        R2 = int.Parse(input[idx++]);
        C1 = int.Parse(input[idx++]);
        C2 = int.Parse(input[idx++]);
    }

    public void InitMapSize()
    {
        map = new int[R2 - R1 + 1, C2 - C1 + 1];
    }

    public void SetFractalMap()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (CheckIsInside(y + R1, x + C1, 1))
                {
                    map[y, x] = 1;
                }
            }
        }
    }

    bool CheckIsInside(int y, int x, int _s)
    {
        bool result;

        int outLength = Pow(N, _s);
        int inLength = (int)(outLength * (K / (float)N));
        int padding = (outLength - inLength) / 2;

        int x_margin = x - (x % outLength);
        int y_margin = y - (y % outLength);

        result = y - y_margin >= padding && y - y_margin < outLength - padding && x - x_margin >= padding && x - x_margin < outLength - padding;

        if (!result && _s < s)
        {
            result = CheckIsInside(y, x, _s + 1);
        }

        return result;
    }

    public void PrintConsole()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            StringBuilder lineString = new StringBuilder();
            for (int x = 0; x < map.GetLength(1); x++)
            {
                lineString.Append($"{map[y, x]}");
            }
            Console.WriteLine(lineString.ToString());
        }
    }

    int Pow(int n, int c)
    {
        int result = 1;

        for (int i = 0; i < c; i++)
        {
            result *= n;
        }

        return result;
    }
}

#elif other2
// #include <cstdio>
using namespace std;

void proc() {
	int s, n, k, r1, r2, c1, c2;
	scanf("%d %d %d %d %d %d %d", &s, &n, &k, &r1, &r2, &c1, &c2);

	for (int i = r1; i <= r2; ++i) {
		for (int j = c1; j <= c2; ++j) {
			bool fill = false;

			int ti = i, tj = j;
			for (int t = 0; t < s + 1; ++t) {
				if ((n - k) / 2 <= ti % n && ti % n < (n + k) / 2 && (n - k) / 2 <= tj % n && tj % n < (n + k) / 2) {
					fill = true;
					break;
				}
				ti /= n;
				tj /= n;
			}

			putchar(fill ? '1' : '0');
		}
		putchar('\n');
	}
}

int main() {
	//freopen("input.txt", "r", stdin);
	proc();
	return 0;
}
#elif other3
// #include<cstdio>
using namespace std;
int main(){
	int s,N,K,R1,R2,C1,C2,xt,yt,i,j,k;
	scanf("%d %d %d %d %d %d %d",&s,&N,&K,&R1,&R2,&C1,&C2);
	for(i=R1;i<=R2;i++){
		for(j=C1;j<=C2;j++){
			xt=i,yt=j;
			for(k=0;k<s;k++){
				if((xt%N >= (N-K)/2) && (xt%N <(N+K)/2)&&(yt%N>=(N-K)/2)&&(yt%N<(N+K)/2)){
					printf("1");
					break;
				}
				xt/=N;
				yt/=N;
			}
			if(k==s)
				printf("0");
		}
		printf("\n");
	}
}
#endif
}
