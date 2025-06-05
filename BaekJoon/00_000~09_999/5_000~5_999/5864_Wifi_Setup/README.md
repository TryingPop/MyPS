# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 다이나믹 프로그래밍

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Farmer John's N cows (1 <= N <= 2000) are all standing at various positions along the straight path from the barn to the pasture, which we can think of as a one-dimensional number line. Since his cows like to stay in email contact with each-other, FJ wants to install Wifi base stations at various positions so that all of the cows have wireless coverage.<br/>
After shopping around, FJ learns that the cost of a Wifi base station depends on distance it can transmit: a base station of power r costs A + B*r, where A is a fixed cost for installing the base station and B is a cost per unit of transmission distance. If FJ installs such a device at position x, then it can transmit data to any cow located in the range x-r ... x+r. A base station with transmission power of r=0 is allowed, but this only provides coverage to a cow located at the same position as the transmitter.<br/>
Given the values of A and B, as well as the locations of FJ's cows, please determine the least expensive way FJ can provide wireless coverage for all his cows.<br/>


## 입력
  - Line 1: Three space-separated integers: N A B (0 <= A, B <= 1000).
  - Lines 2..1+N: Each line contains an integer in the range 0..1,000,000 describing the location of one of FJ's cows.


## 출력
  - Line 1: The minimum cost of providing wireless coverage to all cows.


## 예제 입력
3 20 5<br/>
7<br/>
0<br/>
100<br/>


## 예제 출력
57.5<br/>


## 힌트
Input Details<br/>
There are 3 cows at positions 7, 0, and 100. Installation of a base station of power r costs 20 + 5*r.<br/>
Output Details<br/>
The optimal solution is to build a base station at position 3.5 (with power 3.5) and another at position 100 (with power 0). The first base station covers cows 1 and 2, and the second covers cow 3.<br/>


## 풀이
N이 1000으로 N^2의 방법이 유효하다.<br/>
먼저 위치에 따라 오름차순 정렬한다.<br/>


이후 dp[i]를 정렬된 상태의 i번까지 전력을 공급하는데 가장 싼 가격을 담는다.<br/>
r[j][i]를 j번 위치와 i번 위치의 중앙에서 j번 위치까지의 거리라 하자.<br/>
그러면 위치는 정렬되어 있으므로 j번 위치와 i번 위치에 전력이 r인 발전소를 설치하면 j번에서 i번까지 모두 전력이 공급되게 할 수 있다.<br/>
그래서 그리디로 dp[i]는 dp[j - 1] + (j 에서 i번까지 전력을 공급하는 최소 발전소) 중에서 나옴을 알 수 있다.<br/>
따라서 점화식 dp[i] = min(dp[j - 1] + a + b * r[j][i])를 얻는다.<br/>


이렇게 N^2의 방법으로 찾아 제출했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/5864