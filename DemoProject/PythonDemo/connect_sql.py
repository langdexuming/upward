#!/usr/bin/python3

# connect to mysql

from common.mysql_client import *
from pymssql import *

def connect_mysql():
    print("no implement! ")	
	
def connect_sqlserver():
    return pymssql.connect()

def connect_sqlite():
    print("no implement! ")	

    
# main
conn=connect_sqlserver()


