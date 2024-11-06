using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 21
이름 : 배성훈
내용 : 방 번호 2
    문제번호 : 1084번

    구현, 그리디 문제다
    ect_0892 문제 즉, 1082의 풀이 방법을 1084에 맞게 수정만 했다
    쓸 수 있는 숫자의 개수를 n이라 하면 
    찾는데 시간복잡도는 O(n) 이고, 
    출력 조건 맞추는데 O(1)이라 시간이 얼마 안걸린다
*/

namespace BaekJoon.etc
{
    internal class etc_0893
    {

        static void Main893(string[] args)
        {

            StreamReader sr;
            int n;
            long m;
            long[] p;
            long[] cnt;
            long len, r;
            long min1, min2;
            long minVal1, minVal2;

            List<int> ret1, ret2;

            Solve();
            void Solve()
            {

                Input();

                GetLength();

                RemoveR();

                GetRet();
            }

            void RemoveR()
            {

                if (len == 0) return;

                for (int i = n - 1; i > minVal1; i--)
                {

                    if (r < p[i] - min1) continue;

                    long add = r / (p[i] - min1);
                    if (cnt[minVal1] < add) add = cnt[minVal1];
                    cnt[minVal1] -= add;
                    cnt[i] += add;

                    r -= add * (p[i] - min1);
                }

                for (int i = n - 1; i > minVal2; i--)
                {

                    if (r < p[i] - min2) continue;

                    long add = r / (p[i] - min2);
                    if (cnt[minVal2] < add) add = cnt[minVal2];
                    cnt[minVal2] -= add;
                    cnt[i] += add;

                    r -= add * (p[i] - min2);
                }
            }

            void GetRet()
            {

                if (len == 0 && p[0] <= r) 
                { 

                    cnt[0]++;
                    len++;
                }

                ret1 = new(50);
                ret2 = new(50);

                for (int i = n - 1; i >= 0; i--)
                {

                    if (cnt[i] == 0) continue;

                    for (int j = 0; j < cnt[i]; j++)
                    {

                        ret1.Add(i);
                        if (ret1.Count == 50) break;
                    }

                    if (ret1.Count == 50) break;
                }

                for (int i = 0; i < n; i++)
                {

                    if (cnt[i] == 0) continue;

                    for (int j = 0; j < cnt[i]; j++)
                    {

                        ret2.Add(i);
                        if (ret2.Count == 50) break;
                    }

                    if (ret2.Count == 50) break;
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(),bufferSize: 65536))
                {

                    sw.Write($"{len}\n");

                    for (int i = 0; i < ret1.Count; i++)
                    {

                        sw.Write(ret1[i]);
                    }
                    sw.Write('\n');

                    for (int i = ret2.Count - 1; i >= 0; i--)
                    {

                        sw.Write(ret2[i]);
                    }
                }
            }

            void GetLength()
            {

                cnt = new long[n];

                len = 0;

                r = m;
                minVal1 = 0;
                minVal2 = 0;
                min1 = 1_000_000_000_000_000_001;       // 0 포함 X
                min2 = p[0];                            // 0 포함

                for (int i = 1; i < n; i++)
                {

                    if (p[i] < min1) min1 = p[i];
                    if (p[i] < min2) min2 = p[i];
                }

                if (r < min1) return;

                for (int i = n - 1; i > 0; i--)
                {

                    if (min1 == p[i])
                    {

                        minVal1 = i;
                        r -= p[i];
                        cnt[i]++;
                        break;
                    }
                }

                len = 1;

                for (int i = n - 1; i >= 0; i--)
                {

                    if (p[i] == min2)
                    {

                        long add = r / min2;
                        len += add;
                        cnt[i] += add;
                        r -= min2 * add;
                        minVal2 = i;
                        break;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                p = new long[n];

                for (int i = 0; i < n; i++)
                {

                    p[i] = ReadLong();
                }

                m = ReadLong();
                sr.Close();
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

            long ReadLong()
            {

                int c;
                long ret = 0L;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.Linq;
using System.Text;

public class Program
{
    struct Digit
    {
        public int digit;
        public long price;
        public Digit(int d, long p)
        {
            digit = d; price = p;
        }
    }
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        long[] price = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
        long m = long.Parse(Console.ReadLine());
        Digit[] digits = new Digit[n];
        for (int i = 0; i < n; i++)
        {
            digits[i] = new(i, price[i]);
        }
        Array.Sort(digits, (x, y) =>
        {
            if (x.price == y.price)
                return y.digit - x.digit;
            return x.price.CompareTo(y.price);
        });
        if (digits[0].price > m)
        {
            Console.Write($"0\n\n");
            return;
        }
        if ((n == 1 || digits[1].price > m) && digits[0].digit == 0)
        {
            Console.Write($"1\n0\n0");
            return;
        }
        long[] count = new long[n];
        if (digits[0].digit == 0)
        {
            count[digits[1].digit]++;
            m -= digits[1].price;
        }
        else
        {
            count[digits[0].digit]++;
            m -= digits[0].price;
        }
        count[digits[0].digit] += m / digits[0].price;
        m %= digits[0].price;
        Array.Sort(digits, (x, y) => y.digit - x.digit);
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                long diff = digits[i].price - digits[j].price;
                if (diff <= 0)
                    continue;
                long change = Math.Min(count[digits[j].digit], m / diff);
                count[digits[i].digit] += change;
                count[digits[j].digit] -= change;
                m -= diff * change;
            }
        }
        long sum = count.Sum();
        StringBuilder sb = new($"{sum}\n"), front = new(), rear = new();
        for (int i = n - 1; i >= 0; i--)
        {
            for (int j = 0; front.Length < 50 && j < count[i]; j++)
            {
                front.Append(i);
            }
        }
        sb.Append(front).Append('\n');
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; rear.Length < 50 && j < count[i]; j++)
            {
                rear.Append(i);
            }
        }
        sb.Append(string.Concat(rear.ToString().Reverse()));
        Console.Write(sb.ToString());
    }
}
#elif other2
using System.Numerics;
using System.Text;
class Program
{
	public static BigInteger[] prices;
	public static int N;
	public static bool Debug = false;
	static void Main()
	{
		N = int.Parse(Console.ReadLine());
		prices = new BigInteger[N];
		var earn = new BigInteger[N];
		string[] p = Console.ReadLine().Split(' ');
		for (int i = 0; i < N; i++)
		{
			prices[i] = BigInteger.Parse(p[i]);
		}
		BigInteger M = BigInteger.Parse(Console.ReadLine());
		int cheaper = -1;
		BigInteger curp = BigInteger.Pow(10, 30);
		for (int i = N - 1; i >= 0; i--)
		{
			if (curp > prices[i] && M >= prices[i])
			{
				cheaper = i;
				curp = prices[i];
			}
		}
			
		if (cheaper == -1) // 구매 가능한 품목이 없다면 반환
		{
			Console.WriteLine("0\n\n");
			return;
		}
		for (int i = 1; i < N; i++)
		{
			if (M >= prices[i]) // 0외엔 구매 불가 체크
			{
				goto OK;
			}
		}
		Console.WriteLine("1");
		Console.WriteLine("0");
		Console.WriteLine("0");
		return;
	OK:
		DebugLog($"First Buy : {cheaper} {M / curp}");
		earn[cheaper] += M / curp;
		M -= curp * earn[cheaper];
		bool firstrun = true;
		while (true)
		{
			bool Changed = false;
			for (int i = 0; i < N - 1; i++)
			{
				DebugLog($"Try sell {i}");
				if (earn[i] == 0) continue;
				int buy = -1;
				BigInteger buynum = 0;
				BigInteger sellnum = 0;
				for (int j = i + 1; j < N; j++)
				{
					BigInteger remain = M / (prices[j] - prices[i]) > earn[i] ? earn[i] : M / (prices[j] - prices[i]);
					if (remain > 0) // 본전 - 현재 자릿수를 유지하면서 더 큰수 j를 구매
					{
						buy = j;
						buynum = remain;
						sellnum = remain;
						DebugLog($"MainTain Do sell {i} buy {buy} sellnum {sellnum} buynum {buynum}");
						continue;
					}
					if (i == 0 && firstrun)// 손절 - 0만 있으면 번호를 이룰 수 없으므로 첫시기에 0은 최소한 강제 판매
					{
							
						if (buy != -1 && buynum == sellnum) continue; // 본전 판매가 체결됐으면 하지 않음
						if (buy != -1 && ((prices[j] - M) / prices[i]) + ((prices[j] - M) % prices[i] == 0 ? 0 : 1) > sellnum) continue; // 이전에 체결한 손절보다 더 손해(자릿수가 더 감소)면 하지 않음
						buy = j;
						buynum = 1;
						sellnum = ((prices[j] - M) / prices[i]) + ((prices[j] - M) % prices[i] == 0 ? 0 : 1);
						DebugLog($"NotZero Do sell {i} buy {buy} sellnum {sellnum} buynum {buynum}");
						continue;
					}
				}
				firstrun = false;
				if (buy == -1)
				{
					/*
					if (earn[i] > 1)
					{
						for (int j = N - 1; j >= i; j--) // 특수 케이스. 윗 자리 하나와 아무 숫자 하나를 현재 자리 2개로 구매하기
						{
							for (int k = N - 1; k >= 0; k--)
							{
								if (j == i || k == i || j == k) continue;
								if (prices[i] * 2 + M >= prices[j] + prices[k])
								{
									earn[i] -= 2;
									earn[j] += 1;
									earn[k] += 1;
									M += prices[i] * 2 - (prices[j] + prices[k]);
									DebugLog($"Special sell {i} buy {j} {k}");
									goto CHANGED;
								}
							}
						}
					}
					*/
					DebugLog("No Change");
					continue;
				}
				DebugLog($"sell {i} buy {buy} sellnum {sellnum} buynum {buynum}");
				earn[i] -= sellnum;
				earn[buy] += buynum;
				M += sellnum * prices[i];
				M -= buynum * prices[buy];
			CHANGED:
				Changed = true;
			}
			DebugLog($"Over Check");
			if (!Changed)
			{
				DebugLog($"Over");
				break;
			}

		}

		BigInteger leng = 0;
		foreach (var big in earn)
		{
			leng += big;
		}
		Console.WriteLine(leng);
		StringBuilder builder = new StringBuilder();
		int v = 50;
		for (int i = earn.Length - 1; i >= 0; i--)
		{
			for (int j = 0; j < earn[i]; j++)
			{
				builder.Append(i);
				v -= 1;
				if (v == 0) goto PRINT1OVER;
			}
		}
	PRINT1OVER:
		DebugLog(builder.ToString().Length.ToString());
		Console.WriteLine(builder.ToString());
		builder = new StringBuilder();
		v = 50;
		for (int i = 0; i < earn.Length; i++)
		{
			for (int j = 0; j < earn[i]; j++)
			{
				builder.Append(i);
				v -= 1;
				if (v == 0) goto PRINT2OVER;
			}
		}
	PRINT2OVER:

		string s = string.Empty;
		foreach (var c in builder.ToString().Reverse())
		{
			s += c;
		}
		DebugLog(s.Length.ToString());
		Console.WriteLine(s);
		return;
	}
	public static long S = 0;
	public static void DebugLog(string msg)
	{
		if (Debug)
		{
			Console.WriteLine(msg);
		}
	}
}
#elif other3
// #define _CRT_SECURE_NO_WARNINGS
// #include <cstdio>
// #define max(x,y) (x>y?x:y)
// #define min(x,y) (x>y?y:x)
long long int ans[51], ans2[51];
long long int cnt , m, j, k, tmp, l, t;
long long int n, s, i, arr[11];
int main(){
	while (scanf("%lld", &n) != EOF)
	{
		for (i = 0; i < n; scanf("%lld", &arr[i++]));
		scanf("%lld", &s);
		if (s == 0){
			printf("0\n");
			continue;
		}
		tmp = m = s;
		k = 0, j = 0, cnt = 0;
		for (i = 1; i < n; ++i){
			if (arr[i] <= m){
				m = arr[i];
				j = i;
			}
		}
		if (j == 0 && arr[0]>m ){ printf("0\n"); continue;}
		else if (j == 0) {
			printf("1\n0\n0\n"); continue;
		}
		tmp -= m;
		for (i = 0; i < n; ++i){
			if (arr[i] <= arr[j]){
				j = i;
			}
		}
		cnt = tmp / arr[j] + 1;
		s -= cnt * arr[j];
		m = arr[j];
		printf("%lld\n", cnt);
		for (i = 0; i < 50 && i < cnt; ans[i++] = j);
		for (i = 0; i < 50; ans2[i++] = j);
		if (cnt <= 50){
			for (i = n - 1; i > j; --i){
				m = arr[i] - arr[j];
				if (s - m < 0) continue;
				tmp = s / m;
				for (l = k; l < min(k + tmp, cnt); ans[l++] = i);
				k += tmp;
				s -= tmp * m;
			}
			for (i = 0; i < cnt; printf("%lld", ans[i++]));
			printf("\n");
			for (i = 0; i < cnt; printf("%lld", ans[i++]));
		}
		else{
			for (i = n - 1; i > j; --i){
				m = arr[i] - arr[j];
				if (s - m < 0) continue;
				tmp = s / m;
				if (k < 50){
					for (l = k; l < min(k + tmp, 50); ans[l++] = i);
				}
				k += tmp;
				if (cnt - k < 50){
					for (l = max(k - tmp - cnt + 50, 0); l < min(50,k - cnt + 50); ans2[l++] = i);
				}
				if (k >= cnt) break;
				s -= tmp * m;
			}
			for (i = 0; i < 50; printf("%lld", ans[i++]));
			printf("\n");
			for (i = 0; i < 50; printf("%lld", ans2[i++]));
		}
		printf("\n");
	}
}
#endif
}
