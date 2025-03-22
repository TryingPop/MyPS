using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 27
이름 : 배성훈
내용 : 부분수열의 합 2
    문제번호 : 1208번

    경우의 수 입력 범위에 먼저 주의해줘야한다
    2^40 > 2^20 * 2^20 > 100만 * 100만 = 1조
    그리고 0인 경우 카운트 안하면 1개 더 나온다!
    그래서 찾는 값이 0인 경우 1개 빼줘야한다! <- 이걸 캐치 못해 많이 틀렸다
    예제 확인하면서 발견했다;
*/

namespace BaekJoon.etc
{
    internal class etc_0111
    {

        static void Main111(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int find = ReadInt(sr);

            int mid = len / 2;
            int[] left = new int[mid];
            int[] right = new int[len - mid];

            for (int i = 0; i < mid; i++)
            {

                left[i] = ReadInt(sr);
            }

            for (int i = 0; i < right.Length; i++)
            {

                right[i] = ReadInt(sr);
            }

            sr.Close();

            Dictionary<int, int> dic = new Dictionary<int, int>(1_050_000);

            // 추가
            Add(dic, left, 0, 0);
            
            long ret = Chk(dic, right, 0, 0, find);
            if (find == 0) ret--;
            Console.WriteLine(ret);
        }

        static void Add(Dictionary<int, int> _dic, int[] _arr, int _cur, int _curVal)
        {

            if (_cur == _arr.Length)
            {

                if (_dic.ContainsKey(_curVal)) _dic[_curVal] += 1;
                else _dic[_curVal] = 1;
                return;
            }

            Add(_dic, _arr, _cur + 1, _curVal);
            Add(_dic, _arr, _cur + 1, _curVal + _arr[_cur]);
        }

        static long Chk(Dictionary<int, int> _dic, int[] _arr, int _cur, int _curVal, int _chk)
        {

            long ret = 0;
            if (_cur == _arr.Length)
            {

                if (_dic.ContainsKey(_chk - _curVal))
                {

                    ret += _dic[_chk - _curVal];
                }

                return ret;
            }

            ret += Chk(_dic, _arr, _cur + 1, _curVal, _chk);
            ret += Chk(_dic, _arr, _cur + 1, _curVal + _arr[_cur], _chk);

            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
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

#if other
var input = Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
int s = input[1],n=input[0], mid = n>>1;
var list = Array.ConvertAll(Console.ReadLine().Split(),int.Parse);

int[] arrs = new int[80000001];

long result = 0;

void Left(int index=0,long sum=0)
{
    if (index >= mid)
    {
        arrs[sum+40000000]++;
        return;
    }

    Left(index+1, sum + list[index]);
    Left(index+1,sum);
}

void Right(int index,long sum=0)
{
    if (index >= n)
    {
        result += arrs[s-sum+40000000];
        return;
    }

    Right(index+1,sum);
    Right(index+1,sum + list[index]);
}

Left();
Right(mid);
if (s==0)result--;

Console.Write(result);
#elif other2
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

        var ns = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = ns[0];
        var s = ns[1];

        var arr = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var sub1 = arr.Take(arr.Length / 2).ToArray();
        var sub2 = arr.Skip(sub1.Length).ToArray();

        var sum1 = new Dictionary<int, int>();
        var sum2 = new Dictionary<int, int>();

        EvalSums(sum1, sub1, 0, 0);
        EvalSums(sum2, sub2, 0, 0);

        var count = 0L;
        foreach (var (v, c1) in sum1)
            if (sum2.TryGetValue(s - v, out var c2))
                count += (long)c1 * c2;

        // empty set
        if (s == 0)
            count--;

        sw.WriteLine(count);
    }

    public static void EvalSums(Dictionary<int, int> result, int[] arr, int index, int sum)
    {
        if (index == arr.Length)
        {
            result[sum] = 1 + result.GetValueOrDefault(sum);
            return;
        }

        EvalSums(result, arr, index + 1, sum);
        EvalSums(result, arr, index + 1, sum + arr[index]);
    }
}
#elif other3
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int[] inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
int N = inputs[0];
int S = inputs[1];
int[] seq = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

Dictionary<long, long> curCount = new Dictionary<long, long>();
Dictionary<long, long> curCount2 = new Dictionary<long, long>();
curCount.Add(0, 1);
curCount2.Add(0, 1);

// 가능한 합을 모두 찾기
for (int i=0; i<N; i++)
{
    var thisCount = ((i < N / 2) ? curCount : curCount2);
    Dictionary<long, long> nextCount = new Dictionary<long, long>();
        
    foreach (var kvp in thisCount)
    {
        long nextWeight = kvp.Key + seq[i];

        if (nextCount.ContainsKey(nextWeight))
            nextCount[nextWeight]+= kvp.Value;
        else
            nextCount[nextWeight] = kvp.Value;
    }
    foreach (var kvp in nextCount)
    {
        if (thisCount.ContainsKey(kvp.Key))
            thisCount[kvp.Key]+= kvp.Value;
        else
            thisCount[kvp.Key] = kvp.Value;
    }
}

// 두 모음을 정렬
List<(long w, long count)> firstList = new();
foreach (var kvp in curCount)
    firstList.Add((kvp.Key, kvp.Value));
firstList.Sort((x, y) => (x.w.CompareTo(y.w)));

List<(long w, long count)> lastList = new();
foreach (var kvp in curCount2)
    lastList.Add((kvp.Key, kvp.Value));
lastList.Sort((x, y) => (x.w.CompareTo(y.w)));

long ans = (S==0)?-1:0;
int flag = lastList.Count - 1;

for (int i=0; i<firstList.Count; i++)
{
    while (flag >= 0 && firstList[i].w + lastList[flag].w > S)        flag--;
    if (flag >= 0 && firstList[i].w + lastList[flag].w == S)
        ans += firstList[i].count * lastList[flag].count;
}

sw.WriteLine(ans);
sr.Close();
sw.Close();
#endif
}
