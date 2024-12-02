using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 2
이름 : 배성훈
내용 : 치즈
    문제번호 : 2638번

    구현, BFS, 시뮬레이션 문제다.
    아이디어는 다음과 같다.
    녹는 치즈를 찾는다 -> 치즈들을 일괄적으로 녹인다.
    -> 녹이면서 인접한 공기가 있는 장소에 실내 공기로 동기화한다.
    -> 동기화된 공기와 인접한 치즈 그리고 녹은 치즈와 인접한 치즈에 대해 녹이는거 판별을 한다.
    이렇게 시뮬레이션 돌렸다.

    여기서 동기화된 공기와 인접한 치즈를 고려안해 여러 번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_1141
    {

        static void Main1141(string[] args)
        {

            int OUT = 1;
            int CHEESE = 1;
            int AIR = 0;

            int row, col;

            int[][] board;
            int[][] group;
            bool[][] visit;

            Queue<(int r, int c)> q, cq, nq;
            int[] dirR, dirC;
            Solve();
            void Solve()
            {

                Input();

                InitAirGroup();

                GetRet();
            }

            void GetRet()
            {

                cq = new(row * col);
                nq = new(row * col);

                visit = new bool[row][];
                for (int r = 0; r < row; r++)
                {

                    visit[r] = new bool[col];
                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == AIR)
                        {

                            if (group[r][c] == OUT) visit[r][c] = true;
                        }
                        else if (ChkRemove(r, c))
                        {

                            cq.Enqueue((r, c));
                            visit[r][c] = true;
                        }
                    }
                }

                int ret = 0;
                while (cq.Count > 0)
                {

                    ret++;

                    // 치즈 녹이기
                    while (cq.Count > 0)
                    {

                        var node = cq.Dequeue();
                        board[node.r][node.c] = AIR;
                        group[node.r][node.c] = OUT;
                        nq.Enqueue(node);

                        for (int i = 0; i < 4; i++)
                        {

                            int nR = node.r + dirR[i];
                            int nC = node.c + dirC[i];

                            if (ChkInvalidPos(nR, nC) || visit[nR][nC] || board[nR][nC] == CHEESE) continue;
                            q.Enqueue((nR, nC));
                            ChkAirBFS(group[nR][nC]);
                            group[nR][nC] = OUT;
                        }
                    }

                    while (nq.Count > 0)
                    {

                        var node = nq.Dequeue();
                        for (int i = 0; i < 4; i++)
                        {

                            int nR = node.r + dirR[i];
                            int nC = node.c + dirC[i];

                            if (ChkInvalidPos(nR, nC) || board[nR][nC] == AIR || visit[nR][nC]) continue;

                            if (ChkRemove(nR, nC)) 
                            { 
                                
                                cq.Enqueue((nR, nC)); 
                                visit[nR][nC] = true;
                            }
                        }
                    }
                }

                Console.Write(ret);
            }

            bool ChkRemove(int _r, int _c)
            {

                int cnt = 0;
                for (int i = 0; i < 4; i++)
                {

                    int nR = _r + dirR[i];
                    int nC = _c + dirC[i];

                    if (ChkInvalidPos(nR, nC) || board[nR][nC] == CHEESE || group[nR][nC] != OUT) continue;
                    cnt++;
                }

                return 1 < cnt;
            }

            void InitAirGroup()
            {

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
                q = new(row * col);
                int g = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == CHEESE || group[r][c] != 0) continue;
                        group[r][c] = ++g;

                        q.Enqueue((r, c));
                        while (q.Count > 0)
                        {

                            var node = q.Dequeue();

                            for (int i = 0; i < 4; i++)
                            {

                                int nR = node.r + dirR[i];
                                int nC = node.c + dirC[i];

                                if (ChkInvalidPos(nR, nC) || board[nR][nC] == CHEESE || group[nR][nC] > 0) continue;
                                group[nR][nC] = g;
                                q.Enqueue((nR, nC));
                            }
                        }
                    }
                }
            }

            void ChkAirBFS(int _change)
            {

                while (q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();
                    bool flag = true;
                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];
                        if (ChkInvalidPos(nR, nC) || visit[nR][nC]) continue;

                        if (board[nR][nC] == AIR)
                        {

                            group[nR][nC] = OUT;
                            q.Enqueue((nR, nC));
                            visit[nR][nC] = true;
                        }
                        else if (flag)
                        {

                            nq.Enqueue((node.r, node.c));
                            flag = false;
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c) 
                => _r < 0 || _c < 0 || _r >= row || _c >= col;

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                group = new int[row][];
                for (int r = 0; r < row; r++) 
                {

                    board[r] = new int[col];
                    group[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                sr.Close();
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
    }

#if other
// #include <stdio.h>

int Y, X;
short Map[100][100], Map2[100][100];
int Date;
int C, M;

void dfs(int y, int x) {
	Map[y][x] = 0;
	M--;

	if (y - 1 >= 0 && Map[y - 1][x] == -1) {
		dfs(y - 1, x);		
	}
	if (y + 1 < Y && Map[y + 1][x] == -1) {
		dfs(y + 1, x);
	} 
	if (x - 1 >= 0 && Map[y][x - 1] == -1) {
		dfs(y, x - 1);
	} 
	if (x + 1 < X && Map[y][x + 1] == -1) {
		dfs(y, x + 1);
	}
}

void check() {
	for (int i = 1; i < Y - 1; i++) {
		for (int j = 1; j < X - 1; j++) {
			if (Map[i][j] == -1) {
				if ((i - 1 >= 0 && Map[i - 1][j] == 0) || (i + 1 < Y && Map[i+1][j] == 0) || (j - 1 >= 0 && Map[i][j-1] == 0) || (j+1 < X && Map[i][j+1] == 0)) {
					dfs(i, j);					
				}
			}
		}
	}
}

void melting() {
	for (int i = 0; i < Y; i++) {
		for (int j = 0; j < X; j++) {
			Map2[i][j] = Map[i][j];
		}
	}
		
	for (int i = 0; i < Y; i++) {
		for (int j = 0; j < X; j++) {
			int w = 0;
			if (Map[i][j] == 1) {
				if (i - 1 >= 0 && Map2[i - 1][j] == 0)
					w++;
				if (i + 1 < Y && Map2[i + 1][j] == 0)
					w++;
				if (j - 1 >= 0 && Map2[i][j - 1] == 0)
					w++;
				if (j + 1 < X && Map2[i][j + 1] == 0)
					w++;
				if (w > 1) {					
					Map[i][j] = 0;
					C--;					
				}
			}
		}
	}
	
	Date++;

	if (M) {
		check();
	}	

	if (C) {
		melting();
	}	
}

int main() {
	scanf("%d %d", &Y, &X);
	int t;
	for (int i = 0; i < Y; i++) {
		for (int j = 0; j < X; j++) {
			scanf("%d", &t);
			if (t) {
				Map[i][j] = t;
				C++;
			}
			else {
				Map[i][j] = -1;
				M++;
			}
		}
	}

	for (int j = 0; j < X - 1; j++)
		Map[0][j] = 0;	
	for (int i = 0; i < Y - 1; i++)
		Map[i][X - 1] = 0;
	for (int j = X - 1; j > 0; j--)
		Map[Y - 1][j] = 0;
	for (int i = Y - 1; i > 0; i--)
		Map[i][0] = 0;
	M -= 2 * Y + 2 * X - 4;

	if (M) {
		check();
	}
	melting();

	printf("%d", Date);	
	return 0;
}
#elif other2
using System.Runtime.InteropServices;
using static IO;
public class IO{
public static string? Cin()=>reader.ReadLine();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static void Cin(out int num)=>num=int.Parse(Cin());
public static void Cin(out string str)=>str=Cin();
public static void Cin(out string a,out string b,char c=' '){var r=Cin().Split(c);a=r[0];b=r[1];}
public static void Cin(out int[] numarr,char c= ' ')=>numarr=Array.ConvertAll(Cin().Split(c),int.Parse);
public static void Cin(out string[] strarr,char c=' ')=>strarr=Cin().Split(c);
public static void Cin(out double d)=>d=double.Parse(Cin());
public static void Cin(out string t,out int n){var s=Cin().Split();n=int.Parse(s[1]);t=s[0];}
public static void Cin(out int a,out int b,char c= ' '){Cin(out int[] s);(a,b)=(s[0],s[1]);}
public static void Cin(out int a,out int b,out int c,char e=' '){Cin(out int[] s);(a,b,c)=(s[0],s[1],s[2]);}
public static void Cin(out int a,out int b,out int c,out int d,char e = ' '){Cin(out int[] arr,e);(a,b,c,d)=(arr[0],arr[1],arr[2],arr[3]);}
public static void Cin(out int n,out string t) {var s=Cin().Split();n=int.Parse(s[0]);t=s[1];}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    public static void Coding() {
        Cin(out int height,out int width);
        int endwidth = width-1, endheigth = height-1;
        Stat[,] map = new Stat[width,height];
        for(int i=0;i<height;i++) {
            Cin(out string[] input);
            for(int j=0;j<width;j++) map[j,i] = input[j] is "0" ? Stat.InsideAir : Stat.Cheese;
        }
        // bfs용 큐
        Queue<(int x,int y)> queue = new();
        queue.Enqueue((0,0));
        // 업데이트가 필요한 치즈의 좌표
        HashSet<(int x,int y)> update = new();
        int time = 0;
        //시뮬레이션 시작
        for(;true;time++) {
            //외부 공기 주입
            while(queue.Count is not 0) {
                var ret = queue.Dequeue();
                //이미 방문했다면
                if (map[ret.x,ret.y] is Stat.OutsideAir) continue;
                //치즈라면 업뎃 요소에 넣고 끝
                if (map[ret.x,ret.y] is Stat.Cheese) {
                    update.Add((ret.x,ret.y));
                    continue;
                }
                //그럼 점령
                map[ret.x,ret.y] = Stat.OutsideAir;
                //그리고 전파.
                if (ret.x > 0) queue.Enqueue((ret.x-1,ret.y));
                if (ret.x < endwidth) queue.Enqueue((ret.x+1,ret.y));
                if (ret.y > 0) queue.Enqueue((ret.x,ret.y-1));
                if (ret.y < endheigth) queue.Enqueue((ret.x,ret.y+1));
            }
            //아니 외부공기에 닿은 치즈가 하나도 없다...? 그럼 다 없어진거잖아!
            if (update.Count is 0) {
                break;
            }
            //외부 공기에 닿은 치즈들 갱신
            foreach(var chs in update) {
                int contact = 0;
                if (map[chs.x-1,chs.y] is Stat.OutsideAir) contact++;
                if (map[chs.x+1,chs.y] is Stat.OutsideAir) contact++;
                if (map[chs.x,chs.y-1] is Stat.OutsideAir) contact++;
                if (map[chs.x,chs.y+1] is Stat.OutsideAir) contact++;
                //제거될 운명이라면
                if (contact >= 2) {
                    //제거하고 다시 큐에 넣어서 외부공기 전파 준비.
                    map[chs.x,chs.y] = Stat.InsideAir;
                    queue.Enqueue(chs);
                }
            }
            update.Clear();
        }

        writer.Write(time);
    }
}

enum Stat : byte {
    InsideAir,
    OutsideAir,
    Cheese
}
#endif
}
