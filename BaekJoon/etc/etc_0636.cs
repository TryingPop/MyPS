using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 27
이름 : 배성훈
내용 : 아카라카
    문제번호 : 23304번

    문자열, 재귀 문제다
    분할 정복으로 해결했다

    처음에 그냥 팰린드롬이면 되는줄 알아서 접두사 접미사가 팰린드롬인지만 비교했으나,
    3번 틀리고 문제를 다시 읽어보니 접두사 접미사도 아카라카 팰린드롬인지 체크했어야 했다
    아카라카 팰린드롬 확인은 분할정복으로 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0636
    {

        static void Main636(string[] args)
        {

            string YES = "AKARAKA";
            string NO = "IPSELENTI";
            StreamReader sr;
            string str;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                str = sr.ReadLine();
                sr.Close();

                bool ret = DivConq(0, str.Length);
                Console.Write(ret ? YES : NO);
            }

            bool DivConq(int _s, int _len)
            {

                if(_len == 1) return true;

                for (int i = _s; i < _len; i++)
                {

                    int a = i;
                    int b = _len - 1 - i;
                    if (a > b) break;
                    if (str[a] == str[b]) continue;

                    return false;
                }

                int half = _len / 2;
                bool ret = DivConq(_s, half);
                ret &= DivConq(_s + _len - half, half);

                return ret;
            }
        }
    }

#if other
import java.util.*;
import java.io.*;

public class Main {
	
	public static void main(String[] args) throws Exception {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		String S = br.readLine();
		String first = S;
		
		while(S.length() != 1) {
			if(!logic(S)) {
				System.out.println("IPSELENTI");
				return;
			}
			S = S.substring(0, (S.length() / 2));
		}
		System.out.println("AKARAKA");
	}
	
	public static boolean logic(String str) {
		int start = 0;
		int end = str.length() - 1;
		//짝수 일 때
		if(str.length() % 2 == 0) {
			while(true) {
				if(str.charAt(start) != str.charAt(end)) return false;
				start++;
				end--;
				if(start > end) break;
			}
		}
		//홀수 일 때
		else {
			while(true) {
				if(str.charAt(start) != str.charAt(end)) return false;
				start++;
				end--;
				if(start == end) break;
			}
		}
		return true;
	}

}
#endif
}
