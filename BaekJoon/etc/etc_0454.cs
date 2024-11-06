using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : 이 사람 왜 이렇게 1122를 좋아함?
    문제번호 : 26597번

    구현 문제다
    아이디어는 다음과 같다
    매번 업 다운으로 상한과 하한을 갱신한다
    상한과 하한을 설정해 같아 지는 경우 시작 지점을 기록하고, 뒤에 모순이 없는지만 확인한다
    하한이 상한보다 커지는 경우 모순이다라고 결론을 내리며 바로 탈출한다
    그리고 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0454
    {

        static void Main454(string[] args)
        {

            string RET1 = "Hmm...";
            string RET2 = "I got it!";
            string RET3 = "Paradox!";

            StreamReader sr = new(Console.OpenStandardInput());
            int n = (int)ReadLong();

            long sup = 1_000_000_000_000_000_000;
            long inf = -1_000_000_000_000_000_000;
            
            int type = 1;
            int ret = 111_222;

            for (int i = 1; i <= n; i++)
            {

                long cur = ReadLong();

                long op = ReadLong();

                // 46L : up
                if (op == 46L) inf = inf < cur + 1 ? cur + 1 : inf;
                else sup = sup < cur - 1 ? sup : cur - 1;


                if (sup < inf)
                {

                    type = 3;
                    ret = i;
                    break;
                }
                else if (sup == inf)
                {

                    type = 2;
                    ret = ret < i ? ret : i;
                }
            }

            sr.Close();

            if (type == 1)
            {

                Console.WriteLine(RET1);
            }
            else
            {

                Console.WriteLine(type == 2 ? RET2 : RET3);
                Console.WriteLine(ret);
            }

            long ReadLong()
            {

                int c;
                long ret = 0;
                bool plus = true;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
#if other
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class Main {
	
	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringBuilder sb = new StringBuilder();
		int Q = Integer.parseInt(br.readLine());
		long low = -1000000000000000001L, high = 1000000000000000001L;
		int num = 0;
		boolean flag = false;
		for(int q=1; q<=Q; ++q) {
			String s = br.readLine();
			long x = Long.parseLong(s.substring(0, s.length()-2));
			char c = s.charAt(s.length()-1);
			if(c=='^') {
				if(x>low) low = x;
			}
			else {
				if(x<high) high = x;
			}
			if(low+2>high) {
				System.out.print(sb.append("Paradox!\n").append(q));
				return;
			}
			//최애 수를 찾았지만 남은 질문 모순 확인해야 됨
			if(!flag && high-low==2) {
				flag = true;
				num = q;
			}
		}
		if(flag) System.out.print(sb.append("I got it!\n").append(num));
		else System.out.print("Hmm..."); //최애 수 후보 여러개
	}
}
#endif
}
