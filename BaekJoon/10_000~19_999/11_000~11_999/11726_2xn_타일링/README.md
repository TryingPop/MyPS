# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 다이나믹 프로그래밍

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
2×n 크기의 직사각형을 1×2, 2×1 타일로 채우는 방법의 수를 구하는 프로그램을 작성하시오.<br/>
아래 그림은 2×5 크기의 직사각형을 채운 한 가지 방법의 예이다.<br/>

## 입력
첫째 줄에 n이 주어진다. (1 ≤ n ≤ 1,000)<br/>

## 출력
첫째 줄에 2×n 크기의 직사각형을 채우는 방법의 수를 10,007로 나눈 나머지를 출력한다.<br/>

## 예제 입력
9<br/>

## 예제 출력
55<br/>

## 풀이
가로로 배치하는 경우 세로로 배치하는 경우로 길이를 늘려갈 수 있다.<br/>
그래서 dp[i]를 가로 길이 i일 때 경우의 수라 하면<br/>
dp[i] = dp[i - 1] + dp[i - 2]인 점화식을 얻을 수 있다.<br/>
해당 점화식으로 정답을 찾아갔다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/11726