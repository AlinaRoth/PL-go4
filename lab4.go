package main
import (
	"fmt"
    	"strconv"
)

type Token struct {
	data string
	recipient int
}

func thread(id int, token Token, c chan string) {
	if (token.recipient == id) {
		response := token.data + " (" + strconv.Itoa(id) + ")"
		c <- response
	} else {
		response := "Passed further"
		c <- response
		go thread(id+1, token, c)
	}
}

func main() {
	channel := make(chan string);
	recipient := 24;
 	go thread(1, Token{data: "I only show for chosen one", recipient: recipient}, channel)	
	for i := 0; i < recipient-1; i++ {
		fmt.Println(<-channel)
	}
	fmt.Println(<-channel)
}