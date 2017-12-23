#!/usr/bin/python
# -*- coding: utf-8 -*-


import math,sys,json

data = {'num': 1002, 'name': 'xiao zhi'}
json_str = json.dumps(data)
print(json_str)

print(json.loads(json_str)['num'])

# buff=[1]
# print(b''.join(buff))

def hello(str):
    print("hello %s!\n" % str)

hello("tiky")
print("Hello World!\n")



print(math.pi)
print(sys.path)

if 1==1:
    print('123')

a = 5
b = 10
print(a * b)

str = "[123]"

print(str[0])

print([123] * 5)

print(list('hello'))

print(('a', 'b'))

print(tuple("abc"))

print(tuple("def"))
