# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 다이나믹 프로그래밍

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
Cissi går från sitt hem till skolan längs en lång gata som går i väst-östlig riktning. På sin väg passerar hon ett antal korsningar där tvärgator utgår norrut (N), söderut (S) eller både norrut och söderut (B). Vid varje korsning finns övergångsställen på både tvärgator och huvudgata (se figuren ovan), och dessa måste givetvis följas.<br/>
Både hemmet och skolan ligger på norra sidan av gatan. Skriv ett program som hjälper Cissi att beräkna det minsta antalet gator hon måste korsa på sin väg till skolan.<br/>

## 입력
Indata består av en enda rad med högst 1,000 bokstäver, som vardera är N, S eller B. Bokstäverna beskriver korsningarna i precis den ordning som Cissi passerar dem.<br/>

## 출력
En rad med ett heltal, det minsta antalet gator Cissi behöver korsa.<br/>

## 예제 입력
SNBNNSB<br/>

## 예제 출력
4<br>

## 풀이
dp를 이용해 해결했다.<br/>
위 up[i]와 아래 down[i]로 구분한 뒤 각각 i번째에 최소한 다리 건넌 횟수를 저장했다.<br/>
그리고 순차적으로 최소가되게 저장해가며 찾아갔다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/26922