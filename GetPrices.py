import sys
import fxcmpy
import datetime as dt
import time
import sys
import pypyodbc as odbc
token = '397c1c5608318167aad85f88f6c96cfa2c368e8a'
con = fxcmpy.fxcmpy(access_token = token, log_level='error', log_file=None)

global pair
pair = sys.argv[1]
global tID
tID = sys.argv[2]







DRIVER = 'SQL SERVER'
SERVER_NAME = 'localhost\SQLEXPRESS'
DATABASE_NAME = 'Trades'


conn_string = f"""
    Driver={{{DRIVER}}};
    Server={SERVER_NAME};
    Database={DATABASE_NAME};
    Trust_Connection=yes;
    Uid=sa;
    Pwd=sa;
"""

try:
    conn = odbc.connect(conn_string)
except Exception as e:
    print (e)
    print("failed")
    sys.exit()
else:
    cursor = conn.cursor()


update_statement = """
    UPDATE TradeInfo
    SET tLimitTwo = ?
    where tid = ?
"""




def on_tick(data,df):
    
    print(data['Rates'][0])
    price = data['Rates'][0]
    
    try:
        cursor.execute(update_statement, [price,tID])
    except Exception as e:
        cursor.rollback()
        print(e.value)
        print("trans rollback")
    else:
        print("success")
        cursor.commit()
  

con.subscribe_market_data(pair,(on_tick,))


