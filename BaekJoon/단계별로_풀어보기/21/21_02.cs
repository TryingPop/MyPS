using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 19(2024. 1. 26)
이름 : 배성훈
내용 : 문자열 집합
    문제번호 : 14425번

    *******************(2024. 1. 26)*******************
    이전에 풀었으나 트라이 자료구조를 쓰기위해 다시 왔다
    ***************************************************

    트라이 자료구조를 string으로도 제출하고, char로도 제출해보니, char는 시간초과 뜬다!
    char는 문자열 길이만큼 생성하는데, 여기서 시간초과 만드는거 같다
    ... 42_04를 풀려면 char로 만들어야하는데 여기서는 시간초과 난다
*/

namespace BaekJoon._21
{
    internal class _21_02
    {

        static void Main2(string[] args)
        {

#if before
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            
            HashSet<string> mySet = new HashSet<string>();
            
            for (int i = 0; i < info[0]; i++)
            {

                mySet.Add(sr.ReadLine());
            }

            int result = 0;
            for (int i = 0; i < info[1]; i++)
            {

                if (mySet.Contains(sr.ReadLine()))
                {

                    result++;
                }
            }

            Console.WriteLine(result);
#endif

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            Node node = new();

            for (int i = 0; i < info[0]; i++)
            {

                node.Add(sr.ReadLine());
            }

            int result = 0;
            for (int i = 0; i < info[1]; i++)
            {

                if (node.Search(sr.ReadLine())) result++;
            }
            sr.Close();

            Console.WriteLine(result);
        }

        class Node
        {

            // Dictionary<string, Node> _child;
            // public string name;
            // Dictionary<char, Node> _child;
            Node[] _child;
            bool isEnd;

            public Node()
            {

                // _child = new();
                _child = new Node[26];
            }
            /*
            public void Add(string _word)
            {

                if (!_child.ContainsKey(_word))
                {

                    _child[_word] = new Node();
                }

                _child[_word].name = _word;
            }

            public bool Search(string _word)
            {

                if (!_child.ContainsKey(_word)) return false;

                return true;
            }
            */

            public void Add(string _word)
            {

                Node node = this;

                for (int i = 0; i < _word.Length; i++)
                {

                    int idx = _word[i] - 'a';
                    /*
                    if (!node._child.ContainsKey(_word[i]))
                    {

                        node._child[_word[i]] = new();
                    }

                    node = node._child[_word[i]];
                    */

                    if (node._child[idx] == null)
                    {

                        node._child[idx] = new();
                    }

                    node = node._child[idx];
                }

                node.isEnd = true;
            }

            public bool Search(string _word)
            {

                Node node = this;

                for (int i = 0; i < _word.Length; i++)
                {

                    // if (!node._child.ContainsKey(_word[i])) return false;
                    // node = node._child[_word[i]];

                    int idx = _word[i] - 'a';

                    if (node._child[idx] == null) return false;
                    node = node._child[idx];
                }

                
                return node.isEnd;
            }
        }
    }
}
