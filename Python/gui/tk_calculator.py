from tkinter import *

window = Tk()
window.title("ElQwerto Caculator")
winWidth = '350'
winHeight = '600'
window.geometry(winWidth + 'x' + winHeight)

sv = StringVar()

def callback():
    print('yeet')

resultTextInput = Entry(window, width=winWidth, font=10, textvariable=sv, validate="focusout", validatecommand=callback)
resultTextInput.grid(row=0, column=0)

buttonFrame = Frame(window)
buttonFrame.grid(row=1, column=0, columnspan=2, sticky='w')

reset = False

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
            if len(digits) != len(operators): #should have the same amount of number (groups) to operators
                resultTextInput.delete(0,END)
            else:
                # caculate from string
                for opr in operators:
                    if opr == '+':
                        digits[0] = int(digits[0]) + int(digits[1])
                        digits.remove(digits[1])
                    if opr == '-':
                        digits[0] = int(digits[0]) - int(digits[1])
                        digits.remove(digits[1])
                    if opr == '/':
                        digits[0] = int(digits[0]) / int(digits[1])
                        digits.remove(digits[1])
                    if opr == '=':
                        resultTextInput.insert(len(resultTextInput.get()), digits[0])
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
