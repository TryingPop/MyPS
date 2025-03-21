# C#

## 난이도 : 브론즈 3

## 알고리즘 분류
  - 수학
  - 사칙연산

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
Du har bestämt dig för att laga mat. För att laga maten behöver du N ingredienser. För varje ingrediens vet du hur mycket du redan har hemma, hur mycket du behöver totalt samt kostnaden för ingrediensen om du måste köpa den. Du skall alltså köpa den mängd av varje ingrediens som du saknar. Uppgiften är att beräkna kostnaden för att laga maten.<br/>


## 입력
Den första raden består av ett heltal N (1 ≤ N ≤ 5), antalet ingredienser.<br/>
De nästa N raderna beskriver en ingrediens med tre heltal H, B, K (0 ≤ H, B ≤ 1,000, 1 ≤ K ≤ 1,000) som står för hur mycket du har, hur mycket som behövs och hur mycket den kostar.<br/>


## 출력
Skriv ut ett enda heltal -- den totala kostnaden för ingredienserna.<br/>


## 예제 입력
3<br/>
10 5 1000<br/>
0 4 5<br/>
2 5 1<br/>


## 예제 출력
23<br>


## 풀이
요리하는데 필요한 재료 가격을 누적하는 단순 사칙연산 문제다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/26933