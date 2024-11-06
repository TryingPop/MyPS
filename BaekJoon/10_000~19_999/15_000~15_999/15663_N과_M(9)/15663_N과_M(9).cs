using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 8
이름 : 배성훈
내용 : N과 M(9)
    문제번호 : 15663번

    예를들어 9 7 9 1에서 2개를 꺼낸다고 가정하면,
    먼저 정렬해서 1 7 9 9로 만들고
    앞에서부터 2개씩 꺼내갈 것이다
        1 7, 1 9, 1 9
    그런데, 1 9 가 두 번 나왔다

    9를 한 번 세겠다고 9를 하나 제거하기에는
    9 9 인 경우를 배제하기에 9를 그냥 제거하는 것은 위험하다

    이러한 상황을 해결하고자 입력된 수들을 키로, 개수를 값으로 하는 dictionary를 이용했다
    그러면 키에서 입력된 수 중복을 막고, 값으로 몇개 인지 확인도 가능하다

    오름차순으로 출력해야하기에 처음에는 딕셔너리를 정렬하는 법을 몰라 key를 따로 배열로 추출하고
    해당 배열을 정렬해서 넘겨주는 식으로 썼다
    이후에 딕셔너리 정렬 방법을 찾아봤고, 그냥 arr을 넣을때 정렬해주면, 키가 arr 순으로 붙여짐을 알게되었다
    실제로 마지막 방법이 가장 빨리 풀렸다

    이제 저장하는 배열을 했으니 읽어야한다
    읽는건 DFS 탐색으로 읽었다

    예를들어 
        4 2
        9 7 9 1
        를 입력받은 경우라하자

        그러면 딕셔너리 dic의 키 값은 1, 7, 9가 된다
        또한 각각의 값은 dic[1] = 1, dic[7] = 1, dic[9] = 2이다

        키값을 불러온다    1, 7, 9
        이제 1부터 확인한다 아직 1을 사용안해서 1을 사용할 수 있다
        그러면 앞 자리에 1을 넣는다
            1
        1을 사용했다는 의미로 dic[1] = 0으로 만든다
        이제 두번째 자리에 넣을 숫자를 찾으러 dfs 탐색을 한다

        다음 dfs 탐색에서 다시 1, 7, 9 키 값을 불러온다
        이제 1에 값이 있는지 확인한다
        앞에서 1을 사용해서 더 사용할 수 없다 dic[1] = 0이므로 이제 다음 키 값 7을 본다
        dic[7] = 1이므로 7을 사용할 수 있다 
        dic[7] = 0으로 만든다
        그래서 7을 두번째 자리에 기록한다
            1 7

        그리고 dfs 탐색을 한다
        여기서 우리가 찾을 길이는 2개짜리이므로 
            1 7 찾았고 출력한다
        그리고 해당 3번째 dfs를 탈출한다
        2번째 dfs로 돌아오고, 
        첫번쩌 재라이 1과 두번째 자리에서 7을 쓴 모든 경우를 확인했으므로 다시 7을 채워준다
        dic[7] = 1로 다시 만든다

        이제 7 다음 9값을 확인한다
        dic[9] = 2이므로 사용가능하고 여기서 1개를 꺼내어 2번째 자리에 기록한다
            1 9
        dic[9] = 2 - 1 = 1로 하나 사용했음을 기록한다
        그리고 3번째 dfs 탐색으로 들어가고, 여기서 찾으려는 길이를 만족했으므로 
            1 9를 출력한 뒤 3번째 dfs를 탈출한다

        이후에 2번째 dfs로 돌아온다
        1번째 자리에 1이고 2번째 자리에 9를 쓰는 모든 경우를 확인했으므로 이제 9를 다시 채워줘야한다
        dic[9] = 1 + 1
        이제 해당 값에 올 수 있는 수를 모두 탐색했다
        그러면 다시 첫 번째 dfs로 돌아오고
        첫 번째 자리에 1인 경우를 모두 탐색했으므로 1을 다시 채워준다
        dic[1] = 1

        이제 첫번째 자리에 1인 경우는 끝났다 다음 키 값 7을 확인한다
        ... 이렇게 출력하며 찾아간다

    키 값을 오름차순 정렬 했으므로 결과도 오름차순으로 정렬된다
*/

namespace BaekJoon.etc
{
    internal class etc_0003
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[] arr = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            sr.Close();

            // 딕셔너리 키값은 생성된 순서로 추가된다
            // 즉 정렬된 키값을 만들기 위해 arr을 정렬한다
            Array.Sort(arr);

            Dictionary<int, int> dic = new();
            for (int i = 0; i < arr.Length; i++)
            {

                if (!dic.ContainsKey(arr[i]))
                {

                    dic[arr[i]] = 0;
                }

                dic[arr[i]]++;
            }

            int[] calc = new int[info[1]];

            // 딕셔너리를 키로 오름차순 정렬 하는 방법
            // dic = dic.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            DFS(dic, 0, calc, sw);
            sw.Close();
        }

        static void DFS(Dictionary<int, int> _dic, int _len, int[] _record, StreamWriter _sw)
        {

            // 탈출 구문
            if (_len == _record.Length)
            {

                // 만족하는 경우 찾았으므로 결과 출력 버퍼에 저장
                for (int i = 0; i <_len; i++)
                {

                    _sw.Write(_record[i]);
                    _sw.Write(' ');
                }

                _sw.Write('\n');
                return;
            }

            // 깊이 탐색
            foreach(var key in _dic.Keys)
            {

                // 해당 수를 모두 소진한 경우 탈출
                if (_dic[key] <= 0) continue;

                // 여기자리에 추가한다는 의미
                _record[_len] = key;
                _dic[key]--;

                // 다음 자리 탐색
                DFS(_dic, _len + 1, _record, _sw);

                // 해당 경우 모든 경우 탐색했으므로 다음 키 값으로 진행
                _dic[key]++;
            }
        }
    }

#if other
using System;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int n = ScanInt(), m = ScanInt();
var seq = new int[n];
for (int i = 0; i < n; i++)
    seq[i] = ScanInt();
Array.Sort(seq);
using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
var output = new int[m];
var unableUse = new bool[n];

var pre = 0;
for (var i = 0; i < n; i++)
{
    if (pre == seq[i])
        continue;
    pre = seq[i];
    Print(0, i);
}


void Print(int pos, int seqIndex)
{
    output[pos] = seq[seqIndex];
    if (++pos == m)
    {
        for (int i = 0; i < m; i++)
        {
            sw.Write(output[i]);
            if (i < m - 1)
                sw.Write(' ');
        }
        sw.Write('\n');
        return;
    }

    unableUse[seqIndex] = true;
    var pre = 0;
    for (var i = 0; i < n; i++)
    {
        if (unableUse[i] || pre == seq[i])
            continue;
        pre = seq[i];
        Print(pos, i);
    }
    unableUse[seqIndex] = false;
}

int ScanInt()
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
#endif
}
