using System;
using System.Collections.Generic;

public class Stop
{
    public string arrival { get; set; }
    public string departure { get; set; }
    public string name { get; set; }
    public string stopid { get; set; }
    public int x { get; set; }
    public int y { get; set; }
}

public class Exit
{
    public string arrival { get; set; }
    public string stopid { get; set; }
    public object x { get; set; }
    public object y { get; set; }
    public string name { get; set; }
    public string sbb_name { get; set; }
    public string track { get; set; }
    public bool isaddress { get; set; }
}

public class Attributes
{
    public string type { get; set; }
}

public class Leg
{
    public string departure { get; set; }
    public string tripid { get; set; }
    public string number { get; set; }
    public string stopid { get; set; }
    public object x { get; set; }
    public object y { get; set; }
    public string name { get; set; }
    public string sbb_name { get; set; }
    public string type { get; set; }
    public string line { get; set; }
    public string terminal { get; set; }
    public string fgcolor { get; set; }
    public string bgcolor { get; set; }
    public string G { get; set; }
    public string L { get; set; }
    public string carrier { get; set; }
    public List<Stop> stops { get; set; }
    public int runningtime { get; set; }
    public Exit exit { get; set; }
    public Attributes attributes { get; set; }
    public string arrival { get; set; }
    public int normal_time { get; set; }
    public bool isaddress { get; set; }
}

public class Connection
{
    public string from { get; set; }
    public string departure { get; set; }
    public string to { get; set; }
    public string arrival { get; set; }
    public int duration { get; set; }
    public List<Leg> legs { get; set; }
}

public class Station
{
    public string text { get; set; }
    public string url { get; set; }
    public string id { get; set; }
    public string x { get; set; }
    public string y { get; set; }
}

public class TransportRootObject
{
    public int count { get; set; }
    public int rawtime { get; set; }
    public int maxtime { get; set; }
    public List<Connection> connections { get; set; }
    public string url { get; set; }
    public List<Station> points { get; set; }
    public string description { get; set; }
    public string request { get; set; }
    public int eof { get; set; }
}

public class ScheduleObject
{
    public string Number { get; set; }
    public DateTime Time { get; set; }
}