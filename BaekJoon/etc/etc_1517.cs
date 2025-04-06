using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 4
이름 : 배성훈
내용 : 정확해
    문제번호 : 1457번

    수학, 정수론 문제다.
    아이디어는 다음과 같다.    
    먼저 1과 자기자신을 제외한 약수의 갯수를 에라토스테네스처럼 찾고 k^n이 나눠떨어지는 갯수를 확인했다.
    그리고 두 차를 빼면 정답이 된다.
    시간 복잡도는 O (M log M) 이고 여기서 M은 A + B가 된다.
    디버깅 중 시간이 오래 걸렸고 그냥 제출하면 시간초과날거 같았다.

    그래서 해당 코드를 분석하니 배수 k에 대해 갯수를 세고 k^n 갯수만큼 제거하면 된다.
    이렇게 하면 O(M)에 해결 가능하다.

    다른 사람 풀이를 확인하니 harmonic lemma 를 쓰는 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1517
    {

        static void Main1517(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            long[] val = new long[3_500];
            val[1] = 1;
            int len = 1;
            for (len = 2; len < 4_000; len++)
            {

                val[len] = len;
                for (int j = 1; j < input[2]; j++)
                {

                    val[len] *= len;
                    if (len > input[0] + input[1]) break;
                }

                if (val[len] <= input[0] + input[1]) continue;
                len--;
                break;
            }

#if first
            long sub = 0;
            for (int i = 2; i <= len; i++)
            {

                sub += Cnt(input[0] + input[1], val[i]) - Cnt(input[0] - 1, val[i]);
            }

            int[] divs = new int[input[0] + input[1] + 1];
            for (int i = 2; i < divs.Length; i++)
            {

                for (int j = i + i; j < divs.Length; j += i)
                {

                    divs[j]++;
                }
            }

            long ret = 0;
            for (int i = input[0]; i <= input[0] + input[1]; i++)
            {

                ret += divs[i];
            }


            Console.Write(ret - sub);

#else

            int end = input[0] + input[1];
            int start = input[0] - 1;
            int half = 1 + end / 2;
            long ret = 0;
            for (int i = 2; i <= half; i++)
            {

                ret += Cnt(end, i, false) - Cnt(start, i, false);
                
                if (i > len) continue;
                ret -= Cnt(end, val[i], true) - Cnt(start, val[i], true);
            }
            
            Console.Write(ret);
#endif
            long Cnt(long _end, long _div, bool _chk = true)
            {


                long ret = _end / _div;
                if (_chk) return ret;
                else return ret > 0 ? ret - 1 : ret;
            }
        }
    }

#if other
// #include<stdio.h>
typedef long long ll;
ll i,j,k,a,b,c,n,ans=0,cnt,tmp=1;
int main(){
    scanf("%lld%lld%lld",&a,&b,&n);
    for(ll c=a-1;c<=a+b;c+=b+1){
        tmp*=-1;
        if(c<=0)continue;
        cnt=0;
        for(i=1;i*i<=c;++i)cnt+=c/i;
        i--;
        ans+=tmp*(cnt*2-i*i-c);
        cnt=0;
        for(i=1;;++i){
            k=i;
            for(j=1;j<n;++j)k*=i;
            if(k>c)break;
            cnt+=c/k;
        }
        ans-=tmp*(cnt-1);
    }
    printf("%lld",ans);
    return 0;
}
#endif
}
