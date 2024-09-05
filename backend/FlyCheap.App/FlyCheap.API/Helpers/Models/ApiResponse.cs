﻿public class ApiResponse
{
    public Meta meta { get; set; }
    public List<FlightOffer> data { get; set; }
}

public class Meta
{
    public int count { get; set; }
}

public class FlightOffer
{
    public string id { get; set; }
    public List<Itinerary> itineraries { get; set; }
    public Price price { get; set; }
    public List<TravelerPricing> travelerPricings { get; set; }
}

public class Itinerary
{
    public string duration { get; set; }
    public List<Segment> segments { get; set; }
}

public class Segment
{
    public Departure departure { get; set; }
    public Arrival arrival { get; set; }
}

public class Departure
{
    public string iataCode { get; set; }
    public DateTime at { get; set; }
}

public class Arrival
{
    public string iataCode { get; set; }
    public DateTime at { get; set; }
}

public class Price
{
    public string currency { get; set; }
    public string total { get; set; }
}

public class TravelerPricing
{
    public string travelerId { get; set; }
}
