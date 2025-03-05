# C#

## 난이도 : 브론즈 1

## 알고리즘 분류
  - 수학

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
Karlas Gausaitis sugalvojo N neneigiamų sveikųjų skaičių. Savo draugui Gustavui jis pasakė pirmojo ir paskutiniojo, antrojo ir paskutiniojo, trečiojo ir paskutiniojo ir t. t. sumas – iki priešpaskutiniojo ir paskutiniojo.<br/>
Taip pat jis nurodė visų skaičių sumą. Žinoma, kad Karlas moka labai gerai skaičiuoti, todėl iš jo suteiktos informacijos visada galima rasti jo sugalvotus skaičius.<br/>
Pagal skaičių N, nurodytas pirmojo ir paskutiniojo, anrojo ir paskutiniojo ir t. t. sumas bei visų skaičių sumą padėkite Gustavui surasti Karlo sugalvotus skaičius.<br/>


## 입력
Pirmojoje eilutėje pateiktas skaičius N – Karlo sugalvotų skaičių kiekis. Tolimesnėse N − 1 eilučių pateiktos pirmojo ir paskutiniojo, antrojo ir paskutiniojo ir t. t. skačių sumos.<br/>
Paskutinėje eilutėje pateikta visų sugalvotų skaičių suma.<br/>


## 출력
Išveskite N eilučių – po vieną Karlo sugalvotą skaičių kiekvienoje.<br/>


## 제한
  - 3 ≤ N ≤ 1,000,000
  - Bet kuris sugalvotas skaičius yra neneigiamas sveikasis ir neviršija 1,000,000,000.


## 예제 입력
4<br/>
7<br/>
3<br/>
10<br/>
14<br/>


## 예제 출력
4<br/>
0<br/>
7<br/>
3<br/>


## 힌트
Karlas sugalvojo skaičius 4, 0, 7 ir 3, ir pasakė Gustavui pirmo ir ketvirto, antro ir ketvirto, trečio ir ketvirto ir visų 4 skaičių sumas: 7, 3, 10 ir 14.<br/>


## 풀이
우리가 찾는 정답을 수열 {an}이라 하자.<br/>
그러면 입력되는 값은 n이후에는 a1 + an, a2 + an, ... a(n-1) + an, a1 + a2 + ... + an이다.<br/>
그리고 모두 더하면 (a1 + a2 + ... + an) * 2 + (n - 2) * an을 얻는다.<br/>

마지막 입력 값을 2번 빼주면 (n - 2) * an의 값을 얻는다.<br/>
이에 an의 값을 알게되고 입력 값에서 an을 빼주면 a1, a2, ..., a(n-1)을 모두 찾을 수 있다.<br/>

정답 범위가 10억을 넘지 않는 음이아닌 정수라고 하는데, 이로 입력 범위가 int 범위를 벗어날 수 있다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/7269