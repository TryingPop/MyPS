using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 25
이름 : 배성훈
내용 : 레슬러
    문제번호 : 1900

    그리디, 정렬 문제다

    입력이 1만개까지 들어오고,
    힘과 마술링은 1000까지 온다

    처음에는, 승 수를 모두 찾고 계산해서 풀었는데,
    다른 사람 풀이와 시간을 보니 그냥 정렬에 바로 이용하는게 빠르다!
    308ms -> 80ms로 4배 가까이 빠르다!

    아이디어는 다음과 같다
    모두 1번씩 경기하는데, 승 수가 높은 사람을 먼저 배치하는게 메달 지급이 적다
*/

namespace BaekJoon.etc
{
    internal class etc_0612
    {

        static void Main612(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int n;
            (int pow, int ring, int idx)[] arr;
            Solve();

            void Solve()
            {

                Input();

                Array.Sort(arr, (x, y) => 
                {

                    return (y.pow + x.pow * y.ring).CompareTo(x.pow + y.pow * x.ring);
                });
                
                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput());

                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{arr[i].idx}\n");
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();

                arr = new (int pow, int ring, int idx)[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt(), i + 1);
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
import java.io.*;
import java.util.Arrays;

public class Main {
  private static final FastIO io = new FastIO();

  public static void main(String[] args) throws IOException {
    int n = io.nextInt();
    Wrestler[] wrestlers = new Wrestler[n];
    for (int i = 0; i < n; i++) {
      wrestlers[i] = new Wrestler(i, io.nextInt(), io.nextInt());
    }

    Arrays.sort(wrestlers);
    for (Wrestler wrestler : wrestlers) {
      io.writelnInt(wrestler.index + 1);
    }
    io.flushAndClose();
  }

  private static class Wrestler implements Comparable<Wrestler> {

    private final int index;
    private final int power;
    private final int ringPower;

    private Wrestler(int index, int power, int ringPower) {
      this.index = index;
      this.power = power;
      this.ringPower = ringPower;
    }

    @Override
    public int compareTo(Wrestler o) {
      return this.power + this.ringPower * o.power < o.power + o.ringPower * this.power ? 1 : -1;
    }
  }

  private static class FastIO {

    private static final int BUFFER_SIZE = 1 << 16;
    private final DataInputStream in;
    private final DataOutputStream out;
    private final byte[] inBuffer;
    private final byte[] outBuffer;
    private final byte[] bytebuffer;

    private int inBufferPointer, bytesRead;
    private int outBufferPointer;

    private FastIO() {
      in = new DataInputStream(System.in);
      out = new DataOutputStream(System.out);
      inBuffer = new byte[BUFFER_SIZE];
      outBuffer = new byte[BUFFER_SIZE];
      bytesRead = outBufferPointer = 0;
      bytebuffer = new byte[10];
    }

    private int nextInt() throws IOException {
      byte c = read();
      while (c <= ' ') {
        c = read();
      }

      boolean neg = (c == '-');
      if (neg) {
        c = read();
      }

      int ret = 0;
      do {
        ret = ret * 10 + c - '0';
      } while ((c = read()) >= '0' && c <= '9');

      return neg ? -ret : ret;
    }

    private byte read() throws IOException {
      if (inBufferPointer == bytesRead) {
        fillBuffer();
      }
      return inBuffer[inBufferPointer++];
    }

    private void fillBuffer() throws IOException {
      bytesRead = in.read(inBuffer, inBufferPointer = 0, BUFFER_SIZE);
      if (bytesRead == -1) {
        inBuffer[0] = -1;
      }
    }

    private void writelnInt(int i) {
      writeInt(i);
      writeBuffer((byte) '\n');
    }

    private void writeInt(int i) {
      if (i == 0) {
        writeBuffer((byte) '0');
        return;
      }

      if (i < 0) {
        writeBuffer((byte) '-');
        i = -i;
      }

      int index = 0;
      while (i > 0) {
        bytebuffer[index++] = (byte) ((i % 10) + '0');
        i /= 10;
      }

      while (index-- > 0) {
        writeBuffer(bytebuffer[index]);
      }
    }

    private void writeBuffer(byte b) {
      if (outBufferPointer == outBuffer.length) {
        flushBuffer();
      }
      outBuffer[outBufferPointer++] = b;
    }

    private void flushBuffer() {
      if (outBufferPointer != 0) {
        try {
          out.write(outBuffer, 0, outBufferPointer);
        } catch (Exception ignored) {
        }
      }
      outBufferPointer = 0;
    }

    private void flushAndClose() throws IOException {
      flushBuffer();
      close();
    }

    private void close() throws IOException {
      in.close();
      out.close();
    }
  }
}

#endif
}
