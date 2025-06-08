using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 11
이름 : 배성훈
내용 : 부분 문자열
    문제번호 : 16916번

    KMP 알고리즘 연습해본다
*/

namespace BaekJoon.etc
{
    internal class etc_0019
    {

        static void Main19(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            string str = sr.ReadLine();

            string chk = sr.ReadLine();

            sr.Close();

            /// 검사에서 쓰일 조건에 맞게 만들어준다!
            /// 그래서 검사 부분을 먼저 알아야한다!
            // 해당 인덱스까지 같고 다음 인덱스에서 다른 경우
            // 돌아갈 구간을 값으로 갖는다
            int[] back = new int[chk.Length];

            for (int i = 1; i < chk.Length; i++)
            {

                // 앞번에 같은 것의 값을 가져온다
                int cur = back[i - 1];

                if (chk[i] == chk[cur])
                {

                    // 현재 값과 가져온 값이 같은 경우
                    // 돌아갈 인덱스 값 추가
                    back[i] = cur + 1;
                }
                else
                {

                    // 현재 문자와 찾는 문자가 다르다
                    // cur == 0번과 다르면 다음 문자로 건너가야하는 경우다!
                    while(cur > 0)
                    {

                        // cur != 0인 경우 여기로 온다
                        // 그러면, back[cur - 1]까지는 같다가 보장된 상태이다
                        // 계속 반복해서 조사한다
                        cur = back[cur - 1];
                        
                        if (chk[i] == chk[cur])
                        {

                            // 같은 경우 + 1
                            back[i] = cur + 1;
                            break;
                        }
                    }
                }
            }

            // 이제검사
            // matching된 처음부터 문자 개수
            int matching = 0;
            bool contains = false;
            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == chk[matching])
                {

                    // 같은 경우 자리수 증가
                    matching++;
                }
                else
                {

                    // 다른 경우다!
                    // 0번과 미스매칭하면 다음 문자로 넘겨야 한다!
                    while (matching > 0)
                    {

                        // ABCDABCDABE 에서 
                        // ABCDABE 가 있는지 확인하자!
                        // ABCDAB'C'
                        // 에서 C != E이므로 여기로 온다
                        //
                        // C에서 그러면 처음으로 돌아가서 비교해야하는가?
                        // 처음으로 돌아가면 없다고 뜬다!
                        // 실제로 ABCD'ABCDABE'가 존재하는데도 말이다
                        //
                        // 앞의 AB까지는 같다
                        // 그러면 C와 비교해서 ABCD'ABC'로 matching 되는게 3개로 줄어드는게 맞아보인다
                        // 여기서 C와 비교하게 도와주는게 back 배열이고 해당 구간에서 검증한다!
                        matching = back[matching - 1];
                        if (str[i] == chk[matching])
                        {

                            // 같은 경우 문자수 증가 시키고 탈출
                            matching++;
                            break;
                        }
                    }
                }

                // 전부 찾은 경우 포함되었다!
                if (matching == chk.Length) 
                {

                    contains = true;
                    break; 
                }
            }

            // 출력
            Console.WriteLine(contains ? 1 : 0);
        }
    }

#if other1
using System;

public class Program
{
	static int[] pi;
	static bool flag = false;
	
	public static void Kmp(string p){
		var n = p.Length;
		int i = -1, j = 0;
		pi[j] = i;
		while(j < n){
			if(i == -1 || p[i] == p[j])
				pi[++j] = ++i;
			else
				i = pi[i];
		}
	}
	
	public static void FindP(string t, string p){
		var n = t.Length;
		var m = p.Length;
		int i = 0, j = 0;
		while(i < n){
			if(j == -1 || t[i] == p[j]){
				i++; j++;
			}else
				j = pi[j];
			
			if(j == m){
				flag = true;
				j = pi[j];
				break;
			}				
		}		
	}
	
	public static void Main()
	{
		var t = Console.ReadLine();
		var p = Console.ReadLine();
		
		pi = new int[p.Length+1];
		Kmp(p);
		FindP(t, p);
		
		if(flag == true)
			Console.WriteLine(1);
		else
			Console.WriteLine(0);
	}
}
#elif other2
using System;

class GFG
{
    public static bool print;
    void KMPSearch(string pat, string txt)
    {
        int M = pat.Length;
        int N = txt.Length;

        int[] lps = new int[M];
        int j = 0; 

        computeLPSArray(pat, M, lps);

        int i = 0;
        while (i < N)
        {
            if (pat[j] == txt[i])
            {
                j++;
                i++;
            }
            if (j == M)
            {
               
                print = true;
                break;
            }

            else if (i < N && pat[j] != txt[i])
            {
   
                if (j != 0)
                    j = lps[j - 1];
                else
                    i = i + 1;
            }
        }
        if (print)
        {
            Console.WriteLine(1);
        }
        else
        {
            Console.WriteLine(0);
        }
    }

    void computeLPSArray(string pat, int M, int[] lps)
    {

        int len = 0;
        int i = 1;
        lps[0] = 0; // lps[0] is always 0

 
        while (i < M)
        {
            if (pat[i] == pat[len])
            {
                len++;
                lps[i] = len;
                i++;
            }
            else 
            {
                
                if (len != 0)
                {
                    len = lps[len - 1];

                   
                }
                else
                {
                    lps[i] = len;
                    i++;
                }
            }
        }
    }

   
    public static void Main()
    {
        string txt = (Console.ReadLine());

        string pat = (Console.ReadLine());
        print = false;
        new GFG().KMPSearch(pat, txt);
    }
}


#elif other3

using System;

// CapitalLetters for class names and methods, camelCase for variable names.
// Write your code clearly enough so that it doesn't need to be commented, or at least, so that it rarely needs to be commented.

namespace Testpad
{
    public class BaekJoon16916
    {
        const long x = 34;
        const long mod = 200560490131;
     
        static void Main()
        {
            char[] input = Console.ReadLine().ToCharArray(); // 주어지는 문자열
            char[] child = Console.ReadLine().ToCharArray(); // 부분 문자열

            long currentInputHash = 0; // 주어지는 문자열에서 현재 보는 부분의 해시값
            long childHash = 0; // 부분 문자열의 해시값

            // 주어지는 문자열보다 부분 문자열의 길이가 더 긴 경우의 예외처리
            if (input.Length < child.Length)
            {
                Console.Write(0);
                return;
            }

            long[] dp = new long[child.Length]; // x^(특정값) % mod 저장용

            // dp값을 채움
            dp[0] = 1;
            for (int i = 1; i < dp.Length; i++)
            {
                dp[i] = dp[i - 1] * x % mod;
            }

            // 부분 문자열의 해시값과 주어지는 문자열 처음 부분의 해시값을 구함
            for (int i = 0; i < child.Length; i++)
            {
                childHash = (childHash + child[i] * dp[child.Length - i - 1]) % mod;
                currentInputHash = (currentInputHash + input[i] * dp[child.Length - i - 1]) % mod;
            }

            if (childHash == currentInputHash)
            {
                Console.Write(1);
                return;
            }

            // 나머지 해시값들도 구해 비교함
            for (int i = 1; i <= input.Length - child.Length; i++)
            {
                currentInputHash = x * (currentInputHash - input[i - 1] * dp[child.Length - 1]) % mod;

                if (currentInputHash < 0)
                {
                    currentInputHash += mod;
                }

                currentInputHash = (currentInputHash + input[i + child.Length - 1]) % mod;

                if (childHash == currentInputHash)
                {
                    Console.Write(1);
                    return;
                }
            }

            // 답이 안나왔으면 0 출력
            Console.Write(0);
        }
    }
}
#endif
}
