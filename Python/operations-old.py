import cv2
import os
import sys

path1 = sys.argv[1]
path2 = sys.argv[2]
op = sys.argv[3]

if os.path.exists(path1) and os.path.exists(path2) and op:
    
    imagem1 = cv2.imread(path1)
    imagem2 = cv2.imread(path2)
    height, width, channels = imagem1.shape
    dim = (width, height)
    imagem1 = cv2.resize(imagem1, dim)
    imagem2 = cv2.resize(imagem2, dim)
    
    imagem = imagem1
    if op == "add":
        imagem = cv2.add(imagem1, imagem2)
    elif op == "sub":
        imagem = cv2.subtract(imagem1, imagem2)
    
    tempPath = "temp"
    if not os.path.exists(tempPath):
        os.mkdir(tempPath)
    cv2.imwrite(tempPath + "/" + op + ".png", imagem)

