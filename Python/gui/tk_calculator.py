from tkinter import *

window = Tk()
window.title("ElQwerto Caculator")
winWidth = '350'
winHeight = '600'
window.geometry(winWidth + 'x' + winHeight)

sv = StringVar()
def callback(*args):
    result = sv.get()
    calculate()
sv.trace("w", callback)

resultTextInput = Entry(window, width=winWidth, font=10, textvariable=sv)
resultTextInput.grid(row=0, column=0)

buttonFrame = Frame(window)
buttonFrame.grid(row=1, column=0, columnspan=2, sticky='w')

reset = False


def calculate():
    #if keyboard deletes the last number on left side of '=' delete all
    if resultTextInput.get().find('=') == 0:
        resultTextInput.delete(0,END)
        return
    digits = ['']
    operators = []
    digIndex = 0
    optIndex = 0
    result = 0
    for char in resultTextInput.get():
        if char.isdigit():
            digits[digIndex] = digits[digIndex] + char
        if char == '=':
            operators.append(char)
            break
        if not char.isdigit():
            digits.append('')
            digIndex += 1
            operators.append(char)
    digits = [dig for dig in digits if dig.isdigit()]

    # caculate from string
    for opr in operators:
        try:
            if opr == '+':
                digits[0] = int(digits[0]) + int(digits[1])
                digits.remove(digits[1])
            if opr == '-':
                digits[0] = int(digits[0]) - int(digits[1])
                digits.remove(digits[1])
            if opr == '/':
                digits[0] = int(digits[0]) / int(digits[1])
                digits.remove(digits[1])
        except:
            pass
        if opr == '=':
            #this will update the entry to corect the output
            rti = resultTextInput.get()
            if rti[len(rti) - 1].isdigit():
                resultTextInput.delete(rti.find('=') + 1, END)
            resultTextInput.insert(len(resultTextInput.get()), digits[0])


class numButtons:
    def __init__(self,numName='',x=0,y=0):
        btnSize = 10
        self.text = numName
        btnOne = Button(
                buttonFrame,
                text=numName,
                width=btnSize,
                height=(int(btnSize/2)),
                command=self.addInput
        ).grid(row=x, column=y, sticky='w')

    def addInput(self):
        # make it intuitive to input another equation
        global reset
        if reset:
            resultTextInput.delete(0,END)
            reset = False
        resultTextInput.insert(len(resultTextInput.get()), self.text)
        if self.text == '=':
            reset = True

row = 1
col = 1
num = 1
while num != 10:
    numButtons(str(num),row,col)
    if col % 3 == 0:
        row += 1#move to new line
        col = 0
    col += 1
    num += 1
#any button will automatically set its self with its function
#be better two have two class were one inherents from a parent
numButtons('+',1,4)
numButtons('-',2,4)
numButtons('/',3,4)
numButtons('0',row+1,2)
numButtons('=',row+1,4)

window.mainloop()
