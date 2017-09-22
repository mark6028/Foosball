from tkinter import *
from tkinter import ttk
from tkinter import messagebox
import time
import nxppy
#import RPi.GPIO as GPIO
#import _thread


#mifare = nxppy.Mifare()

def flashbutton(buttonnr,period):
    while True:
        print(buttonnr + "Aan")
        time.sleep(period/2)
        print(buttonnr + "Uit")
        time.sleep(period/2)

def defencegrey(*args):
    #try:
        win = Toplevel()
        win.title("scan your tag")

        l = ttk.Label(win, text="Scan your tag", font='helvetica 40')
        l.grid(row=0, column=0)

        b = ttk.Button(win, text="cancel", command=win.destroy)
        b.grid(row=1, column=0)
        #thread.start_new_thread(flashbutton,("GreyDefence",1))
    #except:
     #   pass

def attackgrey(*args):
    try:
        win = Toplevel()
        win.title("scan your tag")
        
        l = ttk.Label(win, text="Scan your tag", font='helvetica 40')
        l.grid(row=0, column=0)

        b = ttk.Button(win, text="cancel", command=win.destroy)
        b.grid(row=1, column=0)
    except:
        pass
        
def defenceblack(*args):
    try:
        win = Toplevel()
        win.title("scan your tag")
        
        l = ttk.Label(win, text="Scan your tag", font='helvetica 40')
        l.grid(row=0, column=0)

        b = ttk.Button(win, text="cancel", command=win.destroy)
        b.grid(row=1, column=0)
    except:
        pass
        
def attackblack(*args):
    try:
        win = Toplevel()
        win.title("scan your tag")
        
        l = ttk.Label(win, text="Scan your tag", font='helvetica 40')
        l.grid(row=0, column=0)

        b = ttk.Button(win, text="cancel", command=win.destroy)
        b.grid(row=1, column=0)
    except:
        pass


root = Tk()
#root.attributes("-fullscreen", True)
root.title("Altran Table Football")
greydefencephoto = PhotoImage(file="../Images/GreyDefence.png")
greyattackphoto = PhotoImage(file="../Images/GreyAttack.png")
blackdefencephoto = PhotoImage(file="../Images/BlackDefence.png")
blackattackphoto = PhotoImage(file="../Images/BlackAttack.png")
tablephoto = PhotoImage(file="../Images/table.png")


content = ttk.Frame(root, padding=(3,3,12,12), width=800, height=480)
mainframe = ttk.Frame(content, borderwidth=0, relief="sunken", width=800, height=480)
mainframe.grid_propagate(False)
frame1 = ttk.Frame(mainframe, borderwidth=0, relief="sunken", width=200, height=240)
frame1.grid_propagate(False)
frame2 = ttk.Frame(mainframe, borderwidth=0, relief="sunken", width=200, height=240)
frame2.grid_propagate(False)
frame3 = ttk.Frame(mainframe, borderwidth=0, relief="sunken", width=200, height=240)
frame3.grid_propagate(False)
frame4 = ttk.Frame(mainframe, borderwidth=0, relief="sunken", width=200, height=240)
frame4.grid_propagate(False)
imageframe= ttk.Frame(mainframe, borderwidth=0, relief="sunken", width=400, height=480)
imageframe.grid_propagate(False)

defencegreybutton = ttk.Button(frame1, image = greydefencephoto, command=defencegrey).grid(sticky=(N, W, S, E))
attackgreybutton = ttk.Button(frame2, image = greyattackphoto, command=attackgrey).grid(sticky=(N, W, S, E))
defenceblackbutton = ttk.Button(frame3, image = blackdefencephoto, command=defenceblack).grid(sticky=(N, W, S, E))
attackblackbutton = ttk.Button(frame4, image = blackattackphoto, command=attackblack).grid(sticky=(N, W, S, E))
label = ttk.Label(imageframe, image = tablephoto).grid(sticky=(N, W, S, E))

content.grid(column=0, row=0, sticky=(N, S, E, W))
mainframe.grid(column=0, row=0, columnspan=1, rowspan=2, sticky=(N, W, S, E))
frame1.grid(column=3, row=1, columnspan=1, rowspan=1)
frame2.grid(column=3, row=0, columnspan=1, rowspan=1)
frame3.grid(column=0, row=0, columnspan=1, rowspan=1)
frame4.grid(column=0, row=1, columnspan=1, rowspan=1)
imageframe.grid(column=1,row = 0, columnspan=2, rowspan=2)


root.columnconfigure(0, weight=1)
root.rowconfigure(0, weight=1)
content.columnconfigure(0, weight=3)
content.columnconfigure(1, weight=3)
content.columnconfigure(2, weight=3)
content.columnconfigure(3, weight=1)
content.columnconfigure(4, weight=1)
content.rowconfigure(1, weight=1)

root.mainloop()
