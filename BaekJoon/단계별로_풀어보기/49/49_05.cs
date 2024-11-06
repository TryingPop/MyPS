using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 12
이름 : 배성훈
내용 : 단순 다각형
    문제번호 : 3679번

    기하학, 볼록껍질, 정렬 문제다
    아이디어는 다음과 같다
    먼저 그리는 방법을 설정해야한다
    볼록껍질 단원에 있어, 볼록껍질과 연관된 힌트를 알고 있어
    최대한 볼록 껍질과 연관 시켜볼려고 했다
    그러니 볼록껍질을 구할 때, 정렬하는 방법이 떠올랐고
    손바닥 그리듯이 그리면 어떨까 생각했다
    여기서 주의할 것은 마지막 부분은 위에서 내려오게 해야한다
    아래부터 하면 일직선에 있을 시 새끼손가락 부분에서 선분 교차가 일어날 수 있다

    컨벡스헐에서 쓰던 정렬방법을 쓰고,
    마지막 점과, 시작점 그리고 뒤에서부터 점 하나씩 판별하면서 일직선인 경우를 판별했다
    세 점이 일직선에 있다는 것은 세 점으로 ccw를 구했을 때 0인 경우와 같기에 ccw로 일직선을 구했다

    이렇게 제출하니 80ms에 이상없이 통과했다
*/

namespace BaekJoon._49
{
    internal class _49_05
    {

        static void Main5(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            StringBuilder sb;
            Point[] pos;
            int n;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();
                while(test-- > 0)
                {

                    Input();

                    Array.Sort(pos, 1, n - 1);

                    int len = GetReverse();

                    for (int i = 0; i < len; i++)
                    {

                        sb.Append(pos[i].Idx);
                        sb.Append(' ');
                    }

                    for (int i = n - 1; i >= len; i--)
                    {

                        sb.Append(pos[i].Idx);
                        sb.Append(' ');
                    }
                    sb.Append('\n');

                    sw.Write(sb);
                    sw.Flush();
                    sb.Clear();
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                pos = new Point[2_000];

                sb = new(5_000);
            }

            void Input()
            {

                n = ReadInt();

                int minX = 10_000;
                int minY = 10_000;
                int minIdx = 0;

                for (int i = 0; i < n; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    pos[i].Set(x, y, i);

                    if (y < minY)
                    {

                        minY = y;
                        minX = x;
                        minIdx = i;
                    }
                    else if (y == minY && x < minX)
                    {

                        minX = x;
                        minIdx = i;
                    }
                }

                Point temp = pos[0];
                pos[0] = pos[minIdx];
                pos[minIdx] = temp;

                Point.SetFix(pos[0]);
            }

            int GetReverse()
            {

                int ret = 1;
                for (int i = n - 2; i >= 1; i--)
                {

                    int ccw = Point.CCW(pos[0], pos[n - 1], pos[i]);
                    if (ccw == 0) continue;

                    ret = i + 1;
                    break;
                }

                return ret;
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

            private int x;
            private int y;
            private int idx;

            public int X => x;
            public int Y => y;
            public int Idx => idx;

            public static int CCW(Point _a, Point _b, Point _c)
            {

                int ccw = _a.x * _b.y;
                ccw += _b.x * _c.y;
                ccw += _c.x * _a.y;

                ccw -= _b.x * _a.y;
                ccw -= _c.x * _b.y;
                ccw -= _a.x * _c.y;

                if (ccw > 0) return 1;
                if (ccw < 0) return -1;
                return 0;
            }

            public static int Dis(Point _a, Point _b)
            {

                int x = _a.x - _b.x;
                x *= x;

                int y = _a.y - _b.y;
                y *= y;

                return x + y;
            }

            public void Set(int _x, int _y, int _idx)
            {

                x = _x;
                y = _y;
                idx = _idx;
            }

            public static void SetFix(Point _fix)
            {

                fix = _fix;
            }

            public int CompareTo(Point _other)
            {

                int ccw = CCW(this, _other, fix);
                if (ccw == 0) return Dis(this, fix).CompareTo(Dis(_other, fix));

                return -ccw;
            }
        }
    }

#if other
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections;

class Programs
{

    static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), Encoding.Default);
    static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), Encoding.Default);
    class Vector : IComparable,IComparer
    {
        public int x, y,num;
        public Vector(int x=0, int y=0,int num=0){ this.x = x; this.y = y;this.num = num; }
        public static Vector operator -(Vector a, Vector b)
        {
            Vector c = new Vector(a.x - b.x, a.y - b.y);
            return c;
        }


        public int CompareTo(object obj)
        {
            Vector b = (Vector)obj;
            Vector a = this;
            if(a.x==b.x)
            {
                return a.y.CompareTo(b.y);
            }
            return a.x.CompareTo(b.x);

        }

        public int Compare(object x, object y)
        {
            Vector a = (Vector)x;
            Vector b = (Vector)y;
         //반시계로 통일
         int dir= CCW(arr[0], a, b);
            if(dir>0)
            {
                return -1;
            }
            else if(dir<0)
            {
                return 1;
            }
            else
            {
                return Distance(arr[0],a).CompareTo(Distance(arr[0],b));//거리로 해야됨.
            }
        }
    }
    static int CCW(Vector a,Vector b,Vector c)
    {
        Vector A = b - a;
        Vector B = c - b;
        int r = A.x * B.y - A.y * B.x;
        if(r>0)
        {
            return 1;
        }
        else if(r<0)
        {
            return -1;
        }
        return 0;
    }
    static int Distance(Vector a,Vector b)
    {
        int x = a.x - b.x;
        int y = a.y - b.y;
        return x * x + y * y;
    }
    static Vector[] arr = new Vector[2000];
    static int n;
    static void CreateConvexHull(int size )
    {
        Array.Sort(arr,0,size);
        Array.Sort(arr, 1, size - 1, new Vector());
        //마지막 점 2개와 시작점이 직선인 경우 마지막 점 2개의 순서를 바꿔주어야한다. 이건 그림으로 직접 그려보고 정렬해보면 알게됨.
        int i = 1;
        for (i = 1; i < n - 1; i++)
        {
            Vector a = arr[i];
            Vector b = arr[n - 1];
            if (CCW(arr[0], b, a) == 0)
            {
                break;
            }
        }
        Array.Reverse(arr, i, size - i);

    }
    static void Main(String[] args)
    {
        int c= int.Parse(sr.ReadLine());
        for (int i = 0; i < c; i++)
        {
            string[] str = sr.ReadLine().Split();
             n = int.Parse(str[0]);
            int idx = 0;
            for (int j = 0; j < n*2; j+=2)
            {
                arr[idx] =new Vector(int.Parse(str[j+1]),int.Parse(str[j+2]),idx);
                idx++;
            }
            CreateConvexHull(n);
            for (int j = 0; j < n; j++)
            {
                sw.Write($"{arr[j].num} ");
            }
            sw.WriteLine();
        }
        sw.Dispose();
    }
}
#elif other2
// #include <bits/stdc++.h>
// #include <sys/stat.h>
// #include <sys/mman.h>
using namespace std;

/////////////////////////////////////////////////////////////////////////////////////////////
/*
 * Author : jinhan814
 * Date : 2021-03-22
 * Description : FastIO implementation for cin, cout. (mmap ver.)
 */
const int INPUT_SZ = 8000000;
const int OUTPUT_SZ = 1 << 20;

class INPUT {
private:
	char* p;
	bool __END_FLAG__, __GETLINE_FLAG__;
public:
	explicit operator bool() { return !__END_FLAG__; }
    INPUT() { p = (char*)mmap(0, INPUT_SZ, PROT_READ, MAP_SHARED, 0, 0); }
	bool is_blank(char c) { return c == ' ' || c == '\n'; }
	bool is_end(char c) { return c == '\0'; }
	char _readChar() { return *p++; }
	char readChar() {
		char ret = _readChar();
		while (is_blank(ret)) ret = _readChar();
		return ret;
	}
	template<typename T>
	T _readInt() {
		T ret = 0;
		char cur = _readChar();
		bool flag = 0;
		while (is_blank(cur)) cur = _readChar();
		if (cur == '-') flag = 1, cur = _readChar();
		while (!is_blank(cur) && !is_end(cur)) ret = 10 * ret + cur - '0', cur = _readChar();
		if (is_end(cur)) __END_FLAG__ = 1;
		return flag ? -ret : ret;
	}
	int readInt() { return _readInt<int>(); }
	long long readLL() { return _readInt<long long>(); }
	string readString() {
		string ret;
		char cur = _readChar();
		while (is_blank(cur)) cur = _readChar();
		while (!is_blank(cur) && !is_end(cur)) ret.push_back(cur), cur = _readChar();
		if (is_end(cur)) __END_FLAG__ = 1;
		return ret;
	}
	double readDouble() {
		string ret = readString();
		return stod(ret);
	}
	string getline() {
		string ret;
		char cur = _readChar();
		while (cur != '\n' && !is_end(cur)) ret.push_back(cur), cur = _readChar();
        if (__GETLINE_FLAG__) __END_FLAG__ = 1;
		if (is_end(cur)) __GETLINE_FLAG__ = 1;
		return ret;
	}
	friend INPUT& getline(INPUT& in, string& s) { s = in.getline(); return in; }
} _in;

class OUTPUT {
private:
	char write_buf[OUTPUT_SZ];
	int write_idx;
public:
	~OUTPUT() { bflush(); }
	template<typename T>
	int getSize(T n) {
		int ret = 1;
		if (n < 0) n = -n;
		while (n >= 10) ret++, n /= 10;
		return ret;
	}
	void bflush() {
		fwrite(write_buf, sizeof(char), write_idx, stdout);
		write_idx = 0;
	}
	void writeChar(char c) {
		if (write_idx == OUTPUT_SZ) bflush();
		write_buf[write_idx++] = c;
	}
	void newLine() { writeChar('\n'); }
	template<typename T>
	void _writeInt(T n) {
		int sz = getSize(n);
		if (write_idx + sz >= OUTPUT_SZ) bflush();
		if (n < 0) write_buf[write_idx++] = '-', n = -n;
		for (int i = sz - 1; i >= 0; i--) write_buf[write_idx + i] = n % 10 + '0', n /= 10;
		write_idx += sz;
	}
	void writeInt(int n) { _writeInt<int>(n); }
	void writeLL(long long n) { _writeInt<long long>(n); }
	void writeString(string s) { for (auto& c : s) writeChar(c); }
	void writeDouble(double d) { writeString(to_string(d)); }
} _out;

/* operators */
INPUT& operator>> (INPUT& in, char& i) { i = in.readChar(); return in; }
INPUT& operator>> (INPUT& in, int& i) { i = in.readInt(); return in; }
INPUT& operator>> (INPUT& in, long long& i) { i = in.readLL(); return in; }
INPUT& operator>> (INPUT& in, string& i) { i = in.readString(); return in; }
INPUT& operator>> (INPUT& in, double& i) { i = in.readDouble(); return in; }

OUTPUT& operator<< (OUTPUT& out, char i) { out.writeChar(i); return out; }
OUTPUT& operator<< (OUTPUT& out, int i) { out.writeInt(i); return out; }
OUTPUT& operator<< (OUTPUT& out, long long i) { out.writeLL(i); return out; }
OUTPUT& operator<< (OUTPUT& out, size_t i) { out.writeInt(i); return out; }
OUTPUT& operator<< (OUTPUT& out, string i) { out.writeString(i); return out; }
OUTPUT& operator<< (OUTPUT& out, double i) { out.writeDouble(i); return out; }

/* macros */
// #define fastio 1
// #define cin _in
// #define cout _out
// #define istream INPUT
// #define ostream OUTPUT
/////////////////////////////////////////////////////////////////////////////////////////////

// #define X first
// #define Y second
// #define all(v) (v).begin(), (v).end()
using pii = pair<int, int>;
using p = pair<pii, int>;

int ccw(pii a, pii b, pii c) {
	int t = (b.X - a.X) * (c.Y - b.Y) - (c.X - b.X) * (b.Y - a.Y);
	return t ? t > 0 ? 1 : -1 : 0;
}

int dist(pii a, pii b) {
	return (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
}


int main() {
	fastio;
	int N; cin >> N;
	while (N--) {
		int n; cin >> n;
		vector<p> v(n);
		for (int i = 0; i < n; i++) {
			int a, b; cin >> a >> b;
			v[i] = { { a, b }, i };
		}
		swap(v[0], *min_element(all(v)));
		sort(v.begin() + 1, v.end(), [&](p a, p b) {
			if (ccw(v[0].X, a.X, b.X)) return ccw(v[0].X, a.X, b.X) > 0;
			return dist(v[0].X, a.X) < dist(v[0].X, b.X);
		});
		int i = n - 1;
		while (!ccw(v[0].X, v[i - 1].X, v[i].X)) i--;
		reverse(v.begin() + i, v.end());
		for (auto& [a, b] : v) cout << b << ' '; cout << '\n';
	}
}
#elif other3
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.Arrays;
import java.util.StringTokenizer;

public class Main {
	
	static Point origin;
	
	static class Point implements Comparable<Point>{
		
		int x, y, idx;
		
		public Point(int x, int y, int i) {
			this.x = x;
			this.y = y;
			this.idx = i;
		}
		
		public int compareTo(Point o) {
			int ccw = ccw(origin, this, o);
			if(ccw > 0) {
				return -1;
			}
			else if(ccw < 0) {
				return 1;
			}
			else {
				return distance(origin, this) - distance(origin, o);
			}
		}
	}
	
	public static void main(String[] args) throws IOException{
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
		StringTokenizer st;
		
		int T = Integer.parseInt(br.readLine());
		for(int t = 0; t<T; t++) {
			st = new StringTokenizer(br.readLine());
			int n = Integer.parseInt(st.nextToken());
			Point[] arr = new Point[n];
			for(int i = 0; i<n; i++) {
				int x = Integer.parseInt(st.nextToken());
				int y = Integer.parseInt(st.nextToken());
				arr[i] = new Point(x,y, i);
				if(i == 0) {
					origin = arr[i];
				}
				else {
					if(y<origin.y) {
						origin = arr[i];
					}
					else if(y == origin.y && x<origin.x) {
						origin = arr[i];
					}
				}
			}
			
			Arrays.sort(arr);
			int cnt = 0;
			while(true) {
				if(ccw(origin, arr[n-1-cnt], arr[n-2-cnt]) == 0) {
					cnt ++;
				}
				else {
					break;
				}
			}
			
			for(int i = 0; i<n-cnt-1; i++) {
				bw.write(arr[i].idx+" ");
			}
			for(int i = n-1; i>=n-cnt-1; i--) {
				bw.write(arr[i].idx+" ");
			}
			bw.newLine();
			
			
		}
		

		bw.flush();
		br.close();
		bw.close();
	}
	
	public static int ccw(Point a, Point b, Point c) {
		int x1 = b.x - a.x;
		int y1 = b.y - a.y;
		int x2 = c.x - b.x;
		int y2 = c.y - b.y;
		
		int ccw = x1*y2 - y1*x2;
		return ccw > 0 ? 1 : ccw < 0 ? -1 : 0;
	}
	
	public static int distance(Point a, Point b) {
		return (a.x-b.x)*(a.x-b.x) + (a.y-b.y)*(a.y-b.y); 
	}
}

#elif other4
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public struct Point : IEquatable<Point>
{
    public long Y;
    public long X;

    public Point(long y, long x)
    {
        Y = y;
        X = x;
    }

    public bool Equals(Point other)
    {
        return Y == other.Y && X == other.X;
    }

    public static Point operator +(Point lhs, Point rhs)
    {
        return new Point(lhs.Y + rhs.Y, lhs.X + rhs.X);
    }
    public static Point operator -(Point lhs, Point rhs)
    {
        return new Point(lhs.Y - rhs.Y, lhs.X - rhs.X);
    }

    public static bool operator ==(Point lhs, Point rhs)
    {
        return lhs.Equals(rhs);
    }
    public static bool operator !=(Point lhs, Point rhs)
    {
        return !lhs.Equals(rhs);
    }

    public override bool Equals(object obj)
    {
        return obj is Point p && Equals(p);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Y, X);
    }
    public override string ToString()
    {
        return $"{X}, {Y}";
    }
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var c = Int32.Parse(sr.ReadLine());
        while (c-- > 0)
        {
            var l = sr.ReadLine().Split(' ').Select(Int32.Parse).Skip(1).ToArray();
            var points = new Point[l.Length / 2];

            for (var idx = 0; idx < l.Length / 2; idx++)
                points[idx] = new Point(l[2 * idx], l[2 * idx + 1]);

            var hull = MonotoneChain(points);
            var pointToIdx = points
                .Select((p, idx) => (p, idx))
                .ToDictionary(p => p.p, p => p.idx);

            if (hull.Length == points.Length)
            {
                foreach (var v in hull)
                    sw.Write($"{pointToIdx[v]} ");

                sw.WriteLine();
            }
            else
            {
                var centerIdx = Enumerable
                    .Range(0, points.Length)
                    .First(idx => !hull.Contains(points[idx]));

                var sorted = Enumerable.Range(0, points.Length)
                    .Where(idx => idx != centerIdx)
                    .OrderBy(idx => points[idx], Comparer<Point>.Create((l, r) => AngleDistCompare(l, r, points[centerIdx])))
                    .ToArray();

                sw.Write($"{centerIdx} ");
                foreach (var v in sorted)
                    sw.Write($"{v} ");

                sw.WriteLine();
            }
        }
    }

    private static int AngleDistCompare(Point l, Point r, Point center)
    {
        var cl = l - center;
        var cr = r - center;

        if (Math.Sign(cl.X) == Math.Sign(cr.X) && Math.Sign(cl.Y) == Math.Sign(cr.Y) && cl.Y * cr.X == cr.Y * cl.X)
        {
            var dl = cl.X * cl.X + cl.Y * cl.Y;
            var dr = cr.X * cr.X + cr.Y * cr.Y;

            return dl.CompareTo(dr);
        }
        else
        {
            var al = Math.Atan2(cl.Y, cl.X);
            var ar = Math.Atan2(cr.Y, cr.X);

            return al.CompareTo(ar);
        }
    }

    private static Point[] MonotoneChain(Point[] points)
    {
        var sorted = points
            .OrderBy(v => v.X)
            .ThenBy(v => v.Y)
            .ToArray();

        var stack = new Stack<Point>();
        var set = new HashSet<Point>();

        foreach (var curr in sorted)
        {
            while (stack.Count >= 2)
            {
                var middle = stack.Pop();
                var prev = stack.Peek();

                if (CCW(middle, prev, curr) >= 0)
                {
                    stack.Push(middle);
                    break;
                }
                else
                {
                    set.Remove(middle);
                }
            }

            stack.Push(curr);
            set.Add(curr);
        }

        foreach (var curr in sorted.Reverse())
        {
            if (set.Contains(curr))
                continue;

            while (stack.Count >= 2)
            {
                var middle = stack.Pop();
                var prev = stack.Peek();

                if (CCW(middle, prev, curr) >= 0)
                {
                    stack.Push(middle);
                    break;
                }
                else
                {
                    set.Remove(middle);
                }
            }

            stack.Push(curr);
            set.Add(curr);
        }

        return stack.ToArray();
    }

    private static int CCW(Point p, Point a, Point b)
    {
        var va = a - p;
        var vb = b - p;

        return Math.Sign(va.Y * vb.X - va.X * vb.Y);
    }
}

#endif
}
