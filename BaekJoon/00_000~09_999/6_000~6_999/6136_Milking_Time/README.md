# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 정렬

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Bessie is such a hard-working cow. In fact, she is so focused on maximizing her productivity that she decides to schedule her next N (1 <= N <= 1,000,000) hours (conveniently labeled 0..N-1) so that she produces as much milk as possible.<br/>
Farmer John has a list of M (1 <= M <= 1,000) possibly overlapping intervals in which he is available for milking. Each interval i has a starting hour (0 <= starting_hour_i < N), an ending hour (starting_hour_i < ending_hour_i <= N), and a corresponding efficiency (1 <= efficiency_i <= 1,000,000) which indicates how many gallons of milk that he can get out of Bessie in that interval. Farmer John starts and stops milking at the beginning of the starting hour and ending hour, respectively. When being milked, Bessie must be milked through an entire interval.<br/>
Even Bessie has her limitations, though. After being milked during any interval, she must rest R (1 <= R <= N) hours before she can start milking again. Given Farmer Johns list of intervals, determine the maximum amount of milk that Bessie can produce in the N hours.<br/>


## 입력
  - Line 1: Three space-separated integers: N, M, and R
  - Lines 2..M+1: Line i+1 describes FJ's ith milking interval with three space-separated integers: starting_hour_i, ending_hour_i, and efficiency_i


## 출력
  - Line 1: The maximum number of gallons of milk that Bessie can product in the N hours


## 예제 입력
12 4 2<br/>
1 2 8<br/>
10 12 19<br/>
3 6 24<br/>
7 10 31<br/>


## 예제 출력
43<br/>


## 풀이
시간은 100만까지 들어오고, M은 1000으로 1초 단위로 접근하면 시간초과 난다.<br/>
그런데 변화가 오는 시간만 나타내면 3M으로 줄일 수 있다.<br/>
시작 시간, 끝 시간, 끝나고 휴식이 끝난 시간 이렇게 3개로 구분 가능하다.<br/>
여기서 끝 시간을 휴식 시간까지 포함하면 2M으로 줄일 수 있다.<br/>


그래서 해당 특정 시간에 대해 배낭 비슷하게 접근했다.<br/>
먼저 시작 시간을 기준으로 정렬한다.<br/>
그리고 이전 가능한 최대 경우에 대해 해당 일을 시켜 최댓값을 찾았다.<br/>


처음에는 한 일이 없으므로 0초에 0효율이된다.<br/>
해당 일을 끝내면 해당 일의 효율, 해당일이 끝나는시간 + 휴식시간을 배열에 저장한다.<br/>


두 번째 일부터는 이전에 끝마친 경우를 확인한다.<br/>
처음한 일이 끝나는 시간과 효율이 있다.<br/>
처음한 일이 끝나는 시간이 현재 일의 시작 시간보다 늦은 경우 해당 일을 할 수 없다.<br/>
그래서 0효율로 시작해 현재 일의 효율만 있게 된다.<br/>
반면 처음한 일이 끝나는 시간이 현재 일의 시작 시간보다 빠른 경우 해당 일을 할 수 있다.<br/>
그래서 해당 효율은 처음 일의 효율과 현재 일의효율이 합쳐진 값을 갖는다.<br/>
이렇게 이전 최대 효율을 찾아 누적하는 식으로 찾는다.<br/>


마지막에 이렇게 찾은 효율들 중 최댓값을 출력하면 된다. O(M^2)이 된다.<br/>
조금 더 효율적으로 한다면, 끝 시간을 기준으로 정렬해서 시작 시간을 초과하는 경우 중단하면 된다.<br/>
최악의 경우는 시간이 같지만 매번 모든 경우를 탐색하지 않는 경우도 있기에 평균적으로 빠를거라 예상된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/6136