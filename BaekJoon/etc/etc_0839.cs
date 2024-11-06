using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 26
이름 : 배성훈
내용 : 벽록의 가면
    문제번호 : 31851번

    기하학, 브루트포스 알고리즘 문제다
    CCW 로 오목이 있는지 판정했다
    중복이 4번씩 탐색되어 효율적인 방법은 아니다
*/

namespace BaekJoon.etc
{
    internal class etc_0839
    {

        static void Main839(string[] args)
        {

            StreamReader sr;

            int n;
            (int x, int y)[] pos;

            int[] chk;
            bool[] use;
            int ret;

            Solve();
            void Solve()
            {

                Input();

                ret = 0;
                DFS(0);

                Console.Write(ret / 4);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                pos = new (int x, int y)[n];
                for (int i = 0; i < n; i++)
                {

                    pos[i] = (ReadInt(), ReadInt());
                }

                use = new bool[n];
                chk = new int[4];
                sr.Close();
            }

            int CCW(int _idx1, int _idx2, int _idx3)
            {

                long ccw = 1L * pos[_idx1].x * pos[_idx2].y
                    + 1L * pos[_idx2].x * pos[_idx3].y
                    + 1L * pos[_idx3].x * pos[_idx1].y;

                ccw -= 1L * pos[_idx2].x * pos[_idx1].y
                    + 1L * pos[_idx3].x * pos[_idx2].y
                    + 1L * pos[_idx1].x * pos[_idx3].y;

                if (ccw > 0) return 1;
                else if (ccw < 0) return -1;
                return 0;
            }

            void DFS(int _depth)
            {

                if (_depth == 4)
                {

                    if (CCW(chk[2], chk[3], chk[0]) <= 0
                        || CCW(chk[3], chk[0], chk[1]) <= 0
                        || CCW(chk[0], chk[1], chk[2]) <= 0
                        || CCW(chk[1], chk[2], chk[3]) <= 0) return;

                    ret++;
                    return;
                }

                for (int i = 0; i < n; i++)
                {

                    if (use[i]) continue;
                    use[i] = true;

                    chk[_depth] = i;
                    DFS(_depth + 1);
                    use[i] = false;
                }
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool negative = c == '-';

                int ret = negative ? 0 : c - '0';

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return negative ? -ret : ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
using ll=long long;

struct pt{ll x,y;};
vector<pt> v;

int ccw(pt a,pt b,pt c){
    if((b.x-a.x)*(c.y-a.y)-(c.x-a.x)*(b.y-a.y)>0) return 1;
    else return -1;
}

bool cmp(pt a,pt b){
    int res=ccw(v[0],a,b);
    if(res) return res>0;
    if(a.y==b.y) return a.x<b.x;
    return a.y<b.y;
}

int main() {
    cin.tie(NULL)->sync_with_stdio(false);
    int N,ans=0; cin>>N;
    v.resize(N);
    for(int i=0;i<N;i++) cin>>v[i].x>>v[i].y;
    for(int i=1;i<N;i++){
        if(v[i].y<v[0].y||v[i].y==v[0].y&&v[i].x<v[0].x)
            swap(v[i],v[0]);
    }
    sort(v.begin()+1,v.end(),cmp);
    for(int a=0;a<N;a++){
        for(int b=a+1;b<N;b++){
            for(int c=b+1;c<N;c++){
                for(int d=c+1;d<N;d++){
                    int ccw1=ccw(v[a],v[b],v[c]);
                    int ccw2=ccw(v[a],v[b],v[d]);
                    int ccw3=ccw(v[c],v[d],v[a]);
                    int ccw4=ccw(v[c],v[d],v[b]);
                    if(ccw1*ccw2>=0&&ccw3*ccw4>=0) ans++;
                }
            }
        }
    }
    cout<<ans;
}
#endif
}
