using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 27
이름 : 배성훈
내용 : 휴대폰 자판
    문제번호 : 5670번

    트라이 자료구조를 이용한 문제
    C# 에서 트라이 자료구조를 이용할 시 1초 내로 안풀린다
    (가장 빠른 사람이 1.5초)

    주요 아이디어는 다음과 같다
    트라이 자료구조에 문자 하나씩 넣는다
    빠른 사람은 딕셔너리를 했으나 여기서는 배열을 이용했다

    그리고 해당 문자열을 탐색할 때,
    현재 위치에서 다른 입력된 문자가 있는지 확인한다
        예를 들면 hell, heaven이 있을 때,
            he'l'l
            he'a'ven

        서로 다른 문자가 3번째에서 발생한다
        이 경우 cnt 변수를 할당해 cnt를 분기점의 개수로 맞춰줬다
    그래서 cnt > 1이면 결과값 한 개를 추가한다

    그리고 해당 문자 뒤가 있으나 끝나는 문자가 있는지 확인한다
        예를들어 hell과 hello이다
            hel'l'
            hel'l'o

        두 문자의 4번째 l에서 뒤가 없거나 o가 있다
        이 경우 길이 1개라도 o를 입력받는지 안받는지 확인해야한다
    그래서 현재 노드에서 cnt == 1이고 isEnd인 경우 결과값 한 개를 추가했다
    
    hell 탐색할 때, hel'l'에 isEnd가 있다
    그러나 hell을 탐색할 때 실상 hel까지만 탐색한다!
    4개의 노드를 탐색하나 h -> e -> l -> l이 아닌 trie -> h -> e -> l 순으로 4개를 탐색한다
    후자로 탐색해도 결과는 같게 나와서 이상없다!

    전자로 하면, 코드 작성이나 읽기가 수월하지만, 노드 탐색이 1회씩 늘어나 후자로 했다
    그런데 #if other에 있는 다른 사람의 코드를 보니 그게 더 읽기 좋고, 깔끔해 보인다.
*/
namespace BaekJoon._42
{
    internal class _42_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            Node trie = new Node();

            while (true)
            {

                int len = 0;
                int c;
                while(!((c = sr.Read()) is -1 or '\n'))
                {

                    if (c == '\r') continue;
                    len *= 10;
                    len += c - '0';
                }

                if (len == 0) break;

                string[] input = new string[len];
                for (int i = 0; i < len; i++)
                {

                    input[i] = sr.ReadLine();
                    trie.Add(input[i]);
                }

                // 이제 시치!
                int typing = 0;
                for (int i = 0; i < len; i++)
                {

                    typing += trie.Search(input[i]);
                }

                float result = typing / (float)len;
                sw.WriteLine($"{result:0.00}");

                trie.Clear();
            }

            sr.Close();
            sw.Close();
        }

        class Node
        {

            Node[] child = new Node[26];
            bool isEnd;
            public int cnt;

            public void Add(string _str)
            {

                Node node = this;
                

                for (int i = 0; i < _str.Length; i++)
                {

                    int idx = _str[i] - 'a';
                    if (node.child[idx] == null)
                    {

                        // 현재 노드에 검색 분기점 추가!
                        node.cnt++;
                        node.child[idx] = new Node();
                    }

                    node = node.child[idx];
                }

                // 끝에 노드에 isEnd = true가 된다!
                node.isEnd = true;
            }

            public int Search(string _str)
            {

                // 처음 한개 입력!
                Node node = child[_str[0] - 'a'];
                int result = 1;

                // 현재 선택된 노드를 기준으로 탐색한다!
                for (int i = 1; i < _str.Length; i++)
                {

                    int idx = _str[i] - 'a';

                    // 검색할께 2개 이상인 경우
                    if (node.cnt > 1) result++;
                    // 검색할게 1개지만, 여기서 다른 단어의 끝맺음인 경우
                    // hell, hello 이렇게 trie에 들어가 있고,
                    // hell 검색이라 하면
                    // 3번째 인덱스인 마지막 he'l' l에서는 isEnd가 안된다!
                    // 왜냐하면 자식 hel'l' 의 l이 isEnd이기에!
                    else if (node.cnt == 1 && node.isEnd) result++;
                    
                    // 다음 노드로 바꾼다!
                    node = node.child[idx];
                }

                return result;
            }

            // 노드 초기화
            public void Clear()
            {

                for (int i = 0; i < child.Length; i++)
                {

                    // 널로 향함으로써 다 끊어버린다;
                    child[i] = null;
                }

                isEnd = false;
            }
        }
    }

#if other
using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

    do
    {
	    Node trie = new();
	    var n = ScanInt();
	    if (n == 0)
		    break;
	    for (int i = 0; i < n; i++)
		    trie.Insert(sr.ReadLine()!);
	    var sum = trie.GetKetCountSum();
	    var ret = (float)sum / n;
	    sw.WriteLine(ret.ToString("F2"));
    } while (true);

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

    class Node
    {
	    Dictionary<char, Node> children = new();
	    bool isTerminal;

	    public void Insert(string value, int index = 0)
	    {
		    if (value.Length == index)
		    {
			    isTerminal = true;
			    return;
		    }

		    var c = value[index];
		    if (!children.ContainsKey(c))
			    children[c] = new();
		    var child = children[c];
		    child.Insert(value, index + 1);
	    }

	    public int GetKetCountSum(int keyCount = 0)
	    {
		    var ret = isTerminal ? keyCount : 0;
		    var newKeyCount = keyCount;
		    if (keyCount == 0 || children.Count >= 2 | | isTerminal)
		    {
			    newKeyCount++;
		    }
		    foreach ((var key, var child) in children)
		    {
			    ret += child.GetKetCountSum(newKeyCount);
		    }
		    return ret;
	    }
    }
#endif
}
