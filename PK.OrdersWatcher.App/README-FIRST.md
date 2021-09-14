# Orders - monitor CSV file and loads to XML file
# By Punprom Kasemsant

CSV File format
===============
CSV file MUST be formatted orders_dd-MM-yyy where dd - 2 digits date, MM - 2 digits month, yyyy - full 4 digits year.
Sample of data is in Sample folder in the main App project.

Configuration (appsettings.json)
================================
WatchPath - directory path which data file is dropped in. Note only file with same name as format described above will gets activated. Note, application will only pick up file that has current date on.
CsvSeperator - CSV delimiter sign
XmlOutputPath - destination of XML file output

Instruction
============
Setup monitor folder and XML output folders on config file, make sure that it is valid.

Run application, drop CSV data file onto monitor folder path, makes sure that dd-MM-yyyy has current date on and makes sure that no previous same date exist, otherwise it will ignored.

Application will waits till you drop a new current date CSV file onto monitor folder. When it is done, the application will start to process CSV file and convert to XML file format.


