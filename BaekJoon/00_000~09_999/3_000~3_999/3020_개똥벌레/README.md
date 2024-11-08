# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 이분 탐색
  - 누적합

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
개똥벌레 한 마리가 장애물(석순과 종유석)로 가득찬 동굴에 들어갔다. 동굴의 길이는 N미터이고, 높이는 H미터이다. (N은 짝수) 첫 번째 장애물은 항상 석순이고, 그 다음에는 종유석과 석순이 번갈아가면서 등장한다.<br/>
아래 그림은 길이가 14미터이고 높이가 5미터인 동굴이다.<br/>
이 개똥벌레는 장애물을 피하지 않는다. 자신이 지나갈 구간을 정한 다음 일직선으로 지나가면서 만나는 모든 장애물을 파괴한다.<br/>
위의 그림에서 4번째 구간으로 개똥벌레가 날아간다면 파괴해야하는 장애물의 수는 총 여덟개이다. (4번째 구간은 길이가 3인 석순과 길이가 4인 석순의 중간지점을 말한다)<br/>
하지만, 첫 번째 구간이나 다섯 번째 구간으로 날아간다면 개똥벌레는 장애물 일곱개만 파괴하면 된다.<br/>
동굴의 크기와 높이, 모든 장애물의 크기가 주어진다. 이때, 개똥벌레가 파괴해야하는 장애물의 최솟값과 그러한 구간이 총 몇 개 있는지 구하는 프로그램을 작성하시오.<br/>

## 입력
첫째 줄에 N과 H가 주어진다. N은 항상 짝수이다. (2 ≤ N ≤ 200,000, 2 ≤ H ≤ 500,000)<br/>
다음 N개 줄에는 장애물의 크기가 순서대로 주어진다. 장애물의 크기는 H보다 작은 양수이다.<br/>

## 출력
첫째 줄에 개똥벌레가 파괴해야 하는 장애물의 최솟값과 그러한 구간의 수를 공백으로 구분하여 출력한다.<br/>

## 예제 입력
6 7<br/>
1<br/>
5<br/>
3<br/>
3<br/>
5<br/>
1<br/>

## 예제 출력
2 3<br/>

## 풀이
벽이 놓이는 것을 보면 범위로 놓이고 2배로 늘려도 100만이므로 세그먼트 트리도 할만하다 생각했다.<br/>
땅과 높이가 정수 부분만 이동가능한게 아닌 실수 부분도 이동가능하기에 2배수했다.<br/>
그리고 벽을 하나씩 세그먼트 트리에 저장하고 마지막에 가능한 각 높이에 벽이 몇개 있는지 일일히 찾아갔다.<br/>
벽 중 가장 작은 값을 제출했다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/3020