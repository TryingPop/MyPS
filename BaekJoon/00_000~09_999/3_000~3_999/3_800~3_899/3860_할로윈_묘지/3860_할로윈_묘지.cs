using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 9
이름 : 배성훈
내용 : 할로윈 묘지
    문제번호 : 3860번

    벨만 - 포드 문제다.
    목적지에서 다른 방향으로 이동할 필요가 없고,
    귀신 구멍은 도착하면 강제로 타야 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1529
    {

        static void Main1529(string[] args)
        {

            // 3860번
            int INF = 2_000_000_000;
            string IMPO = "Impossible\n";
            string NEVER = "Never\n";

            int row, col;
            bool[] chkRip;
            int[][] newIdx;
            int[] dirR, dirC, dis;
            List<(int dst, int dis)>[]holeEdge;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            Init();

            while (Input())
            {

                GetRet();
            }

            void Init()
            {

                int MAX = 30;
                newIdx = new int[MAX][];
                int idx = 0;

                for (int i = 0; i < MAX; i++)
                {

                    newIdx[i] = new int[MAX];
                    for (int j = 0; j < MAX; j++)
                    {

                        newIdx[i][j] = idx++;
                    }
                }

                chkRip = new bool[idx];

                dis = new int[idx];

                holeEdge = new List<(int dst, int dis)>[idx];
                for (int i = 0; i < idx; i++)
                {

                    holeEdge[i] = new(1);
                }

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
            }

            bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= row || _c >= col;

            void GetRet()
            {

                int startIdx = newIdx[0][0];
                int endIdx = newIdx[row - 1][col - 1];

                BellmanFord();

                if (ChkNegativeCycle())
                    sw.Write(NEVER);
                else
                {

                    int ret = dis[newIdx[row - 1][col - 1]];
                    if (ret == INF) sw.Write(IMPO);
                    else sw.Write($"{ret}\n");
                }

                void BellmanFord()
                {

                    dis[startIdx] = 0;

                    int cycle = row * col - 1;

                    for (int i = 0; i < cycle; i++)
                    {

                        for (int r = 0; r < row; r++)
                        {

                            for (int c = 0; c < col; c++)
                            {

                                int curIdx = newIdx[r][c];
                                if (curIdx == endIdx || chkRip[curIdx] || dis[curIdx] == INF) continue;

                                if (holeEdge[curIdx].Count == 0)
                                {

                                    for (int dir = 0; dir < 4; dir++)
                                    {

                                        int nR = r + dirR[dir];
                                        int nC = c + dirC[dir];

                                        if (ChkInvalidPos(nR, nC)) continue;
                                        int nextIdx = newIdx[nR][nC];

                                        if (chkRip[nextIdx]) continue;
                                        dis[nextIdx] = Math.Min(dis[nextIdx], dis[curIdx] + 1);
                                    }
                                }
                                else
                                {

                                    int next = holeEdge[curIdx][0].dst;
                                    int nDis = dis[curIdx] + holeEdge[curIdx][0].dis;
                                    dis[next] = Math.Min(dis[next], nDis);
                                }
                            }
                        }
                    }
                }

                bool ChkNegativeCycle()
                {

                    for (int r = 0; r < row; r++)
                    {

                        for (int c = 0; c < col; c++)
                        {

                            int curIdx = newIdx[r][c];
                            if (curIdx == endIdx || chkRip[curIdx] || dis[curIdx] == INF) continue;

                            if (holeEdge[curIdx].Count == 0)
                            {

                                for (int dir = 0; dir < 4; dir++)
                                {

                                    int nR = r + dirR[dir];
                                    int nC = c + dirC[dir];

                                    if (ChkInvalidPos(nR, nC)) continue;
                                    int nextIdx = newIdx[nR][nC];
                                    if (chkRip[nextIdx]) continue;

                                    if (dis[curIdx] + 1 < dis[nextIdx]) return true;
                                }
                            }
                            else
                            {

                                int next = holeEdge[curIdx][0].dst;
                                int nDis = dis[curIdx] + holeEdge[curIdx][0].dis;
                                if (nDis < dis[next]) return true;
                            }
                        }
                    }

                    return false;
                }
            }

            bool Input()
            {

                row = ReadInt();
                col = ReadInt();

                if (row == 0 && col == 0) return false;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int idx = newIdx[r][c];
                        chkRip[idx] = false;
                        holeEdge[idx].Clear();
                        dis[idx] = INF;
                    }
                }

                int rip = ReadInt();
                for (int i = 0; i < rip; i++)
                {

                    int r = ReadInt();
                    int c = ReadInt();

                    int idx = newIdx[r][c];
                    chkRip[idx] = true;
                }

                int hole = ReadInt();

                for (int h = 0; h < hole; h++)
                {

                    int sR = ReadInt();
                    int sC = ReadInt();
                    int eR = ReadInt();
                    int eC = ReadInt();

                    int dis = ReadInt();

                    int fIdx = newIdx[sR][sC];
                    int tIdx = newIdx[eR][eC];

                    holeEdge[fIdx].Add((tIdx, dis));
                }

                return true;
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
                    bool positive = c != '-';

                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }
#if other
// #include <stdio.h>
// #include <vector>
using namespace std;
// #define INF 30*30*10000+1
int W,H,G,E;
struct info{
	int from, to, cost;
};
vector <info> edge;
int map[31][31],hole[31][31];
int dr[]={-1,1,0,0}, dc[]={0,0,-1,1};
int D[31*31];
int main()
{
	while(1)
	{
		scanf("%d%d",&W,&H);
		if(W==0&&H==0)break;
		edge.clear();
		for(int i=0;i<H;i++) 
			for(int j=0;j<W;j++) 
			{
				map[i][j] = i*W+j;
				hole[i][j] = 0;
				D[map[i][j]] = INF;
			}
				
		scanf("%d",&G);
		for(int i=0,x,y;i<G;i++)
		{
			scanf("%d%d",&x,&y);
			map[y][x]=-1;
		}
				
		scanf("%d",&E);
		for(int i=0,x1,y1,x2,y2,t;i<E;i++)
		{
			scanf("%d%d%d%d%d",&x1,&y1,&x2,&y2,&t);
			hole[y1][x1] = 1;
			edge.push_back({map[y1][x1],map[y2][x2],t});
		}
		
		for(int i=0;i<H;i++)
			for(int j=0;j<W;j++)
			{
				if(map[i][j]==-1 || hole[i][j])continue;
				for(int k=0,r,c;k<4;k++)
				{
					r = i + dr[k];
					c = j + dc[k];
					if(r<0||c<0||r==H||c==W||map[r][c]==-1) continue;
					edge.push_back({map[i][j],map[r][c],1});
				}
			}
		
		D[0] = 0;
		int updated=1;
		for(int v=0;v<W*H-1-G&&updated==1;v++)
		{
			updated=0;
			for(info e : edge)
			{
				if(D[e.from] == INF) continue;
				if(e.from == W*H-1) continue;
				if(D[e.to] > D[e.from] + e.cost)
				{
					updated=1;
					D[e.to] = D[e.from] + e.cost;
				}
			}
		}
		updated=0;
		for(info e : edge)
		{
			if(D[e.from] == INF) continue;
			if(e.from == W*H-1) continue;
			if(D[e.to] > D[e.from] + e.cost)
			{
				updated=1;
				break;
			}
		}
		/*
		for(int i=0;i<H;i++)
		{
			for(int j=0;j<W;j++)
			{
				printf("%d",map[i][j]);
				if(map[i][j]==-1) printf("[%d] ",-1);
				else printf("[%d] ",D[map[i][j]]);
			}
			printf("\n");
		}
		*/
		if(updated == 1) printf("Never\n");
		else if(D[H*W-1] == INF) printf("Impossible\n");
		else printf("%d\n",D[H*W-1]);
	}
}
#endif
}
