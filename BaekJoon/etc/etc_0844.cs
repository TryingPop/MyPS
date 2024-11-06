using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 27
이름 : 배성훈
내용 : 정렬하기
    문제번호 : 16219번

    많은 조건 분기 문제다
    아이디어는 다음과 같다
    처음에 정렬되어있다면 0이다
    이외 2개짜리에서만 정렬 안된경우 1번의 이동으로 가능하다
    나머지는 정렬할 수 없다

    왜냐하면 3개 이상이고,
    다른 것을 3개 다르게 만들 수 있기 때문에
    2개 바꾸는 걸로는 정렬할 수 없다
*/

namespace BaekJoon.etc
{
    internal class etc_0844
    {

        static void Main844(string[] args)
        {

            string ZERO = "0 ";
            string ONE = "1 ";
            string NO = "-1 ";

            StreamReader sr;
            StreamWriter sw;

            int[] arr;
            int n;
            int chk;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    if (arr[x] == x) chk++;
                    if (arr[y] == y) chk++;

                    Swap(x, y);

                    if (arr[x] == x) chk--;
                    if (arr[y] == y) chk--;

                    if (chk == 0) sw.Write(ZERO);
                    else if (n == 2) sw.Write(ONE);
                    else sw.Write(NO);
                }

                sr.Close();
                sw.Close();
            }

            void Swap(int _idx1, int _idx2)
            {

                int temp = arr[_idx1];
                arr[_idx1] = arr[_idx2];
                arr[_idx2] = temp;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                chk = 0;

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;
                    if (i != cur) chk++;
                }
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
using System;
using System.IO;

namespace Baekjoon_16219
{
    class Program
    {
        static void Main()
        {
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = int.Parse(sr.ReadLine()), k = 0;
            string[] ss = sr.ReadLine().Split(' ');
            int[] s = new int[n];
            for (int i = 0; i < n; i++)
            {
                s[i] = int.Parse(ss[i]);
                if (s[i] == i)
                {
                    k++;
                }
            }
            int m = int.Parse(sr.ReadLine());
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            while (m-- > 0)
            {
                int[] xy = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                int x = xy[0], y = xy[1];
                if (s[x] == x) k--;
                if (s[y] == y) k--;
                int temp = s[x];
                s[x] = s[y];
                s[y] = temp;
                if (s[x] == x) k++;
                if (s[y] == y) k++;
                sw.Write(k == n ? "0 " : n > 2 ? "-1 " : "1 ");
            }
            sw.Flush();
        }
    }
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;

int main() {
    ios_base::sync_with_stdio(0); cin.tie(0);
    
    int n;
    cin >> n;

    int cnt=0;
    vector<int> v(n);
    for (int i=0; i<n; i++) {
        cin >> v[i];
        cnt+=v[i]!=i;
    }

    int m;
    cin >> m;
    for (int i=0; i<m; i++) {
        int a, b;
        cin >> a >> b;

        if (a!=b) {
            if (v[a]!=a) cnt--;
            if (v[b]!=b) cnt--;
        }
        swap(v[a], v[b]);
        if (a!=b) {
            if (v[a]!=a) cnt++;
            if (v[b]!=b) cnt++;
        }

        int ans=-2;
        if (n==1) ans=0;
        else if (n==2) {
            if (cnt==0) ans=0;
            else ans=1;
        }
        else {
            if (cnt==0) ans=0;
            else ans=-1;
        }

        cout << ans << ' ';
    } 

    return 0;
}

#endif
}
