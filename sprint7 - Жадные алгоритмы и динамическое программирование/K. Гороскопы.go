package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func rev(arr []int) []string {
	b := make([]string, len(arr))
	for i := 0; i < len(arr); i++ {
		b[i] = strconv.Itoa(arr[len(arr)-i-1])
	}
	return b
}

func main() {
	r, err := os.Open("input.txt")
	check(err)
	w, err := os.Create("output.txt")
	check(err)
	defer func() { err := w.Close(); check(err) }()

	scanner := bufio.NewScanner(r)
	scanner.Scan()

	var n, m int
	fmt.Sscanf(scanner.Text(), "%d", &n)
	scanner.Scan()
	nstr := strings.Fields(scanner.Text())

	scanner.Scan()
	fmt.Sscanf(scanner.Text(), "%d", &m)
	scanner.Scan()
	mstr := strings.Fields(scanner.Text())
	// fmt.Println(nstr)
	// fmt.Println(mstr)

	dp := make([][]int, n+1)
	for i := 0; i < n+1; i++ {
		dp[i] = make([]int, m+1)
	}

	for i := 1; i <= n; i++ {
		for j := 1; j <= m; j++ {
			nchar := nstr[i-1]
			mchar := mstr[j-1]
			value := 0

			if nchar == mchar {
				if i > 0 && j > 0 {
					value = dp[i-1][j-1] + 1
				}
			} else {
				left := 0
				top := 0
				if j > 0 {
					left = dp[i][j-1]
				}
				if i > 0 {
					top = dp[i-1][j]
				}
				value = max(top, left)
			}

			dp[i][j] = value

		}
	}
	// fmt.Println(dp)

	result := dp[n][m]
	fmt.Fprintln(w, result)
	if result > 0 {
		first, second := make([]int, 0), make([]int, 0)
		i, j := n, m
		for dp[i][j] != 0 {
			nchar := nstr[i-1]
			mchar := mstr[j-1]
			if nchar == mchar {
				first = append(first, i)
				second = append(second, j)
				i--
				j--
			} else {
				if dp[i][j] == dp[i-1][j] {
					i--
				} else {
					j--
				}
			}
		}
		fmt.Fprintln(w, strings.Join(rev(first), " "))
		fmt.Fprintln(w, strings.Join(rev(second), " "))
	}
}
