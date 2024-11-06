using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 27
이름 : 배성훈
내용 : 가르침
    문제번호 : 1062번

    브루트포스, 백트래킹, 비트마스킹 문제다
    처음에는 bool 배열을 만들었다가
    26개의 ox여부만 판별하면 되기에 비트마스킹으로 바꿨다
    제출하니 시간은 1.1초지만 통과했다
    그리고 힌트를 보니 힌트대로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0360
    {

        static void Main360(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int k = ReadInt();

            int[] str = new int[n];
            for (int i = 0; i < n; i++)
            {

                string temp = sr.ReadLine();
                for (int j = 0; j < temp.Length; j++)
                {

                    // 문자열에서 사용한 알파벳 저장
                    int idx = temp[j] - 'a';
                    str[i] |= 1 << idx;
                }
            }

            sr.Close();

            int ret = DFS(0, 0);
            Console.WriteLine(ret);

            int DFS(int _depth, int _use, int _before = 0)
            {

                int ret = 0;
                if (_depth == k)
                {

                    for (int i = 0; i < n; i++)
                    {

                        if ((str[i] | _use) == _use) ret++;
                    }

                    return ret;
                }

                for (int i = _before; i < 26; i++)
                {

                    int calc = DFS(_depth + 1, _use | 1 << i, i + 1);
                    ret = ret < calc ? calc : ret;
                }

                return ret;
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
var reader = new Reader();
var (n, k) = (reader.NextInt(), reader.NextInt());

if (k < 5)
{
    Console.Write(0);
    return;
}

var bitPos = new int[26] {
    -1, 0, -1, 1, 2, 3, 4, 5, -1, 6, 7, 8, 9, -1, 10, 11, 12, 13, 14, -1, 15, 16, 17, 18, 19, 20
};
var charSets = new uint[n];
for (int i = 0; i < n; i++)
    foreach (var c in reader.NextString(15))
        if (bitPos[c - 'a'] != -1)
            charSets[i] |= 1u << bitPos[c - 'a'];

var mod37BitPosition = new int[]
{
    32, 0, 1, 26, 2, 23, 27, 0, 3, 16, 24, 30, 28, 11, 0, 13, 4,
    7, 17, 0, 25, 22, 31, 15, 29, 10, 12, 6, 0, 21, 14, 9, 5,
    20, 8, 19, 18
};
uint v = FlipRightMostBits(k - 5); // current permutation of bits 
uint w = BitTwiddlePermute(v); // next permutation of bits
uint u = FlipLeftMostBits(k - 5, 20); // last permutation of bits

int max = 0;
int cnt = 0;
while (v <= u) 
{
    foreach (var chars in charSets)
        if ((v & chars) == chars)
            cnt++;

    v = w;
    w = BitTwiddlePermute(v);
    max = Math.Max(max, cnt);
    cnt = 0;
}

Console.Write(max);

int CountTrailingZeros(uint n) => mod37BitPosition[(-n & n) % 37];

uint FlipLeftMostBits(int n, int msbPos)
{
    uint r = 0;
    for (int i = 0; i < n; i++) 
        r |= 1u << (msbPos - i);

    return r;
}

uint FlipRightMostBits(int n)
{
    uint r = 0;
    for (int i = 0; i < n; i++)
        r |= 1u << i;
    
    return r;
}

uint BitTwiddlePermute(uint v)
{
    // t gets v's least significant 0 bits set to 1
    // Next set to 1 the most significant bit to change, 
    // set to 0 the least significant ones, and add the necessary 1 bits.
    uint t = v | (v - 1); 
    return (uint)((t + 1) | (((~t & -~t) - 1) >> (CountTrailingZeros(v) + 1)));
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
public string NextString(int m){var(v,r,l)=(new char[m+1],false,0);while(true){int c=R.Read();if(r==false&&(c==' '||c=='\n'||c=='\r'))continue;if(r==true&&(l==m||c==' '||c=='\n'||c=='\r'))break;v[l++]=(char)c;r=true;}return new string(v,0,l);}
}
#elif other2
var sr = new StreamReader(Console.OpenStandardInput());
var sw = new StreamWriter(Console.OpenStandardOutput());

var inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
var K = inputs[0];
var C = inputs[1];
var requirementChar = new char[5] { 'a', 'n', 't', 'i', 'c' };
var charArray = new bool[26];
foreach (var c in requirementChar)
{
    var current = c - 'a';
    charArray[current] = true;
}

var lst = new List<string>();
for (var i = 0; i < K; i++)
{
    var str = sr.ReadLine();
    str = str.Substring(4, str.Length - 8);
    lst.Add(str);
}

var answer = 0;

void DFS(int charIndex, int count)
{
    if (count == C)
    {
        var currentCount = 0;
        foreach (var word in lst)
        {
            var check = true;
            foreach (var currentChar in word)
            {
                var index = currentChar - 'a';
                if (charArray[index]) continue;
                check = false;
                break;
            }

            if (check) currentCount++;
        }

        answer = Math.Max(currentCount, answer);
        return;
    }

    for (var index = charIndex + 1; index < 26; index++)
    {
        if (charArray[index]) continue;
        charArray[index] = true;
        DFS(index, count + 1);
        charArray[index] = false;
    }
}

switch (C)
{
    case < 5:
        sw.WriteLine(0);
        break;
    case 26:
        sw.WriteLine(K);
        break;
    default:
        DFS(0, 5);
        sw.WriteLine(answer);
        break;
}


sr.Close();
sw.Close();
#elif other3
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var must = new Char[] { 'a', 'n', 't', 'i', 'c' };
        var map = new Dictionary<Char, int>();
        for (var v = 'a'; v <= 'z'; v++)
            if (!must.Contains(v))
                map.Add(v, map.Count);

        var nk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nk[0];
        var k = nk[1];
        k -= 5;

        if (k < 0)
        {
            sw.WriteLine(0);
            return;
        }

        var words = new List<int>();
        while (n-- > 0)
        {
            var val = sr.ReadLine()
                .Where(v => !must.Contains(v))
                .Select(v => 1 << map[v])
                .DefaultIfEmpty(0)
                .Aggregate((l, r) => l | r);

            words.Add(val);
        }

        var max = 0;
        for (var mask = 0; mask < (1 << map.Count); mask++)
        {
            if (BitOperations.PopCount((uint)mask) > k)
                continue;

            var count = 0;
            foreach (var w in words)
                if ((w & mask) == w)
                    count++;

            max = Math.Max(max, count);
        }

        sw.WriteLine(max);
    }
}
#elif other4
using System;

public class Program1
{
    static int n;
    static int k;
    static string[] words;
    static bool[] studied = new bool[26];
    static int max = 0;

    public static void Main(string[] args)
    {
        string[] tmp = Console.ReadLine().Split();
        n = int.Parse(tmp[0]);
        k = int.Parse(tmp[1]);
        words = new string[n];

        for (int i = 0; i < n; i++)
        {
            words[i] = Console.ReadLine();
        }

        studied['a' - 'a'] = true;
        studied['n' - 'a'] = true;
        studied['t' - 'a'] = true;
        studied['i' - 'a'] = true;
        studied['c' - 'a'] = true;

        if (k < 5)
        {
            Console.WriteLine(max);
        }
        else
        {
            dfs(0, 0);
            Console.WriteLine(max);
        }
    }

    public static void dfs(int idx, int count)
    {
        if (count + 5 == k)
        {
            countWord();
            return;
        }

        for (int i = idx; i < 26; i++)
        {
            if (studied[i] == true)
            {
                continue;
            }

            studied[i] = true;
            dfs(i + 1, count + 1);
            studied[i] = false;
        }
    }

    public static void countWord()
    {
        int count = 0;
        for (int i = 0; i < n; i++)
        {
            char[] alphas = words[i].ToCharArray();
            bool flag = true;
            foreach (char c in alphas)
            {
                if (studied[c - 'a'] == false)
                {
                    flag = false;
                    break;
                }
            }
            if (flag == true)
            {
                count++;
            }
        }
        max = Math.Max(max, count);
    }
}
#endif
}
