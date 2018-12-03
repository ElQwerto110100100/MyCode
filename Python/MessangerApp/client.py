import socket
from _thread import *
import threading
from tkinter import *

s = socket.socket(socket.AF_INET,socket.SOCK_STREAM)

class gui:
    def __init__(self, root):
        self.root = root
        root.title("Client")
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
        s.send(msg.encode('ascii'))

    def recv_msg(self, msg):
        self.text.insert('end', "Server: " + msg + "\n")

def recv(s):
    while True:
        data = s.recv(1024).decode('ascii')
        main_gui.recv_msg(data)

def Main():
    try:
        print 'Client'
        host = '' #server ip addres
        port = 12345
        s.connect((host,port))
        start_new_thread(recv, (s,))
    except Exception,e:
        print "except 1" + str(e)

if __name__ == '__main__':
    Main()
    try:
        root = Tk()
        main_gui = gui(root)
        root.mainloop()
    except Exception,e:
        print "except 2" + str(e)
