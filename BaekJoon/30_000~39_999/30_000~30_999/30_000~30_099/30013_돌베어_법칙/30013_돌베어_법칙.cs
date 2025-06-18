using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 6
이름 : 배성훈
내용 : 돌베어의 법칙
    문제번호 : 30013번

    브루트포스 문제다
    점프 간격을 1칸씩 올려가면서, 그리디하게 최소인 귀뚜라미 수를 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0366
    {

        static void Main366(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 4096);

            int n = int.Parse(sr.ReadLine());
            string str = sr.ReadLine();

            sr.Close();

            int ret = n;

            bool[] owl = new bool[str.Length];
            // 귀뚜라미 점프간격
            for (int jump = 1; jump <= n; jump++)
            {

                int cur = 0;
                for (int i = 0; i < n; i++)
                {
                    owl[i] = false;

                    if (str[i] == '#')
                    {

                        // 앞번에 울었는지 확인
                        // 울었으면 해당 귀뚜라미가 계속 우는 것이다
                        if (i - jump < 0 || !owl[i - jump]) cur++;
                        owl[i] = true;
                    }
                }

                // 최소값 확인
                ret = cur < ret ? cur : ret;
            }

            Console.WriteLine(ret);
        }
    }

#if other
import java.util.Scanner;

public class Main {
	public static void main(String[] args) {
		Scanner sc = new Scanner(System.in);
		int n = sc.nextInt(); // 측정 시간
		char[] pattern = new char[n]; // .인지 #인지
		String str = sc.next();
		for (int i = 0; i < n; i++) {
			pattern[i] = str.charAt(i);
		}
		int min = Integer.MAX_VALUE; // 출력값
		int term = 1; // 패턴 주기
		while (term <= n) { // 주기가 길이보다 길어지면 반복 종료
			boolean[] visited = new boolean[n]; // 방문여부
			int cnt = 0; // 귀뚜라미 수

			for (int i = 0; i < n; i++) { // 시작점 찾기
				if (pattern[i] == '#' && !visited[i]) {
					cnt++; // 마리 수 증가
					visited[i] = true;

					for (int j = i; j < n; j += term) { // 주기만큼 더해서 뛰어넘기
						if (pattern[j] == '.') // 주기가 깨짐
							break;
						visited[j] = true;
					}
				}
			}
			term++; // 주기 증가
			min = Math.min(min, cnt); // 최소 마리 수 갱신
		}
		System.out.println(min);
	}
}
#endif
}
