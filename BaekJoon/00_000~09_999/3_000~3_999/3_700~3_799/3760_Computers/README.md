# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 문자열
  - 파싱

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Everybody is fond of computers, but buying a new one is always a money challenge. Fortunately, there is always a convenient way to deal with. You can replace your computer and get a brand new one, thus saving some maintenance cost. Of course, you must pay a fixed cost for each new computer you get.<br/>
Suppose you are considering a n year period over which you want to have a computer. Suppose you buy a new computer in year y, 1<=y<=n. Then you have to pay a fixed cost c, in the year y, and a maintenance cost m(y,z) each year you own that computer, starting from year y through the year z, z<=n, when you plan to buy - eventually - another computer.<br/>
Write a program that computes the minimum cost of having a computer over the n year period.<br/>


## 입력
The program input is from a text file. Each data set in the file stands for a particular set of costs. A data set starts with the cost c for getting a new computer. Follows the number n of years, and the maintenance costs m(y,z), y=1..n, z=y..n. The program prints the minimum cost of having a computer throughout the n year period.<br/>
White spaces can occur freely in the input. The input data are correct and terminate with an end of file.<br/>


## 출력
For each set of data the program prints the result to the standard output from the beginning of a line.<br/>


## 예제 입력
3<br/>
3<br/>
5 7 50<br/>
6 8<br/>
10<br/>


## 예제 출력
19<br/>


## 힌트
An input/output sample is in the table below. There is a single data set. The cost for getting a new computer is c=3. The time period n is n=3 years, and the maintenance costs are:<br/>

  - For the first computer, which is certainly bought: m(1,1)=5, m(1,2)=7, m(1,3)=50,
  - For the second computer, in the event the current computer is replaced: m(2,2)=6, m(2,3)=8
  - For the third computer, in the event the current computer is replaced: m(3,3)=10.

The result for the data set is the minimum cost 19.<br/>


## 풀이
각 j < n일에 컴퓨터를 살 수 있고, j일부터 k일까지 유지비 m[j][k]가 순차적으로 주어진다.<br/>
n의 범위가 없어 한참을 고민했고, 질문 게시판을 보니 문제를 푼 다른 사람이 1_000이하라고 명시해줬다.<br/>
그래서 배낭 알고리즘을 적용해 풀었다.<br/>
실제로 1_000개의 배열을 선언하고 해당 배열을 초기화하면서 사용했는데 통과했으므로 n ≤ 1_000으로 보면 된다.<br/>


dp를 다음과 같이 선언했다.<br/>
dp[i] = val를 i일에 최소 비용이 되게 담는다.<br/>


그러면 다음과 같은 점화식을 얻는다.<br/>
각 j < n에 대해 j ≤ k ≤ n를 채운다.<br/>
dp[k] = Min(dp[k], dp[j - 1] + c + m[j][k])임을 알 수 있다.<br/>


그래서 j = 1, 2, ..., n에대해 오름차순으로 조사하면 dp[n]에 가장 작은 값이 담기게 된다.<br/>


해당 경우 각 j에 대해 j ≤ k ≤ n을 조사하므로 n의 연산을 한다.<br/>
j = 1, 2, 3, ..., n을 조사하므로 시간 복잡도는 O(n^2)에 찾을 수 있다.<br/>


문제 입력에서 \"White spaces can occur freely in the input. The input data are correct and terminate with an end of file.\" 같은 문장이 있다.<br/>
입력에서 숫자를 받을 때 White spaces 처리를 해줘야 한다.<br/>


단순 string의 Trim 만으론 White spaces를 모두 처리하지 못한다. 실제로 쓰니 문자열 에러인 Format 에러떴다.<br/>
그래서 숫자를 읽는데 White spaces를 처리하는 읽는 함수를 구현해서 해결했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/3760