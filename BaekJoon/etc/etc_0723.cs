using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 25
이름 : 배성훈
내용 : 행사장 대여 (Small, Large)
    문제번호 : 14732번, 14733번

    스위핑, 좌표 압축 문제다
    출제자 의도는 x축으로 정렬하고 y축을 좌표압축한 뒤에 y축 선택 여부를 색칠하고
    구분구적법 아이디어로 사각형 넓이를 구하라는것 같다

    여기서 y의 범위가 -50_000 ~ 50_000이므로
    10만 크기밖에 안된다 만약 -10억 ~ 10억이면 좌표압축을 했을 것이다

    그러면, O(N^2)이 나온다
    그런데, 세그먼트 트리로하면 N logN으로 해결가능하다
    세그먼트 트리를 시작 구간과 끝 구간의 카운트를 기록하면 된다
    그리고 길이도 같이 저장해야한다 cnt로 길이를 판별하기에 모든 노드들을 확인할 수 없다;
    기록과 동시에 값을 갱신해줘야한다
        만약 길이와 cnt를 분할하려고 한다면, 이전 cnt 값들도 영향을 끼치기에 모든 노드들을 다시 탐색해야한다;

    세그먼트 트리는 기본적으로 분할 정복 아이디어로 값을 저장하기에
    음수구간이 있는 분할 정복은 그냥 처음 + 끝 / 2로 하면 무한 루프에 빠질 수 있으니 주의해야한다! -> 스택 오버플로우 발생한다!
    이렇게 길이를 x가 변할때 바로 바로 체크해주면 이상없이 통과된다
*/

namespace BaekJoon.etc
{
    internal class etc_0723
    {

        static void Main723(string[] args)
        {

            int START = 0;
            int END = 100_000;
            int ADD = 50_000;
            StreamReader sr;

            Point[] pos;
            int n;
            (int cnt, int len)[] seg;

            long ret;
            Solve();

            void Solve()
            {

                Input();
                
                Init();

                GetRet();

                Console.WriteLine(ret);
            }

            void Init()
            {

                Array.Sort(pos);
                int log = (int)Math.Ceiling(Math.Log2(100_000 + 1)) + 1;
                seg = new (int cnt, int len)[1 << log];
            }

            void GetRet()
            {

                // 넓이 계산
                ret = 0;
                int bX = pos[0].X;
                Update(START, END, pos[0].MinY, pos[0].MaxY - 1, pos[0].IsEnd);

                for (int i = 1; i < pos.Length; i++)
                {

                    // X가 변할 때, 막대기 넓이 계산
                    if (bX != pos[i].X)
                    {

                        // seg[0].len : 높이
                        // 1L은 long 범위까지 갈 수 있어 long 값으로 계산하라고 먼저 곱해준다
                        // 해당 구문 없을 시, (pos[i].X - bX) * seg[0].len은 int로 연산해서
                        // 오버플로우 발생할 수도 있다
                        ret += 1L * (pos[i].X - bX) * seg[0].len;
                        bX = pos[i].X;
                    }

                    Update(START, END, pos[i].MinY, pos[i].MaxY - 1, pos[i].IsEnd);
                }
            }

            void Update(int _s, int _e, int _chkS, int _chkE, int _add, int _idx = 0)
            {

                int l = 2 * _idx + 1;
                int r = 2 * _idx + 2;
                if (_chkE < _s || _e < _chkS || _e < _s) return;
                if (_chkS <= _s && _e <= _chkE)
                {

                    // 사용횟수 저장
                    seg[_idx].cnt += _add;
                    // 길이 재기
                    if (seg[_idx].cnt > 0) seg[_idx].len = _e - _s + 1;
                    else
                    {

                        // 자신이 cnt안되면 자식들을 살핀다
                        // 여기서는 자식들이 보장 X
                        seg[_idx].len = 0;
                        if (l < seg.Length) seg[_idx].len += seg[l].len;
                        if (r < seg.Length) seg[_idx].len += seg[r].len;
                    }

                    return;
                }

                int mid = (_s + _e) / 2;
                Update(_s, mid, _chkS, _chkE, _add, l);
                Update(mid + 1, _e, _chkS, _chkE, _add, r);

                // 위의 길이 재기와 같다
                // 다만 여기서는 자식이 항상 보장된다!
                // 트리 크기를 설정할 때, s < e 일 때는 자식이 존재하게 크기를 잡았다!
                seg[_idx].len = seg[_idx].cnt > 0 ? _e - _s + 1 : seg[l].len + seg[r].len;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                pos = new Point[2 * n];
                for (int i = 0; i < n; i++)
                {

                    int x1 = ReadInt();
                    int y1 = ReadInt();
                    int x2 = ReadInt();
                    int y2 = ReadInt();

                    y1 += ADD;
                    y2 += ADD;
                    pos[2 * i].Set(x1, y1, y2, 1);
                    pos[2 * i + 1].Set(x2, y1, y2, -1);
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == -1) return 0;

                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;

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

            private int x;
            private int minY;
            private int maxY;

            private int isEnd;

            public int X => x;
            public int MinY => minY;
            public int MaxY => maxY;
            public int IsEnd => isEnd;

            public void Set(int _x, int _minY, int _maxY, int _isEnd)
            {

                x = _x;
                minY = _minY;
                maxY = _maxY;
                isEnd = _isEnd;
            }

            public int CompareTo(Point _other)
            {

                int ret = x.CompareTo(_other.x);
                if (ret != 0) return ret;

                ret = _other.maxY.CompareTo(maxY);
                if (ret != 0) return ret;

                return minY.CompareTo(_other.minY);
            }
        }
    }


#if other
// cs14732 - rbybound
// 2023-04-08 19:09:28
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
StringBuilder sb = new StringBuilder();
////////////////////////////////////////////////////////////////////////////////

int N = int.Parse(sr.ReadLine());
int[] line;

bool[,] map = new bool[500, 500];

for (int t = 1; t <= N; t++)
{
    line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    
    for(int i = line[0]; i < line[2]; i++)
    {
        for (int j = line[1]; j < line[3]; j++)
            map[i, j] = true;
    }
}

int area = 0;
foreach (var item in map)
    if (item)
        area++;

sw.WriteLine(area);

sw.Close();
sr.Close();
return;

#elif other2
/*
 * Author: Minho Kim (ISKU)
 * Date: March 1, 2018
 */

import java.util.*;
import java.io.*;

public class Main {

	private static long[] tree, count;
	private static int H;

	public static void main(String... args) throws Exception {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		int N = Integer.parseInt(st.nextToken());

		Rectangle[] rectangle = new Rectangle[N * 2];
		for (int i = 0; i < rectangle.length; i += 2) {
			st = new StringTokenizer(br.readLine());
			int x1 = Integer.parseInt(st.nextToken()) + 50000;
			int y1 = Integer.parseInt(st.nextToken()) + 50000;
			int x2 = Integer.parseInt(st.nextToken()) + 50000;
			int y2 = Integer.parseInt(st.nextToken()) + 50000;
			rectangle[i] = new Rectangle(x1, y1, y2, 1);
			rectangle[i + 1] = new Rectangle(x2, y1, y2, -1);
		}
		Arrays.sort(rectangle);

		H = 1 << (int) Math.ceil(Math.log(100001) / Math.log(2));
		tree = new long[H * 2];
		count = new long[H * 2];

		int dx = rectangle[0].x;
		long area = 0;
		for (Rectangle p : rectangle) {
			area += tree[1] * (p.x - dx);
			update(1, H, 1, p.y1 + 1, p.y2, p.status);
			dx = p.x;
		}

		System.out.print(area);
	}

	private static void update(int l, int r, int i, int L, int R, int value) {
		if (r < L || R < l)
			return;
		if (L <= l && r <= R) {
			count[i] += value;
			merge(l, r, i);
			return;
		}

		int mid = (l + r) / 2;
		update(l, mid, i * 2, L, R, value);
		update(mid + 1, r, i * 2 + 1, L, R, value);
		merge(l, r, i);
	}

	private static void merge(int l, int r, int i) {
		if (count[i] > 0)
			tree[i] = r - l + 1;
		else if (l != r)
			tree[i] = tree[i * 2] + tree[i * 2 + 1];
		else
			tree[i] = 0;
	}

	private static class Rectangle implements Comparable<Rectangle> {
		public int x, y1, y2;
		public int status;

		public Rectangle(int x, int y1, int y2, int status) {
			this.x = x;
			this.y1 = y1;
			this.y2 = y2;
			this.status = status;
		}

		@Override
		public int compareTo(Rectangle o) {
			return this.x - o.x;
		}
	}
}
#elif other3
class SegTree:
    def __init__(self, n, width):
        size = 1
        while size < n: size<<= 1
        self.arr = [0]*(size*2); self.size = size
        self.cover = [0]*(size*2); self.width = [0]*(size*2)
        for i in range(n): self.width[size+i] = width[i]
        for i in range(size-1, 0, -1): self.width[i] = self.width[2*i] + self.width[2*i+1]
    
    def update(self, l, r, delta):
        self._internal(l, r, 1, 0, self.size-1, delta)
    
    def _internal(self, l, r, node, nl, nr, delta):
        if r < nl or l > nr: return
        if l <= nl and nr <= r: self.arr[node]+= delta
        else:
            mid = (nl+nr)//2
            self._internal(l, r, node*2, nl, mid, delta)
            self._internal(l, r, node*2+1, mid+1, nr, delta)
        if self.arr[node]: self.cover[node] = self.width[node]
        elif node >= self.size: self.cover[node] = 0
        else: self.cover[node] = self.cover[2*node] + self.cover[2*node+1]
    
from sys import stdin
input = stdin.readline
n = int(input())
event = []
xvals = set()
for i in range(n):
    x1, y1, x2, y2 = map(int,input().split())
    event.append((y1,x1,x2,1))
    event.append((y2,x1,x2,-1))
    xvals.add(x1)
    xvals.add(x2)
event.sort()
xvals = sorted(xvals)
xdic = dict(zip(xvals, range(len(xvals))))
width = [xvals[i]-xvals[i-1] for i in range(1, len(xvals))]

ST = SegTree(len(xvals)-1, width)
prev = 0
area = 0
for y, x1, x2, d in event:
    area+= (y-prev) * ST.cover[1]
    prev = y
    ST.update(xdic[x1], xdic[x2]-1, d)
print(area)
#elif other4
// #include <cstdio>
// #include <algorithm>
// #include <cstdlib>
// #include <cmath>
// #include <climits>
// #include <cstring>
// #include <string>
// #include <vector>
// #include <queue>
// #include <numeric>
// #include <functional>
// #include <set>
// #include <map>
// #include <unordered_map>
// #include <unordered_set>
// #include <memory>
// #include <thread>
// #include <tuple>

using namespace std;

struct UnionRect {
  typedef long long coord_t;
  typedef long long area_t;

  struct RangeCoverTree {
    struct node_t {
      area_t masked;
      area_t size;
      int cover_cnt;
      node_t() : masked(0), size(0), cover_cnt(0) { }
    };
    vector<node_t> nodes;
    int bt;

    RangeCoverTree(const vector<coord_t>& sorted_coords) {
      int size = (int)sorted_coords.size() - 1;
      size |= (size >> 1); size |= (size >> 2); size |= (size >> 4);
      size |= (size >> 8); size |= (size >> 16);
      size++;
      bt = size;
      nodes.assign(bt * 2, node_t());
      for (int i = 0; i + 1 < sorted_coords.size(); i++) {
        nodes[i + bt].size = sorted_coords[i + 1] - sorted_coords[i];
      }
      for (int i = bt - 1; i >= 1; i--) {
        nodes[i].size = nodes[i << 1].size + nodes[(i << 1) + 1].size;
      }
    }

    /* [s,e) += add. s,e: index */
    void cover(int s, int e, int add) {
      e--;
      if (s > e) return;
      s += bt, e += bt;
      int os = s, oe = e;
      while (s <= e) {
        if (s & 1) {
          nodes[s].cover_cnt += add;
          update(s);
        }
        if (!(e & 1)) {
          nodes[e].cover_cnt += add;
          update(e);
        }
        s = (s + 1) >> 1;
        e = (e - 1) >> 1;
      }
      while (os) {
        update(os);
        os >>= 1;
      }
      while (oe) {
        update(oe);
        oe >>= 1;
      }
    }

    area_t get_whole_mask() const {
      return nodes[1].masked;
    }

  private:
    void update(int i) {
      if (nodes[i].cover_cnt > 0) {
        nodes[i].masked = nodes[i].size;
        return;
      }
      if (i >= bt) {
        nodes[i].masked = 0;
      } else {
        nodes[i].masked = nodes[i << 1].masked + nodes[(i << 1) + 1].masked;
      }
    }
  };

  area_t solve(
    vector<pair<coord_t, pair<coord_t, coord_t>>> start,
    vector<pair<coord_t, pair<coord_t, coord_t>>> end
  ) {
    if (start.empty()) {
      return 0;
    }
    vector<coord_t> xcomp, ycomp;
    xcomp.reserve(start.size() + end.size());
    ycomp.reserve(start.size() + end.size());
    for (const auto& line : start) {
      ycomp.emplace_back(line.first);
      xcomp.emplace_back(line.second.first);
      xcomp.emplace_back(line.second.second);
    }
    // assumption: start, end는 항상 같은 구간 쌍으로 옴
    for (const auto& line : end) {
      ycomp.emplace_back(line.first);
    }

    sort(xcomp.begin(), xcomp.end());
    xcomp.resize(unique(xcomp.begin(), xcomp.end()) - xcomp.begin());
    sort(ycomp.begin(), ycomp.end());
    ycomp.resize(unique(ycomp.begin(), ycomp.end()) - ycomp.begin());
    sort(start.begin(), start.end());
    sort(end.begin(), end.end());
    RangeCoverTree tree(xcomp);
    area_t ans = 0;
    area_t occupied_length = 0;
    coord_t lasty = ycomp[0];
    int sind = 0, eind = 0;
    for (int yind = 0; yind < ycomp.size(); yind++) {
      coord_t y = ycomp[yind];
      area_t diff = y - lasty;
      ans += occupied_length * diff;
      while (sind < start.size() && start[sind].first == y) {
        coord_t x1 = start[sind].second.first;
        coord_t x2 = start[sind].second.second;
        sind++;
        int xind1 = lower_bound(xcomp.begin(), xcomp.end(), x1) - xcomp.begin();
        int xind2 = lower_bound(xcomp.begin(), xcomp.end(), x2) - xcomp.begin();
        tree.cover(xind1, xind2, 1);
      }
      while (eind < end.size() && end[eind].first == y) {
        coord_t x1 = end[eind].second.first;
        coord_t x2 = end[eind].second.second;
        eind++;
        int xind1 = lower_bound(xcomp.begin(), xcomp.end(), x1) - xcomp.begin();
        int xind2 = lower_bound(xcomp.begin(), xcomp.end(), x2) - xcomp.begin();
        tree.cover(xind1, xind2, -1);
      }
      occupied_length = tree.get_whole_mask();
      lasty = y;
    }

    return ans;
  }
};

int main() {
  int n;
  scanf("%d", &n);
  vector<pair<long long, pair<long long, long long>>> start;
  vector<pair<long long, pair<long long, long long>>> end;
  for (int i = 0; i < n; i++) {
    int a, b, c, d;
    scanf("%d%d%d%d", &a, &b, &c, &d);
    start.emplace_back(b, make_pair(a, c));
    end.emplace_back(d, make_pair(a, c));
  }

  UnionRect ur;
  long long result = ur.solve(move(start), move(end));
  printf("%lld\n", result);
  return 0;
}

#endif
}
