# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 누적합

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
N개의 수 A1, A2, ..., AN이 입력으로 주어진다. 총 M개의 구간 i, j가 주어졌을 때, i번째 수부터 j번째 수까지 합을 구하는 프로그램을 작성하시오.<br/>

## 입력
첫째 줄에 수의 개수 N이 주어진다. (1 ≤ N ≤ 100,000) 둘째 줄에는 A1, A2, ..., AN이 주어진다. (-1,000 ≤ Ai ≤ 1,000) 셋째 줄에는 구간의 개수 M이 주어진다. (1 ≤ M ≤ 100,000) 넷째 줄부터 M개의 줄에는 각 구간을 나타내는 i와 j가 주어진다. (1 ≤ i ≤ j ≤ N)<br/>

## 출력
총 M개의 줄에 걸쳐 입력으로 주어진 구간의 합을 출력한다.<br/>

## 예제 입력
5<br/>
10 20 30 40 50<br/>
5<br/>
1 3<br/>
2 4<br/>
3 5<br/>
1 5<br/>
4 4<br/>

## 예제 출력
60<br/>
90<br/>
120<br/>
150<br/>
40<br/>

## 풀이
dp[i]를 1 ~ i까지의 누적합을 저장했다.<br/>
그리고 범위 [s, e]가 주어지면 dp[e] - dp[s - 1]로 구하면 된다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/11441