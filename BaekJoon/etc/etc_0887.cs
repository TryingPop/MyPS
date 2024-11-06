using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
날짜 : 2024. 8. 19
이름 : 배성훈
내용 : 벡터 매칭
    문제번호 : 1007번

    수학, 브루트포스 알고리즘 문제다
    벡터들을 보면 한쪽은 시작지점 s, 다른 쪽은 끝점 e로
    e - s 의 형태로 만들어진다

    그리고 반드시 점은 짝수개 주어지므로
    절반의 뺄거를 정하고 나머지를 더하는 식으로 DFS 함수를 구현했다
    이렇게 제출하니 이상없이 통과했다

    다만 0번부터 시작해 중복이 반이나 생겨 504ms 시간대에 통과했고,
    이후 1부터 시작해 중복을 제거하니 212ms 시간대로 절반 줄였다
*/

namespace BaekJoon.etc
{
    internal class etc_0887
    {

        static void Main887(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            (int x, int y)[] vec;
            bool[] use;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                while (test-- > 0)
                {

                    Input();

                    double ret = DFS(n / 2);

                    sw.Write($"{ret:0.00000000}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                vec = new (int x, int y)[20];
                use = new bool[20];
            }

            void Input()
            {

                n = ReadInt();

                for(int i = 0; i < n; i++)
                {

                    vec[i] = (ReadInt(), ReadInt());
                    use[i] = false;
                }
            }

            double DFS(int _depth, int _s = 1)
            {

                if (_depth == 0)
                {

                    int x = 0, y = 0;
                    for (int i = 0; i < n; i++)
                    {

                        if (use[i])
                        {

                            x -= vec[i].x;
                            y -= vec[i].y;
                        }
                        else
                        {

                            x += vec[i].x;
                            y += vec[i].y;
                        }
                    }

                    return GetDis(x, y);
                }

                double ret = 1e15;
                if (n <= _s) return ret;

                use[_s] = true;
                double chk = DFS(_depth - 1, _s + 1);
                use[_s] = false;

                ret = Math.Min(ret, chk);

                chk = DFS(_depth, _s + 1);
                ret = Math.Min(ret, chk);

                return ret;
            }

            double GetDis(int _x, int _y)
            {

                long x = 1L * _x * _x + 1L * _y * _y;
                return Math.Sqrt(x);
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool positive = c != '-';

                int ret = positive ? c - '0' : 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }

#if other
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
class main {
    static StreamReader rd = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
    static StreamWriter wr = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
    static StringBuilder std = new StringBuilder();
    static long minDist;
    static int[,] arr;
    static int n;
  static void Main(){
      int loop = int.Parse(rd.ReadLine());
      while(0 < loop)
      {
          n = int.Parse(rd.ReadLine());
          arr = new int[n,2];
          int ally = 0;
          int allx = 0;
          for(int i = 0; i < n; i++)
          {
              int[] data = Array.ConvertAll(rd.ReadLine().Split(), int.Parse);
              arr[i,0] = data[0];
              arr[i,1] = data[1];
              ally += data[0];
              allx += data[1];
          }
          minDist = long.MaxValue;
          bitmasking(0,ally,allx,0,0);
          std.Append(Math.Sqrt(minDist) + "\n");
          
          loop -= 1;
      }
      
      
      wr.Write(std);
      wr.Close();
  }
  static void bitmasking(int bit, int y, int x, int level, int min)
  {
      if(level == n)
      {
          long ly = y;
          long lx = x;
          long d = ly * ly + lx * lx;
          minDist = Math.Min(minDist, d);
          return;
      }
      for(int i = min; i < n; i++)
      {
          if((bit & (1 << i)) == 0)
          {
              int dy = y + (-2 * arr[i,0]);
              int dx = x + (-2 * arr[i,1]);
              bitmasking(bit | (1 << i), dy, dx, level + 2, i+1);
          }
      }
      
  }
}
#elif other2
// #include<cstdio>
// #include<cmath>
// #define inf 1e10
using namespace std;
int t,n,x[20],y[20],tx,ty;
double ans;
void combi(int cnt,int idx,int sx,int sy){
	if(cnt==0){
		double temp=sqrt(pow(tx-2*sx,2)+pow(ty-2*sy,2));
		if(temp<ans) ans=temp;
		return;
	}
	for(int i=idx;i<=n-cnt;++i){
		combi(cnt-1,i+1,sx+x[i],sy+y[i]);
	}
}
int main(){
	scanf("%d",&t);
	while(t--){
		ans=inf;tx=0;ty=0;
		scanf("%d",&n);
		for(int i=0;i<n;++i){
			scanf("%d %d",x+i,y+i);
			tx+=x[i];
			ty+=y[i];
		}
		combi(n/2-1,1,x[0],y[0]);
		printf("%.9lf\n",ans);
	}
}
#endif
}
