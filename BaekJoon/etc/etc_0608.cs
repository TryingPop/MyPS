using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 24
이름 : 배성훈
내용 : 최대 합 순서쌍의 개수
    문제번호 : 30646번

    누적합, 해시 문제다
    풀고나서 힌트를 보니 힌트대로 풀었다

    아이디어는 다음과 같다
    해당 값의 맨 앞에있는 인덱스와 맨 끝 인덱스를 딕셔너리에 기록한다
    누적합 배열의 끝인덱스 값 - 시작 인덱스 값이 해당 연속된 누적합이 되게 누적합 배열을 만든다
    그리고 딕셔너리 원소를 꺼내면서 최대 값과 개수를 세어 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0608
    {

        static void Main608(string[] args)
        {

            StreamReader sr;

            int n;
            long[] sums;

            Dictionary<int, (int s, int e)> dic;

            Solve();
            void Solve()
            {

                Input();

                long max = 0;
                int cnt = 1;
                foreach(var item in dic.Values)
                {

                    long cur = sums[item.e] - sums[item.s];
                    if (max < cur)
                    {

                        max = cur;
                        cnt = 1;
                    }
                    else if (max == cur) cnt++;
                }

                Console.Write($"{max} {cnt}");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);
                n = ReadInt();
                dic = new(n);
                sums = new long[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    int cur = ReadInt();
                    sums[i] = sums[i - 1] + cur;

                    if (dic.ContainsKey(cur)) dic[cur] = (dic[cur].s, i);
                    else dic[cur] = (i - 1, i);
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while(( c= sr.Read()) != -1 && c != ' ' && c != '\n')
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
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var a = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();

        var sum = new long[n];
        sum[0] = a[0];

        for (var idx = 1; idx < n; idx++)
            sum[idx] = sum[idx - 1] + a[idx];

        var grouped = new Dictionary<long, List<int>>();

        for (var idx = 0; idx < n; idx++)
        {
            var v = a[idx];

            if (!grouped.ContainsKey(v))
                grouped.Add(v, new List<int>());

            grouped[v].Add(idx);
        }

        var max = 0L;
        var maxCount = 0;

        foreach (var g in grouped)
        {
            var minIndex = g.Value.Min();
            var maxIndex = g.Value.Max();

            var psum = sum[maxIndex];
            if (minIndex != 0)
                psum -= sum[minIndex - 1];

            if (max == psum)
            {
                maxCount++;
            }
            if (max < psum)
            {
                max = psum;
                maxCount = 1;
            }
        }

        sw.WriteLine($"{max} {maxCount}");
    }
}

#elif other2
// BOJ 30646 [Pairs with Maximum Sum]
// Supported by GitHub Copilot

use std::io::{self, Read, Write};
fn read<T>(si: &mut T) -> String where T: Read {
    let mut s = String::new();
    si.read_to_string(&mut s).unwrap();
    s
}
fn next<T>(it: &mut std::str::SplitAsciiWhitespace) -> T where
    T: std::str::FromStr,
    <T as std::str::FromStr>::Err: std::fmt::Debug {
    it.next().unwrap().parse().unwrap()
}

pub fn main() -> Result<(), Box<dyn std::error::Error>> {
    let mut si = io::BufReader::new(io::stdin().lock());
    let mut so = io::BufWriter::new(io::stdout().lock());
    let s = read(&mut si);
    let mut it = s.split_ascii_whitespace();

    let n = next::<usize>(&mut it);
    let mut a = (0..n).map(|i| (next::<i64>(&mut it), i)).collect::<Vec<_>>();
    let mut p = vec![0; n+1];
    for i in 0..n {
        p[i+1] = p[i] + a[i].0;
    }
    a.sort_unstable();

    let (mut ans, mut cnt) = (0, 0);
    let (mut x, mut k) = (a[0].0, a[0].1);
    for i in 1..n {
        if x != a[i].0 {
            let sum = p[a[i-1].1 + 1] - p[k];
            if sum > ans { ans = sum; cnt = 1; }
            else if sum == ans { cnt += 1; }
            x = a[i].0;
            k = a[i].1;
        }
    }
    let sum = p[a[n-1].1 + 1] - p[k];
    if sum > ans { ans = sum; cnt = 1; }
    else if sum == ans { cnt += 1; }

    writeln!(so, "{} {}", ans, cnt)?;

    Ok(())
}

#elif other3
// #include <iostream>
// #include <algorithm>
// #include <vector>
using namespace std;
typedef long long ll;
typedef pair<int,int> pii;

const ll mod = 1000000007;
const int inf = 1000000009;

int main(){
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;
    vector<pii> a(n+1);
    vector<ll> sum(n+1);
    for(int i=1;i<=n;i++) {
        cin >> a[i].first;
        sum[i] = sum[i-1] + a[i].first;
        a[i].second = i;
    }

    sort(a.begin()+1, a.end());

    int fst = 1;
    ll mx = 0;
    int cnt = 0;
    for(int i=1;i<=n;i++) {
        if (i == n || a[i].first != a[i+1].first) {
            ll s = sum[a[i].second] - sum[a[fst].second-1];
            fst = i + 1;
            if (mx < s) {
                mx = s;
                cnt = 1;
            } else if (mx == s) {
                cnt++;
            }
        }
    }

    cout << mx << ' ' << cnt << '\n';
}

#endif
}
