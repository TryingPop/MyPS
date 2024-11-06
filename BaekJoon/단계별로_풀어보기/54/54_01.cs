using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 12
이름 : 배성훈
내용 : 돌아온 떡파이어
    문제번호 : 15718번

    중국인의 나머지 정리, 뤼카의 정리 문제다    

    아이디어는 다음과 같다
    N살까지만 살아야한다

    그러면 n - 1살까지는 적어도 1개의 떡국을 먹으면서 생존해야한다
    만약 이는 중복조합이 되고
    m - 1개에 중복을 허용해서 n - m + 1 개를 뽑아라는 의미가된다
    즉, m - 1 H n - m + 1 = n - 1 C n - m + 1 = n - 1 C m - 2가된다

    여기서 n, m의 범위가 10억 범위를 가진다 100007은 소수 97과 1031로 나뉘어지므로
    조합(C)을 구하기 위해서는 뤼카의정리로 풀어야한다

    97과 1031의 범위가 작으므로 팩토리얼 값과 역원을 모두 찾았다;
    역원을 찾는 것은 정수론 때 배운 위수가 mod - 1인 원소를 찾아 풀었다
    그러니 97은 5, 1031은 61이되었다 (전처리로 몇 개 대입하면서 찾았다)
    그리고 페르마 소정리로 역원을 찾을 수 있다

    이렇게 역원과 팩토리얼을 모두 찾고 
    조합은 nCr 은 n! * ((r!)^-1) * (((n-r)!)^-1)의 형태이다
        이는 nCr = (n!) / (r!) * ((n - r)!)
        의 양변에 (r!)^(p-1) * (n-r)^(p-1)을 곱해준다
        그러면, 
            n! * (r!)^(p-2) * ((n-r)!)^(p-2)    = (r!)^(p-1) * (n-r)^(p-1) * nCr 
                                                = 1 * 1 * nCr(mod p)
        그리고 (r!)^(p-2) = (r!)^-1과 같다 -1승의 정의 & 페르마 소정리!
        이렇게 해당 식이 나온다

    그런데 중국인의 나머지 정리를 잘못했고
    중간에 오버플로우가 날 수 있는 식이 있긴했다(뤼카의 정리) ret 값 구할 때가 그렇다!
    그리고 예외처리를 반대로 한 경우도 있었고, 뤼카 자체도 잘못된 경우가 있었다
    이렇게 엄청나게 틀리고 통과했다
*/
namespace BaekJoon._54
{
    internal class _54_01
    {

        static void Main1(string[] args)
        {

            // 100007
            int MOD = 100_007;
            int MOD1 = 97;
            int MOD2 = 1_031;

            string ZERO = "0\n";
            string ONE = "1\n";

            StreamReader sr;
            StreamWriter sw;

            int[] fac1, fac2;
            int[] inv1, inv2;

            int[] div;
            int n, m;

            int[] ord;
            List<int> divN, divM;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while (test-- > 0)
                {

                    n = ReadInt();
                    m = ReadInt();

                    if (m - n == 1)
                    {

                        sw.Write(ONE);
                        continue;
                    }
                    else if (m - n > 1)
                    {

                        sw.Write(ZERO);
                        continue;
                    }

                    n--;
                    m -= 2;
                    if (m == -1)
                    {

                        sw.Write(ZERO);
                        continue;
                    }

                    div[0] = Lucas(n, m, fac1, inv1, MOD1);
                    div[1] = Lucas(n, m, fac2, inv2, MOD2);

                    int ret = Crt();
                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            int Crt()
            {

                return (97 * 659 * div[1] + 1031 * 35 * div[0]) % MOD;
            }

            int Lucas(int _n, int _m, int[] _fac, int[] _inv, int _mod)
            {

                while(_n > 0 || _m > 0)
                {

                    divN.Add(_n % _mod);
                    divM.Add(_m % _mod);

                    _n /= _mod;
                    _m /= _mod;
                }

                int ret = 1;
                for (int i = 0; i < divN.Count; i++)
                {

                    ret = (ret * GetComb(divN[i], divM[i], _fac, _inv, _mod)) % _mod;
                }

                divN.Clear();
                divM.Clear();
                return ret;
            }

            int GetComb(int _a, int _b, int[] _fac, int[] _inv, int _mod)
            {

                if (_a < _b) return 0;

                int f1 = _fac[_a];
                int f2 = _fac[_b];
                int f3 = _fac[_a - _b];

                return (f1 * _inv[f2] * _inv[f3]) % _mod;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                divN = new(10);
                divM = new(10);

                fac1 = new int[MOD1];
                fac2 = new int[MOD2];

                FillFac(fac1, MOD1);
                FillFac(fac2, MOD2);

                int[] calc = new int[MOD2];
                inv1 = new int[MOD1];
                inv2 = new int[MOD2];

                ord = new int[2] { 5, 61 };
                FillInv(inv1, calc, ord[0], MOD1);
                FillInv(inv2, calc, ord[1], MOD2);

                div = new int[2];
            }

            void FillFac(int[] _fac, int _mod)
            {

                _fac[0] = 1;

                for (int i = 1; i < _mod; i++)
                {

                    _fac[i] = (_fac[i - 1] * i) % _mod;
                }
            }

            void FillInv(int[] _inv, int[] _calc, int _ord, int _mod)
            {

                _calc[0] = 1;
                for (int i = 1; i <= _mod - 1; i++)
                {

                    _calc[i] = (_calc[i - 1] * _ord) % _mod;
                }

                _inv[1] = 1;
                for (int i = 1; i < _mod; i++)
                {

                    _inv[_calc[i]] = _calc[_mod - 1 - i];
                }
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
    }

#if other
using System;
using System.Text;
using System.Numerics;
using System.Collections.Generic;

BigInteger C(BigInteger n, BigInteger k)
{
	if (k > n)
		return 0;
	
	if (n - k < k)
		return C(n, n - k);
	
	BigInteger result = 1;
	
	for (BigInteger i = n - k + 1; i <= n; i++)
		result *= i;
	
	for (BigInteger i = 2; i <= k; i++)
		result /= i;
	
	return result;
}

int t = int.Parse(Console.ReadLine());

StringBuilder outputBuilder = new();

for (int testCount = 0; testCount < t; testCount++)
{
	string[] input = Console.ReadLine().Split(' ');
	
	BigInteger n = BigInteger.Parse(input[0]);
	BigInteger m = BigInteger.Parse(input[1]);
	
	if (n == 0 && m == 1)
	{
		outputBuilder.AppendLine("1");
		continue;
	}
	else if (n == 0 || m == 1)
	{
		outputBuilder.AppendLine("0");
		continue;
	}
	
	List<BigInteger> n97 = new();
	List<BigInteger> m97 = new();

	BigInteger tempn = n - 1;
	BigInteger tempm = m - 2;
	
	while (tempn != 0 || tempm != 0)
	{
		n97.Add(tempn % 97);
		m97.Add(tempm % 97);
		
		tempn /= 97;
		tempm /= 97;
	}
	
	BigInteger a97 = 1;
	
	for (int i = 0; i < n97.Count; i++)
	{
		a97 *= C(n97[i], m97[i]);
		a97 %= 97;
	}
	
	List<BigInteger> n1031 = new();
	List<BigInteger> m1031 = new();

	tempn = n - 1;
	tempm = m - 2;
	
	while (tempn != 0 || tempm != 0)
	{
		n1031.Add(tempn % 1031);
		m1031.Add(tempm % 1031);
		
		tempn /= 1031;
		tempm /= 1031;
	}
	
	BigInteger a1031 = 1;
	
	for (int i = 0; i < n1031.Count; i++)
	{
		a1031 *= C(n1031[i], m1031[i]);
		a1031 %= 1031;
	}
	
	BigInteger result = (36085 * a97 + 63923 * a1031) % 100007;
	
	outputBuilder.AppendLine(result.ToString());
}

Console.Write(outputBuilder.ToString());
#elif other2
// #include <cstdio>
// #define ff 97
// #define ss 1031
typedef long long ll;

int lucas(int n, int k, int fac[], int inv[], int p) {
	int t1, t2, sum = 1;

	while (n || k) {
		t1 = n % p; t2 = k % p;
		if (t1 < t2) return 0;
		sum = sum * ((fac[t1] * inv[t2] % p) * inv[t1 - t2] % p) % p;
		n /= p, k /= p;
	}

	return sum;
}

int uclid(int r1, int r2) {
	int r, t, s, q, p = r1;
	int s1 = 1, s2 = 0;
	int t1 = 0, t2 = 1;

	while (r2) {
		q = r1 / r2;
		r = r1 - q * r2;
		r1 = r2; r2 = r;

		s = s1 - q * s2;
		s1 = s2; s2 = s;

		t = t1 - q * t2;
		t1 = t2; t2 = t;
	}

	if (t1 < 0) t1 += p;
	return t1;
}

void init(int fac[], int inv[], int p) {
	for (ll i = 1; i < p; i++)
		fac[i] = fac[i - 1] * i % p;

	inv[p - 1] = uclid(p, fac[p - 1]);
	for (ll i = p - 1; i >= 1; i--)
		inv[i - 1] = inv[i] * i % p;
}

int china(int a, int b, int ap, int bp, int MOD) {
	int sum = 0, t1 = MOD / ap, t2 = MOD / bp;

	sum = (sum + (ll)t1 * uclid(ap, t1 % ap) % MOD * a % MOD) % MOD;
	sum = (sum + (ll)t2 * uclid(bp, t2 % bp) % MOD * b % MOD) % MOD;

	return sum;
}

int main() {
	int t, n, k, a1, a2;
	scanf("%d", &t);

	int fac1[ff] = { 1 }, fac2[ss] = { 1 };
	int inv1[ff], inv2[ss];

	init(fac1, inv1, ff);
	init(fac2, inv2, ss);

	while (t--) {
		scanf("%d %d", &n, &k);

		if (n == 0 && k == 1) { printf("1\n"); continue; }
		n--; k -= 2;
		if (k == -1) { printf("0\n"); continue; }

		a1 = lucas(n, k, fac1, inv1, ff);
		a2 = lucas(n, k, fac2, inv2, ss);

		printf("%d\n", china(a1, a2, ff, ss, ff * ss));
	}
}
#elif other3
fac97_list = [1 for i in range(97)]
fac1031_list = [1 for i in range(1031)]
facinv97_list = [1 for i in range(97)]
facinv1031_list = [1 for i in range(1031)]

def modular_inverse_exEuclid(a, mod):
// # gcd(a,mod) == 1 
    if a == 0:
        return mod, 0, 1
    else:
        gcd, x, y = modular_inverse_exEuclid(mod % a, a)
        
        return gcd, y - (mod // a) * x, x

def get_fac97_list():
    for i in range(1,97):
        fac97_list[i] = (i * fac97_list[i-1]) % 97
        facinv97_list[i] = modular_inverse_exEuclid(fac97_list[i],97)[1] %97
def get_fac1031_list():
    for i in range(1,1031):
        fac1031_list[i] = (i * fac1031_list[i-1]) % 1031
        facinv1031_list[i] = modular_inverse_exEuclid(fac1031_list[i],1031)[1] %1031

get_fac97_list()
get_fac1031_list()

def get_ujin(n,m,u):
    n_j = []
    m_j = []
    while n != 0:
        n_j.append(n%u)
        n //= u
    for _ in range(len(n_j)):
        m_j.append(m%u)
        m //= u
    return n_j, m_j

def get_bi_co_97(n,m):
    n_j,m_j = get_ujin(n,m,97)

    res_97 = 1
    for j in range(len(n_j)):
        if n_j[j] < m_j[j]: return 0
        res_97 *= fac97_list[n_j[j]] * facinv97_list[m_j[j]] * facinv97_list[n_j[j]-m_j[j]]
        res_97 %= 97
    return res_97
    
def get_bi_co_1031(n,m):
    n_j,m_j = get_ujin(n,m,1031)

    res_1031 = 1
    for j in range(len(n_j)):
        if n_j[j] < m_j[j]: return 0
        res_1031 *= fac1031_list[n_j[j]] * facinv1031_list[m_j[j]] * facinv1031_list[n_j[j]-m_j[j]]
        res_1031 %= 1031
    return res_1031

def Chinese_Remainder_Theorem(Ns,Rs):
    sum = 0
    prod = 1
    for n in Ns:
        prod *= n
    for i in range(len(Ns)):
        p = prod // Ns[i]
        sum += Rs[i] * (modular_inverse_exEuclid(p, Ns[i])[1] % Ns[i]) * p

    return sum % prod
def sol(N,M):
    if not(M-1 >= 0 and N-M+1 >= 0): return 0
    if (N,M) == (0,1): return 1
    if (N,M) == (1,1): return 0
    res_97 = get_bi_co_97(N-1,M-2)
    res_1031 = get_bi_co_1031(N-1,M-2)
    return Chinese_Remainder_Theorem([97,1031],[res_97,res_1031])

import sys
input_s = sys.stdin.readline

T = int(input_s())

for _ in range(T):
    N,M = map(int, input_s().split())
    print(sol(N,M))

#elif other4
import java.io.*;
import java.util.*;

public class Main {
    static Reader r = new Reader();
    static StringBuilder sb = new StringBuilder();

    static int power(long a, long b, int c){
        // return a^b (mod c)
        long x = 1; long y = a;
        while(b>0){
            if((b&1)>0) x = (x*y)%c;
            y = (y*y)%c;
            b>>=1;
        }
        return (int)x;
    }
    static int small_nCk(int n, int k, int p){
        // return nCk(mod p) (n<p<40000)
        if(n<k) return 0;
        int a = 1, b = 1;
        if(2*k>n) k = n-k;
        for(int i=0;i<k;i++){
            a = (a*(n-i))%p;
            b = (b*(i+1))%p;
        }
        return (a*power(b,p-2,p))%p;
    }

    static int nCk(long n, long k, int p){
        // return nCk(mod p) (n<10**18, p<2000)
        // using Lucas' Theorem
        if(k<0) return n<0 ? 1 : 0;
        if(n<k) return 0;
        int res = 1;
        while(n>0){
            res = (res * small_nCk((int)(n%p),(int)(k%p),p))%p;
            n/=p; k/=p;
        }
        return res;
    }

    public static void main(String args[]) throws Exception {
        int T = r.readInt();
        while(T-->0){
            int n = r.readInt(), m = r.readInt();
            int r = 36085*nCk(n-1,m-2,97)-36084*nCk(n-1,m-2,1031);
            if(r<0) r = ((r+1)%100007)+100006;
            else r = r%100007;
            sb.append(r).append("\n");
        }
        System.out.println(sb);
    }
}

class Reader {
    final private int BUFFER_SIZE = 1 << 16;
    private DataInputStream din;
    private byte[] buffer;
    private int bufferPointer, bytesRead;

    public Reader() {
        din = new DataInputStream(System.in);
        buffer = new byte[BUFFER_SIZE];
        bufferPointer = bytesRead = 0;
    }
    public int readInt() throws IOException {
        int ret = 0;
        byte c = read();
        while(c <= ' '){ c = read();}
        boolean neg = (c == '-');
        if(neg) c = read();
        do{
            ret = (ret<<3) + (ret<<1) + c - '0';
        } while ((c = read()) >= '0' && c <= '9');
        return neg ? -ret : ret;
    }

    public String readLine() throws IOException {
        byte[] buf = new byte[10000005]; // line length
        int cnt = 0, c;
        while((c=read())!=-1){
            if(c=='\n'){
                if(cnt!=0) break;
                else continue;
            }
            buf[cnt++] = (byte)c;
        }
        return new String(buf, 0, cnt);
    }

    public long readLong() throws IOException {
        long ret = 0;
        byte c = read();
        while(c <= ' '){ c = read();}
        boolean neg = (c == '-');
        if(neg) c = read();
        do{
            ret = (ret<<3) + (ret<<1) + c - '0';
        } while ((c = read()) >= '0' && c <= '9');
        return neg ? -ret : ret;
    }

    private void fillBuffer() throws IOException {
        bytesRead = din.read(buffer, bufferPointer = 0, BUFFER_SIZE);
        if(bytesRead == -1) buffer[0] = -1;
    }

    private byte read() throws IOException {
        if(bufferPointer == bytesRead) fillBuffer();
        return buffer[bufferPointer++];
    }

    public void close() throws IOException {
        if(din==null) return;
        din.close();
    }
}
#endif
}
