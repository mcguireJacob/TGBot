from pickle import NONE
from turtle import position
from unicodedata import decimal
import MetaTrader5 as mt
import pandas as pd
from datetime import datetime
import pypyodbc as odbc
import json
import sys
from decimal import Decimal
import logging


logging.basicConfig(filename='LogForAutoTrader.txt', filemode='w', format='%(name)s - %(levelname)s - %(message)s')
logging.warning('It Does work')
global manuallyClosedTID
if(len(sys.argv) > 1):
    manuallyClosedTID = sys.argv[1]
    logging.warning(manuallyClosedTID)
    
else:
    manuallyClosedTID = 0

#opens or checks if client is open
didItitalize = mt.initialize()
logging.warning(didItitalize)




#connect to database
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

getAccounts_statement = """
    select *
    from Account
"""
#connect To Database
try:
    conn = odbc.connect(conn_string)
except Exception as e:
    print (e)
    print("failed")
else:
    cursor = conn.cursor()




#grab accounts from database and throw them in a list
try:
    returnData = cursor.execute(getAccounts_statement)
    rows = cursor.fetchall()
    print(rows)
except Exception as e:
    cursor.rollback()
    print(e.value)
    print("trans rollback")
else:
    print("success")
    cursor.commit()
#get the last trade placed

getLastTrade_statement = """
    SELECT TOP (1) [tID]
      ,[tTradeType]
      ,[tTradingPair]
      ,[tCurrentPrice]
      ,[tLimitOne]
      ,[tLimitTwo]
      ,[tSL]
      ,[tTp]
      ,[tSlPips]
      ,[tTPPips]
      ,[tHitSl]
      ,[tHitTp]
      ,[tTimePlaced]
      ,[tTradeClosed]
      ,[tManuallyClosedPips]
      ,[tManuallyClosed]
      ,[tTelegramMessageID]
      ,[tRiskRewardRadio]
      ,[tLimitOrderHit]
  FROM [Trades].[dbo].[TradeInfo]
  order by tid desc
"""


try:
    returnData = cursor.execute(getLastTrade_statement)
    trade = cursor.fetchall()
    print(trade[0][2])
except Exception as e:
    cursor.rollback()
    print(e.value)
    print("trans rollback")
else:
    print("success")
    cursor.commit()


getTradePair_statement = """
    SELECT TOP (1000) [lTradePairLookupID]
      ,[lTradePair]
      ,[lApiLink]
  FROM [Trades].[dbo].[lTradePairLookup]
  where lTradePairLookupID = ?
"""

setAccountTrade_statement = """
    INSERT INTO AccountTrades 
VALUES (?,?,?,?, ?)
"""

getAccountTrade_statement = """
     select * 
    from AccountTrades 
    where atTradeInfoID = ?
	and atAccountLogin = ?
"""



try:
    cursor.execute(getTradePair_statement, [trade[0][2]])
    tradePair = cursor.fetchall()
    print(tradePair[0][1])
except Exception as e:
    cursor.rollback()
    print(e.value)
    print("trans rollback")
else:
    print("success")
    cursor.commit()

print(trade[0][6])



tradeType = {1: mt.ORDER_TYPE_BUY,
             2: mt.ORDER_TYPE_SELL,
             3: mt.ORDER_TYPE_BUY_LIMIT,
             4: mt.ORDER_TYPE_SELL_LIMIT}
print("tradeType")
print(tradeType[1])
print(tradeType[2])
print(tradeType[3])
print(tradeType[4])

tradePair = tradePair[0][1]
tradeType = tradeType[trade[0][1]]
SL = float(trade[0][6])

TP = float(trade[0][7])

print(tradePair)













def placeTrade():
    for acc in rows:

        loginBool = mt.login(acc[1], acc[2], acc[3])
        logging.warning(loginBool)
        print("Yup")
        slPips = trade[0][8]
        volume = (mt.account_info().equity * (float(acc[4]) /100 )) / slPips
        volume = float(str(round(volume, 2)))

        
        
        if(tradeType < 2):

            request = {
            "action": mt.TRADE_ACTION_DEAL,
            "symbol": tradePair + ".a",
            "volume": volume,
            "type": tradeType,
            "price": mt.symbol_info_tick(tradePair + ".a").ask,
            "sl": SL,
            "tp": TP,
            "deviation": 20,
            "magic": 234000,
            "comment": "python script open",
            "type_time": mt.ORDER_TIME_GTC,
            "type_filling": mt.ORDER_FILLING_IOC,
            }
        else:
            request = {
            "action": mt.TRADE_ACTION_PENDING,
            "symbol": tradePair + ".a",
            "volume": volume,
            "type": tradeType,
            "price": float(trade[0][4]),
            "sl": SL,
            "tp": TP,
            "deviation": 20,
            "magic": 234000,
            "comment": "python script open",
            "type_time": mt.ORDER_TIME_GTC,
            "type_filling": mt.ORDER_FILLING_IOC,
            }
        
        print(request)
        order = mt.order_send(request)
        print(order)
        try:
            tid = trade[0][0]
            print(tid)
            cursor.execute(setAccountTrade_statement, [order.order, order.volume, mt.account_info().login, trade[0][0], tradeType])
        
            
        except Exception as e:
            cursor.rollback()
            print(e.value)
            print("trans rollback")
        else:
            print("success")
            cursor.commit()


def closeTrade():
    for acc in rows:
        loginBool = mt.login(acc[1], acc[2], acc[3])


        try:
            cursor.execute(getAccountTrade_statement, [manuallyClosedTID, mt.account_info().login])
            accountTrades = cursor.fetchall()
            print(accountTrades)
        except Exception as e:
            cursor.rollback()
            print(e.value)
            print("trans rollback")
        else:
            print("success")
            cursor.commit()

        tradeTypeClose = {0: 1,
                          1: 0,
                          2: 2,
                          3: 3}
        print(tradeTypeClose[accountTrades[0][5]] )
        tradeTypeClose = tradeTypeClose[accountTrades[0][5]] 
        print(tradeTypeClose)
        print(accountTrades[0][5])
        if tradeTypeClose < 2:
            request = {
                "action": mt.TRADE_ACTION_DEAL,
                "symbol": tradePair + ".a",
                "volume": accountTrades[0][2],
                "type": tradeTypeClose,
                "position": accountTrades[0][1],
                "price": mt.symbol_info_tick(tradePair + ".a").ask,
                "sl": 0.0,
                "tp": 0.0,
                "deviation": 20,
                "magic": 234000,
                "comment": "python script open",
                "type_time": mt.ORDER_TIME_GTC,
                "type_filling": mt.ORDER_FILLING_IOC,
            }
            order = mt.order_send(request)
            print(order)
            
        else:
            request = {
                "action": mt.TRADE_ACTION_REMOVE,
                "order": accountTrades[0][1],
                
               
            }
            
            order = mt.order_send(request)
            print(order)
                
            
        






#trade to be initialized


if(manuallyClosedTID != 0):
    closeTrade()
else:
    placeTrade()
