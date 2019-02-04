'''
Created on Jun 27, 2018

@author: brian
'''

from apiclient.discovery import build
from httplib2 import Http
from oauth2client import file, client, tools

# Setup and calls the Sheets API, returning the service
def setup_sheets():
    SCOPES = 'https://www.googleapis.com/auth/spreadsheets \
              https://www.googleapis.com/auth/drive'
    store = file.Storage('credentials.json')
    creds = store.get()
    if not creds or creds.invalid:
        flow = client.flow_from_clientsecrets('client_secret.json', SCOPES)
        creds = tools.run_flow(flow, store)
    service = build('sheets', 'v4', http=creds.authorize(Http()))
    return service

# Setup and calls the Drive API, returning the service
def setup_drive():
    SCOPES = 'https://www.googleapis.com/auth/spreadsheets \
              https://www.googleapis.com/auth/drive'
    store = file.Storage('credentials.json')
    creds = store.get()
    if not creds or creds.invalid:
        flow = client.flow_from_clientsecrets('client_secret.json', SCOPES)
        creds = tools.run_flow(flow, store)
    service = build('drive', 'v3', http=creds.authorize(Http()))
    return service


# Call the Sheets API to get a nested list of values
def get_values(sheet_id, service, range_arg):
    try:
        result = service.spreadsheets().values().batchGet(spreadsheetId=sheet_id, \
                                                      ranges=range_arg).execute()
    except Exception as err:
        print('Encountered Error:', err)
    else:
        values = result.get('valueRanges')
        if not values:
            print('No data found.')
        return values


# Call the Sheets API to get the sheet titles
def get_sheet_titles(sheet_id, service):
    try:
        sheet_titles = []
        result = service.spreadsheets().get(spreadsheetId=sheet_id).execute()
        for sheet in result["sheets"]:
            sheet_titles.append(sheet["properties"]["title"])
    except Exception as err:
        print('Encountered Error:', err)
    else:
        if not sheet_titles:
            print('No titles found.')
        return sheet_titles

# Writing data to new sheet
def sheet_write(sheet_id, body, service):
    service.spreadsheets().values().batchUpdate(spreadsheetId=sheet_id, body=body).execute()


