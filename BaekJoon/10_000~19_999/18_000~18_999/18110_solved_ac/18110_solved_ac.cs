using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 12
이름 : 배성훈
내용 : solved.ac
    문제번호 : 18110번

    수학, 구현, 정렬 문제다
    C#에서 반올림은 오사오입이라 한다
    https://velog.io/@chj7239/%EC%88%98%ED%95%99-%EC%98%A4%EC%82%AC%EC%98%A4%EC%9E%85

    아래처럼 round 함수에 두번째 인자로 
    MidpointRounding.AwayFromZero를 전달해
    사사오입으로 만들어줘야한다
    혹은 매우 작은 값을 더해줘도 된다

        2
        2
        3
    을 입력하면 3을 기대해야하나 2가 나온다
*/

namespace BaekJoon.etc
{
    internal class etc_1054
    {

        static void Main1054(string[] args)
        {

            int n, ret;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Console.Write(ret);
            }

            void GetRet()
            {

                ret = 0;
                if (n == 0) return;

                Array.Sort(arr);
                int pop = GetPop();

                double sum = 0;
                for (int i = pop; i < n - pop; i++)
                {

                    sum += arr[i];
                }

                sum /= n - (pop << 1);
                ret = (int)(Math.Round(sum + 1e-9) + 1e-9);
                // ret = (int)(Math.Round(sum, MidpointRounding.AwayFromZero) + 1e-9);

                int GetPop()
                {

                    double chk = n * 0.15;
                    return (int)(Math.Round(chk + 1e-9) + 1e-9);
                    // return (int)(Math.Round(chk, MidpointRounding.AwayFromZero) + 1e-9);
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();

                int ReadInt()
                {

                    int ret = 0;
                    
                    while(TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == -1 || c == ' ' || c == '\n') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
using System;
using System.IO;
class Program {
  public static void Main (string[] args) {
    int N = IO.NextInt();
    int P = (int)Math.Round(N*0.15,0,MidpointRounding.AwayFromZero);
    int[] scores = new int[31];
    
    for(int i = 0; i < N; i++) scores[IO.NextInt()]++;

    if (N == 0){
      IO.Write(0);
      IO.Close();
      return;
    }
    
    
    int remove = P;
    int j = 0; // 반복 횟수
    int hap = 0;
    for(int i = 0; i < scores.Length; i++){
      for (int k = 0; k < scores[i]; k++){
        if (j++ >= N-P) break;
        hap += remove-- <= 0 ? i : 0;
      }
      if (j >= N-P) break;
    }
    
    IO.Write(Math.Round((double)hap /
      (N-P-P),0,MidpointRounding.AwayFromZero));
    IO.Close();
  }
}
public static class IO{
  static StreamReader R = new(new BufferedStream(Console.OpenStandardInput(),131072));
  static StreamWriter W = new(new BufferedStream(Console.OpenStandardOutput(),131072));
  public static void Close(){R.Close();W.Close();}
  public static void Write(object s) => W.Write(s);
  public static int NextInt(){
    var (r, n, v) = (false, false, 0);
    while (true){
      int c = R.Read();
      if (!n && c == '-'){
        n = true;
        r = true;
        continue;
      }
      if ('0' <= c && '9' >= c){
        v = v * 10 + (c - '0');
        r = true;
        continue;
      }
      if (r) break;
    }
    return n?-v:v;
  }
}
#endif
}
