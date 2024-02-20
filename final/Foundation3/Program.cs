using System;

// Address class to encapsulate address information
class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public override string ToString()
    {
        return $"{Street}, {City}, {State} {ZipCode}";
    }
}

// Base class for all events
class Event
{
    protected string title;
    protected string description;
    protected DateTime date;
    protected TimeSpan time;
    protected Address address;

    // Constructor
    public Event(string title, string description, DateTime date, TimeSpan time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    // Method to generate standard details message
    public virtual string GetStandardDetails()
    {
        return $"Title: {title}\nDescription: {description}\nDate: {date.ToShortDateString()}\nTime: {time.ToString()}\nAddress: {address.ToString()}";
    }

    // Method to generate full details message
    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }

    // Method to generate short description message
    public virtual string GetShortDescription()
    {
        return $"Type: Generic Event\nTitle: {title}\nDate: {date.ToShortDateString()}";
    }
}

// Derived class for Lectures
class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, DateTime date, TimeSpan time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return base.GetFullDetails() + $"\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
    }

    public override string GetShortDescription()
    {
        return $"Type: Lecture\nTitle: {title}\nDate: {date.ToShortDateString()}";
    }
}

// Derived class for Receptions
class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, DateTime date, TimeSpan time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return base.GetFullDetails() + $"\nType: Reception\nRSVP Email: {rsvpEmail}";
    }

    public override string GetShortDescription()
    {
        return $"Type: Reception\nTitle: {title}\nDate: {date.ToShortDateString()}";
    }
}

// Derived class for Outdoor Gatherings
class OutdoorGathering : Event
{
    private string weatherForecast;

    public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return base.GetFullDetails() + $"\nType: Outdoor Gathering\nWeather Forecast: {weatherForecast}";
    }

    public override string GetShortDescription()
    {
        return $"Type: Outdoor Gathering\nTitle: {title}\nDate: {date.ToShortDateString()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating instances of each event type
        Address eventAddress = new Address { Street = "123 Main St", City = "Cityville", State = "Stateville", ZipCode = "12345" };

        Lecture lectureEvent = new Lecture("Tech Talk", "A discussion on latest technologies", DateTime.Parse("2024-03-15"), new TimeSpan(14, 0, 0), eventAddress, "John Doe", 50);
        Reception receptionEvent = new Reception("Networking Mixer", "A networking event for professionals", DateTime.Parse("2024-03-20"), new TimeSpan(18, 0, 0), eventAddress, "rsvp@example.com");
        OutdoorGathering outdoorEvent = new OutdoorGathering("Picnic in the Park", "A fun day out with friends and family", DateTime.Parse("2024-04-05"), new TimeSpan(11, 0, 0), eventAddress, "Sunny skies");

        // Generating and outputting marketing messages for each event
        Console.WriteLine("Marketing Messages:\n");

        Console.WriteLine("Lecture Event:");
        Console.WriteLine(lectureEvent.GetStandardDetails());
        Console.WriteLine(lectureEvent.GetFullDetails());
        Console.WriteLine(lectureEvent.GetShortDescription());
        Console.WriteLine();

        Console.WriteLine("Reception Event:");
        Console.WriteLine(receptionEvent.GetStandardDetails());
        Console.WriteLine(receptionEvent.GetFullDetails());
        Console.WriteLine(receptionEvent.GetShortDescription());
        Console.WriteLine();

        Console.WriteLine("Outdoor Gathering Event:");
        Console.WriteLine(outdoorEvent.GetStandardDetails());
        Console.WriteLine(outdoorEvent.GetFullDetails());
        Console.WriteLine(outdoorEvent.GetShortDescription());
    }
}
