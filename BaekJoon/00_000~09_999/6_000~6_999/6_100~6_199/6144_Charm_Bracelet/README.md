# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 배낭 문제

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Bessie has gone to the mall's jewelry store and spies a charm bracelet. Of course, she'd like to fill it with the best charms possible from the N (1 <= N <= 3,402) available charms. Each charm i in the supplied list has a weight W_i (1 <= W_i <= 400), a 'desirability' factor D_i (1 <= D_i <= 100), and can be used at most once.  Bessie can only support a charm bracelet whose weight is no more than M (1 <= M <= 12,880).<br/>
Given that weight limit as a constraint and a list of the charms with their weights and desirability rating, deduce the maximum possible sum of ratings.<br/>


## 입력
  - Line 1: Two space-separated integers: N and M
  - Lines 2..N+1: Line i+1 describes charm i with two space-separated integers: W_i and D_i


## 출력
  - Line 1: A single integer that is the greatest sum of charm desirabilities that can be achieved given the weight constraints


## 예제 입력
4 6<br/>
1 4<br/>
2 6<br/>
3 12<br/>
2 7<br/>


## 예제 출력
23<br/>


## 힌트
Without the second possible charm, the 4+12+7=23 is the highest value for weight 1+2+3 ≤ 6<br/>


## 풀이
무게 m이하인 경우 많아야 1개를 사용해 가장 큰 가치 d를 찾는 문제다.<br/>
무게 m은 13_000을 넘지 않고 n은 3500을 넘지 않는다.<br/>


배낭 형식으로 접근해도 13_000 x 3_500 < 5000만이므로 유효하다.<br/>
그래서 배낭 형식으로 풀었다.<br/>


dp[i] = val를 무게 i일 때 최대 가치 val를 담는다.<br/>
각 보석에 대해 무게를 w, 가치를 d라 하자.<br/>
보석은 많아야 1번만 사용가능하기에 무게가 큰 i부터 탐색한다.<br/>
그리고 dp[i]에 값이 있고 i + w가 m이하인 경우 dp[i + w] = Max(dp[i + w], dp[i] + d)점화식을 얻는다.<br/>
처음엔 dp[0]에만 값 0을 채운다.<br/>
여기서 Max(a, b)는 a, b 중 큰 값을 반환하는 함수이다.<br/>


이렇게 각 보석에 대해 dp의 값을 채웠다.<br/>
이후 dp에서 가장 큰 값을 찾아 제출했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/6144