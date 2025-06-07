using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 7
이름 : 배성훈
내용 : 조각 움직이기
    문제번호 : 1035번

    브루트포스 문제다.
    DFS로 도형을 만들면 ㅏ와 같이 불가능한 경우가 있다.

    그렇다고 이어진 경우로 만든다면 다음과 같은 반례가 있다.
    ┌
    ┘
    
    그래서 5개를 선택하고 BFS로 이어졌는지 확인했다.
    이어진 경우 각 좌표를 이동시키며 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1684
    {

        static void Main1684(string[] args)
        {

            bool[][] visit;
            (int r, int c)[] pieces;
            int len;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = 255;
                (int r, int c)[] pos = new (int r, int c)[len];

                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
                Queue<(int r, int c)> q = new(5);


                SelectFirst();

                Console.Write(ret);

                bool ChkBFS()
                {

                    q.Enqueue((pos[0].r, pos[0].c));
                    visit[pos[0].r][pos[0].c] = false;
                    int cnt = 1;


                    while (q.Count > 0)
                    {

                        var node = q.Dequeue();
                        
                        for (int i = 0; i < 4; i++)
                        {

                            int nR = node.r + dirR[i];
                            int nC = node.c + dirC[i];

                            if (ChkInvalidPos(nR, nC) || !visit[nR][nC]) continue;
                            visit[nR][nC] = false;
                            cnt++;
                            q.Enqueue((nR, nC));
                        }
                    }

                    for (int i = 0; i < len; i++)
                    {

                        visit[pos[i].r][pos[i].c] = true;
                    }

                    return cnt == len;
                }

                void SelectFirst()
                {

                    for (int r = 0; r < 5; r++)
                    {

                        for (int c = 0; c < 5; c++)
                        {

                            visit[r][c] = true;
                            pos[0] = (r, c);
                            SelectDFS(r, c);
                            visit[r][c] = false;
                        }
                    }
                }

                void SelectDFS(int _r, int _c, int _dep = 1)
                {

                    if (_dep == len)
                    {

                        if (ChkBFS()) CalcDFS();
                        return;
                    }

                    for (int r = _r; r < 5; r++)
                    {

                        int s = r == _r ? _c + 1 : 0;
                        for (int c = s; c < 5; c++)
                        {

                            visit[r][c] = true;
                            pos[_dep] = (r, c);
                            SelectDFS(r, c, _dep + 1);
                            visit[r][c] = false;
                        }
                    }
                }

                void CalcDFS(int _dep = 0, int _state = 0, int _val = 0)
                {

                    if (_dep == len)
                    {

                        ret = Math.Min(ret, _val);
                        return;
                    }

                    for (int i = 0; i < len; i++)
                    {

                        if ((_state & (1 << i)) != 0) continue;

                        CalcDFS(_dep + 1, _state | (1 << i), _val + TaxiDis(i, pos[_dep].r, pos[_dep].c));
                    }
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= 5 || _c >= 5;

                int TaxiDis(int _idx, int _r, int _c)
                    => Math.Abs(pieces[_idx].r - _r) + Math.Abs(pieces[_idx].c - _c);
            }

            void Input()
            {

                visit = new bool[5][];
                pieces = new (int r, int c)[5];
                len = 0;

                for (int i = 0; i < 5; i++)
                {

                    visit[i] = new bool[5];
                    string temp = Console.ReadLine();

                    for (int j = 0; j < 5; j++)
                    {

                        if (temp[j] == '.') continue;
                        pieces[len++] = (i, j);
                    }
                }
            }
        }
    }

#if other
// #include<iostream>
using namespace std;
int abs(int x){
	return x<0 ? -x:x;
}
void swap(int x, int y, int arr[]){
	int tmp=arr[x];
	arr[x]=arr[y];arr[y]=tmp;
}
void swap1(int i, int x, int y, int arr[][2]){
	int tmp=arr[x][i];
	arr[x][i]=arr[y][i];arr[y][i]=tmp;
}
int mag(int x){
	return x==0 ? 0:x/abs(x);
}
int d[5][5], m[2];
int main(){
	ios::sync_with_stdio(false);
	cin.tie(0);
	cout.tie(0);
	int k=0, mm=125, minus=0;
	int p[5][2], dist[5], a[5][5], pt[5][2], loc[2];
	char chr;
	for(int i=0;i<5;++i)for(int j=0;j<5;++j){
		cin>>chr;
		if(chr=='*'){
			p[k][0]=i, p[k++][1]=j;
			for(int y=0;y<5;++y)for(int x=0;x<5;++x){
				d[y][x]+=(abs(j-x)+abs(i-y));
			}
		}
	}
	for(int i=0;i<5;++i)for(int j=0;j<5;++j){
		if(mm>d[i][j]){
			mm=d[i][j];m[0]=i;m[1]=j;
		}
	}
	for(int i=0;i<k;++i)	dist[i]=abs(p[i][0]-m[0])+abs(p[i][1]-m[1]);
	for(int x=0;x<k;++x)for(int y=0;y<k-1;++y){
		if(dist[y]>dist[y+1]){
			swap(y, y+1, dist);
			swap1(0, y, y+1, p);
			swap1(1, y, y+1, p);
		}
	}
	for(int i=0;i<k;++i){
		int tmp=0, dir;
		for(int x=0;x<5;++x)for(int y=0;y<5;++y)	a[x][y]=0;
		for(int x=0;x<k;++x)
			pt[x][0]=p[x][0]-m[0], pt[x][1]=p[x][1]-m[1];
		for(int j=0;j<k;++j){
			if(i==j)	continue;
			int py=pt[j][0], px=pt[j][1];
			loc[0]=m[0], loc[1]=m[1];
			if(px==0 && py==0)	continue;
			for(int x=0;x<dist[j];++x){
				int rl=abs(mag(px))*(1+a[loc[0]][loc[1]+mag(px)]);
				int ud=abs(mag(pt[j][0]))*(1+a[loc[0]+mag(pt[j][0])][loc[1]]);
				if(rl+ud==0)	break;
				else if(rl>ud)	dir=0;
				else if(rl<ud)	dir=1;
				else{
					for(int v=0;v<k;++v){
						if(mag(pt[v][1])==mag(px))	++rl;
						if(mag(pt[v][0])==mag(py))	++ud;
					}
					if(rl>ud)	dir=0;
					else	dir=1;
				}
				if(dir==0)	pt[j][1]-=mag(px), loc[1]+=mag(px);
				else	pt[j][0]-=mag(py), loc[0]+=mag(py);
				--tmp;
				if(a[loc[0]][loc[1]]==1)	continue;
				a[loc[0]][loc[1]]=1;
				pt[j][0]=pt[j][1]=0;
				break;
			}
		}
		if(tmp<minus)	minus=tmp;
	}
	cout<<mm+minus<<"\n";
	return 0;
}
#endif
}
