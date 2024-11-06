using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 14
이름 : 배성훈
내용 : 직사각형 집합
    문제번호 : 9464번

    자료구조, 그리디 알고리즘, 해시를 사용한 집합과 맵 문제다
    피타고라스 세 수 문제다
    정수론 시간에 가우스 정수로 들은 기억이 있어
    전처리를 통해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0813
    {
#if first
        static void Main813(string[] args)
        {

            int MAX = 355;
            StreamReader sr;
            StreamWriter sw;

            int[] arr;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                int test = ReadInt();

                while(test-- > 0)
                {

                    int n = ReadInt();

                    int ret = BinarySearch(n);
                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            int BinarySearch(int _n)
            {

                int l = 0;
                int r = MAX;

                while(l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (_n < arr[mid]) r = mid - 1;
                    else l = mid + 1;
                }

                return r;
            }

            void Init()
            {

                HashSet<PythagoreanTriple> p = new(10_000);

                for (int i = 2; i <= MAX; i++)
                {

                    for (int j = 1; j < i; j++)
                    {

                        long x = i * i - j * j;
                        long y = i * j * 2;

                        long z = (long)Math.Sqrt((x * x) + (y * y));

                        p.Add(new(x, y, z));
                    }
                }

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                PythagoreanTriple[] temp = p.ToArray();
                Array.Sort(temp);

                arr = new int[MAX + 1];
                for (int i = 0; i < MAX; i++)
                {

                    arr[i + 1] = (int)temp[i].Len + arr[i];
                }

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            }

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

        struct PythagoreanTriple : IComparable<PythagoreanTriple>
        {

            private long a, b, c;
            private static HashSet<long> chk = new(3);

            public long Len => (a + b) * 2;
            public PythagoreanTriple(long _a, long _b, long _c)
            {

                if (_b < _a)
                {

                    long temp = _a;
                    _a = _b;
                    _b = temp;
                }

                if (_c < _a)
                {

                    long temp = _a;
                    _a = _c;
                    _c = temp;
                }

                if (_c < _b)
                {

                    long temp = _b;
                    _b = _c;
                    _c = temp;
                }

                long gcd = GetGCD(_a, _b, _c);

                a = _a / gcd;
                b = _b / gcd;
                c = _c / gcd;
            }

            private static long GetGCD(long _a, long _b, long _c)
            {

                long gcd = GCD(_a, _b);
                gcd = GCD(gcd, _c);

                return gcd;
            }

            private static long GCD(long _a, long _b)
            {

                while(_b > 0)
                {

                    long temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            public int CompareTo(PythagoreanTriple _other)
            {

                return (a + b).CompareTo(_other.a + _other.b);
            }
        }

#else

        static void Main813(string[] args)
        {


            const int MAX = 355;
            StreamReader sr;
            StreamWriter sw;

            int[] arr;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                int test = ReadInt();

                while (test-- > 0)
                {

                    int n = ReadInt();

                    int ret = BinarySearch(n);
                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            int BinarySearch(int _n)
            {

                int l = 0;
                int r = MAX;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (_n < arr[mid]) r = mid - 1;
                    else l = mid + 1;
                }

                return r;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                arr = new int[MAX + 1] { 0, 14, 48, 94, 156, 238, 332, 430, 572, 718, 876, 1054, 1248, 1454, 1680, 1918, 2156, 2410, 2684, 2986, 3308, 3630, 3964, 4346, 4732, 5130, 5564, 5998, 6444, 6910, 7388, 7870, 8384, 8910, 9452, 10014, 10588, 11162, 11740, 12362, 12988, 13646, 14304, 14978, 15664, 16370, 17088, 17822, 18588, 19370, 20152, 20954, 21772, 22634, 23500, 24378, 25276, 26190, 27116, 28074, 29048, 30042, 31036, 32042, 33064, 34086, 35128, 36182, 37236, 38294, 39400, 40506, 41644, 42798, 43984, 45182, 46384, 47598, 48832, 50078, 51324, 52586, 53868, 55162, 56508, 57866, 59224, 60618, 62012, 63438, 64864, 66302, 67744, 69186, 70640, 72126, 73628, 75150, 76688, 78270, 79852, 81450, 83048, 84666, 86312, 87978, 89644, 91322, 93036, 94762, 96524, 98298, 100076, 101854, 103676, 105514, 107372, 109246, 111132, 113018, 114924, 116842, 118760, 120682, 122616, 124570, 126536, 128518, 130536, 132598, 134664, 136742, 138840, 140954, 143068, 145194, 147356, 149518, 151692, 153886, 156092, 158346, 160600, 162858, 165160, 167466, 169804, 172142, 174528, 176930, 179344, 181758, 184192, 186638, 189100, 191582, 194064, 196562, 199104, 201646, 204204, 206782, 209376, 211982, 214620, 217262, 219916, 222590, 225264, 227950, 230636, 233338, 236040, 238762, 241496, 244282, 247068, 249866, 252684, 255530, 258396, 261274, 264168, 267082, 269996, 272938, 275900, 278874, 281852, 284874, 287900, 290926, 293964, 297002, 300088, 303194, 306312, 309434, 312556, 315690, 318856, 322058, 325272, 328490, 331752, 335014, 338280, 341546, 344844, 348142, 351456, 354782, 358128, 361474, 364832, 368190, 371552, 374926, 378300, 381694, 385136, 388638, 392140, 395646, 399164, 402718, 406284, 409882, 413480, 417082, 420716, 424350, 427996, 431658, 435340, 439022, 442716, 446458, 450204, 453962, 457740, 461534, 465328, 469154, 472996, 476838, 480692, 484546, 488448, 492382, 496316, 500302, 504300, 508318, 512336, 516370, 520416, 524462, 528540, 532634, 536728, 540854, 545016, 549190, 553368, 557590, 561816, 566074, 570348, 574634, 578940, 583258, 587576, 591898, 596252, 600606, 604988, 609370, 613772, 618174, 622588, 627006, 631468, 635930, 640408, 644934, 649460, 654006, 658568, 663142, 667736, 672342, 676948, 681570, 686228, 690886, 695588, 700306, 705024, 709762, 714500, 719254, 724020, 728806, 733604, 738406, 743240, 748086, 752968, 757862, 762760, 767658, 772600, 777542, 782488, 787494, 792520, 797546, 802588, 807674, 812776, 817910, 823044, 828182, 833320, 838502, 843688, 848886, 854084, 859302, 864536, 869802, 875096, 880410, 885736, 891078, 896440, 901802, 907176, 912554, 917976, 923402, 928840, 934298, 939772, 945246, 950720, 956194, 961700, 967218, 972736, 978270, 983824, 989406, 995008, 1000622 };
            }

            int ReadInt()
            {

                int c, ret = 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
#endif
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var pyt = new Dictionary<(long r1, long r2), (long w, long h)>();
        var maxlen = 1010;

        for (var y = 1L; y < maxlen; y++)
            for (var x = 1 + y; x < maxlen; x++)
            {
                var a = 2 * x * y;
                var b = x * x - y * y;

                var w = Math.Min(a, b);
                var h = Math.Max(a, b);
                var g = GCD(w, h);
                var k = (w / g, h / g);

                if (!pyt.ContainsKey(k))
                {
                    pyt[k] = (w, h);
                }
                else
                {
                    var prev = pyt[k];
                    if (w < prev.w)
                        pyt[k] = (w, h);
                }
            }

        var ordered = pyt.Select(v => 2 * (v.Value.w + v.Value.h)).OrderBy(v => v).ToArray();
        for (var idx = 0; idx < ordered.Length - 1; idx++)
            ordered[idx + 1] += ordered[idx];

        var t = Int32.Parse(sr.ReadLine());
        while (t-- > 0)
        {
            var n = Int32.Parse(sr.ReadLine());

            var pos = Array.BinarySearch(ordered, n);
            if (pos >= 0)
                sw.WriteLine(1 + pos);
            else
                sw.WriteLine(~pos);
        }
    }

    public static long GCD(long x, long y)
    {
        while (x != 0 && y != 0)
            if (x > y)
                x %= y;
            else
                y %= x;

        return Math.Max(x, y);
    }
}

#elif other2
// #include<stdio.h>
// #include<algorithm>
int add[2100]={0},ac;
int gcd(int x,int y){
    if(y == 0) return x;
    return gcd(y,x%y);
}
int main(){
    int x,y,q,p,r;
    for(x=2;x<=100;x++){
        for(y=x-1;y>=1;y-=2){
            if(gcd(x,y) == 1){
                q = x*x-y*y;
                p = 2 * x * y;
                r = x*x + y*y;
                if(gcd(gcd(q,p),r) == 1){
                    add[ac++] = 2*(p+q);
                }
            }
        }
    }
    std::sort(add,add+ac);
    int T;
    scanf("%d",&T);
    int N,count;
    while(T--){
        scanf("%d",&N);
        count = 0;
        while(N >= add[count]){
            N -= add[count];
            count ++;
        }
        printf("%d\n",count);
    }
    return 0;
}
#elif other3
import java.io.*;
import java.util.*;
class Node implements Comparable<Node>{
	int u,v,w;
	public Node(int u,int v,int w){
		this.u=u;
		this.v=v;
		this.w=w;
	}
	@Override
	public int compareTo(Node a){
		return this.w-a.w;
	}
	
}
class Main{
	public static int gcd(int a,int b){
		b%=a;
		if(b==0) return a;
		return gcd(b,a);				
	}
    public static void main(String[] args)throws Exception{    	
    	BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
    	BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
    	PriorityQueue<Integer> queue=new PriorityQueue<Integer>();
    	for(int i=1;i<75;i++){
    		for(int j=i+1;j<75;j++){
    			if((i+j)%2==1&&gcd(i,j)==1){    				
    				queue.add((j*j)+(2*i*j)-(i*i));
    			}
    		}
    	}
    	int size=queue.size();
    	int[] sum=new int[size+1];
    	for(int i=0;i<size;i++){
    		sum[i+1]=sum[i]+(queue.poll()*2);
    	}
    	int T=Integer.parseInt(br.readLine().trim());
    	for(int i=0;i<T;i++){
    		long L=Integer.parseInt(br.readLine().trim());
    		int mid=0;
    		for(int upper=size,lower=1;;){
    			mid=(upper+lower)/2;
    			if(sum[mid]==L){
    				break;
    			}else if(sum[mid]>L){
    				upper=mid-1;
    			}else{
    				lower=mid+1;
    			}
    			if(lower>upper){
    				mid=upper;
    				break;
    			}
    		}
    		bw.write(mid+"\n");
    	}
    	bw.close();
    }
}

#elif other4
from bisect import bisect
import sys
input=sys.stdin.readline

def gcd(a,b):
    while a!=0:
        a,b=b%a,a
    return b
    


def main():
    U=set()
    for x in range(2,100):
        for y in range(1,x):
            if gcd(2*x*y,x*x-y*y)==1:
                if 2*x*y<x*x-y*y:
                    U.add((2*x*y,x*x-y*y))
                else:
                    U.add((x*x-y*y,2*x*y))
                    
    R=[h+w for h,w in U]
    R.sort()
    prefixS=[0]
    for r in R:
        prefixS.append(prefixS[-1]+r)
    
    for _ in range(int(input())):
        print(bisect(prefixS,int(input())//2)-1)
main()
#endif
}
