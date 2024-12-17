using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 17
이름 : 배성훈
내용 : 전설
    문제번호 : 19585번

    트라이 문제다.
    트라이를 이용해 색깔을 저장하고 
    해시셋을 통해 이름을 저장한다.

    그리고 색상이 있는지 확인하고 나머지 부분
    이름이 있는지 확인을 해서 정답을 구했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1198
    {

        class Trie
        {

            public static HashSet<string> name;

            private bool isEnd;
            private Trie[] arr;

            public Trie()
            {

                arr = new Trie[26];
                isEnd = false;
            }

            public void InsertColor(string _str)
            {

                Trie cur = this;
                int curPtr = 0;
                while (curPtr < _str.Length)
                {

                    int idx = _str[curPtr] - 'a';
                    if (cur.arr[idx] == null) cur.arr[idx] = new();
                    cur = cur.arr[idx];
                    curPtr++;
                }

                cur.isEnd = true;
            }

            public bool Find(string _str)
            {

                Trie cur = this;
                int curPtr = 0;

                while (curPtr < _str.Length)
                {

                    int idx = _str[curPtr] - 'a';
                    cur = cur.arr[idx];
                    if (cur == null) return false;
                    curPtr++;
                    if (cur.isEnd 
                        && name.Contains(_str.Substring(curPtr))) return true;
                }

                return false;
            }
        }


        static void Main1198(string[] args)
        {

            string YES = "Yes\n";
            string NO = "No\n";

            StreamReader sr;

            Trie trie;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int t = int.Parse(sr.ReadLine());
                while(t-- > 0)
                {

                    string temp = sr.ReadLine();
                    if (trie.Find(temp)) sw.Write(YES);
                    else sw.Write(NO);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();
                int n = int.Parse(temp[0]);
                int m = int.Parse(temp[1]);

                trie = new();
                Trie.name = new(m);
                for (int i = 0; i < n; i++)
                {

                    trie.InsertColor(sr.ReadLine());
                }

                for (int i = 0; i < m; i++)
                {

                    Trie.name.Add(sr.ReadLine());
                }
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

        var cn = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var c = cn[0];
        var n = cn[1];

        var rabinKarpMod = 31L;
        var colorDict = new Dictionary<long, List<string>>();
        var nickNameDict = new Dictionary<long, List<string>>();

        for (var idx = 0; idx < c; idx++)
        {
            var s = sr.ReadLine();
            var h = 0L;
            foreach (var ch in s)
                h = h * rabinKarpMod + (ch - 'a' + 1);

            if (!colorDict.ContainsKey(h))
                colorDict.Add(h, new List<string>());

            colorDict[h].Add(s);
        }

        for (var idx = 0; idx < n; idx++)
        {
            var s = sr.ReadLine();
            var h = 0L;
            foreach (var ch in s.Reverse())
                h = h * rabinKarpMod + (ch - 'a' + 1);

            if (!nickNameDict.ContainsKey(h))
                nickNameDict.Add(h, new List<string>());

            nickNameDict[h].Add(s);
        }

        var q = Int32.Parse(sr.ReadLine());
        var forwardHash = new long[2001];
        var reverseHash = new long[2001];

        while (q-- > 0)
        {
            var query = sr.ReadLine();

            forwardHash[0] = query[0] - 'a' + 1;
            reverseHash[query.Length - 1] = query[query.Length - 1] - 'a' + 1;

            for (var idx = 1; idx < query.Length; idx++)
            {
                forwardHash[idx] = forwardHash[idx - 1] * rabinKarpMod + (query[idx] - 'a' + 1);
                reverseHash[query.Length - 1 - idx] = reverseHash[query.Length - 1 - idx + 1] * rabinKarpMod + (query[query.Length - 1 - idx] - 'a' + 1);
            }

            var find = false;

            for (var endIncl = 0; endIncl < query.Length - 1; endIncl++)
            {
                var colorHash = forwardHash[endIncl];

                if (!colorDict.ContainsKey(colorHash))
                    continue;

                var colorSubstr = query.Substring(0, 1 + endIncl);
                if (!colorDict[colorHash].Contains(colorSubstr))
                    continue;

                var nickNameHash = reverseHash[1 + endIncl];
                if (!nickNameDict.ContainsKey(nickNameHash))
                    continue;

                var nicknameSubstr = query.Substring(1 + endIncl, query.Length - colorSubstr.Length);
                if (nickNameDict[nickNameHash].Contains(nicknameSubstr))
                {
                    find = true;
                    break;
                }
            }

            sw.WriteLine(find ? "Yes" : "No");
        }
    }
}

#endif
}
