using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 13
이름 : 배성훈
내용 : 개똥벌레
    문제번호 : 3020번

    이분 탐색 문제인데, 
    경우의 수가 100만이므로 N log N 방법을 쓸 수 있다
    그래서 세그먼트 트리로 풀었다!

    구간의 의미가 
    높이 0 ~ 1사이 --> 1구간
        1 ~ 2 --> 1구간
    ... n ~ n +1 --> 1구간으로 취급한다
    중간에 벽이 어떻게 있던 상관없이 문제에서는 해당 방법으로 구간을 정의했다

    그래서 int형으로 해결하기 위해 개똥벌레가 이동하는 구간은 홀수,
    벽은 짝수로 해서 세팅했다
    (높이가 50만까지인데, 해당 방법 때문에 100만으로 변경된다!)

    세그먼트 트리에 저장은 해당 구간이 벽의 범위에 포함되면 값을 넣었다
    자식과, 부모에겐 계승안한다!

    값을 얻을 때는 루트부터 해당 값에 맞는 리프까지 들어가는 탐색을 한다
    다시 보니 for문 탐색이 아닌 DFS 탐색을 하니 시간 단축이 되었다(288ms -> 168ms)

    그리고 뿌순 벽에 맞는 구간들을 저장했다

    다른사람 풀이를 보니, 스위핑? 정렬해서 이분 탐색으로 문제를 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0026
    {

        static void Main26(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // n : 가로 길이, h : 최대 높이
            int len = ReadInt(sr);
            int height = ReadInt(sr);
            // 높이 2배를 사용!
            // 홀수번은 이동 경로고, 짝수는 높이 배치
            height *= 2;

            // 세그먼트 트리 세팅
            int[] seg;
            {

                int log = (int)Math.Ceiling(Math.Log2(height)) + 1;
                seg = new int[1 << log];
            }

            // 가로 길이만큼 벽 배치
            for (int i = 0; i < len; i++)
            {

                int wall = ReadInt(sr);
                // 벽 배치이므로 2배해서 한다
                wall *= 2;

                // 밑바닥, 천장에 번갈아 붙여가면서 해당 높이의 벽을 설치
                if ((i & 1) == 0) Update(seg, 0, height, 0, wall);
                else Update(seg, 0, height, height - wall, height);
            }

            // idx : 벽 뿌순 개수
            // val : 구간 개수
            int[] dp = new int[len + 1];

#if first

            // 해당 방법으로 if문 없이 짜니 더 느리게 통과
            // for (int i = 1; i < height; i+= 2)
            for (int i = 0; i < height; i++)
            {

                // 탈출 구문
                if (height < 2 * i + 1) break;
                // 뿌신 벽 찾기 구간별로 한다!
                int idx = GetValue(seg, 0, height, 2 * i + 1);
                // 구간 추가
                dp[idx]++;
            }
#else

            RecordValue(seg, 0, height, 0, dp);
#endif

            int min = 0;
            for (int i = 0; i <= len; i++)
            {

                // 가장 적게 뿌신 구간을 찾으면 바로 탈출!
                if (dp[i] != 0) 
                { 
                    
                    // min에는 개수가 담긴다!
                    min = i;
                    break;
                }
            }

            // 정답 출력
            Console.Write(min);
            Console.Write(' ');
            Console.Write(dp[min]);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret *= 10;
                ret += c - '0';
            }

            return ret;
        }

        static void Update(int[] _seg, int _start, int _end, int _chkStart, int _chkEnd, int _idx = 1)
        {

            if (_end < _chkStart || _chkEnd < _start) return;
            else if (_chkStart <= _start && _end <= _chkEnd) 
            { 
                
                _seg[_idx - 1]++;
                return;
            }

            int mid = (_start + _end) / 2;
            Update(_seg, _start, mid, _chkStart, _chkEnd, _idx * 2);
            Update(_seg, mid + 1, _end, _chkStart, _chkEnd, _idx * 2 + 1);
        }

#if first
        static int GetValue(int[] _seg, int _start, int _end, int _chkIdx, int _idx = 1)
        {

            if (_start == _end) return _seg[_idx - 1];

            int mid = (_start + _end) / 2;
            int ret;
            if (mid < _chkIdx) ret = GetValue(_seg, mid + 1, _end, _chkIdx, _idx * 2 + 1);
            else ret = GetValue(_seg, _start, mid, _chkIdx, _idx * 2);

            ret += _seg[_idx - 1];
            return ret;
        }
#else
        static void RecordValue(int[] _seg, int _start, int _end, int _value, int[] _dp, int _idx = 1)
        {

            if (_start == _end)
            {

                if ((_start & 1) == 0) return;

                _value += _seg[_idx - 1];
                _dp[_value]++;
                return;
            }

            _value += _seg[_idx - 1];
            int mid = (_start + _end) / 2;

            RecordValue(_seg, _start, mid, _value, _dp, _idx * 2);
            RecordValue(_seg, mid + 1, _end, _value, _dp, _idx * 2 + 1);
        }

#endif
    }

#if other
namespace Baekjoon;

public class Program
{
    private static void Main(string[] args)
    {
        var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        int num = ScanInt(sr), height = ScanInt(sr);
        int[] stalagmite = new int[num / 2], stalactite = new int[num / 2];
        for (int i = 0; i < num / 2; i++)
        {
            stalactite[i] = ScanInt(sr);
            stalagmite[i] = height - ScanInt(sr);
        }
        Array.Sort(stalactite);
        Array.Sort(stalagmite);

        int ret = num / 2, rCount = 1, pa = 0, pb = 0, broken = num / 2;
        for (int i = 1; i < height; i++)
        {
            while (pa < stalactite.Length && i == stalactite[pa])
            {
                broken--;
                pa++;
            }
            while (pb < stalagmite.Length && i == stalagmite[pb])
            {
                broken++;
                pb++;
            }
            if (ret > broken)
            {
                ret = broken;
                rCount = 1;
            }
            else if (ret == broken)
                rCount++;
        }
        Console.Write($"{ret} {rCount}");
    }

    static int ScanInt(StreamReader sr)
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
}
#elif other2
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
class Baekjoon{
    static void Main(String[] args){ 
        int[] index = Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
        int length = index[0];
        int height = index[1];

        int[] low = new int[height+1];
        int[] high = new int[height+1];
        for(int i = 0 ; i< length ; i++){
            int input = int.Parse(Console.ReadLine());
            if(i%2==1){
                high[height-input+1]++;
            }else{
                low[input]++;
            }
        }

        for(int i = 1 ; i <= height ; i++){
            high[i]+=high[i-1];
            low[height-i]+=low[height-i+1];
        }

        long ans = 9999999;
        int cnt = 0 ;
        for(int i = 1 ; i<=height ; i++){
            if(high[i]+low[i]<ans){
                cnt=1;
                ans = high[i]+low[i];
            }else if(high[i]+low[i]==ans){
                cnt++;
            }
        }

        Console.WriteLine(ans+" "+cnt);

    }
}
#endif
}
