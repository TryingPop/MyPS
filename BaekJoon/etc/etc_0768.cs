using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 21
이름 : 배성훈
내용 : 수식 완성하기
    문제번호 : 10421번

    브루트포스, 백트래킹 문제다
    구현에 시간이 많이 걸렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0768
    {

        static void Main768(string[] args)
        {

            int[] digit, calc, total;
            StreamReader sr;
            int[] u, d;
            int[] s;

            bool[] valid;

            Solve();

            void Solve()
            {

                Input();

                Init();

                int ret = UpDFS(0);

                Console.Write(ret);
            }

            int DownDFS(int _depth)
            {

                if (_depth == s[1])
                {

                    if (ChkInvalidMul()) 
                        return 1;
                    return 0;
                }

                int ret = 0;

                for (int i = 0; i < 10; i++)
                {

                    if (!valid[i]) continue;
                    d[_depth] = i;
                    ret += DownDFS(_depth + 1);
                }

                return ret;
            }

            int UpDFS(int _depth)
            {

                int ret = 0;

                if (_depth == s[0]) return DownDFS(0);

                for (int i = 0; i < 10; i++)
                {

                    if (!valid[i]) continue;
                    u[_depth] = i;
                    ret += UpDFS(_depth + 1);
                }

                return ret;
            }

            bool ChkMulVal(int _idx)
            {

                int len = s[_idx + 2];
                for (int i = 0; i < len; i++)
                {

                    int cur = calc[i];
                    if (cur > 9)
                    {

                        if (i + 1 == len) return true;
                        calc[i + 1] += cur / 10;
                        cur %= 10;
                    }

                    if (!valid[cur]) return true;
                    calc[i] = cur;
                }


                return false;
            }

            void UpMul(int _n)
            {

                for (int i = 0; i < 6; i++)
                {

                    calc[i] = 0;
                }

                for (int i = 0; i < u.Length; i++)
                {

                    calc[i] = u[i] * _n;
                }
            }

            void TotalMul(int _add)
            {

                for (int i = 0; i < calc.Length; i++)
                {

                    int cur = total[i + _add] + calc[i];
                    if (cur > 9)
                    {

                        total[i + _add + 1] += cur / 10;
                        cur %= 10;
                    }

                    total[i + _add] = cur;
                }
            }

            bool ChkInvalidMul()
            {

                for (int i = 0; i < total.Length; i++)
                {

                    total[i] = 0;
                }

                for (int i = 0; i < d.Length; i++)
                {

                    UpMul(d[i]);
                    if (ChkMulVal(i)) return false;
                    TotalMul(i);
                }


                int start = 0;
                for (int i = total.Length - 1; i >= 0; i--)
                {

                    if (total[i] == 0) continue;
                    start = i;
                    break;
                }

                for (int i = 0; i <= start; i++)
                {

                    if (valid[total[i]]) continue;
                    return false;
                }

                return start + 1 == s[s.Length - 1];
            }

            void Init()
            {

                u = new int[s[0]];
                d = new int[s[1]];

                digit = new int[8];
                calc = new int[6];
                total = new int[8];

                digit[0] = 1;
                for (int i = 1; i < digit.Length; i++)
                {

                    digit[i] = digit[i - 1] * 10;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 1024 * 4);
                int len = ReadInt();

                s = new int[len];
                for (int i = 0; i < len; i++)
                {

                    s[i] = ReadInt();
                }

                len = ReadInt();
                valid = new bool[10];
                for (int i = 0; i < len; i++)
                {

                    valid[ReadInt()] = true;
                }

                sr.Close();
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
// #include <bits/stdc++.h>
using namespace std;

int n, k;
vector<int> s;
vector<bool> poss(10, false);

vector<int> prd(10), plen(10);
vector<vector<int>> opt;
int ans;

void sel(int res, int idx) {
    if (idx == s[1]) {
        ans++;
        int rlen = 0;
        for (; res; res /= 10, rlen++) {
            if (!poss[res % 10]) {
                ans--;
                return;
            }
        }
        if (rlen != s.back()) ans--;
        return;
    }

    for (int o: opt[idx]) sel(res * 10 + prd[o], idx + 1);
}

void fst(int f, int len) {
    if (len == s[0]) {
        for (int i = 1; i < 10; i++) {
            if (poss[i]) {
                prd[i] = f * i;
                plen[i] = 0;
                for (int p = prd[i]; p; p /= 10, plen[i]++) {
                    if (!poss[p % 10]) prd[i] = -1;
                }
            } else {
                prd[i] = -1;
            }
        }

        opt = vector<vector<int>>(s[1]);
        for (int i = 0; i < s[1]; i++) {
            int l = s[2 + i];
            for (int j = 1; j < 10; j++) {
                if (prd[j] > 0 && plen[j] == l) opt[i].push_back(j);
            }
        }

        sel(0, 0);
        return;
    }

    for (int i = 1; i < 10; i++) {
        if (poss[i]) fst(10 * f + i, len + 1);
    }
}

int main() {
    cin.tie(0); ios_base::sync_with_stdio(0);
    cin >> n;
    s.resize(n);
    for (int i = 0; i < n; i++) cin >> s[i];
    cin >> k;
    for (int i = 0; i < k; i++) {
        int x; cin >> x;
        poss[x] = true;
    }

    fst(0, 0);
    cout << ans << '\n';
}
#elif other2
import java.io.*;
import java.util.*;

class Main {

    static int n, k;
    static int[] counts;
    static int[] numbers;
    static int count;
    static boolean[] check = new boolean[10];

    public static void main(String[] args) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
        n = Integer.parseInt(br.readLine());
        counts = new int[n];

        StringTokenizer st = new StringTokenizer(br.readLine());
        for (int i = 0; i < n; i++) {
            counts[i] = Integer.parseInt(st.nextToken());
        }

        k = Integer.parseInt(br.readLine());
        numbers = new int[k];
        st = new StringTokenizer(br.readLine());
        for (int i = 0; i < k; i++) {
            numbers[i] = Integer.parseInt(st.nextToken());
            check[numbers[i]] = true;
        }

        search(0, new int[counts[0] + counts[1]]);

        bw.write(Integer.toString(count));
        bw.flush();
        bw.close();
    }

    public static void search(int index, int[] out) {
        if (index == out.length) {
            if (isPossible(out)) {
                count++;
            }
            return;
        }

        for (int number : numbers) {
            out[index] = number;
            if (isPossibleNumber(index + 1, out)) {
                search(index + 1, out);
            }
        }
    }

    public static boolean isPossibleNumber(int count, int[] out) {
        if (count > counts[0]) {
            int first = 0;
            for (int i = 0; i < counts[0]; i++) {
                first = first * 10 + out[i];
            }
            String value = String.valueOf(out[count - 1] * first);
            if (value.length() != counts[2 + counts[1] - (count - counts[0])]) {
                return false;
            }
            for (int i = 0; i < value.length(); i++) {
                if (!check[value.charAt(i) - '0']) {
                    return false;
                }
            }
        }
        return true;
    }

    public static boolean isPossible(int[] out) {
        int first = 0;
        int second = 0;
        for (int i = 0; i < counts[0]; i++) {
            first = first * 10 + out[i];
        }
        for (int i = counts[0]; i < out.length; i++) {
            second = second * 10 + out[i];
        }
        String result = String.valueOf(first * second);
        if (result.length() != counts[n - 1]) {
            return false;
        }
        for (int i = 0; i < result.length(); i++) {
            if (!check[result.charAt(i) - '0']) {
                return false;
            }
        }
        return true;
    }
}

#elif other3
import sys


def input():
    return sys.stdin.readline().rstrip()

def choose_1(idx,val):
    global K
    if idx == line_cnt[0]:
        choose_2(0,0,val)
        return
    else:
        for i in range(K):
            choose_1(idx+1,val*10+number_list[i])
def check_length(val):
    val = str(val)
    return len(val)
def check_number(val):
    while val:
        if not number_visit[val%10]:
            return False
        val = val//10
    return True

def choose_2(idx,val,first_num):
    global K,result
    if idx == line_cnt[1]:
        last_num = first_num * val
        if check_length(last_num) == line_cnt[-1] and check_number(last_num):
            result += 1
        return
    else:
        for i in range(K):
            if check_length(first_num*number_list[i]) == line_cnt[idx+2] and check_number(first_num*number_list[i]):
                choose_2(idx+1,val*10+number_list[i],first_num)

N = int(input())
list_ = input().split()

K = input()
list__ = input().split()

if len(K) == 0:
    K = int(list_[N])
    list__ = list_[N + 1 : ]
    list_  = list_[ : N]
else:
    K = int(K)
result = 0
line_cnt = list(map(int,list_))
number_list = list(map(int,list__))
number_visit = [False for _ in range(10)]

for num in number_list:
    number_visit[num] = True


choose_1(0,0)

print(result)
#endif
}
