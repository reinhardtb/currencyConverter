1. How long did you spend on the coding assignment? What would you add to your solution if you had
more time? If you didn't spend much time on the coding assignment then use this as an opportunity to
explain what you would add.

  5 hours
  I would add a suitable UI.  I am not experienced in UI development at all though, so this would have taken a lot more time.

2. What was the most useful feature that was added to the latest version of your language of choice?
Please include a snippet of code that shows how you've used it.

  Record struct types
  Using immutable record types in event streaming architecture is very useful:
  
  record Foo(string Bar);
  var foo = new Foo("bar");
  foo.Bar = "new bar"; // throws exception

3. How would you track down a performance issue in production? Have you ever had to do this?

  I have had to do this a lot.
  First approach would be to pin down where the performance issue is.  With the correct logging or telemetry in place to track performance of different components
  of a system, this should be relatively straight-forward.  Once the offending component is identified, the next step would be dependent on the technology:
  API layer - code evaluation for possible performance gains, evaluating memory dumps, etc.
  DB layer - execution plan evaluation leading to query and / or index optimisation
 
4. What was the latest technical book you have read or tech conference you have been to? What did you
learn?

  Release It! - Michael T. Nygard.  
  A lot of 'aha' moments in this book, and also some familiar scenarios, like that one time we traced a production issue down to an intermittently faulting patch cable.
  It had an influence in my approach to systems' design directly: question the reliability of EVERYTHING!

5. What do you think about this technical assessment?
 
  I think it is useful.  The problem given is a simple one, but the execution of the solution exposes the candidate's abilities quite clearly - if you had to think 
  of the different ways someone may implement it, from junior through to senior level.
  
  If I may make a suggestion - I would remove the testing requirement, as it should be implied.

6. Please, describe yourself using JSON.

{
  "personalDetail": {
    "name": "Reinhardt",
    "surname": "Bron",
    "maritalStatus": "Married",
    "physicalAddress": {
      "line1": "36 23rd street",
      "line2": "Menlo Park",
      "city": "Pretoria",
      "province": "Gauteng",
      "country": "South Africa"
    }
  },
  "pets": {
    "Puma": "cat",
    "Watson": "dog",
    "Elektra": "dog",
    "Simon": "dog"
  },
  "employment": {
    "employer": "Knab",
    "startDate": "2022-11-01T07:00:00.000Z",
    "position": "Principal Developer"
  },
  "status": "HAPPY"
}
