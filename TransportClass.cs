using System;
using System.Collections.Generic;

public class Coordinate
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class Station
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate coordinate { get; set; }
    public object distance { get; set; }
}

public class Prognosis
{
    public object platform { get; set; }
    public object arrival { get; set; }
    public object departure { get; set; }
    public object capacity1st { get; set; }
    public object capacity2nd { get; set; }
}

public class Coordinate2
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class Location
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate2 coordinate { get; set; }
    public object distance { get; set; }
}

public class From
{
    public Station station { get; set; }
    public object arrival { get; set; }
    public object arrivalTimestamp { get; set; }
    public string departure { get; set; }
    public int departureTimestamp { get; set; }
    public object delay { get; set; }
    public string platform { get; set; }
    public Prognosis prognosis { get; set; }
    public object realtimeAvailability { get; set; }
    public Location location { get; set; }
}

public class Coordinate3
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class Station2
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate3 coordinate { get; set; }
    public object distance { get; set; }
}

public class Prognosis2
{
    public object platform { get; set; }
    public object arrival { get; set; }
    public object departure { get; set; }
    public object capacity1st { get; set; }
    public object capacity2nd { get; set; }
}

public class Coordinate4
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class Location2
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate4 coordinate { get; set; }
    public object distance { get; set; }
}

public class To
{
    public Station2 station { get; set; }
    public string arrival { get; set; }
    public int arrivalTimestamp { get; set; }
    public object departure { get; set; }
    public object departureTimestamp { get; set; }
    public object delay { get; set; }
    public string platform { get; set; }
    public Prognosis2 prognosis { get; set; }
    public string realtimeAvailability { get; set; }
    public Location2 location { get; set; }
}

public class Service
{
    public string regular { get; set; }
    public string irregular { get; set; }
}

public class Coordinate5
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class Station3
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate5 coordinate { get; set; }
    public object distance { get; set; }
}

public class Prognosis3
{
    public object platform { get; set; }
    public object arrival { get; set; }
    public object departure { get; set; }
    public object capacity1st { get; set; }
    public object capacity2nd { get; set; }
}

public class Coordinate6
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class Location3
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate6 coordinate { get; set; }
    public object distance { get; set; }
}

public class PassList
{
    public Station3 station { get; set; }
    public string arrival { get; set; }
    public int arrivalTimestamp { get; set; }
    public string departure { get; set; }
    public int? departureTimestamp { get; set; }
    public object delay { get; set; }
    public string platform { get; set; }
    public Prognosis3 prognosis { get; set; }
    public string realtimeAvailability { get; set; }
    public Location3 location { get; set; }
}

public class Journey
{
    public string name { get; set; }
    public string category { get; set; }
    public object subcategory { get; set; }
    public int categoryCode { get; set; }
    public string number { get; set; }
    public string @operator { get; set; }
    public string to { get; set; }
    public List<PassList> passList { get; set; }
    public object capacity1st { get; set; }
    public object capacity2nd { get; set; }
}

public class Walk
{
    public string duration { get; set; }
}

public class Coordinate7
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class Station4
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate7 coordinate { get; set; }
    public object distance { get; set; }
}

public class Prognosis4
{
    public object platform { get; set; }
    public object arrival { get; set; }
    public object departure { get; set; }
    public object capacity1st { get; set; }
    public object capacity2nd { get; set; }
}

public class Coordinate8
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class Location4
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate8 coordinate { get; set; }
    public object distance { get; set; }
}

public class Departure
{
    public Station4 station { get; set; }
    public object arrival { get; set; }
    public object arrivalTimestamp { get; set; }
    public string departure { get; set; }
    public int departureTimestamp { get; set; }
    public object delay { get; set; }
    public string platform { get; set; }
    public Prognosis4 prognosis { get; set; }
    public string realtimeAvailability { get; set; }
    public Location4 location { get; set; }
}

public class Coordinate9
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class Station5
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate9 coordinate { get; set; }
    public object distance { get; set; }
}

public class Prognosis5
{
    public object platform { get; set; }
    public object arrival { get; set; }
    public object departure { get; set; }
    public object capacity1st { get; set; }
    public object capacity2nd { get; set; }
}

public class Coordinate10
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class Location5
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate10 coordinate { get; set; }
    public object distance { get; set; }
}

public class Arrival
{
    public Station5 station { get; set; }
    public string arrival { get; set; }
    public int arrivalTimestamp { get; set; }
    public object departure { get; set; }
    public object departureTimestamp { get; set; }
    public object delay { get; set; }
    public string platform { get; set; }
    public Prognosis5 prognosis { get; set; }
    public string realtimeAvailability { get; set; }
    public Location5 location { get; set; }
}

public class Section
{
    public Journey journey { get; set; }
    public Walk walk { get; set; }
    public Departure departure { get; set; }
    public Arrival arrival { get; set; }
}

public class Connection
{
    public From from { get; set; }
    public To to { get; set; }
    public string duration { get; set; }
    public int transfers { get; set; }
    public Service service { get; set; }
    public List<string> products { get; set; }
    public object capacity1st { get; set; }
    public object capacity2nd { get; set; }
    public List<Section> sections { get; set; }
}

public class Coordinate11
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class From2
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate11 coordinate { get; set; }
    public object distance { get; set; }
}

public class Coordinate12
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class To2
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate12 coordinate { get; set; }
    public object distance { get; set; }
}

public class Coordinate13
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class From3
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate13 coordinate { get; set; }
    public object distance { get; set; }
}

public class Coordinate14
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

public class To3
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate14 coordinate { get; set; }
    public object distance { get; set; }
}

public class Stations
{
    public List<From3> from { get; set; }
    public List<To3> to { get; set; }
}

public class TransportRootObject
{
    public List<Connection> connections { get; set; }
    public From2 from { get; set; }
    public To2 to { get; set; }
    public Stations stations { get; set; }
}

public class ScheduleObject
{
    public string Number { get; set; }
    public DateTime Time { get; set; }
}