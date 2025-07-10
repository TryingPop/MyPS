using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 에디터
    문제번호 : 1406번

    연결 리스트 문제다
    해당 기능을 하는 자료구조를 직접 구현해서 풀었다

    해당 조건은 스택 2개를 써서도 구현가능하다
    왼쪽과 오른쪽 놓고 커서를 이동할 때마다 스택 맨 위의 문자를 이동시킨다
*/

namespace BaekJoon.etc
{
    internal class etc_0416
    {

        static void Main416(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            string str = sr.ReadLine();

            MyEditor editor = new(str);
            int test = int.Parse(sr.ReadLine());

            StringBuilder sb = new(str.Length + test);

            while(test-- > 0)
            {

                int op = sr.Read();

                if (op == 'L') editor.Before();
                else if (op == 'D') editor.After();
                else if (op == 'B') editor.RemoveBefore();
                else
                {

                    sr.Read();
                    char add = (char)sr.Read();
                    editor.InsertBefore(add);
                }

                sr.ReadLine();
            }
            sr.Close();

            editor.GetString(sb);

            Console.Write(sb);
        }

        class MyEditor
        {

            public Node curNode;

            public class Node
            {

                public char c;

                public Node next;
                public Node before;
                
                public Node(char _c)
                {

                    c = _c;
                }
            }

            public MyEditor(string _str)
            {

                curNode = new(_str[0]);
                for (int i = 1; i < _str.Length; i++)
                {

                    Node newNode = new(_str[i]);
                    curNode.next = newNode;
                    newNode.before = curNode;
                    curNode = newNode;
                }

                Node endNode = new('\0');
                endNode.before = curNode;
                curNode.next = endNode;
                curNode = endNode;
            }

            public void Before()
            {

                if (curNode.before == null) return;
                curNode = curNode.before;
            }

            public void After()
            {

                if (curNode.c == '\0') return;
                curNode = curNode.next;
            }

            public void RemoveBefore()
            {

                if (curNode.before == null) return;
                Node remove = curNode.before;

                if (remove.before != null)
                {

                    curNode.before = remove.before;
                    remove.before.next = curNode;

                }
                else
                {

                    curNode.before = null;
                }

                remove.before = null;
                remove.next = null;
            }

            public void InsertBefore(char _c)
            {

                Node add = new Node(_c);

                if (curNode.before != null)
                {

                    Node temp = curNode.before;
                    curNode.before.next = add;

                    add.before = curNode.before;
                    curNode.before = add;
                    add.next = curNode;
                }
                else
                {

                    curNode.before = add;
                    add.next = curNode;
                }
            }

            public void GetString(StringBuilder _sb)
            {

                while(curNode.before != null)
                {

                    curNode = curNode.before;
                }

                while(curNode.c != '\0')
                {

                    _sb.Append(curNode.c);
                    curNode = curNode.next;
                }
            }
        }
    }

#if other
using System;
using System.Diagnostics;
using System.IO;

Node? first;
using (var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
{
    first = new((char)sr.Read());
    Node? cur = first;
    int c;
    while ((c = sr.Read()) != '\n')
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        cur = cur.Next = new((char)c, cur);
    }

    int n = 0;
    while ((c = sr.Read()) != '\n')
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        n = 10 * n + c - '0';
    }

    while (n-- > 0)
    {
        switch (sr.Read())
        {
            case 'L':
                cur = cur?.Prev;
                break;
            case 'D':
                if (cur == null)
                {
                    cur = first;
                    break;
                }
                if (cur.Next != null)
                    cur = cur.Next;
                break;
            case 'B':
                if (cur == null)
                    break;
                if (cur.Prev != null)
                    cur.Prev.Next = cur.Next;
                else
                    first = cur.Next;

                if (cur.Next != null)
                    cur.Next.Prev = cur.Prev;
                cur = cur.Prev;
                break;
            default:
                sr.Read();
                var added = new Node((char)sr.Read(), cur);
                if (cur == null)
                {
                    added.Next = first;
                    if (first != null)
                        first.Prev = added;
                    first = added;
                }
                else
                {
                    added.Next = cur.Next;
                    if (cur.Next != null)
                        cur.Next.Prev = added;
                    cur.Next = added;
                }
                cur = added;
                break;
        }
        if (sr.Read() == '\r')
            sr.Read();
    }
}
using (var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
    for (var n = first; n != null; n = n.Next)
        sw.Write(n.Value);

[DebuggerDisplay("{Value}")]
class Node
{
    public char Value;
    public Node? Prev;
    public Node? Next;

    public Node(char value, Node? front = null)
    {
        Value = value;
        Prev = front;
    }

    public Node(char value, Node? prev, Node? next)
    {
        Prev = prev;
        Next = next;
        Value = value;
    }
}
#endif
}
