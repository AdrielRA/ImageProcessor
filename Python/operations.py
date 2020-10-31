import cv2
import os
import sys

def add(img1, img2):
    return cv2.add(img1, img2)

def sub(img1, img2):
    return cv2.subtract(img1, img2)

def blur(img, intensity):
    return cv2.GaussianBlur(img, (49,49), intensity)

def mul(img, scalar):
    result = img * scalar
    return result.astype('uint8')

path1 = False
path2 = False
op = False
intensity = False

if(len(sys.argv) == 5):
   path1 = sys.argv[1]
   path2 = sys.argv[2]
   op = sys.argv[3]
   intensity = int(sys.argv[4])
else:
   path1 = sys.argv[1]
   op = sys.argv[2]
   intensity = int(sys.argv[3])

if os.path.exists(path1) and op and intensity and (os.path.exists(path2) or (op != "add" and op != "sub")):
    
    imagem1 = cv2.imread(path1)    
    if(path2):      
      height, width, channels = imagem1.shape
      dim = (width, height)
      imagem2 = cv2.imread(path2)
      imagem2 = cv2.resize(imagem2, dim)
    
    imagem = imagem1
    if op == "add":
        imagem = add(imagem1, imagem2)
    elif op == "sub":
        imagem = sub(imagem1, imagem2)
    elif op == "blur":
        imagem = blur(imagem1, intensity)
    elif op == "mul":
        imagem = mul(imagem1, intensity)
    elif op == "preset1":
        imagem = blur(imagem1, intensity)
        imagem = sub(imagem1, imagem)
        imagem = mul(imagem, intensity / 10)
    elif op == "preset2":
        imagem = blur(imagem1, intensity)
        imagem = sub(imagem1, imagem)
        imagem = add(imagem1, imagem)
    
    tempPath = "temp"
    if not os.path.exists(tempPath):
        os.mkdir(tempPath)
    cv2.imwrite(tempPath + "/" + op + ".png", imagem)
