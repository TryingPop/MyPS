using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 14
이름 : 배성훈
내용 : 과제가 너무 많아
    문제번호 : 30867번

    애드 혹 문제다
    그리디와 두 포인터처럼 접근해서 해결했다

    아이디어는 다음과 같다
    w를 적어도 1개 포함한 w, h로만 이루어진 연속된 부분 문자열들만 특별 취급하고 
    이외에는 이동이 불가능하기에 해당 위치 그대로 놓았다

    특별 취급한 문자열들은 w의 시작지점을 저장한다
    시작 지점 이후 특별 문자열 안에 h에 대해 날짜만큼 왼쪽으로 이동시킬 수 있는지 판별한 후 이동시킨다
    이렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0526
    {

        static void Main526(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 4);
            int[] info = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

            string str = sr.ReadLine();
            sr.Close();

            char[] ret = new char[info[0]];

            int wS = -1;
            for (int i = 0; i < info[0]; i++)
            {

                if (str[i] == 'w')
                {

                    if (wS == -1) wS = i;
                    ret[i] = 'w';
                }
                else if (str[i] == 'h' && wS != -1)
                {

                    int diff = i - wS;
                    if (diff > info[1]) diff = info[1];


                    /*
                    // 해당 코드로 작성할 필요가 없다!
                    // wS가 오른쪽으로 한칸 이동하고
                    // 왼쪽에서 오른쪽으로 탐색하기에! 
                    // 항상 i - diff 자리는 w가 보장된다!
                    if (ret[i - diff] == 'w')
                    {

                        ret[i - diff] = 'h';
                        ret[i] = 'w';
                    }
                    else ret[i] = 'h';
                    */


                    wS++;
                    ret[i - diff] = 'h';
                    ret[i] = 'w';
                }
                else
                {

                    wS = -1;
                    ret[i] = str[i];
                }
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 4))
            {

                for (int i = 0; i < info[0]; i++)
                {

                    sw.Write(ret[i]);
                }
            }
        }
    }

#if other
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.StringTokenizer;

public class Main {

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st = new StringTokenizer(br.readLine());
        StringBuilder sb = new StringBuilder();

        int L = Integer.parseInt(st.nextToken());
        int N = Integer.parseInt(st.nextToken());
        char[] str = br.readLine().toCharArray();

        solve(str, N);

        for (char c : str) {
            sb.append(c);
        }
        System.out.println(sb);
    }

    private static void solve(char[] str, int n) {

        int count = 0;

        for (int idx = 0; idx < str.length; idx++) {
            if (str[idx] == 'w') {
                count++;
                if (count > n) {
                    count = n;
                }
            }else if (str[idx] == 'h'){
                swap(str, idx, count);
            }else{
                count = 0;
            }

        }
    }

    private static void swap(char[] str, int idx, int count) {
        char temp = str[idx];
        str[idx] = str[idx - count];
        str[idx - count] = temp;
    }


}

#endif
}
