import threading
import time
import sys

data = ''

def loop1_10():
    for i in range(1, 11):
        time.sleep(1)
        threadLock.acquire()
        print('thread 1 ')
        print(i)
        threadLock.release()

def loop2_20():
    for i in range(10, 21):
        time.sleep(1)
        threadLock.acquire()
        print('thread 2 ')
        print(i)
        threadLock.release()

def loop3():
    while True:
        time.sleep(1)
        print("high")

thread1 = threading.Thread(target=loop3)
thread1.daemon = True
#thread1.daemon.start()

#threading.Thread(target=loop4).start()
#threading.Thread(target=loop1_10).start()
#threading.Thread(target=loop2_20).start()

def background():
    global data
    while True:
        time.sleep(1)
        data = data + 'meme'
        print(data)
        #print "yoooo"
# now threading1 runs regardless of user input
threading1 = threading.Thread(target=background)
threading1.start()

while True:
    data = raw_input()
    if data == 'hi':
        print("yeet")
    else:
        print("yoot")
