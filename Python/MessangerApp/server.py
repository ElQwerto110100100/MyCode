import socket
from _thread import *
import threading
from tkinter import *

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
list_of_clients = []

class gui:
    def __init__(self, root):
        self.root = root
        root.title("Server")
        self.text = Text(root)
        self.text.pack()

        self.entry = Entry(root)
        self.entry.pack()

        self.button = Button(root, text="Submit", command=self.send)
        self.button.pack()

    def send(self):
        msg = self.entry.get()
        self.entry.delete(0,'end')
        self.text.insert('end', "You: " + msg + "\n")
        for clients in list_of_clients:
            clients.send(msg.encode('ascii'))

    def recv_msg(self, msg):
        self.text.insert('end', "Client: " + msg + "\n")

def recv(s):
    while True:
        data = s.recv(1024).decode('ascii')
        main_gui.recv_msg(data)

def Main():

    print 'Server'
    host = ''
    port = 12345
    s.bind((host, port))
    print("socket binded to post", port)
    s.listen(1)
    print("socket is listening")

    c, addr = s.accept()
    list_of_clients.append(c)
    print('Connected to :', addr[0], ':', addr[1])
    start_new_thread(recv, (c,))

if __name__ == '__main__':
    Main()
    try:
        root = Tk()
        main_gui = gui(root)
        root.mainloop()
    except Exception,e:
        print "except 2" + str(e)
