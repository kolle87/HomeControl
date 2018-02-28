using System.Collections.Generic;


public class Features
{
    public int conditions { get; set; }
    public int hourly { get; set; }
}

public class Response
{
    public string version { get; set; }
    public string termsofService { get; set; }
    public Features features { get; set; }
}

public class Image
{
    public string url { get; set; }
    public string title { get; set; }
    public string link { get; set; }
}

public class DisplayLocation
{
    public string full { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string state_name { get; set; }
    public string country { get; set; }
    public string country_iso3166 { get; set; }
    public string zip { get; set; }
    public string magic { get; set; }
    public string wmo { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string elevation { get; set; }
}

public class ObservationLocation
{
    public string full { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public string country_iso3166 { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string elevation { get; set; }
}

public class Estimated
{
}

public class CurrentObservation
{
    public Image image { get; set; }
    public DisplayLocation display_location { get; set; }
    public ObservationLocation observation_location { get; set; }
    public Estimated estimated { get; set; }
    public string station_id { get; set; }
    public string observation_time { get; set; }
    public string observation_time_rfc822 { get; set; }
    public string observation_epoch { get; set; }
    public string local_time_rfc822 { get; set; }
    public string local_epoch { get; set; }
    public string local_tz_short { get; set; }
    public string local_tz_long { get; set; }
    public string local_tz_offset { get; set; }
    public string weather { get; set; }
    public string temperature_string { get; set; }
    public double temp_f { get; set; }
    public double temp_c { get; set; }
    public string relative_humidity { get; set; }
    public string wind_string { get; set; }
    public string wind_dir { get; set; }
    public double wind_degrees { get; set; }
    public double wind_mph { get; set; }
    public double wind_gust_mph { get; set; }
    public double wind_kph { get; set; }
    public double wind_gust_kph { get; set; }
    public string pressure_mb { get; set; }
    public string pressure_in { get; set; }
    public string pressure_trend { get; set; }
    public string dewpoint_string { get; set; }
    public double dewpoint_f { get; set; }
    public double dewpoint_c { get; set; }
    public string heat_index_string { get; set; }
    public string heat_index_f { get; set; }
    public string heat_index_c { get; set; }
    public string windchill_string { get; set; }
    public string windchill_f { get; set; }
    public string windchill_c { get; set; }
    public string feelslike_string { get; set; }
    public string feelslike_f { get; set; }
    public string feelslike_c { get; set; }
    public string visibility_mi { get; set; }
    public string visibility_km { get; set; }
    public string solarradiation { get; set; }
    public string UV { get; set; }
    public string precip_1hr_string { get; set; }
    public string precip_1hr_in { get; set; }
    public string precip_1hr_metric { get; set; }
    public string precip_today_string { get; set; }
    public string precip_today_in { get; set; }
    public string precip_today_metric { get; set; }
    public string icon { get; set; }
    public string icon_url { get; set; }
    public string forecast_url { get; set; }
    public string history_url { get; set; }
    public string ob_url { get; set; }
    public string nowcast { get; set; }
}

public class FCTTIME
{
    public string hour { get; set; }
    public string hour_padded { get; set; }
    public string min { get; set; }
    public string min_unpadded { get; set; }
    public string sec { get; set; }
    public string year { get; set; }
    public string mon { get; set; }
    public string mon_padded { get; set; }
    public string mon_abbrev { get; set; }
    public string mday { get; set; }
    public string mday_padded { get; set; }
    public string yday { get; set; }
    public string isdst { get; set; }
    public string epoch { get; set; }
    public string pretty { get; set; }
    public string civil { get; set; }
    public string month_name { get; set; }
    public string month_name_abbrev { get; set; }
    public string weekday_name { get; set; }
    public string weekday_name_night { get; set; }
    public string weekday_name_abbrev { get; set; }
    public string weekday_name_unlang { get; set; }
    public string weekday_name_night_unlang { get; set; }
    public string ampm { get; set; }
    public string tz { get; set; }
    public string age { get; set; }
    public string UTCDATE { get; set; }
}

public class Temp
{
    public string english { get; set; }
    public string metric { get; set; }
}

public class Dewpoint
{
    public string english { get; set; }
    public string metric { get; set; }
}

public class Wspd
{
    public string english { get; set; }
    public string metric { get; set; }
}

public class Wdir
{
    public string dir { get; set; }
    public string degrees { get; set; }
}

public class Windchill
{
    public string english { get; set; }
    public string metric { get; set; }
}

public class Heatindex
{
    public string english { get; set; }
    public string metric { get; set; }
}

public class Feelslike
{
    public string english { get; set; }
    public string metric { get; set; }
}

public class Qpf
{
    public string english { get; set; }
    public string metric { get; set; }
}

public class Snow
{
    public string english { get; set; }
    public string metric { get; set; }
}

public class Mslp
{
    public string english { get; set; }
    public string metric { get; set; }
}

public class HourlyForecast
{
    public FCTTIME FCTTIME { get; set; }
    public Temp temp { get; set; }
    public Dewpoint dewpoint { get; set; }
    public string condition { get; set; }
    public string icon { get; set; }
    public string icon_url { get; set; }
    public string fctcode { get; set; }
    public string sky { get; set; }
    public Wspd wspd { get; set; }
    public Wdir wdir { get; set; }
    public string wx { get; set; }
    public string uvi { get; set; }
    public string humidity { get; set; }
    public Windchill windchill { get; set; }
    public Heatindex heatindex { get; set; }
    public Feelslike feelslike { get; set; }
    public Qpf qpf { get; set; }
    public Snow snow { get; set; }
    public string pop { get; set; }
    public Mslp mslp { get; set; }
}

public class WeatherRootObject
{
    public Response response { get; set; }
    public CurrentObservation current_observation { get; set; }
    public List<HourlyForecast> hourly_forecast { get; set; }
}
