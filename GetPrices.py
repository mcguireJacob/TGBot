try:
    import sys
    import fxcmpy
    import datetime as dt
    import time
    import sys
    import pypyodbc as odbc
    import logging
    logging.basicConfig(filename='C:\\Users\\Jacob\\Documents\\repo\\TGBot\\LogForGetPrices.txt', filemode='w', format='%(name)s - %(levelname)s - %(message)s')
    logging.warning('imports work')
except Exception as e:
    logging.basicConfig(filename='C:\\Users\\Jacob\\Documents\\repo\\TGBot\\LogForGetPrices.txt', filemode='w', format='%(name)s - %(levelname)s - %(message)s')
    logging.warning(e)

logging.basicConfig(filename='C:\\Users\\Jacob\\Documents\\repo\\TGBot\\LogForGetPrices.txt', filemode='w', format='%(name)s - %(levelname)s - %(message)s')
logging.warning('imports work')
token = '397c1c5608318167aad85f88f6c96cfa2c368e8a'
try:
    con = fxcmpy.fxcmpy(access_token = token, log_level='error', log_file=None)
except Exception as e:
    logging.warning("sauce")


    
global pair
global tID
pair = sys.argv[1]
tID = sys.argv[2]
#pair = "EUR/USD"
#tID = 0
logging.warning(pair)
logging.warning(tID)










DRIVER = 'SQL SERVER'
SERVER_NAME = 'localhost\SQLEXPRESS'
DATABASE_NAME = 'Trades'


conn_string = f"""
    Driver={{{DRIVER}}};
    Server={SERVER_NAME};
    Database={DATABASE_NAME};
    Trust_Connection=yes;
    MultipleActiveResultSets=true;
    Uid=sa;
    Pwd=sa;
"""

try:
    conn = odbc.connect(conn_string)
except Exception as e:
    print (e)
    print("failed")
    logging.warning("sauce")
    sys.exit()
else:
    cursor = conn.cursor()


update_statement = """
    UPDATE TradeInfo
    SET tLimitTwo = ?
    where tid = ?
"""

updatePriceOfSelected_statement = """
    UPDATE PriceOfSelected
    SET pPriceOfSelected = ?
    
"""




def on_tick(data,df):
   
    print(data['Rates'][0])
    price = data['Rates'][0]
    logging.warning(price)
    
    try:
        if tID == "0":
            logging.warning("Im in HEre")
            cursor.execute(updatePriceOfSelected_statement, [price])
        else:
            logging.warning("Im in HEre 2222")
            cursor.execute(update_statement, [price,tID])
    except Exception as e:
        cursor.rollback()
        print(e.value)
        print("trans rollback")
        logging.warning("RollBack")
    else:
        logging.warning("Sucess")
        print("success")
        cursor.commit()
  

con.subscribe_market_data(pair,(on_tick,))


