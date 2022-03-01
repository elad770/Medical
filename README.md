# Medical
MedicalManager An application for managing medical examinations.

Before running the program, it is important to emphasize that this is just a sample program that diagnoses any
diseases (and non-profit of course) that the person diagnosed should answer and then get a 
report on what type of disease should be referred to refer him to appropriate 
treatment or recommendations to help minimize the disease. The manner in which the program 
calculates or assigns any disease to the diagnosed is determined only arbitrarily and is mainly based in such a way that diseases that have a common connection with other diseases validate any disease in the diagnosed. Inside the dubug folder you will find, among other things, text files that contain all possible types of diseases (in fact, the list of diseases is written in Hebrew, unlike the other things in the program).

After running the program from the MainWindow window (if you want to run the program using debugger) or by running with the existing exe file in the dubug folder, you will be asked to enter a username, password and ID number, 
because this is a sample program please enter the debug folder name A user text file appears, copy the texts one by one according to the boxes and click the
login button. Now in the main window select Test New for a test, in the next window you will fill in the required details and additional questions, 
after you have filled most of the text fields with standard values (for example in the text fields you will be asked to enter only numbers and not words otherwise you will be notified) 
An indication of the types of diseases in which you are diagnosed according to the values you entered in the boxes. 
In the window of the diagnosis itself you can save the patient's information in an xml file that will be saved in the dubug folder. 
*** 
A little about the code itself. The app itself is built with WPF C# technology. The implementation in the program of most of the controls in the program works according to the principle of databiding and resources. In addition we added to the reference project taken from the git from the boostrap design project in wpf (in practice the use of this reference was actually only in one particular thing and this is so that we could in principle give it up). Unfortunately in this version we did not work according to the MVVM design template, so in the code behind all the windows we had to add functions and logic.
