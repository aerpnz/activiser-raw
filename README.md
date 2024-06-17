# activiser (tm)

This is a "warts and all" archive of the activiser(tm) system as it was when the namesake company closed in 2009, with some changes made in 2013 to support a short-lived HTML5 client.

It is here on GitHub for posterity and reference in the hope that it might provide help and inspiration for others.

It is provided with permission from Kinetics Group Ltd, the company who paid for it, and for whom it was originally produced, and who probably have rights to any intellectual property.

# Disclaimers

IT IS NOT SUITABLE FOR USE IN THE REAL WORLD IN 2024.

It is OLD. The code is (more-or-less) as it was in 2009 when the project was abandoned. I.e. it has not seen any major work since 2009; or any work at all since 2013.

There were several contributors to the project over time, usually just one at a time, and the quality of documentation varies from poor to below average.

It includes experimental code and work-in-progress migrations from .NET 2.0 to .NET 3.5.

I do not have the source code for the Android client. It didn't work properly anyway.

It probably includes non-attributed code snippets acquired "from the Internet". I will endeavour to identify them over time. 


# Background

Activiser started life as an in-house Windows Mobile application for Kinetics Group Ltd (https://kinetics.co.nz), an (award-winning) I.T. Services company based in Auckland, New Zealand.

Originally developed by a team of final-year university students for a key paper, its purpose was to allow field engineers to record their time on a mobile device 
instead of on paper, with the information going straight back into a SQL database instead of needing someone in the office to re-key the information 
from an engineer's handwriting.

There was no comparable product on the market at the time, and so seeing a potential opportunity, an effort was made to turn Activiser into a commercial product
and a startup company formed with that goal in mind. This included building an integration with Microsoft CRM, which at the time was being marketed to service
companies. It also included admin functionality and an Outlook plugin.

While it attracted a fair bit of interest, and a few small clients, it wasn't able to gain traction, and the startup was closed a few years later. 

Kinetics continued to use it for several years until Windows Mobile devices were no longer readily available. They did hire a freelance developer to produce
and Android client, whom I assisted this effort by modifying the ASMX-based web services to return JSON as well as XML.

The Android client was short-lived as Kinetics had soon out-grown their in-house developed ticketing system, and so moved to a new platform.

Originally developed circa 2003 using .NET Framework 1.1, the product was ahead of its time in many ways, but was also limited by the platforms available at the time, 
both the "Pocket PC" hardware and the GPRS mobile network which was the main synchronisation mechanism.

I was the involved in the project throughout its lifetime, initially as a mentor to the students, but only got directly involved in development much later on. So while I can claim
credit/admit guilt for a lot of the code, I would not be so bold as to call it 'mine'.

# Potentially interesting aspects

## Controls
With a dearth of controls available that early in the .NET Framework era, it uses only "Stock" Microsoft-provided controls, or controls derived from them. 

### Date/Time Picker

### Number PickerThis includes 

### Sortable ListView

### Signature Capture Control
A requirement of the project was the capture of signatures. The obvious choice for this is some kind of bitmap. But because space and network bandwidth was 
limited, a few different things were tried, culminating in a bespoke "vector" capturing tool that would record the position of the stylus over the control in an 18-bit
integer that could be neatly serialised into Base64. The signature data was augmented with a timestamp and GPS coordinates before being encrypted.

### GPS
The device would record GPS coordinates with the aforementioned signature.

# Plans
There are no plans to develop the project, although it would be an interesting exercise to migrate it to modern platforms.

I may be tempted to at least restructure it into a less chaotic folder structure if I run out of real work.

# License
This project was never intended to be made public, and so the source code is largely missing any kind of license headers, and technically Copyright 2004-2013 Kinetics Group Ltd.

Kinetics Group paid for the work so, strictly speaking, they hold rights to any novel intellectual property created within the project. However, Kinetics
stopped its involvement in the software development business years ago, and have kindly agreed to the release of this source code, for "academic" purposes.

And so, the code is being made available with an MIT license.

If you find the code helpful, and especially if you use any in your own code, the multiple authors of this project would appreciate some recognition in your own code, 
including a link the repository you got it from.