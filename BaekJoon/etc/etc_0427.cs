using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 7
이름 : 배성훈
내용 : 먼저 가세요
    문제번호 : 3655번

    그리디, 정렬, 문자열 문제다
    예제 몇 개 진행해보니 그리디로 해결했다

    아이디어는 다음과 같다
    문자열을 보면 앞부터 이동시키던, 
    뒤로 이동시키던 같은 문자를 한 곳에 모아두면 시간 절약이 되고,
    앞이던 뒤던 전체 절약하는 시간이 모두 같다

    예를들어 AQAAQQ 라 보면, A가 만나는 시간은 20초, Q는 30초이다
    이를 A를 앞쪽에 모은다 치면 AAAQQQ가 되는데 여기서 절약된 시간은 전체 15초(3명이 5초씩 절약이다
    반면 QQQAAA라 하면 Q는 45초가 절약되고 A는 30초가 더 걸린다 둘이 합치면 45 - 30 = 15초이다

    그래서 처음 전부 만나는 시간을 계산하고, 이후에 정렬해서 만나는 시간을 구한 뒤 빼주니,
    이상없이 56ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0427
    {

        static void Main427(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(Console.OpenStandardOutput());

            int test = ReadInt();
            // 연산용
            int[] time = new int[128];
            int[] cnt = new int[128];

            while(test-- > 0)
            {

                int n = ReadInt();
                string str = sr.ReadLine();

                long ret = 0;

                // 전체 시간 계산을 위한 전처리
                for (int i = n - 1; i >= 0; i--)
                {

                    if (time[str[i]] == 0) time[str[i]] = (i + 1);
                    cnt[str[i]]++;
                }

                // 전체 시간 계산 및 정렬된 시간 계산
                long totalTime = 0;
                int cntTotal = 0;
                for (int i = 0; i < cnt.Length; i++)
                {

                    if (cnt[i] == 0) continue;
                    cntTotal += cnt[i];

                    totalTime += cnt[i] * time[i];
                    ret += cntTotal * cnt[i];

                    // 다썼으니 초기화
                    cnt[i] = 0;
                    time[i] = 0;
                }

                sw.WriteLine(5 * (totalTime - ret));
            }

            sr.Close();
            sw.Close();
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
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class Main {
	static Map<Character, Integer> counts;
	static Map<Character, Integer> cost;
	
	public static void main(String[] args) throws IOException {
		Scanner input = new Scanner(System.in);
		int t = input.nextInt();
		while(t-->0){
			counts = new HashMap<Character, Integer>();
			cost = new HashMap<Character, Integer>();
			int n = input.nextInt(), i;
			String s = input.next();
			for(i=0;i<n;i++){
				char ch = s.charAt(i);
				if(!counts.containsKey(ch)){
					counts.put(ch, 1);
					cost.put(ch, i);
				}else{
					int cc = counts.get(ch)+1;
					counts.put(ch, cc);
					cost.put(ch, cc*i);
				}
			}
			int cur_cost = i = 0, min_cost = 0;
			for(char ch : counts.keySet()) {
				int cc = counts.get(ch);
				cur_cost += cost.get(ch);
				i += cc;
				min_cost += (i-1)*cc;
			}
			System.out.println(5*(cur_cost - min_cost));
		}
		input.close();
	}
}

#endif
}
