# C#

## 난이도 : 골드 3

## 알고리즘 분류
  - 그래프 이론
  - 평면 그래프

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Piet Hein was a Dutch naval officer during the Eighty Years’ War between the United Provinces of The Netherlands and Spain. His most famous victory was the capture of the Zilvervloot (‘Silver Fleet’) near Cuba in 1628, where he intercepted a number of Spanish vessels that were carrying silver from the Spanish colonies in the Americas to Spain. Details about this famous naval battle are sketchy, so the description below may contain some historical inaccuracies.<br/>
The Silver Fleet consisted of vessels containing silver coins. Piet Hein’s basic strategy was simple: tow away a number of vessels from the fleet, in order to capture their contents.<br/>
In an attempt to prevent the Dutch from carrying out this plan, the Spanish tied all the ships in their fleet together using huge iron chains. Each vessel in their fleet was fixed to at least one other vessel; any two vessels were connected by at most one chain; and the Spanish made sure that the chains did not cross each other, otherwise they could get tied up into a knot. As an end result, the vessels and the chains connecting them formed a connected, planar graph.<br/>
However, the Spanish preventive measures only made their situation worse. As an experienced naval officer, Piet Hein knew that towing away a group of ships was easiest if, for every two ships in the group, the ships were connected by a chain. He called such groups chaingroups.<br/>
Piet Hein ordered his men to tow away all the ships in the chaingroup that contained the largest amount of booty, after severing the links with the remaining ships in the Spanish fleet with a few highly accurate canon shots. The total booty in a chaingroup is the total number of silver coins in the vessels that make up the chaingroup.<br/>
Figure 1 – The Silver Fleet represented as a graph: each dot denotes a vessel in the fleet, while each line denotes a chain that connects two vessels. The vessels that are connected in the figure by the dashed lines correspond to the chaingroup that provides the highest total value of silver coins. In this case, Piet Hein loots 4500 silver coins from the fleet.<br/>
Given a description of the Silver Fleet, find the value of the chaingroup with the highest amount of booty (i.e., total number of silver coins in the ships that make up the chaingroup).<br/>


## 입력
For each test-case:<br/>

  - A line containing two integers v (2 ≤ v ≤ 450) and e (1 ≤ e ≤ 900), the number of vessels in the fleet and the number of chains, respectively.
  - Then, v lines specifying S1, S2, ... , Sv, the amount of silver coins carried by vessel i (1 ≤ i ≤ v). The Si will be positive integers, where 100 ≤ Si ≤ 6000.
  - Then, for each chain, a line containing two integers cstart and cend , the two vessels connected by the chain, where (1 ≤ cstart < cend ≤ v).

Each fleet forms a connected, planar graph.<br/>


## 출력
For each test case, one line containing a single positive integer: the number of silver coins that is captured by Piet Hein’s fleet.<br/>


## 예제 입력
4 6<br/>
100<br/>
5000<br/>
1000<br/>
2000<br/>
1 2<br/>
1 3<br/>
1 4<br/>
2 3<br/>
2 4<br/>
3 4<br/>
6 8<br/>
1500<br/>
1000<br/>
100<br/>
2000<br/>
500<br/>
300<br/>
1 2<br/>
1 3<br/>
1 4<br/>
2 4<br/>
3 5<br/>
4 5<br/>
4 6<br/>
5 6<br/>

## 예제 출력
8100<br/>
4500<br/>


## 풀이
문제에서 말하는 chain은 완전 그래프를 뜻한다.<br/>
A가 완전 그래프라 함은 A의 모든 점에 대해 간선이 존재하는 경우다.<br/>
문제에서 연결을 짓는 경우 평면그래프가 된다고 했으므로, 가능한 완전 그래프는 점이 4개 이하이다.<br/>
4개인 경우 삼각형을 하나 그리고 내부에 점을 찍은 뒤 4개의 점에 대해 각각 간선을 이으면 완전 그래프가 된다.<br/>
반면 5개의 경우 앞의 23039번의 실 전화기 문제에서 모두 잇는 경우 평면상에서는 교차할 수 밖에 없다.<br/>


이제 각각의 경우에 대해 조사해주면 된다.<br/>
점이 1개인 완전 그래프는 입력과 동시에 확인했다.<br/>
점이 2개인 완전 그래프는 간선의 입력에서 확인했다.<br/>
점이 3개인 완전 그래프는 임의의 점 1개와 간선에 대해<br/>
해당 점과 직선의 두점 사이에 간선이 존재하는지 판별하면 된다.<br/>
점이 4개인 완전 그래프는 임의의 간선 2개에 대해<br/>
서로 다른 선분의 점에 가는 간선이 존재하는지 확인하면 된다.<br/>

간선 존재여부는 점의 크기가 450개 이하이므로 간선 입력에서 두 점의 간선 존재를 배열에 저장했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/9644