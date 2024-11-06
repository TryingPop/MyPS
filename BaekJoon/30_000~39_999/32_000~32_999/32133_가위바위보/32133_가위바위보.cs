using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 5
이름 : 배성훈
내용 : 가위바위보
    문제번호 : 32133번

    브루트 포스 문제다
    아이디어는 다음과 같다
    이분 탐색, 문자열 해싱, 해시 자료구조로 해결했다

    중복을 포함한 길이가 a인 부분 문자열 중 가장 작은게 k개 이하로 되는
    a인 가장 짧은 길이를 찾는다

    만약 존재하지 않으면 -1을 반환한다
    존재하면 해당 문자열 중 가장 작은 문자열에 대해 정답을 출력하게 한다
    이렇게 찾으면 O(n * log m * log m)이 된다
    여기서 n은 문자열의 개수, m은 문자열의 길이다
    문자열 해싱에 log m 시간이 걸린다
*/

namespace BaekJoon.etc
{
    internal class etc_1029
    {

        static void Main1029(string[] args)
        {

            StreamReader sr;
            int n, m, k;
            string[] str;
            Dictionary<int, int> nTc;
            int ret1;
            string ret2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                GetLength();
                Console.Write($"{ret1}\n");

                GetStr();

                for (int i = 0; i < ret1; i++)
                {

                    if (ret2[i] == 'R') Console.Write('S');
                    else if (ret2[i] == 'S') Console.Write('P');
                    else Console.Write('R');
                }
            }

            void GetStr()
            {

                ret2 = string.Empty;
                if (ret1 == -1) return;
                Chk(ret1);

                int hash = 0;
                int min = n;
                foreach (int key in nTc.Keys)
                {

                    if (nTc[key] < min)
                    {

                        min = nTc[key];
                        hash = key;
                    }
                }

                for (int i = 0; i < n; i++)
                {

                    if (GetMyHashCode(str[i], ret1) != hash) continue;
                    ret2 = str[i];
                    return;
                }
            }

            void GetLength()
            {

                int l = 1;
                int r = m;
                ret1 = -1;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (Chk(mid))
                    {

                        r = mid - 1;
                        ret1 = mid;
                    }
                    else
                        l = mid + 1;
                }
            }

            bool Chk(int _len)
            {

                nTc.Clear();
                for (int i = 0; i < n; i++)
                {

                    int hash = GetMyHashCode(str[i], _len);
                    if (nTc.ContainsKey(hash)) nTc[hash]++;
                    else nTc[hash] = 1;
                 }

                int min = n;
                foreach(int cnt in nTc.Values)
                {

                    min = Math.Min(min, cnt);
                }

                return min <= k;
            }

            int GetMyHashCode(string _str, int _len)
            {

                int num = 5381;
                int num2 = num;
                int num3;

                int i = 0;

                while (i < _len)
                {

                    num3 = _str[i];
                    num = ((num << 5) + num) ^ num3;

                    if (i + 1 == _len) break;

                    i++;
                    num3 = _str[i];
                    num2 = ((num2 << 5) + num2) ^ num3;
                }

                return num + num2 * 1566083941;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                str = new string[n];
                for (int i = 0; i < n; i++)
                {

                    str[i] = sr.ReadLine();
                }

                nTc = new(n);
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
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
class Programs
{
    static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), Encoding.Default);
    static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), Encoding.Default);
    static string[] arr;
    static char[] srp = { 'S', 'R', 'P' };
    static int n, k, m, answer = 100;
    static string answer2;
    class Info
    {
        public StringBuilder sb;
        public int depth;
        public List<int> list;
        public Info(StringBuilder sb, int depth, List<int> l)
        {
            this.sb = sb;
            this.depth = depth;
            this.list = l;
        }
    }
    static void BFS()
    {
        Queue<Info> q = new Queue<Info>();
        List<int> list = new List<int>();
        for (int i = 0; i < n; i++)
        {
            list.Add(i);
        }
        q.Enqueue(new Info(new StringBuilder(""), 0, list));
        while (q.Count > 0)
        {
            Info info = q.Dequeue();
            if (info.list.Count == 0)
            {
                continue;
            }
           if (info.list.Count <= k)
            {
                answer = info.depth;
                answer2 = info.sb.ToString();
                break;
            }
            if ( info.depth >= m)
            {
                continue;
            }
            for (int j = 0; j < 3; j++)
            {
                List<int> l = new List<int>();
                for (int i = 0; i < info.list.Count; i++)
                {
                    int idx = info.list[i];
                    if (arr[idx][info.depth] == 'S' && j == 2)
                    {
                        l.Add(idx);
                    }
                    else if (arr[idx][info.depth] == 'R' && j == 0)
                    {
                        l.Add(idx);
                    }
                    else if (arr[idx][info.depth] == 'P' && j == 1)
                    {
                        l.Add(idx);
                    }
                }
                q.Enqueue(new Info(new StringBuilder(String.Concat(info.sb.ToString(), srp[j])), info.depth + 1, l));
            }

        }
    }
    static int ReadInt(StreamReader _sr)
    {
        int c = _sr.Read();
        bool positive = c != '-';           // 부호 확인
        int ret = positive ? c - '0' : 0;
        while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
        {
            if (c == '\r') continue;        // Window
            ret = ret * 10 + c - '0';
        }
        return positive ? ret : -ret;
    }

    public static void Main(string[] args)
    {
        n = ReadInt(sr);
        m = ReadInt(sr);
        k = ReadInt(sr);
        arr = new string[n];
        for (int i = 0; i < n; i++)
        {
            arr[i] = sr.ReadLine();
        }
        BFS();
        answer = answer == 100 ? -1 : answer;
        sw.WriteLine(answer);
        if (answer != -1)
        {
            sw.Write(answer2);
        }
        sw.Close();
    }
}

#elif other2
// #include <iostream>
using namespace std;

int main(void){
    ios::sync_with_stdio(0); cin.tie(0);
    int n,m,k; cin >> n >> m >> k;
    string a[53], b[53];
    for (int i = 0; i < n; i++)
        cin >> a[i];
    for (int j = 0; j < m; j++){
        for (int i = 0; i < n; i++)
            b[i] += a[i][j];
        for (int i = 0; i < n; i++) {
            int c = 0;
            for (int j = 0; j < n; j++){
                c += b[i] == b[j];
            }
            if (c <= k){
                cout << j+1 << "\n";
                for (char c:b[i]){
                    if (c == 'S')
                        cout << 'P';
                    if (c == 'P')
                        cout << 'R';
                    if (c == 'R')
                        cout << 'S';
                }
                cout << "\n";
                exit(0);
            }
        }
    }
    cout << "-1\n";
}

#endif
}
