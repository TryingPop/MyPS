using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 7
이름 : 배성훈
내용 : 신아를 만나러
    문제번호 : 6146번

    BFS 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0869
    {

        static void Main869(string[] args)
        {

            int ADD = 501;

            StreamReader sr;
            Queue<(int r, int c)> q;
            int[][] map;

            Solve();
            void Solve()
            {

                Input();

                BFS();

                GetRet();
            }

            void GetRet()
            {

                Console.Write(map[ADD][ADD] - 1);
            }

            void BFS()
            {

                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };

                while (q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();

                    int cur = map[node.r][node.c];

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || map[nextR][nextC] != 0) continue;
                        map[nextR][nextC] = cur + 1;

                        q.Enqueue((nextR, nextC));
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r > 1_002 || _c > 1_002) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                q = new(65_536);

                map = new int[1_003][];
                for (int i = 0; i < map.Length; i++)
                {

                    map[i] = new int[1_003];
                }

                int r, c, len;
                r = ReadInt() + ADD;
                c = ReadInt() + ADD;
                len = ReadInt();

                q.Enqueue((r, c));
                map[r][c] = 1;

                for (int i = 0; i < len; i++)
                {

                    r = ReadInt() + ADD;
                    c = ReadInt() + ADD;

                    map[r][c] = -1;
                }
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool positive = c != '-';
                int ret = positive ? c - '0' : 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }


#if other
// #include<bits/stdc++.h>
using namespace std;
const int intmax = 2147483647;
int dx[] = { 1,0,-1,0 ,1,1,-1,-1 };
int dy[] = { 0,1,0,-1 ,1,-1,1,-1 };
// #define ll long long

int main() {

    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);

    int X,Y,N;
    cin>>X>>Y>>N;
    vector<vector<bool>> v(1001,vector<bool>(1001,true));
    X+=500;
    Y+=500;
    while(N--){
        int A,B;
        cin>>A>>B;
        A+=500; B+=500;
        v[A][B] = false;
    }

    queue<pair<int,int>> q;
    q.push({500,500});
    v[500][500] = false;
    q.push({-1,-1});

    int value = 0;

    while(!q.empty()){
        int a = q.front().first, b = q.front().second;
        q.pop();
        if(a==X && b==Y) break;
        if(a==-1){
            value ++;
            q.push({-1,-1});
            continue;
        }
        for(int k=0; k<4; k++){
            if(a+dx[k]<0 || a+dx[k]>1000 || b+dy[k]<0 || b+dy[k]>1000 || !v[a+dx[k]][b+dy[k]]) continue;
            v[a+dx[k]][b+dy[k]] = false;
            q.push({a+dx[k],b+dy[k]});
        }
    }
    cout<<value;
}
#endif
}
