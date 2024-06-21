import socket




ip = 'localhost'
port = 8808


def start_server():
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((ip, port))
    server_socket.listen(1)
    print("Server started, waiting for connection...")
    
    try:
        client_socket, addr = server_socket.accept()
        print(f"Connection from {addr}")
        
        while True:
            inputText = input("Enter Object Name: ")
            print("Entered text: ", inputText)
            client_socket.sendall(inputText.encode('utf-8'))
            print("Sent text") 
            
            if inputText == "exit":
                break

            # クライアントが切断されたかどうかをチェックするために少し待機
            try:
                client_socket.sendall(b'')  # 空のバイト列を送信して接続状態を確認
            except:
                print("Client disconnected")
                break

    except Exception as e:
        print(f"Error: {e}")
    finally:
        client_socket.close()
        server_socket.close()
        print("Server closed")

if __name__ == "__main__":
    start_server()
