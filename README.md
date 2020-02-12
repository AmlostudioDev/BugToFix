For declare the control:

using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;



var Data = new KeyValuePair<int, string>();<br/>
var list = new List<Data>();<br/>
list.Add(new Data(30, "Minimal"));<br/>
list.Add(new Data(60, "Normal"));<br/>
list.Add(new Data(90, "Maximal"));<br/>
McTrackbar TrackBarTest = new McTrackbar(new Size(400,60),30,90,60,list);<br/>
TrackBarTest.Font = Zip.Lib.Font(Zip.Storage.Content(0, "Minecraftia.ttf"), 12f, FontStyle.Bold);<br/>
TrackBarTest.Location = new Point(20, 100);<br/>
TrackBarTest.ValueToShow = 0;<br/>
this.Controls.Add(TrackBarTest);<br/>

//----------------------------------------------------------------------------------//<br/>
Replace TrackBarTest.Font by what you wan't like new Font("Arial",12f,FontStyle.Bold)<br/>
TrackBarTest.ValueToShow: 0 = Normal View of values to see with the KeyValuePair<br/>
                          1 = View the Percentage<br/>
//----------------------------------------------------------------------------------//<br/>

Add the McTrackbar.cs in your project and change the namespace 
