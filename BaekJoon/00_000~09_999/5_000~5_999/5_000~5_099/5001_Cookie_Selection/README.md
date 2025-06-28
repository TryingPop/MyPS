# C#

## 난이도 : 골드 1

## 알고리즘 분류
  - 자료 구조
  - 우선순위 큐

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
As chief programmer at a cookie production plant you have many responsibilities, one of them being that the cookies produced and packaged at the plant adhere to the very demanding quality standards of the Nordic Cookie Packaging Consortium (NCPC).<br/>
At any given time, your production line is producing new cookies which are stored in a holding area, awaiting packaging. From time to time there are also requests from the packaging unit to send a cookie from the holding area to be packaged. Naturally, you have programmed the system so that there are never any packaging requests sent if the holding area is empty. What complicates the matter is the fact that representatives of the NCPC might make a surprise inspection to check that your cookies are up to standard. Such an inspection consists of the NCPC representatives demanding that the next few cookies sent to the packaging unit instead be handed over to them; if they are convinced that these cookies look (and taste) the same, you pass the inspection, otherwise you fail.<br/>
Fortunately, the production plant has invested in a new measurement thingamajig capable of measuring cookie diameters with a precision of 1 nanometre (nm). Since you do not have the time to always be on the lookout for cookie craving NCPC inspectors, you have decided that a sensible strategy to cope with the inspections is to always send a cookie having the median diameter among all cookies currently in the holding area to the packaging unit on a request. If there is no cookie exhibiting the median diameter in the holding area, you instead send the smallest cookie larger than the median to the packaging unit, hoping it will please the NCPC inspectors. This means that, if the cookies were sorted in order of ascending diameter, and if there was an odd number c of cookies in the holding area, you would send the cookie at position (c + 1)/2 in the sorted sequence, while if there was an even number c of cookies in the holding area, you would send the cookie at position (c/2) + 1 in the sorted sequence to the packaging unit on a request.<br/>


## 입력
Each line of the input contains either a positive integer d, indicating that a freshly baked cookie with diameter d nm has arrived at the holding area, or the symbol ’#’, indicating a request from the packaging unit to send a cookie for packaging. There are at most 600 000 lines of input, and you may assume that the holding area is empty when the first cookie in the input arrives to the holding area. Furthermore, you have read somewhere that the cookie oven at the plant cannot produce cookies that have a diameter larger than 30 centimetres (cm) (or 300 000 000 nm).<br/>


## 출력
A sequence of lines, each indicating the diameter in nm of a cookie sent for packaging, in the same order as they are sent.<br/>


## 예제 입력
1<br/>
2<br/>
3<br/>
4<br/>
#<br/>
#<br/>
#<br/>
#<br/>


## 예제 출력
3<br/>
2<br/>
4<br/>
1<br/>


## 풀이
현재까지 읽은 수들 중 중앙값을 찾아야 한다.<br/>
원소의 추가와 빠지는게 잦아 중앙값을 찾을 때 정렬 방법을 이용한다면 정렬이 잦다.<br/>
그리고 원소의 수가 60만까지 들어오므로 정렬은 시간초과날 수 있다.<br/>


다른 방법으로 중앙값은 우선순위 큐 2개를 이용하면 빠르게 찾을 수 있다.<br/>
먼저 우선순위 큐 2개를 min, max로 나눈다.<br/>
여기서 min은 전체에서 작은 것의 절반을 담는 우선순위 큐이다.<br/>
max는 전체에서 큰 절반 즉, min의 원소가 아닌 것을 담는 우선순위 큐로 놓는다.<br/>
그러면 min, max의 원소들의 갯수는 많아야 1차이 나야한다.<br/>
문제에서 중앙값이 2개인 경우 큰쪽의 인덱스를 사용한다고 했으므로 일관성을 위해 max를 min보다 작지 않게 설정하자.<br/>


예를들어 보관할 원소가 1, 3, 3, 4, 5라고 하자.<br/>
min에는 작은 원소 2개 1, 3을 보관한다.<br/>
max에는 큰 원소 3개 혹은 min이 아닌 남은 원소 3, 4, 5를 보관한다.<br/>


원소를 넣는 경우를 보자. 넣을 수를 min에 넣는다.<br/>
max가 min보다 원소가 같거나 1개 많아야 하므로 max의 원소가 min보다 작다면 min의 가장 큰 수를 max에 넣는다.<br/>
이제 max의 가장 작은 원소가 min의 가장 큰 원소보다 작은 경우 max의 가장 작은 원소와 min의 가장 큰 원소를 바꿔줘야 한다.<br/>
이렇게 원소를 1개씩 채워나가는 경우 max의 가장 작은 값이 문제에서 요구하는 중앙값이 된다.<br/>
그래서 min은 가장 큰 값을 확인하므로 내림차순으로 비교하게 하고, max는 가장 작은 값을 확인하므로 오름차순으로 비교한다.<br/>
해당 방법을 쓰는 경우 원소를 추가할 때 많아야 2번의 원소 이동이 일어난다.<br/>


또 다른 풀이로는 세그먼트 트리를 이용해 구할 수 있다.<br/>
입력 값의 범위가 3억으로 오프라인 쿼리 방법과 좌표 압축이 필요하다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/5001