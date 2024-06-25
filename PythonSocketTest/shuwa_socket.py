import socket
import threading



class SocketServer:
    def __init__(self, ip='localhost', port=8808):
        self.server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.server_socket.bind((ip, port))
        self.server_socket.listen(1)
        print(f"Server listening on {ip}:{port}")
    
    def wait_for_connection(self):
        while True:
            client_socket, client_address = self.server_socket.accept()
            print(f"Connection from {client_address}")
            threading.Thread(target=self.handle_client, args=(client_socket,)).start()

    def handle_client(self, client_socket):
        try:
            while True:
                data = client_socket.recv(1024).decode('utf-8')
                if not data:
                    break
                if data == "exit":
                    break
                print(f"Received data: {data}")

                response = f"Hello from shuwaSocket: Received data: {data}"


                client_socket.send(response.encode('utf-8'))
                client_socket.send("Done".encode('utf-8'))

        except Exception as e:
            print(f"Error: {e}")
        finally:
            client_socket.close()




class SocketClient:
    def __init__(self, ip='localhost', port=8808):
        self.client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.client_socket.connect((ip, port))
        print(f"Connected to server at {ip}:{port}")

    def send_message(self, message):
        self.client_socket.send(message.encode('utf-8'))
        response = self.client_socket.recv(1024).decode('utf-8')
        print(f"Received response: {response}")

    def close(self):
        self.client_socket.close()
        print("Connection closed")



