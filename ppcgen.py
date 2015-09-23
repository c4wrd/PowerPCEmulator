__author__ = 'cory'
import random

# { rand: { opCodeStr, numReg, numShort } }
args = {
    0: [ "li r%d, %d", 1, 1],
    1: [ "add r%d, r%d, r%d", 3, 0],
    2: [ "addi r%d, r%d, %d", 2, 1],
    3: [ "sub r%d, r%d, r%d", 3, 0],
    4: [ "subi r%d, r%d, %d", 2, 1]
}

NUM_INSTRUCTS = 100000

def main():
    for x in range(NUM_INSTRUCTS):
        ix = random.randrange(0, 5)
        code = args[ix]
        opStr = code[0]
        argArr = []
        regCount = code[1]
        numCount = code[2]
        for x in range(regCount):
            argArr.append(random.randrange(0,32))
        for x in range(numCount):
            argArr.append(random.randrange(0,65536))
        print opStr % tuple(argArr)


if __name__ == "__main__":
    main()