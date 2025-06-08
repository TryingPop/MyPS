using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 12
이름 : 배성훈
내용 : 게임 닉네임
    문제번호 : 16934번

    자료 구조, 문자열, 트리, 해시를 사용한 집합과 맵, 트라이 문제다
    해시에 값을 저장해 풀었다.
    다른 사람의 풀이를 보니 트라이를 이용하는 방법이 더 좋아보인다.
*/

namespace BaekJoon.etc
{
    internal class etc_1105
    {

        static void Main1105(string[] args)
        {

            Solve();
            void Solve()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int n = int.Parse(sr.ReadLine());
                HashSet<string> prefix = new(n * 10);
                Dictionary<string, int> user = new(n);

                for (int i = 0; i < n; i++)
                {

                    string str = sr.ReadLine();
                    if (user.ContainsKey(str))
                    {

                        user[str]++;
                        sw.Write($"{str}{user[str]}\n");
                    }
                    else
                    {

                        bool flag = false;
                        for (int j = 0; j < str.Length; j++)
                        {

                            string sub = str.Substring(0, j + 1);
                            if (prefix.Contains(sub)) continue;
                            prefix.Add(sub);

                            if (flag) continue;
                            flag = true;
                            sw.Write($"{sub}\n");
                        }

                        user[str] = 1;
                        if (flag) continue;
                        sw.Write($"{str}\n");
                    }
                }

                sr.Close();
                sw.Close();
            }
        }
    }

#if other
// #include <stdio.h>
// #include <string.h>

// #define TRIE_DIFF           'a'
// #define TRIE_ROOT           0
// #define TRIE_MAX_NODE       728279
// #define TRIE_ALPHABET       26
// #define TRIE_BUFF_SIZE      3249992
// #define TRIE_STR_MAX_LEN    11

int _idx;
int _num[TRIE_MAX_NODE];
int _next[TRIE_MAX_NODE][TRIE_ALPHABET];
char *_ptr;
char _buff[TRIE_BUFF_SIZE];

int
trie_new()
{
    _num[_idx] = 0;
    memset(&_next[_idx], 0, TRIE_ALPHABET * sizeof(int));
    return _idx++;
}

void
trie_insert(char *str)
{
    int idx;
    int curr;
    char *start;

    for (curr = TRIE_ROOT; *str; str++, curr = _next[curr][idx]) {
        *(_ptr++) = *str;
        idx = *str - TRIE_DIFF;
        if (!_next[curr][idx]) {
            curr = _next[curr][idx] = trie_new();
            str++;
            break;
        }
    }
    if (*str) {
        for (; *str; str++, curr = _next[curr][idx]) {
            idx = *str - TRIE_DIFF;
            if (!_next[curr][idx])
                _next[curr][idx] = trie_new();
        }
    }
    else if (_num[curr])
        _ptr += sprintf(_ptr, "%d", _num[curr] + 1);
    _num[curr]++;
}

int
main()
{
    int n;
    char str[TRIE_STR_MAX_LEN];

    _idx = 0;
    trie_new();
    _ptr = _buff;
    scanf("%d", &n);
    while (n--) {
        scanf("%s", str);
        trie_insert(str);
        *(_ptr++) = '\n';
    }
    *_ptr = '\0';
    printf("%s", _buff);
    return 0;
}
#endif
}
