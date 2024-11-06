using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 1
이름 : 배성훈
내용 : 가운데를 말해요
    문제번호 : 1655번

    우선순위 큐 문제다
    우선 순위 큐 2개를 이용해 중앙값을 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0669
    {

        static void Main669(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            PriorityQueue<int, int> min;
            PriorityQueue<int, int> max;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int n = ReadInt();
                var greater = Comparer<int>.Create((x, y) => y.CompareTo(x));
                min = new(n / 2 + 1, greater);
                max = new(n / 2 + 1);

                
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    if (min.Count == 0)
                    {

                        min.Enqueue(cur, cur);
                        sw.Write($"{min.Peek()}\n");
                        continue;
                    }
                    
                    max.Enqueue(cur, cur);
                    if (max.Peek() < min.Peek())
                    {

                        int m = max.Dequeue();
                        int M = min.Dequeue();

                        min.Enqueue(m, m);
                        max.Enqueue(M, M);
                    }

                    if (min.Count < max.Count)
                    {

                        int M = max.Dequeue();
                        min.Enqueue(M, M);
                    }

                    sw.Write($"{min.Peek()}\n");
                }

                sr.Close();
                sw.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;

                while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if ( c== '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class main {
  static StreamReader rd = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
  static StreamWriter wr = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
  static StringBuilder std = new StringBuilder();
  
  static void Main() {
      int n = int.Parse(rd.ReadLine());
      Minheap min = new Minheap(n);
      Maxheap max = new Maxheap(n);
      int temp;
      for(int i = 0; i < n; i++)
      {
          temp = int.Parse(rd.ReadLine());
          if(max.Size() == min.Size())
          {
              if(max.Size() != 0 && temp > min.Peek())
              {
                  max.Add(min.Remove());
                  min.Add(temp);
              }
              else
              {
                  max.Add(temp);
              }
          }
          else
          {
              if(temp < max.Peek())
              {
                  min.Add(max.Remove());
                  max.Add(temp);
              }
              else
              {
                  min.Add(temp);
              }
          }
          std.Append(max.Peek() + "\n");
      }
      
      wr.WriteLine(std);
      wr.Close();
      rd.Close();
  }
}
class Maxheap
{
    private int[] index;
    private int size;
    public Maxheap(int n)
    {
        index = new int[n+2];
        size = 0;
    }
    public void Add(int n)
    {
        size++;
        int cur = size;
        while(cur != 1)
        {
            if(index[cur/2] < n)
            {
                index[cur] = index[cur/2];
                cur/=2;
            }
            else
            {
                break;
            }
        }
        index[cur] = n;
    }
    public int Remove()
    {
        if(size == 0)
        {
            return 0;
        }
        int data = index[1];
        index[1] = index[size];
        size--;
        int bumo = 1;
        int ddal = 2;
        int temp;
        while(ddal <= size)
        {
            if(index[ddal] < index[ddal+1])
            {
                if(ddal+1 <= size)
                {
                    ddal++;
                }
            }
            if(index[bumo] < index[ddal])
            {
                temp = index[bumo];
                index[bumo] = index[ddal];
                index[ddal] = temp;
                bumo = ddal;
                ddal *= 2;
            }
            else
            {
                break;
            }
        }
        return data;
    }
    public int Peek()
    {
        return index[1];
    }
    public int Size()
    {
        return size;
    }
}

class Minheap
{
    private int[] index;
    private int size;
    public Minheap(int n)
    {
        index = new int[n+2];
        size = 0;
    }
    public void Add(int n)
    {
        size++;
        int cur = size;
        while(cur != 1)
        {
            if(index[cur/2] > n)
            {
                index[cur] = index[cur/2];
                cur /= 2;
            }
            else
            {
                break;
            }
        }
        index[cur] = n;
    }
    public int Remove()
    {
        if(size == 0)
        {
            return 0;
        }
        int data = index[1];
        index[1] = index[size];
        index[size] = 10001;
        size--;
        int bumo = 1;
        int ddal = 2;
        int temp;
        while(ddal <= size)
        {
            if(index[ddal] > index[ddal+1])
            {
                if(ddal+1 <= size)
                {
                    ddal++;
                }
            }
            if(index[bumo] > index[ddal])
            {
                temp = index[bumo];
                index[bumo] = index[ddal];
                index[ddal] = temp;
                bumo = ddal;
                ddal *= 2;
            }
            else
            {
                break;
            }
        }
        return data;
    }
    public int Peek()
    {
        return index[1];
    }
    public int Size()
    {
        return size;
    }
}

#endif
}
