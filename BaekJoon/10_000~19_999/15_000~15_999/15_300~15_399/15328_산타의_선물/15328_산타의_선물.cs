using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 26
이름 : 배성훈
내용 : 산타의 선물
    문제번호 : 15328번

    임의 정밀도 / 큰 수 연산 문제다
    문제 제작 후기를 보니 double 오차로는 무조건 틀린다
    후기 사이트 : https://blog.kyouko.moe/13

    double 의 경우 2^-53까지라 한다
    long double은 2^-64까지라 한다
    그런데 해당 문제에서는 충분하지 못한 정확도라 한다

    푼사람을 보니 Sqrt 함수를 직접 구현했는데,
    해당 방법으로 구현하니 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_1001
    {

        static void Main1001(string[] args)
        {

            string YES = "YES\n";
            string NO = "NO\n";

            StreamReader sr;
            StreamWriter sw;

            int n;
            (int x, int y, int z)[] home;

            Solve();
            void Solve()
            {

                Init();

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    Input();

                    sw.Write(GetRet() ? YES : NO);
                }

                sr.Close();
                sw.Close();
            }

            bool GetRet()
            {

                decimal dis = 0;
                for (int i = 1; i <= 4; i++)
                {

                    int x = Abs(home[i].x - home[i - 1].x);
                    int y = Abs(home[i].y - home[i - 1].y);
                    int z = Abs(home[i].z - home[i - 1].z);

                    dis += MySqrt(x * x + y * y + z * z);
                }

                return dis <= n;

                int Abs(int _n)
                {

                    return _n < 0 ? -_n : _n;
                }

                decimal MySqrt(int _num)
                {

                    if (_num == 0) return 0;
                    decimal a = 1, b = 0;

                    do
                    {

                        decimal t = (a + (_num / a)) / 2;
                        b = a;
                        a = t;
                    } while (a != b);

                    return a;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                home = new (int x, int y, int z)[5];
            }

            void Input()
            {

                n = ReadInt();
                for (int i = 1; i <= 4; i++)
                {

                    home[i] = (ReadInt(), ReadInt(), ReadInt());
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
// #include <bits/stdc++.h>
// #include <unistd.h>
using namespace std;

namespace fio {
const int BSIZE = 524288;
char buffer[BSIZE];
int p = BSIZE;
inline char readChar() {
if(p == BSIZE) {
    syscall(0x00, 0, buffer, BSIZE);
    p = 0;
}
return buffer[p++];
}
int readInt() {
unsigned char c = readChar();
while (c < '-') {
c = readChar();
}
int ret = 0; bool neg = c == '-';
if (neg) c = readChar();
while (c >= '0') {
ret = ret * 10 + c - '0';
c = readChar();
}
return neg ? -ret : ret;
}
}

__float128 sqrt128(int x)
{
    if (x == 0) return 0;
    __float128 ret = sqrt((long double)x);
    ret = (ret + x / ret) * 0.5;
    return ret;
}

int main()
{
    int T = fio::readInt();
    
    while (T--)
    {
        int x = fio::readInt();
        __float128 total = 0;
        int a = 0, b = 0, c = 0;
        for (int i = 0; i < 4; i++)
        {
            int d, e, f;
            d = fio::readInt();
            e = fio::readInt();
            f = fio::readInt();
            total += sqrt128((a-d)*(a-d) + (b-e)*(b-e) + (c-f)*(c-f));
            tie(a,b,c) = tie(d,e,f);
        }

        if (total <= x) printf("YES\n");
        else printf("NO\n");
    }
    fflush(stdout);
    _Exit(0);
}
#elif other2
// #include<unistd.h>
// #include<cmath>
// #include<tuple>
using namespace std;
using real = __float128;
const char *answer[] = {"NO", "YES"};
char buf[3000000], *p = buf;

inline int read(int ret = 0, int sgn = 1) {
    while (*p < 45) ++p;
    if (*p == 45) ++p, sgn = -1;
    while (*p > 47) {
        ret = (ret * 10) + (*p^48), ++p;
    }
    return ret * sgn;
}

const real EPS(1E-80);
inline real absq(real n) {
    return n < 0 ? -n : n;
}

inline real sqrtq(real n, real g = 1) {
    g = sqrtl((long double)n);
    while (absq(n - g * g) > EPS) {
        if (g == (g + n / g) / 2) break;
        g = (g + n / g) / 2;
    }
    return g;
}

inline int square(int x) {
    return x * x;
}

inline real norm(const auto& x) {
    return sqrtq(square(get<0>(x)) + square(get<1>(x)) + square(get<2>(x)));
}

int main()
{
    read(0, buf, sizeof buf);
    tuple<int,int,int> d[4];
    for (int T = read(); T--;) {
        int X = read();
        for (int i = 0; i < 4; i++) d[i] = {read(), read(), read()};
        __float128 t = norm(d[0]);
        for (int i = 1; i < 4; i++) {
            t += norm(make_tuple(get<0>(d[i]) - get<0>(d[i-1]),
                                 get<1>(d[i]) - get<1>(d[i-1]),
                                 get<2>(d[i]) - get<2>(d[i-1])));
        }
        puts(answer[X >= t]);
    }
    return 0;
}

#elif other3
def g(v,w,x,y,z):  # True if z < sqrt(v) + sqrt(w) + sqrt(x) + sqrt(y)
// # here we assume that v < w < x < y
    if z*z<x+y or (z*z-x-y)**2<x*y:  # z < sqrt(x) + sqrt(y)
        return True
// # z - sqrt(x) - sqrt(y) < sqrt(v) + sqrt(w) -> square both side
// # a + 2 sqrt(xy) - 2z sqrt(x) - 2z sqrt(y) < 2 sqrt(vw)
    a=z*z+x+y-v-w
// # first we will check if Left-hand side is less than 0
// # a + 2 sqrt(xy) < 2z (sqrt(x) + sqrt(y)) -> square both side
// # b < (8z^2 - 4a) sqrt(xy)
    b=a*a+4*x*y-4*z*z*(x+y)
    if b<0:
        return True
    if b*b<(8*z*z-4*a)**2*x*y:
        return True
// # now square both side of a + 2 sqrt(xy) - 2z sqrt(x) - 2z sqrt(y) < 2 sqrt(vw)
// # p + q sqrt(xy) < r sqrt(x) + s sqrt(y)
    p,q,r,s=a*a+4*x*y+4*z*z*(x+y)-4*v*w,8*z*z+4*a,4*z*(a+2*y),4*z*(a+2*x)
// # we can check that p,q,r,s are greater than 0, so just square both side
// # t < u sqrt(xy)
    t,u=p*p+q*q*x*y-r*r*x-s*s*y,2*(r*s-p*q)
    if t<0:
        if u>=0:  # t < 0 <= u sqrt(xy)
            return True
        else:  # t < u sqrt(xy) < 0 -> squaring both side flips the inequality
            return u*u*x*y<t*t
    if u<0:  # t >= 0 > u sqrt(xy)
        return False
// # 0 < t < u sqrt(xy) -> square both side
    return t*t<u*u*x*y

def h():
    z=int(input())
    a=[0,0,0]
    d=[]
    for i in range(4):
        b=list(map(int,input().split()))
        d.append(sum([(b[i]-a[i])**2 for i in range(3)]))
        a=b
    d.sort()
    print("NO" if g(d[0],d[1],d[2],d[3],z) else "YES")

T=int(input())
for _ in range(T):
    h()
#endif
}
