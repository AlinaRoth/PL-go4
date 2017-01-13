package main

import (
	"fmt"
	"strconv"
)

type Token struct {
	data      string
	recipient int
}

type CustomThread struct {
	number int
	next   *CustomThread
}

func thread(th *CustomThread, token Token, c chan string) {
	fmt.Println(th.number)
	if token.recipient == th.number {
		response := token.data + " (" + strconv.Itoa(th.number) + ")"
		c <- response
		close (c)
	} else {
		response := "Passed further"
		c <- response
		go thread(th.next, token, c)
	}
}

func main() {
	// Создаем наши потоки
	var threads [51]CustomThread
	
	threads[50] = CustomThread{number: 50}
	for i := 49; i >= 1; i-- {
		threads[i] = CustomThread{number: i-1, next: &threads[i+1]}
	}
	
	fmt.Println(threads)
	channel := make(chan string)
	go thread(&threads[1], Token{data: "I only show for chosen one", recipient: 23}, channel)
	for i := 0; i < 50; i++ {
		fmt.Println(<-channel)
	}
	fmt.Println(<-channel)
}