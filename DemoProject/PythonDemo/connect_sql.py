#!/usr/bin/python3

# connect to mysql
import mysql.connector

def connect_mysql():
	cnx=mysql.connector.connect(user='root',database='upward_api_test')

    curA=cnx.cursor(buffered=True)

	print("connect ok")

	cnx.close()
	# print("no implement! ")	
	
def connect_sqlserver():
    print("no implement! ")	

def connect_sqlite():
    print("no implement! ")	

    
# main
# conn=connect_sqlserver()
cnx=connect_mysql()


