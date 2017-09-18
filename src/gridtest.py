from tkinter import *
from tkinter import ttk


def defencegrey(*args):
    try:
        print("Defence Grey")
    except:
        pass

def attackgrey(*args):
    try:
        print("Defence Grey")
    except:
        pass
        
def defenceblack(*args):
    try:
        print("Defence Grey")
    except:
        pass
        
def attackblack(*args):
    try:
        print("Defence Grey")
    except:
        pass

root = Tk()
root.title("Altran Table Football")


content = ttk.Frame(root, padding=(3,3,12,12), width=200, he)
frame1 = ttk.Frame(content, borderwidth=5, relief="sunken", width=200, height=240)
frame2 = ttk.Frame(content, borderwidth=5, relief="sunken", width=200, height=240)
frame3 = ttk.Frame(content, borderwidth=5, relief="sunken", width=200, height=240)
frame4 = ttk.Frame(content, borderwidth=5, relief="sunken", width=200, height=240)

defencegreybutton = ttk.Button(frame1, text="Defence Grey", command=defencegrey).grid(column=3, row=1, sticky=(W,N))
attackgreybutton = ttk.Button(frame2, text="Attack Grey", command=defencegrey).grid(column=3, row=3, sticky=(W,S))
defenceblackbutton = ttk.Button(frame3, text="Defence Black", command=defencegrey).grid(column=1, row=3, sticky=(E,S))
attackblackbutton = ttk.Button(frame4, text="Attack Black", command=defencegrey).grid(column=1, row=1, sticky=(E,N))

content.grid(column=0, row=0, sticky=(N, S, E, W))
frame1.grid(column=0, row=0, columnspan=1, rowspan=2, sticky=(N, W))
frame2.grid(column=0, row=0, columnspan=1, rowspan=2, sticky=(S, W))
frame3.grid(column=0, row=0, columnspan=1, rowspan=2, sticky=(S, E))
frame4.grid(column=0, row=0, columnspan=1, rowspan=2, sticky=(N, E))
#namelbl.grid(column=3, row=0, columnspan=2, sticky=(N, W), padx=5)
#name.grid(column=3, row=1, columnspan=2, sticky=(N, E, W), pady=5, padx=5)

root.columnconfigure(0, weight=1)
root.rowconfigure(0, weight=1)
content.columnconfigure(0, weight=3)
content.columnconfigure(1, weight=3)
content.columnconfigure(2, weight=3)
content.columnconfigure(3, weight=1)
content.columnconfigure(4, weight=1)
content.rowconfigure(1, weight=1)

root.mainloop()
