# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 그래프 이론
  - 그래프 탐색
  - 최단 경로
  - 플로이드-워셜

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
N (1 <= N <= 100) cows, conveniently numbered 1..N, are participating in a programming contest. As we all know, some cows code better than others. Each cow has a certain constant skill rating that is unique among the competitors.<br/>
The contest is conducted in several head-to-head rounds, each between two cows. If cow A has a greater skill level than cow B (1 <= A <= N; 1 <= B <= N; A != B), then cow A will always beat cow B.<br/>
Farmer John is trying to rank the cows by skill level. Given a list the results of M (1 <= M <= 4,500) two-cow rounds, determine the number of cows whose ranks can be precisely determined from the results. It is guaranteed that the results of the rounds will not be contradictory.<br/>


## 입력
  - Line 1: Two space-separated integers: N and M
  - Lines 2..M+1: Each line contains two space-separated integers that describe the competitors and results (the first integer, A, is the winner) of a single round of competition: A and B


## 출력
  - Line 1: A single integer representing the number of cows whose ranks can be determined


## 예제 입력
5 5<br/>
4 3<br/>
4 2<br/>
3 2<br/>
1 2<br/>
2 5<br/>


## 예제 출력
2<br/>


## 힌트
2번의 경우 1, 3, 4번한테 지고, 5번한테 이긴다.<br/>
그래서 2번은 4등이 확실하다.<br/>


또한 5번의 경우 2번한테 지는데 2번이 1, 3, 4번한테 지므로 5번은 1, 2, 3, 4번한테 진다.<br/>
그래서 5번은 5등이 확실하다.<br/>


이외의 경우는 등수를 확인할 수 없고 2명만 확인가능하다.<br/>


## 풀이
A가 B를 이기고 B가 C를 이기면 A는 C를 이긴다는 조건이 숨어져 있다. 즉, 추이성이 성립한다.<br/>


승리 관계로 그래프를 연결한다.<br/>
그래서 자신을 이기는 사람들의 수를 찾아야 한다.<br/>
노드의 수가 100으로 작기에 플로이드 워셜 방법이 유효하고 O(N^3)에 모두 찾을 수 있다.<br/>
마찬가지로 패배 관계로 그래프를 연결해 자신에게 지는 인원을 찾는다.<br/>


이렇게 자신에게 지는 인원과 이기는 인원을 찾았을 때 n - 1명이면 자신의 등수가 확실하게 정해진다고 볼 수 있다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/6156