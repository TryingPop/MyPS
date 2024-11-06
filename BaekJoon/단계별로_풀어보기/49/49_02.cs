using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 8
이름 : 배성훈
내용 : 고속도로
    문제번호 : 10254번

    //// 해결되었다! ////
    현재 1%에서 계속 틀린다;
    49_01으로 판별한 결과 컨벡스헐 자체의 문제는 아니다!
    ... CCW의 오버플로우가 문제였다!
    long ccw = _a.x * _b.y 이고, x와 y는 int 자료형이다
    그리고 입력 값은 1000만까지 온다
    코드가 실행될 때, _a.x * _b.y는 int로 연산되기에 오버플로우가 발생할 수 있다
    //////////////////////

    그리고, 회전하는 캘리퍼스 알고리즘에서 ccw 부분에서 idx를 잘못해서 또 많이 틀렸다
    컨벡스 헐의 idx1과 idx2 점에 대해 각도가 작은쪽에 옮기는 연산을 한다

    여기서 옮기는 방법은 idx1, idx2을 고정시키고 idx1, idx2를 각각 지나는 평행인 l1, l2 선분을 선택한다
    여기서 l1, l2 선분은 컨벡스 헐로 나온 다각형의 내부 영역을 지나면 안된다!
    이렇게 직선 l1, l2가 주어졌을 때, l1, idx1, idx1 + 1 이 이루는 각도와,
    l2, idx2, idx2 + 1이 이루는 각도에 대해 더 작은 각도의 점의 idx + 1한다
    이렇게 N회 반복을 돌린다 -> 그러면, idx1, idx2의 점 중에 최대 지름이 포함되는게 해당 알고리즘의 주된 원리이다
    이렇게 각도를 작은 것을 찾을 때 외적을 이용한다

    idx1에서 idx1 + 1을 지나는 벡터를 n1, idx2에서 idx2 + 1인 벡터를 n2라하면
    n1, n2를 ccw하면 시계방향일 때, l1, idx1, idx1 + 1 의 각도가 작은 것과 일치한다
    이렇게 진행하면 정답이 나온다

    이렇게 제출하니 636ms에 통과했다
*/

namespace BaekJoon._49
{
    internal class _49_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int test;

            int len;
            Point[] pos;

            int idx;
            int[] arr;

            int ret1;
            int ret2;

            Solve();

            void Solve()
            {

                Input();

                while (test-- > 0)
                {


                    ConvexHull();
                    RotatingCalipers();

                    sw.Write($"{pos[arr[ret1]].X} {pos[arr[ret1]].Y} {pos[arr[ret2]].X} {pos[arr[ret2]].Y}\n");
                }

                sr.Close();
                sw.Close();
            }

            void ConvexHull()
            {

                len = ReadInt();

                int minIdx = 0;
                int minY = 10_000_000;
                int minX = 10_000_000;

                for (int i = 0; i < len; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    pos[i].Set(x, y);
                    if (y < minY)
                    {

                        minIdx = i;
                        minY = y;
                        minX = x;
                    }
                    else if (y == minY && x < minX)
                    {

                        minIdx = i;
                        minX = x;
                    }
                }

                SetMin(minIdx);

                Array.Sort(pos, 1, len - 1);

                idx = 0;
                Push(0);
                Push(1);

                for (int i = 2; i < len; i++)
                {

                    while (idx >= 2 && Point.CCW(pos[arr[idx - 2]], pos[arr[idx - 1]], pos[i]) <= 0)
                    {

                        idx--;
                    }

                    Push(i);
                }
            }

            void RotatingCalipers()
            {

                int idx1 = 0;
                int idx2 = 0;

                for (int i = 0; i < idx; i++)
                {

                    if (pos[arr[idx1]].X > pos[arr[i]].X) idx1 = i;
                    if (pos[arr[idx2]].X < pos[arr[i]].X) idx2 = i;
                }

                ret1 = idx1;
                ret2 = idx2;

                long maxDis = Point.Dis(pos[arr[idx1]], pos[arr[idx2]]);
                for (int i = 0; i < idx; i++)
                {

                    Point p1 = pos[arr[idx1]];
                    Point p2 = pos[arr[idx2]];

                    Point n1 = pos[arr[(idx1 + 1) % idx]];
                    Point n2 = pos[arr[(idx2 + 1) % idx]];

                    n1.Sub(p1);
                    n2.Sub(p2);

                    if (Point.CCW(Point.ZERO, n1, n2) < 0) idx1 = (idx1 + 1) % idx;
                    else idx2 = (idx2 + 1) % idx;

                    long curDis = Point.Dis(pos[arr[idx1]], pos[arr[idx2]]);
                    if (maxDis < curDis)
                    {

                        maxDis = curDis;
                        ret1 = idx1;
                        ret2 = idx2;
                    }
                }
            }

            void SetMin(int _idx)
            {

                Point temp = pos[0];
                pos[0] = pos[_idx];
                pos[_idx] = temp;

                Point.SetFix(pos[0]);
            }

            void Push(int _n)
            {

                arr[idx++] = _n;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                test = ReadInt();

                pos = new Point[200_000];
                arr = new int[200_000];
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == '\r') c = sr.Read();
                if (c == -1 || c == ' ' || c == '\n') return 0;

                bool plus = c != '-';
                int ret;
                if (plus) ret = c - '0';
                else ret = 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }

        struct Point : IComparable<Point>
        {

            private static Point fix;
            public static Point ZERO = new(0, 0);
            private long x = 0;
            private long y = 0;

            public long X => x;
            public long Y => y;

            public Point(long _x, long _y)
            {

                x = _x;
                y = _y;
            }

            public void Set(long _x, long _y)
            {

                x = _x;
                y = _y;
            }

            public static void SetFix(Point _p)
            {

                fix.Set(_p.x, _p.y);
            }

            public static long Dis(Point _a, Point _b)
            {

                long x = _a.x - _b.x;
                x = x * x;

                long y = _a.y - _b.y;
                y = y * y;

                long ret = x + y;
                return ret;
            }

            public static int CCW(Point _a, Point _b, Point _c)
            {

                long ccw = _a.x * _b.y;
                ccw += _b.x * _c.y;
                ccw += _c.x * _a.y;

                ccw -= _b.x * _a.y;
                ccw -= _c.x * _b.y;
                ccw -= _a.x * _c.y;

                if (ccw > 0) return 1;
                if (ccw < 0) return -1;
                return 0;
            }

            public int CompareTo(Point _other)
            {

                int ret = CCW(this, _other, fix);
                if (ret == 0) return Dis(this, fix).CompareTo(Dis(_other, fix));

                return -ret;
            }

            public void Sub(Point _sub)
            {

                x = x - _sub.x;
                y = y - _sub.y;
            }
        }
    }

#if other
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
using System.Collections;

class Programs
{
    static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), Encoding.Default);
    static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), Encoding.Default);
    class Vector: IComparer,IComparable
    {
        public long x, y;
       public  static Vector operator-(Vector a, Vector b)
        {
            Vector r=new Vector(b.x-a.x,b.y-a.y);
            return r;
        }
        public Vector (long x=0,long y=0)
        {
            this.x = x;
            this.y = y;
        }
    
        public int Compare(object x, object y)
        {
            Vector a = (Vector)x;
            Vector b = (Vector)y;

            long r = CCW(arr[0], a, b);
            if (r == 0)
            {
                if (Math.Abs(a.x+ a.y)  <Math.Abs(b.x + b.y) )
                {
                    return -1;
                }
            }
            else if (r > 0)
            {
                return -1;
            }
            return 1;
        }

        public int CompareTo(object obj)
        {
            Vector a = (Vector)this;
            Vector b = (Vector)obj;

            if (a.y == b.y)
            {
                return a.x.CompareTo(b.x);
            }
            return a.y.CompareTo(b.y);
        }
    }
    static long CCW(Vector a, Vector b, Vector c)
    {
        Vector AB = b - a;
        Vector BC = c - b;
        long r = a.x * b.y + b.x * c.y + c.x * a.y - (a.y * b.x + b.y * c.x + c.y * a.x);
        //long r = AB.x * BC.y - AB.y * BC.x;
        return r;
    }
    static long CCW(Vector a, Vector b)
    {
        long r = a.x * b.y - a.y * b.x;
        return r;
    }
    static Vector[] arr=new Vector[200000];
    static Vector[] cv=new Vector[200000];
    static void Main(String[] args)
    {
        int t= int.Parse(sr.ReadLine());

        for (int a = 0; a < t; a++)
        {
            long aX=0, aY=0, aX2=0, aY2=0;
            long maxDistance = 0;
        int n = int.Parse(sr.ReadLine());
            Stack<int> st = new Stack<int>();
            st.Clear();
            for (int i = 0; i < n; i++)
            {
                string[] str = sr.ReadLine().Split();
                int x = int.Parse(str[0]);
                int y = int.Parse(str[1]);
                arr[i] = new Vector(x, y);
            }
            Array.Sort(arr,0,n);
            Array.Sort(arr, 1, n - 1, new Vector());
            st.Push(0);
            st.Push(1);
            int next = 2;
            while (next < n)
            {
                while (st.Count >= 2)
                {
                    int second = st.Pop();
                    int first = st.Peek();
                    if (CCW(arr[first], arr[second], arr[next]) > 0)//반시계 방향
                    {
                        //가능하다 
                        //fist제거후 second-next형태로
                        st.Push(second);
                        break;
                    }
                }
                st.Push(next);
                next++;
            }
            //외곽의 점들을 모두 구했다.
            int size = st.Count;
            int idx = size-1;
            while(st.Count>0)
            {
                long num = st.Pop();
             cv[idx--]=   arr[num];
              
            }
            //점은 아무거나 써도됨.
            //벡터 두개를 반시계방향인지 파악하면 거리가 길어지고 있음을 의미한다.
            //시계방향이라면 해당 첫 기준점에서 나올 수 있는 최대 길이는 사라졌음을 의미한다.
            int e = 1;
            for (int s = 0; s < size; s++)
            {
                while(s!=e)
                { 
                    int s2 = (s + 1 == size ? 0 : s + 1);

                    int e2 = (e + 1 == size ? 0 : e + 1);

                    if (CCW(cv[s] - cv[s2], cv[e] - cv[e2]) >= 0)
                    {
                        //반시계 인 경우와 평행한 경우 
                        //s는 그대로고 e가 한칸 앞으로 진행
                        //거리를 재준다.
                        long xx = (cv[s].x - cv[e].x);
                        long yy = (cv[s].y - cv[e].y);
                        long current = xx * xx + yy * yy;
                        if (maxDistance < current)
                        {
                            maxDistance = current;
                            aX = cv[s].x;
                            aY = cv[s].y;
                            aX2 = cv[e].x;
                            aY2 = cv[e].y;
                        }
                        //그다음 더 길어질 수도 있기에 e를 다음 중점으로 초기화
                        e = e2;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            sw.WriteLine($"{aX} {aY} {aX2} {aY2}");
        }
        sw.Dispose();
    }
    
}

#elif other2
// #include <cstdio>
// #include <cmath>
// #include <utility>
// #include <algorithm>
// #define X first
// #define Y second
using namespace std;

using ll = long long;
using pii = pair<ll, ll>;

char buf[1 << 17];
char read() {
	static int idx = 1 << 17;
	if (idx == 1 << 17) {
		fread(buf, 1, 1 << 17, stdin);
		idx = 0;
	}
	return buf[idx++];
}
int readInt() {
	int ret = 0, f = 1;
	char now = read();

	while (now == 10 || now == 32) now = read();
	if (now == '-') f = -1, now = read();
	while (now >= 48 && now <= 57) {
		ret = ret * 10 + now - 48;
		now = read();
	}

	return ret * f;
}
ll ccw(const pii &a, const pii &b, const pii &c) {
	return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
}
pii operator-(const pii &a, const pii &b) {
	return pii(a.X - b.X, a.Y - b.Y);
}
double dist(const pii &a, const pii &b) {
	return sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
}
int main() {
	int T = readInt();

	pii p[200000];
	while (T--) {
		int n = readInt();

		for (int a, b, i = 0; i < n; ++i) {
			a = readInt(), b = readInt();
			p[i] = pii(a, b);
		}

		swap(p[0], *min_element(p, p + n));
		sort(p + 1, p + n, [&p](pii &a, pii &b) {
			ll r = ccw(p[0], a, b);
			return r ? r > 0 : a < b;
		});

		int SZ = 0;
		for (int i = 0; i < n; ++i) {
			while (SZ > 1 && ccw(p[SZ - 2], p[SZ - 1], p[i]) <= 0) --SZ;
			p[SZ++] = p[i];
		}

		double mx = 0.0;
		int x = -1, y = -1;
		for (int i = 0, j = 0; i < SZ; ++i) {
			do {
				double d = dist(p[i], p[j]);
				if (mx < d) {
					mx = d;
					x = i, y = j;
				}
			} while (j + 1 < SZ && ccw({ 0, 0 }, p[i + 1] - p[i], p[j + 1] - p[j]) >= 0 && ++j);
		}

		printf("%lld %lld %lld %lld\n", p[x].X, p[x].Y, p[y].X, p[y].Y);
	}

	return 0;
}
#endif
}
