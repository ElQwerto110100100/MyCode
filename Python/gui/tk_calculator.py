from tkinter import *

window = Tk()
window.title("ElQwerto Caculator")
winWidth = '350'
winHeight = '600'
window.geometry(winWidth + 'x' + winHeight)

resultTextInput = Entry(window, width=winWidth,font=10)
resultTextInput.grid(row=0, column=0)

buttonFrame = Frame(window)
buttonFrame.grid(row=1, column=0, columnspan=2, sticky='w')

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
            if len(digits) != len(operators):
                resultTextInput.delete(0,END)
'''
            print(resultTextInput.get())
            for index,char in enumerate(resultTextInput.get()):
                try:
                    if char.isdigit():
                        cnt = index
                        while resultTextInput.get()[cnt].isdigit():
                            numGroup += char
                            cnt+=1
                        print(numGroup, index, cnt)
                        digits.append(int(numGroup))
                        numGroup = ''
                except:
                    continue
            print(digits)
'''
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
numButtons('+',1,4)
numButtons('0',row+1,2)
numButtons('=',row+1,4)

window.mainloop()
