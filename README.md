For declare the control:

var Data = new KeyValuePair<int, string>();
var list = new List<Data>();
list.Add(new Data(30, "Minimal"));
list.Add(new Data(60, "Normal"));
list.Add(new Data(90, "Maximal"));
McTrackbar TrackBarTest = new McTrackbar(new Size(400,60),30,90,60,list);
TrackBarTest.Font = Zip.Lib.Font(Zip.Storage.Content(0, "Minecraftia.ttf"), 12f, FontStyle.Bold);
TrackBarTest.Location = new Point(20, 100);
TrackBarTest.ValueToShow = 0;
this.Controls.Add(TrackBarTest);

//----------------------------------------------------------------------------------//
Replace TrackBarTest.Font by what you wan't like new Font("Arial",12f,FontStyle.Bold)
TrackBarTest.ValueToShow: 0 = Normal View of values to see with the KeyValuePair
                          1 = View the Percentage
//----------------------------------------------------------------------------------//
