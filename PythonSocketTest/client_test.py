from shuwa_socket import SocketClient


client = SocketClient()

while True: 
    message = input("Enter message: ")
  
    client.send_message(message)
    if message == 'exit':
        break