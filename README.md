
## Google Forms to Google Sheets and Unity  

This Unity package uses Google Drive API to organize different channels for multiple Android Stores in China. It takes inputs from Google Forms and organizes and stores it on Google Sheets. Running the Unity package calls a Python script to pull information from the Google Sheet and stores it in different sheets, depending on the game. It can either pass the JSON data to Unity directly through Python or write and read from a file first.  

### Installing and Set Up  

A step by step series of examples that tell you how to get the program running.  

1. Download and install Python 3.x off the [official download page](https://www.python.org/downloads/)  
2. Download and install Unity off the [official download page](https://unity3d.com/get-unity/download)  
3. Create a new Unity project and import the Unity package titled "google_sheets_organizer"  
4. Follow the [Google Sheets Python Quickstart Guide](https://developers.google.com/sheets/api/quickstart/python), downloading and replacing the "client_secret.json" installed in the package  
5. Set up the Google Forms, Sheets, and Python and C Sharp Scripts [detailed here](https://docs.google.com/document/d/1AzSFeWSRhcZL6K4h66s7HLnCPJ3WkO2LLpYK6VfCO6I/edit?usp=sharing)  

### Usage

To submit information about a game, open the correct Google Form corresponding to the channel. Users who have editing permissions can submit responses with the “preview” button shaped like an eye. Fill out the information and submit. Google Forms will automatically fill out the information in the master spreadsheet. Alternatively, you can open the master spreadsheet and manually input information to the correct sheet.  

To change and remove information, open the master spreadsheet. Edit cells by clicking the cell and manually change its data. To remove entire rows, right click the on the row number and press delete row. Editing cells and removing rows in the master spreadsheet will change the information in the other Google Sheets when you run the program. Changes you make that are not in the master spreadsheet _are not_ permanent. It will be overwritten by the information in the master spreadsheet.  

### Adding Information

To add more games you need to add that game as an option to all of the google forms, add a game spreadsheet to the game folder, then add sheets to match the channel names  

To add more channels you need to add a google form and link it to the master sheet, rename its spreadsheet name in the master spreadsheet, then add a sheet to each bottom of each game spreadsheet

**Note**: Channel names must be consistent in title of the Google Form, bottom of the master spreadsheet, and in bottom of each of the game spreadsheets. Game names must be consistent in the title of the Google Spreadsheet in the game folder and in each of the Google Forms.
