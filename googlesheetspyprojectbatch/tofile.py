'''
Created on Jun 27, 2018

@author: brian
'''
from __future__ import print_function
import api

if __name__ == "__main__":

    #source spreadsheet id, sheet folder id,
    #manually get and put into editor window, then write new id to these lines
    SHEET_FOLDER_ID = "1gF62I0nzuxq3bh1gasRFh59D8f1832-L"
    SRC_SHEET_ID = "1uG8ejmIkLd0l85sIYI_FphYsSuHwObHLsrzjk1EuTUQ"
    
    AUTO_TITLE = True
    DRIVE_SERVICE = api.setup_drive()
    SHEET_SERVICE = api.setup_sheets()
    
    channels_list = api.get_sheet_titles(SRC_SHEET_ID, SHEET_SERVICE)
    channels_with_quotes = ["\'{0}\'".format(channel) for channel in channels_list]
    batch_values = api.get_values(SRC_SHEET_ID, SHEET_SERVICE, channels_with_quotes)
    range_a1 = ["{0}!A1:Z".format(channel) for channel in channels_list]

    try:
        game_dict = dict()
        file_list = DRIVE_SERVICE.files().list(q="\'{0}\' in parents".format(SHEET_FOLDER_ID)).execute()
        for file in file_list["files"]:
            game_dict[file["name"]] = file["id"]
        if batch_values:
            with open("json_data.txt", "w") as the_file:
                for game in game_dict:
                    data = []
                    counter = 0
                    for sheet in batch_values:
                        entry_cells = []
                        if AUTO_TITLE:
                            entry_cells.append(sheet["values"][0])
                        for entry in sheet["values"]:
                            if entry[1] == game:
                                entry_cells.append(entry)
                        data.append({"range":range_a1[counter], \
                                     "values": entry_cells})
                        counter += 1
                    str_json = "{" + "\"{0}".format(game) + "\": [" \
                                + str(data)[1:-1].replace("\'","\"") + "]}"
                    body = {"valueInputOption": "RAW", "data": data}
                    api.sheet_write(game_dict[game], body, SHEET_SERVICE)
                    the_file.write(str_json + "\n")
                    print(str_json)
    except Exception as err:
        print("Encountered Error:", err)
