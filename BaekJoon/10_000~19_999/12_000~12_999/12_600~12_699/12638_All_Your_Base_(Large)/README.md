# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 수학
  - 그리디 알고리즘
  - 정수론

## 제한조건
  - 시간 제한 : 5초
  - 메모리 제한 : 512 MB

## 문제
In A.D. 2100, aliens came to Earth. They wrote a message in a cryptic language, and next to it they wrote a series of symbols. We've come to the conclusion that the symbols indicate a number: the number of seconds before war begins!<br/>
Unfortunately we have no idea what each symbol means. We've decided that each symbol indicates one digit, but we aren't sure what each digit means or what base the aliens are using. For example, if they wrote "ab2ac999", they could have meant "31536000" in base 10 -- exactly one year -- or they could have meant "12314555" in base 6 -- 398951 seconds, or about four and a half days. We are sure of three things: the number is positive; like us, the aliens will never start a number with a zero; and they aren't using unary (base 1).<br/>
Your job is to determine the minimum possible number of seconds before war begins.<br/>


## 입력
The first line of input contains a single integer, T. T test cases follow. Each test case is a string on a line by itself. The line will contain only characters in the 'a' to 'z' and '0' to '9' ranges (with no spaces and no punctuation), representing the message the aliens left us. The test cases are independent, and can be in different bases with the symbols meaning different things.<br/>
Limits<br/>

  - 1 ≤ T ≤ 100
  - The answer will never exceed 10^18
  - 1 ≤ the length of each line < 61


## 출력
For each test case, output a line in the following format:<br/>

	Case #X: V

Where X is the case number (starting from 1) and V is the minimum number of seconds before war begins.<br/>


## 예제 입력
3<br/>
11001001<br/>
cats<br/>
zig<br/>


## 예제 출력
Case #1: 201<br/>
Case #2: 75<br/>
Case #3: 11<br/>


## 풀이
k자리인 두 숫자 a, b 크기를 비교하는 방법을 보면 큰 단위부터 값이 작은지 비교한다.<br/>
i번째에서 a가 b보다 작은 경우 i + 1번째, i + 2, ..., k번째 수와는 상관없이 a < b가 된다.<br/>
그래서 값이 큰 자리부터 가장 작은 수를 배치하는게 그리디로 결과가 가장 작아짐을 알 수 있다.<br/>


입력된 문자를 읽는데, 처음 보는 문자일 경우 가장 작은 값을 배치해 줬다.<br/>


다만 처음 자리는 0이올 수 없다고 했으므로 1을 배치해주고, 다음 발견된 문자는 0을 배치한다.<br/>
이후는 2, 3, ... 으로 1씩 늘려가며 숫자를 부여한다.<br/>


예를들어 a23753 문자를 위와 같이 변형한다면 다음과 같다.<br/>

  - a → 1
  - 2 → 0
  - 3 → 2
  - 7 → 3
  - 5 → 4

 a23753 → 102342이고, 사용된 숫자는 5개이다.<br/>


이제 k자리를 읽어야 한다.<br/>
서로 다른 문자가 n개 사용되었다면, 각 문자에 고유한 숫자를 부여하려면 최소 n개의 숫자가 필요하므로 최소 n진법이 되어야 한다.<br/>
이 중 가장 작은 것은 n진법으로 읽을 때 숫자 값이 작아진다.<br/>


앞의 예 a23753을 보면, 문자열 `a23753`에서 문자 a, 2, 3, 7, 5가 사용되므로 최소 5진법이어야 한다.<br/>


5진법으로 읽는다면, p = 1 x 5^5 + 0 x 5^4 + 2 x 5^3 + 3 x 5^2 + 4 x 5^1 + 2이다.<br/>
6진법으로 읽는다면, q = 1 x 6^5 + 0 x 6^4 + 2 x 6^3 + 3 x 6^2 + 4 x 6^1 + 2이다.<br/>
계산해보면 p < q가 성립함을 알 수 있다.<br/>


단 하나의 문자만 사용된 경우 1진법처럼 보일 수 있지만, 문제에서는 1진법을 허용하지 않기 때문에 최소 2진법으로 해석해야 한다.<br/>
이렇게 특수 케이스 주의해 위처럼 풀면 된다.<br/>


문자를 읽을 때 자리별로 안쓰인 것인지 찾아야 한다.<br/>
이를 해시로 확인해도 되고, 입력되는 문자는 a ~ z, 0 ~ 9이므로 아스키 코드로 읽어도 된다. 그래서 128개의 배열을 할당해 해시처럼 써도 된다.<br/>
그래서 처음 등장하는 문자인지 판별하는 것은 O(1)에 판별 가능하다.<br/>
이를 문자열의 길이 len 만큼만 확인한다.<br/>


이후 숫자 배치가 끝나면 진법 연산은 각 자리별로 숫자를 넣고, 이전 값 x 단위를 곱하면서 문자열의 길이 len에 찾을 수 있다.<br/>
문자열 탐색, 숫자 배정, 진법 변환 모두 문자열 길이만큼의 연산으로 구성되므로 전체 시간 복잡도는 O(len + len) = O(len)이다.<br/>


[12638번 All Your Base (Small) 문제](https://www.acmicpc.net/problem/12637) 역시 해당 코드로 해결 가능하다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/12638