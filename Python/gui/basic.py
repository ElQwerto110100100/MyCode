from tkinter import *

window = Tk()

window.title("Welcome to LikeGeeks app")
window.geometry('350x200')

lbl = Label(window, text="Yeet", font = ("Arial Bold", 50))
lbl.grid(column = 0, row = 0)

txt = Entry(window, width=10)
txt.grid(column=0,row=3)
txt.focus()

#if i need to ref a global var
counter = 1
def clicked():
    global counter
    if counter:
        lbl.configure(text="YOATED"+txt.get())
        counter = 0
        txt.configure(state='disabled')
    else:
        lbl.configure(text="YEET")
        counter = 1

btn = Button(
        window,
        text="Clicky boi",
        bg="orange",
        fg="red",
        command=clicked)
btn.grid(column = 1, row = 1)

window.mainloop()
