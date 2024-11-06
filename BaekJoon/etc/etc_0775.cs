using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 26
이름 : 배성훈
내용 : 수 이어쓰기
    문제번호 : 1515번

    그리디 알고리즘, 구현, 문자열, 브루트포스 문제다
    브루트포스로 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0775
    {

        static void Main775(string[] args)
        {

            int MAX_DIGIT = 5;
            StreamReader sr;
            string str;
            int idx, ret;
            int[] num;
            Solve();

            void Solve()
            {

                Input();

                idx = -1;
                ret = 0;
                for (int i = 0; i < str.Length; i++)
                {

                    int cur = str[i] - '0';
                    while (!FindNum(cur))
                    {

                        AddNum();
                    }
                }

                Console.Write(ret);
            }

            // 해당 숫자 포함하는지 확인
            bool FindNum(int _n)
            {

                while (idx >= 0)
                {

                    if (num[idx--] == _n) return true;
                }

                return false;
            }

            // 값 증가하고 배열에 넣는다
            void AddNum()
            {

                ret++;
                int n = ret;
                idx = -1;
                while(n > 0)
                {

                    num[++idx] = n % 10;
                    n /= 10;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                str = sr.ReadLine();
                num = new int[MAX_DIGIT + 1];
                sr.Close();
            }
        }
    }

#if other
var line = Console.ReadLine();
var lI = 0;
int n = 1;
for (;lI < line.Length; n++)
{
    var nS = n.ToString();
    foreach (var c in nS)
        if (lI < line.Length && line[lI] == c)
            lI++;
}
Console.Write(n-1);
#elif other2
// #include <cstdio>
// #include <string>


using namespace std;

int solve(string str) {
    int n;
    int i = 0;

    for (n = 1; true; n++) {
        string s = to_string(n);

        for (char digit : s) {
            if (str[i] != digit) {
                continue;
            }

            i++;

            if (str.length() == i) {
                return n;
            }
        }
    }
    return -1;
}


int main() {
    char input[3001];
    scanf("%s", input);
    printf("%d\n", solve(input));
    return 0;
}


#elif other3
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class Main {

    public static void main(String[] args) throws IOException {

        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        String str = br.readLine();

        int original = 0;
        int loca = 0;

        while (original++ <= 30000) {
            // original 문자열 변환
            String originalStr = String.valueOf(original);

            for (int i = 0; i < originalStr.length(); i++) {
                // originalStr의 현재 위치 문자와 loca의 위치 문자의 일치여부 확인
                if(originalStr.charAt(i) == str.charAt(loca)) {
                    // 일치하면 증가
                    loca++;
                }
                // loca랑 str의 길이가 같으면 출력
                if(loca == str.length()) {
                    System.out.println(originalStr);
                    return;
                }
            }
        }

    }

}
#elif other4
S = input().strip()
N,R = 0,''
for c in S:
    if c in R: R = R[R.index(c)+1:]; continue
    while c not in str(N:=N+1): continue
    s = str(N)
    R = s[s.index(c)+1:]
print(N)
#endif
}
