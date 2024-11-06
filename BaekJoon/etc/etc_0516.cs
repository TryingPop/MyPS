using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 12
이름 : 배성훈
내용 : 카잉 달력
    문제번호 : 6064번

    수학, 브루트포스, 정수론, 중국인의 나머지 정리 문제다
    연립 일차 합동식의 차가 gcd를 나누지 않으면 해가 존재하지 않는다
    그리고 중국인의 나머지 정리로 찾아보려고 했으나,
    합성수인 경우, 막혀서 못했다;
*/

namespace BaekJoon.etc
{
    internal class etc_0516
    {

        static void Main516(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();

            while(test-- > 0)
            {

                int m1 = ReadInt();
                int m2 = ReadInt();

                int y1 = ReadInt() - 1;
                int y2 = ReadInt() - 1;

                int gcd = GetGCD(m1, m2);
                if (ChkInvalidRet(y1, y2, gcd))
                {

                    sw.WriteLine(-1);
                    continue;
                }

                int ret = -1;
                int len = m2 / gcd;

                for (int i = 0; i <= len; i++)
                {

                    int calc = y1 + m1 * i;
                    if (calc % m2 != y2) continue;
                    ret = calc;
                    break;
                }
                sw.WriteLine(ret + 1);
            }

            sr.Close();
            sw.Close();

            bool ChkInvalidRet(int _y1, int _y2, int _gcd)
            {

                int diff = _y1 - _y2;
                diff = diff < 0 ? -diff : diff;

                return diff % _gcd != 0;
            }

            int GetGCD(int _a, int _b)
            {

                if (_a < _b)
                {

                    int temp = _a;
                    _a = _b;
                    _b = temp;
                }

                while(_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
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
// # include <stdio.h>

long long K,L;
long long LCM;

bool LinDio(int a, int b, int u){
    LCM = a*b;
    int Quot[200];
    int c, ind = 0;
    while(c = a%b){
        Quot[ind] = a/b;
        ind += 1; a = b; b = c;
    }
    
    if(u%b!=0) return false;
    
    K = 0; L = 1;
    int tmp;
    while(ind--){
        tmp = K;
        K = L;
        L = tmp - Quot[ind] * L;
    }

    K *= u/b; L *= u/b;
    LCM /= b;

    return true;
}

int main(){
    int T;
    scanf("%d",&T);
    int M,N,x,y;
    long long ans;
    while(T--){
        scanf("%d %d %d %d",&M,&N,&x,&y);
        if(LinDio(M,N,y-x)){
            ans = M*K+x;
            while(ans <= 0 || ans > LCM){
                if(ans <= 0) ans += LCM;
                if(ans > LCM) ans -= LCM;
            }
            printf("%u\n",ans);
        }else{
            printf("%d\n",-1);
        }
    }
#elif other2
import java.util.*;
import java.io.*;

// BJ #6064 - 카잉 달력
// Strategy: 중국인의 나머지 정리, 확장 유클리드 호제법
public class Main {
	// EE: 확장 유클리드 호제법
	public static long[] EE(int a, int b) {
		if(b == 0) {
			return new long[] {a,1,0};
		}
		long[] tmp = EE(b, a%b);
		
		return new long[] {tmp[0], tmp[2], tmp[1]-(a/b)*tmp[2]};
	}
	
	// cal: 카잉달력 계산 by 중국인의 나머지 정리
	public static long cal(int M, int N, int x, int y) {
		long[] tmp = EE(M,N);
		long gcd = tmp[0];
		
		if((y-x) % gcd != 0) {	// 베주 항등식에 의해 x-y가 gcd의 배수가 아니면 계산 불가
			return -1;
		}else {
			long k0 = tmp[1];
			long res = (M * k0 * ((y-x)/gcd) + x) % (M * N / gcd);
			while(res <= 0) {
				res += M * N / gcd;   // M*N/gcd -> lcm of M and N
			}
			return res;
		}
	}
	public static void main(String[] args) throws Exception{
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringBuilder sb = new StringBuilder();
		
		// 
		int T = Integer.parseInt(br.readLine());
		
		for(int t=0; t<T; t++) {
			StringTokenizer st = new StringTokenizer(br.readLine());
			int M = Integer.parseInt(st.nextToken());
			int N = Integer.parseInt(st.nextToken());
			int x = Integer.parseInt(st.nextToken());
			int y = Integer.parseInt(st.nextToken());
			
//			if(M < N) {
//				int tmp = M;
//				int tmp2 = x;
//				M = N;
//				N = tmp;
//				x = y;
//				y = tmp2;
//			}
			
			sb.append(cal(M,N,x,y)).append("\n");

		}
		
		// 최종 결과 출력
		System.out.println(sb);
	}
}
#endif
}
