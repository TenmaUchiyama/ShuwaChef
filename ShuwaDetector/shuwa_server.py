import signal
import socket
import threading
from shuwa_detector import ShuwaDetector
import json
import time

class ShuwaServer:
    def __init__(self, ip='localhost', port=4531):
        

        self.server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.server_socket.bind((ip, port))
        self.server_socket.listen(1)
        print(f"Server listening on {ip}:{port}")
        connection_thread = threading.Thread(target=self.wait_for_connection)
        connection_thread.start()
        self.shuwa_detector = ShuwaDetector()   
        # ShuwaDetector のメインループを別スレッドで実行
        self.shuwa_detector.main_loop()
        

      
    
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
                    return
                if data == "record":
                    response = None
                    # client_socket.send("Recording".encode('utf-8'))
                    response = self.shuwa_detector.on_record()
                    if response:
                        print(response)
                        res_json = json.dumps(response)
                        client_socket.send(f"{res_json}".encode('utf-8'))
                    else:
                        client_socket.send("record".encode('utf-8'))



        except Exception as e:
            print(f"Error: {e}")
        finally:
            client_socket.close()



if __name__ == "__main__":
    server = ShuwaServer()