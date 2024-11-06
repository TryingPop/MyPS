using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 13
이름 : 배성훈
내용 : 철벽 보안 알고리즘
    문제번호 : 9322번

    문자열, 해시 문제다
    A -> B순서를 파악한다
    A의 인덱스를 빠르게 확인하기 위해 Dictionary를 썼다
    그리고 해당 순서를 따로 배열에 기록하고
    결과 암호문을 입력받을 때 해당 순서대로 결과 배열에 입력한다
    그리고 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0521
    {

        static void Main521(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            Dictionary<string, int> dic = new(1_000);
            int[] order = new int[1_000];
            string[] ret = new string[1_000];
            int test = ReadInt();

            while(test-- > 0)
            {

                int n = ReadInt();

                string[] temp = sr.ReadLine().Split();
                for (int i = 0; i < n; i++)
                {

                    dic.Add(temp[i], i);
                }

                int idx = 0;
                temp = sr.ReadLine().Split();
                for (int i = 0; i < n; i++)
                {

                    order[i] = dic[temp[i]];
                }

                temp = sr.ReadLine().Split();
                for (int i = 0; i < n; i++)
                {

                    ret[order[i]] = temp[i];
                }
                
                for (int i = 0; i < n; i++)
                {

                    sw.Write(ret[i]);
                    sw.Write(' ');
                }

                sw.Write('\n');
                dic.Clear();
            }

            sw.Close();
            sr.Close();

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
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.lang.reflect.Array;
import java.util.*;

public class Main {
    public static void main(String args[]) throws IOException {
        BufferedReader bf = new BufferedReader(new InputStreamReader(System.in));
        int testCase = Integer.parseInt(bf.readLine());
        StringBuilder sb = new StringBuilder();
        for (int j = 0; j < testCase; j++) {
            int numOfWord = Integer.parseInt(bf.readLine());
            StringTokenizer stz = new StringTokenizer(bf.readLine());
            StringTokenizer stz2 = new StringTokenizer(bf.readLine());
            StringTokenizer crypto = new StringTokenizer(bf.readLine());
            HashMap<String, Integer> hsmap = new HashMap<>();
            for (int i = 0; i < numOfWord; i++) {
                String tmpSt = stz.nextToken();
                hsmap.put(tmpSt, i);
            }
            Stack<Integer> stack = new Stack<>();
            int[] mapping = new int[numOfWord];
            for (int i = 0; i < numOfWord; i++) {
                 mapping[i] = hsmap.get(stz2.nextToken());
            }
            String[] ans = new String[numOfWord];
            for (int i = 0; i < numOfWord; i++) {
                ans[mapping[i]] = crypto.nextToken();
            }
            for (int i = 0; i < numOfWord; i++) {
                sb.append(ans[i]).append(" ");
            }
            sb.append("\n");
        }
        System.out.print(sb);
    }

}
#endif
}
