# C#

## 난이도 : 골드 1

## 알고리즘 분류
  - 다이나믹 프로그래밍

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
There are two coal mines, each employing a group of miners. Mining coal is hard work, so miners need food to keep at it. Every time a shipment of food arrives at their mine, the miners produce some amount of coal. There are three types of food shipments: meat shipments, fish shipments and bread shipments.<br/>
Miners like variety in their diet and they will be more productive if their food supply is kept varied. More precisely, every time a new shipment arrives to their mine, they will consider the new shipment and the previous two shipments (or fewer if there haven't been that many) and then:<br/>

  - If all shipments were of the same type, they will produce one unit of coal.
  - If there were two different types of food among the shipments, they will produce two units of coal.
  - If there were three different types of food, they will produce three units of coal.

We know in advance the types of food shipments and the order in which they will be sent. It is possible to influence the amount of coal that is produced by determining which shipment should go to which mine. Shipments cannot be divided; each shipment must be sent to one mine or the other in its entirety.<br/>
The two mines don't necessarily have to receive the same number of shipments (in fact, it is permitted to send all shipments to one mine).<br/>
Your program will be given the types of food shipments, in the order in which they are to be sent. Write a program that finds the largest total amount of coal that can be produced (in both mines) by deciding which shipments should be sent to mine 1 and which shipments should be sent to mine 2.<br/>


## 입력
The first line of input contains an integer N (1 ≤ N ≤ 100 000), the number of food shipments.<br/>
The second line contains a string consisting of N characters, the types of shipments in the order in which they are to be distributed. Each character will be one of the uppercase letters 'M' (for meat), 'F' (for fish) or 'B' (for bread).<br/>


## 출력
Output a single integer, the largest total amount of coal that can be produced.<br/>


## 예제 입력
6<br/>
MBMFFB<br/>


## 예제 출력
12<br/>


## 힌트
In the sample 1, by distributing the shipments in this order: mine 1, mine 1, mine 2, mine 2, mine 1, mine 2, the shipments will result in 1, 2, 1, 2, 3 and 3 units of coal produced in that order, for a total of 12 units. There are other ways to achieve this largest amount.<br/>


## 풀이
각 음식을 탄광 1, 탄광 2에 보내는 총 2가지 방법이 있다.<br/>
음식의 전체 개수를 N이라 하면 브루트포스로 2^N 경우의 수가 된다.<br/>
N ≤ 100,000이므로 시간 초과이다.<br/>


다른 방법을 찾기위해 모든 경우를 조사할 수 있는 N이 낮은 경우를 확인했다.<br/>
그러면 i번째 음식에 따라 가능한 탄광의 상태는 40가지 경우로 나타낼 수 있음을 확인했다.<br/>


음식을 고기 M, 생선 F, 빵 B, 비어있는 경우를 X라 하면 MMM, MMF, MMB, ..., BBF, BBB, XMM, XMF, XMB, ..., XBF, XBB, XXM, XXF, XXB, XXX로 표현할 수 있다.<br/>
해당 경우 탄광 1, 탄광 2의 전체 상태는 40 x 40으로 1600이 된다.<br/>
N이 10만까지 올 수 있으므로 10만 x 1600 = 1.6억으로 시간초과날 수 있다.<br/>


여기서 가장 최근껀 추가될 때 1개를 알 수 있으니 이전 2가지 경우만 알면 된다.<br/>
그래서 MMM, MMF, MMB, ..., BBF, BBB의 총 27가지 경우가 빠진 총 13가지 MM, MF, MB, ..., BF, BB, XM, XF, XB, XX의 경우만 알면 된다.<br/>


해당 경우 탄광 1, 탄광 2의 전체 상태는 13 x 13 = 169이고 N이 10만이라 고려해도 169 x 10만 = 1690만으로 감당가능한 연산이다.<br/>
그래서 dp의 방법으로 풀었다.<br/>


dp[i][j][k] = val를 i는 음식을 선택하는 상태를 나타내고, j는 탄광 1의 상태, k는 탄광 2의 상태를 뜻한다.<br/>
그리고 val는 생산한 최대 석탄량이되게 설정했다.<br/>


탄광의 상태는 직전 2개의 음식 종류를 고려하며, 왼쪽이 과거(이전), 오른쪽이 최근 음식 순서로 구성된다. 예: 'XM', 'BF' 등.XX, XM, XB, XF, MM, MF, MB, ..., BF, BB 순서로 0번부터 12번까지 부여했다.<br/>


그러면 다음과 같은 점화식을 얻는다.<br/>
dp[i - 1][j][k]에 값이 존재하지 않는 경우 조사할 필요가 없다.<br/>
반면 dp[i - 1][j][k]에 값이 존재하는 경우 다음과 같이 확인한다.<br/>


현재 음식 종류를 편의상 food이라 하자.<br/>
그리고 j는 탄광 1의 상태이므로 j번호에 맞는 음식 상태 cur1를 확인한다.<br/>


그리고 food을 탄광 1에 추가한 경우 생산하는 석탄량을 음식의 종류 cnt로 확인할 수 있다.<br/>
food = 'M'이고, cur이 'MB'인 경우 MMB로 해당 예제에서 총 2가지의 음식 종류가 있다.<br/>
이는 비트 연산자를 이용해 찾았다.<br/>


그리고 탄광 1의 상태에 cur1을 넣은 탄광 1의 최근 2개의 상태를 next1로 갱신한다.<br/>
그리고 next1에 맞는 인덱스 l로 변환한다.<br/>
그러면 dp[i][l][k] = Math.Max(dp[i][l][k], dp[i - 1][j][k] + cnt)이 된다.<br/>


탄광의 상태를 XB, XF로 했기에 왼쪽을 가장 늦게 넣은걸로, 오른쪽을 가장 최근에 넣은걸로 넣는다.<br/>
그래서 food = 'M'이고 cur이 'MB'인 경우 MBM이고 가장 나중에 들어간 M이 빠지고 'BM'이 된다.<br/>


같은 음식이 2개 연속 들어오는 경우(MM, BB, FF)는 처리나 XM, XB, XF방법이나 결과적으로 같다.<br/>
같은 음식이 2개 연속 들어오는 MM, BB, FF을 1개의 음식으로 XM, XB, XF로 치환해 상태 수를 줄였다.<br/>
이를 통해 전체 상태 수를 13 → 10개로 압축했다.<br/>


이후 k에 대해서 탄광 2에 보내려고 한다. k의 상태에 food를 보낸 상태를 m이라 하자.<br/>
이렇게 dp[i][j][m] = Math.Max(dp[i][j][m], dp[i - 1][j][k] + cnt)이 된다.<br/>


모두 조사하는 경우 dp[i][j][k]의 값들을 채워갔다.<br/>


탄광 1, 2의 가능한 상태는 각 10개 x 10개이고, N이 10만개까지 있다.<br/>
그래서 조사하는 경우는 100 x 10만 = 1000만 연산을 한다.<br/>
그래서 시간 복잡도는 O(100 x N)이 된다.<br/>


이전과 현재만 알면 되기에 2개의 배열을 갱신하면서 진행해도 된다.<br/>
그래서 i의 범위를 N로 할당하는게 아닌 2개만 할당해서 이전(0), 현재(1)로 사용했다.<br/>
그러면 사용하는 배열의 크기는 10 x 10 x 2의 배열을 사용한다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/5475