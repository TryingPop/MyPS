using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 14
이름 : 배성훈
내용 : Australian Voting
    문제번호 : 4419번

    구현, 시뮬레이션 문제다
    출력조건 안맞춰서 7번 이상 틀리고, 
    예제를 입력 받아 디버깅을 하는데, 1번 인덱스부터 시작해서 0번 null이 있었다
    이를 잘못봐서 입력이 잘못되는 경우도 있나 의문이 들어 Trim으로 조정하다가
    null입력에서 Trim해서 한 번 틀렸다
    그리고 로직상의 오류로 두 번 틀렸다. 
    -> 투표에서 빠진 후보는 제외해야하는데 이를 안해서 제외된 후보에게 투표해버려
    이상한 결과를 도출해 50%에서 막혔다

    아이디어는 다음과 같다
    유권자는 큐를 이용해 후보에 대한 선호도를 보관했다
    그리고 후보자들은 자신을 찍은 사람들을 보관시켰다

    이제 투표를 시작하고, 후보자들 상태를 조사한다
    여기서 빠지는 후보가 있는 경우!
    
    해당 후보를 제명하고 해당 후보에게 투표한 사람들을 불러온다
    그리고 선호도를 순서대로 꺼내며 제명 안한 후보면 해당 후보에게 다시 투표하고 종료한다

    이렇게 시뮬레이션 돌려 찾을 때까지 계속했다
*/

namespace BaekJoon.etc
{
    internal class etc_0529
    {

        static void Main529(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 2);
            int pLen = ReadInt();
            string[] p = new string[pLen + 1];
            Queue<int>[] give = new Queue<int>[pLen + 1];
            bool[] pops = new bool[pLen + 1];

            for (int i = 1; i <= pLen; i++)
            {

                p[i] = sr.ReadLine();
                give[i] = new(1_000);
            }

            Queue<int>[] v = new Queue<int>[1_000];
            Queue<int> calc = new Queue<int>(pLen);

            int vLen = 0;
            while (true)
            {

#if first
                string str = sr.ReadLine();
                if (str == string.Empty || str == null) break;

                

                string[] temp = str.Split();
                v[vLen] = new(pLen - 1);
                int cur = int.Parse(temp[0]);
                give[cur].Enqueue(vLen);

                for (int i = 1; i < pLen; i++)
                {

                    v[vLen].Enqueue(int.Parse(temp[i]));
                }
#else

                int chk = ReadInt();
                if (chk == 0) break;

                v[vLen] = new(pLen - 1);
                give[chk].Enqueue(vLen);

                for (int i = 1; i < pLen; i++)
                {

                    v[vLen].Enqueue(ReadInt());
                }
#endif
                vLen++;
            }
            sr.Close();
            
            int ret = -1;
            while (true)
            {

                if (ChkEnd()) continue;
                break;
            }

            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                if (ret > 0) sw.Write(p[ret]);
                else
                {

                    for (int i = 1; i <= pLen; i++)
                    {

                        if (pops[i]) continue;
                        sw.WriteLine(p[i]);
                    }
                }
            }

            void RemovePresent()
            {

                while (calc.Count > 0)
                {

                    int idx = calc.Dequeue();
                    while (give[idx].Count > 0)
                    {

                        int h = give[idx].Dequeue();
                        int next = v[h].Dequeue();
                        while (pops[next])
                        {

                            next = v[h].Dequeue();
                        }

                        give[next].Enqueue(h);
                    }
                }
            }

            bool ChkEnd()
            {

                int min = 1_001;
                int max = 0;

                for (int i = 1; i <= pLen; i++)
                {

                    if (pops[i]) continue;
                    int cur = give[i].Count;
                    if (cur < min) min = cur;

                    if (max < cur)
                    {

                        max = cur;
                        ret = i;
                    }
                }

                if (min == max) 
                {

                    ret = 0;
                    return false; 
                }
                else if (max * 2 >= vLen) return false;

                for (int i = 1; i <= pLen; i++)
                {

                    if (min == give[i].Count) 
                    {

                        calc.Enqueue(i); 
                        pops[i] = true;
                    }
                }

                RemovePresent();
                return true;
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

        var names = new Dictionary<int, string>();
        for (var idx = 1; idx <= n; idx++)
            names[idx] = sr.ReadLine();

        var queues = new List<Queue<int>>();
        while (true)
        {
            if (sr.EndOfStream)
                break;

            queues.Add(new Queue<int>(sr.ReadLine().Split(' ').Select(Int32.Parse)));
        }

        var eliminated = new HashSet<int>();
        var counts = new Dictionary<int, int>();
        while (true)
        {
            counts.Clear();
            for (var idx = 1; idx <= n; idx++)
                if (!eliminated.Contains(idx))
                    counts[idx] = 0;

            foreach (var q in queues)
            {
                while (eliminated.Contains(q.Peek()))
                    q.Dequeue();

                counts[q.Peek()]++;
            }

            if (counts.Any(kvp => 2 * kvp.Value >= queues.Count))
            {
                var maxkey = counts.MaxBy(kvp => kvp.Value).Key;
                sw.WriteLine(names[maxkey]);

                return;
            }

            if (counts.Values.Distinct().Count() == 1)
            {
                foreach (var k in counts.Keys.OrderBy(v => v))
                    sw.WriteLine(names[k]);

                return;
            }

            var min = counts.Values.Min();
            eliminated.UnionWith(counts.Where(kvp => kvp.Value == min).Select(kvp => kvp.Key));
        }
    }
}

#endif
}
