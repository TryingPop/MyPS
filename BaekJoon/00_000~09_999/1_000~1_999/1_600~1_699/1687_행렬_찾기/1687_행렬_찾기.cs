using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 25
이름 : 배성훈
내용 : 행렬 찾기
    문제번호 : 1687번

    브루트포스, 누적 합 문제다.
    kadane 알고리즘을 적용해 O(N^3)에 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1580
    {

        static void Main1580(string[] args)
        {

            int row, col;
            int[][] sum;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = 0;
                for (int up = 0; up <= row; up++)
                {

                    for (int down = up + 1; down <= row; down++)
                    {

                        int left = 0;
                        for (int right = 1; right <= col; right++)
                        {

                            if (ChkZero(up, down, left, right))
                                ret = Math.Max(ret, (right - left) * (down - up));
                            else left = right;
                        }
                    }
                }

                Console.Write(ret);

                bool ChkZero(int _up, int _down, int _left, int _right)
                    => sum[_down][_right] - sum[_down][_left] - sum[_up][_right] + sum[_up][_left] == 0;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                sum = new int[row + 1][];
                for (int i = 0; i <= row; i++)
                {

                    sum[i] = new int[col + 1];
                }

                for (int r = 1; r <= row; r++)
                {

                    
                    for (int c = 1; c <= col; c++)
                    {

                        if (sr.Read() == '0') sum[r][c] = sum[r][c - 1];
                        else sum[r][c] = sum[r][c - 1] + 1;
                    }

                    while (sr.Read() != '\n') ;
                }

                for (int c = 1; c <= col; c++)
                {

                    for (int r = 1; r <= row; r++)
                    {

                        sum[r][c] += sum[r - 1][c];
                    }
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

                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
using ProblemSolving.Templates.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.Collections.Generic {}
namespace System.IO {}
namespace System.Linq {}

#nullable disable

public class Bucket
{
    public int StIncl;
    public int EdExcl;

    public PriorityQueue<int, int> MaxSizePq;
    public int[] EmptySize;

    public Bucket(int stIncl, int edExcl)
    {
        StIncl = stIncl;
        EdExcl = edExcl;

        MaxSizePq = new PriorityQueue<int, int>();
        EmptySize = new int[edExcl - stIncl];
    }

    public void SetValue(int offset, int val)
    {
        EmptySize[offset] = val;
        MaxSizePq.Enqueue(offset, -val);
    }
    public void RemoveValue(int offset)
    {
        EmptySize[offset] = 0;
    }

    public bool CanAllocate(int size)
    {
        while (MaxSizePq.TryPeek(out var offset, out var s))
        {
            if (EmptySize[offset] != -s)
            {
                MaxSizePq.Dequeue();
                continue;
            }

            return -s >= size;
        }

        return false;
    }
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var (n, m) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var map = new bool[n, m];

        for (var y = 0; y < n; y++)
        {
            var l = sr.ReadLine();
            for (var x = 0; x < m; x++)
                map[y, x] = (l[x] == '0');
        }

        var maxArea = 0L;
        var line = new bool[m];
        for (var yst = 0; yst < n; yst++)
        {
            for (var x = 0; x < m; x++)
                line[x] = map[yst, x];

            for (var yed = yst; yed < n; yed++)
            {
                var height = (long)(yed - yst + 1);
                for (var x = 0; x < m; x++)
                    line[x] &= map[yed, x];

                var lastZero = -1;
                for (var x = 0; x < m; x++)
                    if (line[x])
                    {
                        maxArea = Math.Max(maxArea, height * (x - lastZero));
                    }
                    else
                    {
                        lastZero = x;
                    }
            }
        }

        sw.WriteLine(maxArea);
    }
}

namespace ProblemSolving.Templates.Utility
{
    public static class DeconstructHelper
    {
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2) => (v1, v2) = (arr[0], arr[1]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3) => (v1, v2, v3) = (arr[0], arr[1], arr[2]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4) => (v1, v2, v3, v4) = (arr[0], arr[1], arr[2], arr[3]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5) => (v1, v2, v3, v4, v5) = (arr[0], arr[1], arr[2], arr[3], arr[4]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6) => (v1, v2, v3, v4, v5, v6) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7) => (v1, v2, v3, v4, v5, v6, v7) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7, out T v8) => (v1, v2, v3, v4, v5, v6, v7, v8) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
    }
}
#elif other2
// #include<bits/stdc++.h>
using namespace std;
struct xy{int x,y;};
int main(void){
	cin.tie(0);
	ios::sync_with_stdio(0);
	int n,m,li=0,ak,u;
	char k;
	cin>>n>>m;
	vector<vector<bool> > x(n,vector<bool>(m+1));
	vector<vector<int> > bh(n,vector<int>());
	vector<int> ac(n);
	stack<xy> s;
	for(int i=0;i<n;++i){
		for(int j=1;j<=m;++j){
			cin>>k;
			x[i][j]=(k=='0');
			if((k=='0')>x[i][j-1]) bh[i].push_back(j-1);
		}
	}for(int i=0;i<n;++i) ac[i]=bh[i].size()-1;
	for(int j=m;j;--j){
		for(int i=0;i<n;++i){
			if(x[i][j]){
				if(bh[i][ac[i]]>j) --ac[i];
				u=j-bh[i][ac[i]];ak=1;
				while(!s.empty()){
					if(s.top().x<=u) break;
					li=max(li,s.top().x*(s.top().y+ak-1));
					ak+=s.top().y;
					s.pop();
				}if(s.empty()||s.top().x!=u) s.push(xy{u,ak});
				else s.top().y+=ak;
			}ak=0;
			if(!x[i][j]||i==n-1) while(!s.empty()) li=max(li,s.top().x*(s.top().y+ak)),ak+=s.top().y,s.pop();
		}
	}cout<<li;
}
#endif
}
