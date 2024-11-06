using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 4
이름 : 배성훈
내용 : 노래
    문제번호 : 3156번

    정렬, 해시 문제다
    아이디어는 다음과 같다

    문자열을 입력 받기에 숫자로 변환해서 풀었다
    그리고 문자열로 숫자를 빠르게 찾는데 딕셔너리 자료구조를 이용했다

    이제 숫자로 변환한 뒤 랭킹을 부여받는데 
    만약 이미 해당 랭킹이 확실한 노래를 찾은 경우 입력하는 랭크를 줄인다
    (해당 랭크 이하이기에 성립한다!)

    그리고 정제된 랭크를 부여한다
    다만 여기서 이미 더 낮은 랭크가 부여된 경우나 이미 자기자리 찾은 랭킹의 경우는 무시한다
    이외에는 해당 랭크를 부여한다

    랭크 부여가 끝나면 이제 완성된 랭킹을 조사한다
    앞에 n명이 있고 자기가 유일한 랭크면 해당 자리가 자기자리이므로!
    랭킹 조사는 누적합과, 현재 랭크의 수로 자리자리 찾은 랭크를 확인했다

    이렇게 확인하고 마지막에 정렬해서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0442
    {

        static void Main442(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            Dictionary<string, int> sTi = new(100);
            string[] iTs = new string[100];

            int[] myRank = new int[100];
            int[] cntRank = new int[101];
            int[] sumRank = new int[101];
            bool[] chkRank = new bool[101];
            int sIdx = 0;

            for (int i = 0; i < n; i++)
            {

                int len = ReadInt();
                ReadInt();
                int rank = ReadInt();

                while (chkRank[rank])
                {

                    // 이미 가득 찬 랭크?
                    rank--;
                }

                string[] temp = sr.ReadLine().Split();
                for (int j = 0; j < len; j++)
                {

                    if (sTi.ContainsKey(temp[j]))
                    {

                        int idx = sTi[temp[j]];
                        int curRank = myRank[idx];

                        // 현재 랭크가 더 작은 경우 작은거 이용,
                        // 확인할 랭크?
                        if (curRank < rank || chkRank[curRank]) continue;
                        cntRank[curRank]--;
                        cntRank[rank]++;
                        myRank[idx] = rank;
                        
                        continue;
                    }

                    iTs[sIdx] = temp[j];
                    sTi[temp[j]] = sIdx;
                    myRank[sIdx] = rank;
                    cntRank[myRank[sIdx]]++;
                    sIdx++;
                    continue;
                }

                for (int j = 1; j <= 100; j++)
                {

                    sumRank[j] = cntRank[j];
                    sumRank[j] += sumRank[j - 1];
                }

                for (int j = 1; j <= 100; j++)
                {

                    if (sumRank[j] == j && cntRank[j] == 1) chkRank[j] = true;
                }
            }

            sr.Close();

            int[] ret = new int[101];
            Array.Fill(ret, -1);
            for (int i = 0; i < 100; i++)
            {

                int curRank = myRank[i];
                if (!chkRank[curRank]) continue;

                ret[curRank] = i;
            }

            using(StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 1; i <= 100; i++)
                {

                    if (ret[i] == -1) continue;

                    sw.WriteLine($"{i} {iTs[ret[i]]}");
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

        // dict<music, rank>
        var dict = new Dictionary<string, int>();

        while (n-- > 0)
        {
            var line = sr.ReadLine().Split(' ');
            var rank = Int32.Parse(line[2]);
            var musics = line.Skip(3).ToArray();

            foreach (var m in musics)
                dict[m] = Math.Min(dict.GetValueOrDefault(m, Int32.MaxValue), rank);
        }

        var specified = new List<(string music, int rank)>();
        foreach (var m in dict.Keys)
        {
            var rank = dict[m];
            if (dict.Count(kvp => kvp.Value < rank) == rank - 1
                && dict.Count(kvp => kvp.Value == rank) == 1)
            {
                specified.Add((m, rank));
            }
        }

        foreach (var (music, rank) in specified.OrderBy(v => v.rank))
            sw.WriteLine($"{rank} {music}");
    }
}

#endif
}
